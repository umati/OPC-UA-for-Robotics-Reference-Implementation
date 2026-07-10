using Opc.Ua;
using Opc.Ua.Client;

namespace Robotics.Client.Core.Client;

public sealed class OpcUaSessionFactory(ApplicationConfiguration configuration)
{
    public async Task<Session> CreateSessionAsync(string endpointUrl, CancellationToken cancellationToken = default)
    {
        EndpointDescription selectedEndpoint = await CoreClientUtils.SelectEndpointAsync(
            configuration,
            endpointUrl,
            useSecurity: true,
            configuration.TransportQuotas.OperationTimeout,
            cancellationToken);

        var endpointConfiguration = EndpointConfiguration.Create(configuration);
        var endpoint = new ConfiguredEndpoint(null, selectedEndpoint, endpointConfiguration);

        var identity = new UserIdentity(new AnonymousIdentityToken());

        return await Session.Create(
            configuration,
            endpoint,
            updateBeforeConnect: false,
            checkDomain: false,
            sessionName: configuration.ApplicationName,
            sessionTimeout: (uint)configuration.ClientConfiguration.DefaultSessionTimeout,
            identity,
            preferredLocales: null,
            cancellationToken);
    }
}
