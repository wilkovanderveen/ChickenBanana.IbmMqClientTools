using ChickenBanana.IbmMq.Client.Authentication.Settings;
using System;
using System.Collections;

namespace ChickenBanana.IbmMq.Client.Authorization.Strategies
{
    public class CertificateAuthorizationStrategy : IMqAuthorizationStrategy
    {
        private readonly CertificateAuthorizationSettings _settings;

        public CertificateAuthorizationStrategy(CertificateAuthorizationSettings settings)
        {

            _settings = settings;
        }

        public void Apply(Hashtable mqSettings)
        {
            Environment.SetEnvironmentVariable("MQCHLLIB", _settings.ChannelLibraryFolder);
            Environment.SetEnvironmentVariable("MQCHLTAB", _settings.ChannelLibraryName);
           
            mqSettings.Add("CertificateLabel", _settings.CertificateLabel);
            mqSettings.Add("SSLCipherSpec", "TLS_RSA_WITH_AES_128_CBC_SHA256");
        }
    }
}
