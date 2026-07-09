using Opc.Ua;
using Opc.Ua.Client;

namespace Robotics.ReferenceClient.Client;

internal sealed class OpcUaSessionFactory(ApplicationConfiguration configuration)
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
            sessionName: OpcUaClientApplication.ApplicationName,
            sessionTimeout: (uint)configuration.ClientConfiguration.DefaultSessionTimeout,
            identity,
            preferredLocales: null,
            cancellationToken);
    }
}
