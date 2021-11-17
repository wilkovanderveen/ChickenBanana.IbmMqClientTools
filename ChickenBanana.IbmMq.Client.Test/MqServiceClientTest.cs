using ChickenBanana.IbmMq.Client;
using ChickenBanana.IbmMq.Client.Authentication.Settings;
using ChickenBanana.IbmMq.Client.Authorization.Strategies;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace IbmMQ.ConnectionTest
{
    public class MqServiceCkientTest
    {
        [Fact]
        public void SendAndRecieve_With_Credentials()
        {
            var credentialSettings = new CredentialsAuthorizationSettings
            {
                Username = "app",
                Password = "Sjakie01!"
            };

            var credentialsAuthorizationStrategy = new CredentialsAuthorizationStrategy(credentialSettings);
            var mqServiceSettings = new MqServiceSettings()
            {
                QueueManagerName = "QM1",
                Channel = "DEV.APP.SVRCONN",
                Hostname = "localhost"
            };

            var logger = NullLogger.Instance;

            var mqService = new IbmMqService(credentialsAuthorizationStrategy, logger, mqServiceSettings);
            mqService.SendMessage("DEV.QUEUE.1", "Hello world");
            var message = mqService.RetrieveMessage("DEV.QUEUE.1");

            Assert.Equal("Hello world", message);
        }

        [Fact]
        public void SendAndRecieve_With_Certificate()
        {
            var certificateSettings = new CertificateAuthorizationSettings
            {
                CertificateLabel = "ibmwebspheremq",
                ChannelLibraryFolder = @"C:\ProgramData\IBM\MQ\qmgrs\QM1\@ipcc",
                ChannelLibraryName = "AMQCLCHL.TAB",
              
            };

            var certificateAuthorizationSettings = new CertificateAuthorizationStrategy(certificateSettings);
            var mqServiceSettings = new MqServiceSettings()
            {
                QueueManagerName = "QM1",
                Channel = "DEV.APP.SVRCONN",
                Hostname = "localhost"
            };

            var logger = NullLogger.Instance;
            var mqService = new IbmMqService(certificateAuthorizationSettings, logger, mqServiceSettings);
            mqService.SendMessage("DEV.QUEUE.1", "Hello world");
            var message = mqService.RetrieveMessage("DEV.QUEUE.1");
        }

        [Fact]
        public void SendAndRecieveMultiple_With_Credentials()
        {
            var credentialSettings = new CredentialsAuthorizationSettings
            {
                Username = "app",
                Password = "Sjakie01!"
            };

            var credentialsAuthorizationStrategy = new CredentialsAuthorizationStrategy(credentialSettings);
            var mqServiceSettings = new MqServiceSettings()
            {
                QueueManagerName = "QM1",
                Channel = "DEV.APP.SVRCONN",
                Hostname = "127.0.0.1"
            };

            var logger = NullLogger.Instance;
            var mqService = new IbmMqService(credentialsAuthorizationStrategy, logger, mqServiceSettings);
            mqService.SendMessage("DEV.QUEUE.1", "Hello world");
            mqService.SendMessage("DEV.QUEUE.1", "Hello world");
            mqService.SendMessage("DEV.QUEUE.1", "Hello world");
            var message = mqService.RetrieveMessages("DEV.QUEUE.1");
        }
    }
}
