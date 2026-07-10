using Opc.Ua;
using Opc.Ua.Configuration;

namespace Robotics.Client.Core.Client;

public sealed class OpcUaClientApplication
{
    public const string ApplicationName = "Robotics Reference Client";

    private readonly string _applicationName;
    private readonly string _applicationUriSuffix;
    private readonly string _productUri;
    private readonly string _pkiRootPath;
    private readonly string _traceOutputPath;
    private readonly Action<string>? _developmentCertificateAccepted;

    public OpcUaClientApplication(
        string applicationName = ApplicationName,
        string applicationUriSuffix = "RoboticsReferenceClient",
        string productUri = "urn:RoboticsReferenceImplementation:ReferenceClient",
        string pkiRootPath = "pki/reference-client",
        string traceOutputPath = "Logs/RoboticsReferenceClient.log",
        Action<string>? developmentCertificateAccepted = null)
    {
        _applicationName = applicationName;
        _applicationUriSuffix = applicationUriSuffix;
        _productUri = productUri;
        _pkiRootPath = pkiRootPath;
        _traceOutputPath = traceOutputPath;
        _developmentCertificateAccepted = developmentCertificateAccepted;
    }

    public async Task<ApplicationConfiguration> CreateConfigurationAsync()
    {
        var configuration = new ApplicationConfiguration
        {
            ApplicationName = _applicationName,
            ApplicationUri = Utils.Format("urn:{0}:{1}", Utils.GetHostName(), _applicationUriSuffix),
            ProductUri = _productUri,
            ApplicationType = ApplicationType.Client,

            SecurityConfiguration = new SecurityConfiguration
            {
                ApplicationCertificate = new CertificateIdentifier
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = $"{_pkiRootPath}/own",
                    SubjectName = $"CN={_applicationName}"
                },

                TrustedPeerCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = $"{_pkiRootPath}/trusted"
                },

                TrustedIssuerCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = $"{_pkiRootPath}/issuers"
                },

                RejectedCertificateStore = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = $"{_pkiRootPath}/rejected"
                },

                AddAppCertToTrustedStore = true,
                AutoAcceptUntrustedCertificates = true,
                RejectSHA1SignedCertificates = false,
                MinimumCertificateKeySize = 2048
            },

            TransportConfigurations = new TransportConfigurationCollection(),

            TransportQuotas = new TransportQuotas
            {
                OperationTimeout = 15000,
                MaxStringLength = 1048576,
                MaxByteStringLength = 1048576,
                MaxArrayLength = 65535,
                MaxMessageSize = 4194304,
                MaxBufferSize = 65535,
                ChannelLifetime = 300000,
                SecurityTokenLifetime = 3600000
            },

            ClientConfiguration = new ClientConfiguration
            {
                DefaultSessionTimeout = 60000
            },

            TraceConfiguration = new TraceConfiguration
            {
                OutputFilePath = _traceOutputPath,
                DeleteOnLoad = true,
                TraceMasks = Utils.TraceMasks.Error |
                             Utils.TraceMasks.Information |
                             Utils.TraceMasks.Security
            }
        };

        await configuration.Validate(ApplicationType.Client);

        // Implementation decision for local C1 development: match the reference server's repo-local PKI style
        // while making server certificate auto-accept explicit and noisy. Production clients should replace this
        // with pinned trust-store provisioning.
        configuration.CertificateValidator.CertificateValidation += (_, eventArgs) =>
        {
            if (eventArgs.Error.StatusCode == StatusCodes.BadCertificateUntrusted)
            {
                _developmentCertificateAccepted?.Invoke(eventArgs.Certificate.Subject);
                eventArgs.Accept = true;
            }
        };

        var application = new ApplicationInstance
        {
            ApplicationName = _applicationName,
            ApplicationType = ApplicationType.Client,
            ApplicationConfiguration = configuration
        };

        bool hasCertificate = await application.CheckApplicationInstanceCertificates(
            silent: false,
            lifeTimeInMonths: 120);

        if (!hasCertificate)
        {
            throw new InvalidOperationException("The OPC UA client application certificate could not be created or validated.");
        }

        return configuration;
    }
}
