using Robotics.ReferenceServer.Simulation;
using Robotics.ReferenceServer.ControlBridge;
using Robotics.Shared;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Robotics.ReferenceServer.Telemetry;

internal sealed class RobotTelemetryWebSocketServer : IRobotTelemetryPublisher, IAsyncDisposable
{
    private const string WebSocketGuid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
    private static readonly TimeSpan BroadcastInterval = TimeSpan.FromMilliseconds(40);
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly ConcurrentDictionary<int, WebSocketClientConnection> _clients = [];
    private readonly string _host;
    private readonly int _port;
    private readonly string _path;
    private readonly RobotControlBridgeHttpHandler _controlBridgeHandler;
    private readonly CancellationTokenSource _shutdown = new();

    private readonly List<TcpListener> _listeners = [];
    private readonly List<Task> _acceptLoopTasks = [];

    private Task? _broadcastLoopTask;
    private string? _latestPayload;
    private int _nextClientId;
    private bool _started;

    public RobotTelemetryWebSocketServer(
        RobotControlBridgeServiceRegistry controlBridgeServiceRegistry,
        string host = "localhost",
        int port = 48080,
        string path = "/telemetry")
    {
        _host = host;
        _port = port;
        _path = NormalizePath(path);
        _controlBridgeHandler = new RobotControlBridgeHttpHandler(controlBridgeServiceRegistry);
        EndpointUrl = $"ws://{_host}:{_port}{_path}";
        ControlEndpointUrl = $"http://{_host}:{_port}/control";
    }

    public string EndpointUrl { get; }

    public string ControlEndpointUrl { get; }

    public Task StartAsync()
    {
        if (_started)
        {
            return Task.CompletedTask;
        }

        try
        {
            StartListener(IPAddress.Loopback);
            StartListener(IPAddress.IPv6Loopback);

            if (_listeners.Count == 0)
            {
                Console.WriteLine($"Warning: Telemetry WebSocket endpoint failed to start at {EndpointUrl}.");
                Console.WriteLine($"Warning: Control bridge endpoint failed to start at {ControlEndpointUrl}.");
                Console.WriteLine("Warning: No loopback listener could bind to the telemetry port.");
                return Task.CompletedTask;
            }

            _started = true;

            _broadcastLoopTask = Task.Run(() => BroadcastLoopAsync(_shutdown.Token));

            Console.WriteLine($"Telemetry WebSocket endpoint: {EndpointUrl}");
            Console.WriteLine($"Control bridge endpoint: {ControlEndpointUrl}");
        }
        catch (Exception exception) when (exception is SocketException or InvalidOperationException)
        {
            Console.WriteLine($"Warning: Telemetry WebSocket endpoint failed to start at {EndpointUrl}.");
            Console.WriteLine($"Warning: Control bridge endpoint failed to start at {ControlEndpointUrl}.");
            Console.WriteLine($"Warning: {exception.Message}");
        }

        return Task.CompletedTask;
    }

    public async Task StopAsync()
    {
        if (!_started)
        {
            return;
        }

        _shutdown.Cancel();
        foreach (TcpListener listener in _listeners)
        {
            listener.Stop();
        }

        foreach (WebSocketClientConnection client in _clients.Values)
        {
            client.Dispose();
        }

        _clients.Clear();

        Task[] tasks = _acceptLoopTasks
            .Append(_broadcastLoopTask)
            .Where(task => task is not null)
            .Cast<Task>()
            .ToArray();

        try
        {
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
        }
        catch (SocketException)
        {
        }
        catch (ObjectDisposedException)
        {
        }

        _started = false;
    }

    public void Publish(RobotTelemetrySnapshot robotSnapshot, RobotProgramExecutionSnapshot programSnapshot)
    {
        RobotTelemetryMessage message = RobotTelemetryMessage.FromSnapshots(robotSnapshot, programSnapshot);
        string payload = JsonSerializer.Serialize(message, JsonOptions);
        Volatile.Write(ref _latestPayload, payload);
    }

    public async ValueTask DisposeAsync()
    {
        await StopAsync().ConfigureAwait(false);
        _shutdown.Dispose();
    }

    private void StartListener(IPAddress address)
    {
        try
        {
            var listener = new TcpListener(address, _port);
            listener.Start();
            _listeners.Add(listener);
            _acceptLoopTasks.Add(Task.Run(() => AcceptLoopAsync(listener, _shutdown.Token)));
        }
        catch (SocketException)
        {
        }
    }

