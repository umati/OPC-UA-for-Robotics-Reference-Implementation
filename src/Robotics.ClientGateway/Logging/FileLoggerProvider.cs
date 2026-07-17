using Microsoft.Extensions.Logging;

namespace Robotics.ClientGateway.Logging;

public sealed class FileLoggerProvider(string path) : ILoggerProvider
{
    private readonly object gate = new();
    public ILogger CreateLogger(string categoryName) => new FileLogger(categoryName, path, gate);
    public void Dispose() { }

    private sealed class FileLogger(string category, string path, object gate) : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) where TState : notnull => NullScope.Instance;
        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            try
            {
                lock (gate)
                    File.AppendAllText(path, $"{DateTimeOffset.Now:O} [{logLevel}] {category}: {formatter(state, exception)}{(exception is null ? string.Empty : Environment.NewLine + exception)}{Environment.NewLine}");
            }
            catch { }
        }
    }

    private sealed class NullScope : IDisposable
    {
        public static readonly NullScope Instance = new();
        public void Dispose() { }
    }
}
