using Opc.Ua;
using Opc.Ua.Configuration;

namespace Robotics.ReferenceServer;

internal static class RoboticsServerConfiguration
{
    public const string ApplicationName = "Robotics Reference Server";

    public static ApplicationConfiguration Create(string endpointUrl)
    {
        var configuration = new ApplicationConfiguration
        {
            ApplicationName = ApplicationName,
            ApplicationUri = Utils.Format("urn:{0}:RoboticsReferenceServer", Utils.GetHostName()),
            ProductUri = "urn:RoboticsReferenceImplementation:ReferenceServer",
            ApplicationType = ApplicationType.Server,

            SecurityConfiguration = new SecurityConfiguration
            {
                ApplicationCertificate = new CertificateIdentifier
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = "pki/own",
                    SubjectName = $"CN={ApplicationName}"
                },

                TrustedPeerCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = "pki/trusted"
                },

                TrustedIssuerCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = "pki/issuers"
                },

                RejectedCertificateStore = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = "pki/rejected"
                },

                AutoAcceptUntrustedCertificates = true,
                AddAppCertToTrustedStore = true,
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

            ServerConfiguration = new ServerConfiguration
            {
                BaseAddresses =
                {
                    endpointUrl
                },

                SecurityPolicies =
                {
                    new ServerSecurityPolicy
                    {
                        SecurityMode = MessageSecurityMode.None,
                        SecurityPolicyUri = SecurityPolicies.None
                    },
                    new ServerSecurityPolicy
                    {
                        SecurityMode = MessageSecurityMode.SignAndEncrypt,
                        SecurityPolicyUri = SecurityPolicies.Basic256Sha256
                    }
                },

                UserTokenPolicies =
                {
                    new UserTokenPolicy(UserTokenType.Anonymous)
                },

                DiagnosticsEnabled = true,
                MaxSessionCount = 100,
                MinSessionTimeout = 10000,
                MaxSessionTimeout = 3600000,
                MaxBrowseContinuationPoints = 10,
                MaxQueryContinuationPoints = 10,
                MaxHistoryContinuationPoints = 10,
                MaxRequestAge = 600000,
                MinPublishingInterval = 100,
                MaxPublishingInterval = 3600000,
                PublishingResolution = 50,
                MaxSubscriptionLifetime = 3600000,
                MaxMessageQueueSize = 100,
                MaxNotificationQueueSize = 100,
                MaxNotificationsPerPublish = 1000
            },

            TraceConfiguration = new TraceConfiguration
            {
                OutputFilePath = "Logs/RoboticsReferenceServer.log",
                DeleteOnLoad = true,
                TraceMasks = Utils.TraceMasks.Error |
                             Utils.TraceMasks.Information |
                             Utils.TraceMasks.Security 
            }
        };

        configuration.Validate(ApplicationType.Server).GetAwaiter().GetResult();

        return configuration;
    }
}