    private async Task AcceptLoopAsync(TcpListener listener, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            TcpClient tcpClient;

            try
            {
                tcpClient = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (SocketException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (ObjectDisposedException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            _ = Task.Run(() => HandleClientAsync(tcpClient, cancellationToken), CancellationToken.None);
        }
    }

    private async Task HandleClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    {
        int clientId = 0;

        try
        {
            tcpClient.NoDelay = true;
            NetworkStream stream = tcpClient.GetStream();
            RobotControlHttpRequest request = await ReadHttpRequestAsync(stream, cancellationToken).ConfigureAwait(false);

            if (!TryCreateHandshakeResponse(request, out string? requestedPath, out byte[]? responseBytes))
            {
                if (request.Path.StartsWith("/control", StringComparison.OrdinalIgnoreCase))
                {
                    RobotControlHttpResponse controlResponse = _controlBridgeHandler.Handle(request);
                    await WriteControlHttpResponseAsync(stream, controlResponse, request, cancellationToken).ConfigureAwait(false);
                    return;
                }

                await WriteEmptyHttpResponseAsync(stream, "400 Bad Request", cancellationToken).ConfigureAwait(false);
                return;
            }

            if (!string.Equals(requestedPath, _path, StringComparison.OrdinalIgnoreCase))
            {
                await WriteEmptyHttpResponseAsync(stream, "404 Not Found", cancellationToken).ConfigureAwait(false);
                return;
            }

            byte[] handshakeResponse = responseBytes
                ?? throw new InvalidDataException("The WebSocket handshake response could not be created.");
            await stream.WriteAsync(handshakeResponse.AsMemory(), cancellationToken).ConfigureAwait(false);

            clientId = Interlocked.Increment(ref _nextClientId);
            var client = new WebSocketClientConnection(clientId, tcpClient);
            _clients[clientId] = client;

            await client.ReadUntilClosedAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception) when (
            exception is IOException
                or SocketException
                or ObjectDisposedException
                or OperationCanceledException
                or InvalidDataException)
        {
        }
        finally
        {
            if (clientId != 0)
            {
                RemoveClient(clientId);
            }
            else
            {
                tcpClient.Dispose();
            }
        }
    }

    private async Task BroadcastLoopAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(BroadcastInterval);

        while (await timer.WaitForNextTickAsync(cancellationToken).ConfigureAwait(false))
        {
            string? payload = Volatile.Read(ref _latestPayload);
            if (payload is null || _clients.IsEmpty)
            {
                continue;
            }

            foreach (KeyValuePair<int, WebSocketClientConnection> entry in _clients.ToArray())
            {
                try
                {
                    await entry.Value.SendTextAsync(payload, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception exception) when (
                    exception is IOException
                        or SocketException
                        or ObjectDisposedException
                        or OperationCanceledException)
                {
                    RemoveClient(entry.Key);
                }
            }
        }
    }

    private void RemoveClient(int clientId)
    {
        if (_clients.TryRemove(clientId, out WebSocketClientConnection? client))
        {
            client.Dispose();
        }
    }

    private static async Task<RobotControlHttpRequest> ReadHttpRequestAsync(NetworkStream stream, CancellationToken cancellationToken)
    {
        using var buffer = new MemoryStream();
        byte[] chunk = new byte[512];
        int headerEndIndex = -1;

        while (buffer.Length < 8192)
        {
            int read = await stream.ReadAsync(chunk.AsMemory(), cancellationToken).ConfigureAwait(false);
            if (read == 0)
            {
                throw new InvalidDataException("The client disconnected before sending an HTTP request.");
            }

            buffer.Write(chunk, 0, read);
            byte[] bufferedBytes = buffer.ToArray();
            headerEndIndex = IndexOfHeaderEnd(bufferedBytes);
            if (headerEndIndex >= 0)
            {
                return await CreateHttpRequestAsync(bufferedBytes, headerEndIndex, stream, cancellationToken).ConfigureAwait(false);
            }
        }

        throw new InvalidDataException("The HTTP request header exceeded the 8 KB limit.");
    }

    private static async Task<RobotControlHttpRequest> CreateHttpRequestAsync(
        byte[] bufferedBytes,
        int headerEndIndex,
        NetworkStream stream,
        CancellationToken cancellationToken)
    {
        string header = Encoding.ASCII.GetString(bufferedBytes, 0, headerEndIndex);
        string[] lines = header.Split("\r\n", StringSplitOptions.None);
        string[] requestParts = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (requestParts.Length < 2)
        {
            throw new InvalidDataException("The HTTP request line is invalid.");
        }

        var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (string line in lines.Skip(1))
        {
            int separatorIndex = line.IndexOf(':', StringComparison.Ordinal);
            if (separatorIndex <= 0)
            {
                continue;
            }

            headers[line[..separatorIndex].Trim()] = line[(separatorIndex + 1)..].Trim();
        }

        int contentLength = 0;
        if (headers.TryGetValue("Content-Length", out string? contentLengthHeader)
            && (!int.TryParse(contentLengthHeader, out contentLength) || contentLength < 0 || contentLength > 1024 * 1024))
        {
            throw new InvalidDataException("The HTTP request Content-Length is invalid or exceeds the 1 MB limit.");
        }

        int bodyStartIndex = headerEndIndex + 4;
        int bufferedBodyLength = bufferedBytes.Length - bodyStartIndex;
        byte[] bodyBytes = new byte[contentLength];
        if (contentLength > 0 && bufferedBodyLength > 0)
        {
            Buffer.BlockCopy(bufferedBytes, bodyStartIndex, bodyBytes, 0, Math.Min(bufferedBodyLength, contentLength));
        }

        int totalBodyRead = Math.Min(bufferedBodyLength, contentLength);
        while (totalBodyRead < contentLength)
        {
            int read = await stream.ReadAsync(bodyBytes.AsMemory(totalBodyRead, contentLength - totalBodyRead), cancellationToken).ConfigureAwait(false);
            if (read == 0)
            {
                throw new InvalidDataException("The client disconnected before sending the full HTTP request body.");
            }

            totalBodyRead += read;
        }

        string path = NormalizePath(requestParts[1].Split('?', 2)[0]);
        string body = contentLength == 0 ? string.Empty : Encoding.UTF8.GetString(bodyBytes);
        return new RobotControlHttpRequest(requestParts[0], path, headers, body);
    }

    private static int IndexOfHeaderEnd(byte[] bytes)
    {
        for (int index = 0; index <= bytes.Length - 4; index++)
        {
            if (bytes[index] == '\r'
                && bytes[index + 1] == '\n'
                && bytes[index + 2] == '\r'
                && bytes[index + 3] == '\n')
            {
                return index;
            }
        }

        return -1;
    }

    private static bool TryCreateHandshakeResponse(
        RobotControlHttpRequest request,
        out string? requestedPath,
        out byte[]? responseBytes)
    {
        requestedPath = null;
        responseBytes = null;

        if (!string.Equals(request.Method, "GET", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        requestedPath = request.Path;

        if (!request.Headers.TryGetValue("Sec-WebSocket-Key", out string? key) || string.IsNullOrWhiteSpace(key))
        {
            return false;
        }

        string accept = Convert.ToBase64String(SHA1.HashData(Encoding.ASCII.GetBytes(key + WebSocketGuid)));
        string response =
            "HTTP/1.1 101 Switching Protocols\r\n" +
            "Upgrade: websocket\r\n" +
            "Connection: Upgrade\r\n" +
            $"Sec-WebSocket-Accept: {accept}\r\n" +
            "\r\n";

        responseBytes = Encoding.ASCII.GetBytes(response);
        return true;
    }

    private static async Task WriteEmptyHttpResponseAsync(
        NetworkStream stream,
        string status,
        CancellationToken cancellationToken)
    {
        byte[] response = Encoding.ASCII.GetBytes(
            $"HTTP/1.1 {status}\r\nConnection: close\r\nContent-Length: 0\r\n\r\n");
        await stream.WriteAsync(response.AsMemory(), cancellationToken).ConfigureAwait(false);
    }

    private static async Task WriteControlHttpResponseAsync(
        NetworkStream stream,
        RobotControlHttpResponse controlResponse,
        RobotControlHttpRequest request,
        CancellationToken cancellationToken)
    {
        byte[] body = Encoding.UTF8.GetBytes(controlResponse.Body);
        string status = controlResponse.StatusCode switch
        {
            200 => "200 OK",
            204 => "204 No Content",
            400 => "400 Bad Request",
            404 => "404 Not Found",
            405 => "405 Method Not Allowed",
            409 => "409 Conflict",
            500 => "500 Internal Server Error",
            503 => "503 Service Unavailable",
            _ => $"{controlResponse.StatusCode} Control Response"
        };

        request.Headers.TryGetValue("Origin", out string? origin);
        string corsHeaders = RobotControlBridgeHttpHandler.IsAllowedDevOrigin(origin)
            ? $"Access-Control-Allow-Origin: {origin}\r\n" +
              "Access-Control-Allow-Methods: POST, OPTIONS\r\n" +
              "Access-Control-Allow-Headers: Content-Type\r\n"
            : string.Empty;

        string header =
            $"HTTP/1.1 {status}\r\n" +
            "Connection: close\r\n" +
            "Content-Type: application/json; charset=utf-8\r\n" +
            $"Content-Length: {body.Length}\r\n" +
            corsHeaders +
            "\r\n";

        byte[] headerBytes = Encoding.ASCII.GetBytes(header);
        await stream.WriteAsync(headerBytes.AsMemory(), cancellationToken).ConfigureAwait(false);
        if (body.Length > 0)
        {
            await stream.WriteAsync(body.AsMemory(), cancellationToken).ConfigureAwait(false);
        }
    }

    private static string NormalizePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return "/";
        }

        return path.StartsWith("/", StringComparison.Ordinal) ? path : $"/{path}";
    }

    private sealed class WebSocketClientConnection : IDisposable
    {
        private readonly TcpClient _tcpClient;
        private readonly SemaphoreSlim _sendLock = new(1, 1);

        public WebSocketClientConnection(int id, TcpClient tcpClient)
        {
            Id = id;
            _tcpClient = tcpClient;
        }

        public int Id { get; }

        public Task SendTextAsync(string payload, CancellationToken cancellationToken)
        {
            return SendFrameAsync(0x1, Encoding.UTF8.GetBytes(payload), cancellationToken);
        }

        public async Task ReadUntilClosedAsync(CancellationToken cancellationToken)
        {
            NetworkStream stream = _tcpClient.GetStream();
            byte[] header = new byte[2];

            while (!cancellationToken.IsCancellationRequested)
            {
                if (!await ReadExactlyAsync(stream, header, cancellationToken).ConfigureAwait(false))
                {
                    return;
                }

                int opcode = header[0] & 0x0F;
                bool masked = (header[1] & 0x80) != 0;
                ulong payloadLength = (ulong)(header[1] & 0x7F);

                if (payloadLength == 126)
                {
                    byte[] extendedLength = new byte[2];
                    if (!await ReadExactlyAsync(stream, extendedLength, cancellationToken).ConfigureAwait(false))
                    {
                        return;
                    }

                    payloadLength = BinaryPrimitives.ReadUInt16BigEndian(extendedLength);
                }
                else if (payloadLength == 127)
                {
                    byte[] extendedLength = new byte[8];
                    if (!await ReadExactlyAsync(stream, extendedLength, cancellationToken).ConfigureAwait(false))
                    {
                        return;
                    }

                    payloadLength = BinaryPrimitives.ReadUInt64BigEndian(extendedLength);
                }

                if (payloadLength > 1024 * 1024)
                {
                    throw new InvalidDataException("WebSocket client frame payload exceeded the 1 MB limit.");
                }

                byte[]? mask = null;
                if (masked)
                {
                    mask = new byte[4];
                    if (!await ReadExactlyAsync(stream, mask, cancellationToken).ConfigureAwait(false))
                    {
                        return;
                    }
                }

                int payloadLengthInt = checked((int)payloadLength);
                byte[] payload = new byte[payloadLengthInt];
                if (payloadLengthInt > 0 && !await ReadExactlyAsync(stream, payload, cancellationToken).ConfigureAwait(false))
                {
                    return;
                }

                if (mask is not null)
                {
                    for (int index = 0; index < payloadLengthInt; index++)
                    {
                        payload[index] = (byte)(payload[index] ^ mask[index % 4]);
                    }
                }

                if (opcode == 0x8)
                {
                    await SendFrameAsync(0x8, [], cancellationToken).ConfigureAwait(false);
                    return;
                }

                if (opcode == 0x9)
                {
                    await SendFrameAsync(0xA, payload, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
            _sendLock.Dispose();
        }

        private async Task SendFrameAsync(byte opcode, byte[] payload, CancellationToken cancellationToken)
        {
            byte[] frame = CreateServerFrame(opcode, payload);

            await _sendLock.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                await _tcpClient.GetStream().WriteAsync(frame.AsMemory(), cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _sendLock.Release();
            }
        }

        private static byte[] CreateServerFrame(byte opcode, byte[] payload)
        {
            int headerLength = payload.Length <= 125 ? 2 : payload.Length <= ushort.MaxValue ? 4 : 10;
            byte[] frame = new byte[headerLength + payload.Length];

            frame[0] = (byte)(0x80 | opcode);
            if (payload.Length <= 125)
            {
                frame[1] = (byte)payload.Length;
            }
            else if (payload.Length <= ushort.MaxValue)
            {
                frame[1] = 126;
                BinaryPrimitives.WriteUInt16BigEndian(frame.AsSpan(2, 2), (ushort)payload.Length);
            }
            else
            {
                frame[1] = 127;
                BinaryPrimitives.WriteUInt64BigEndian(frame.AsSpan(2, 8), (ulong)payload.Length);
            }

            payload.CopyTo(frame.AsSpan(headerLength));
            return frame;
        }

        private static async Task<bool> ReadExactlyAsync(
            NetworkStream stream,
            byte[] buffer,
            CancellationToken cancellationToken)
        {
            int totalRead = 0;

            while (totalRead < buffer.Length)
            {
                int read = await stream
                    .ReadAsync(buffer.AsMemory(totalRead, buffer.Length - totalRead), cancellationToken)
                    .ConfigureAwait(false);

                if (read == 0)
                {
                    return false;
                }

                totalRead += read;
            }

            return true;
        }
    }
}
