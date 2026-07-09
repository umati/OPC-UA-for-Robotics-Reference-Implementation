using Opc.Ua;
using Opc.Ua.Configuration;

namespace Robotics.ReferenceClient.Client;

internal sealed class OpcUaClientApplication
{
    public const string ApplicationName = "Robotics Reference Client";

    public async Task<ApplicationConfiguration> CreateConfigurationAsync()
    {
        var configuration = new ApplicationConfiguration
        {
            ApplicationName = ApplicationName,
            ApplicationUri = Utils.Format("urn:{0}:RoboticsReferenceClient", Utils.GetHostName()),
            ProductUri = "urn:RoboticsReferenceImplementation:ReferenceClient",
            ApplicationType = ApplicationType.Client,

            SecurityConfiguration = new SecurityConfiguration
            {
                ApplicationCertificate = new CertificateIdentifier
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = "pki/reference-client/own",
                    SubjectName = $"CN={ApplicationName}"
                },

                TrustedPeerCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = "pki/reference-client/trusted"
                },

                TrustedIssuerCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = "pki/reference-client/issuers"
                },

                RejectedCertificateStore = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = "pki/reference-client/rejected"
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
                OutputFilePath = "Logs/RoboticsReferenceClient.log",
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
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Development certificate policy: auto-accepting untrusted server certificate.");
                Console.WriteLine($"Certificate subject: {eventArgs.Certificate.Subject}");
                Console.ResetColor();
                eventArgs.Accept = true;
            }
        };

        var application = new ApplicationInstance
        {
            ApplicationName = ApplicationName,
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
