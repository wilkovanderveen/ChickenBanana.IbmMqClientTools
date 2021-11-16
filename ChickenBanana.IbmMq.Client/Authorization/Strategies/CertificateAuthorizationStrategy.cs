using ChickenBanana.IbmMq.Client.Authentication.Settings;
using IBM.WMQ;
using System.Collections;

namespace ChickenBanana.IbmMq.Client.Authorization.Strategies
{
    public class CertificateAuthorizationStrategy : IMqAuthorizationStrategy
    {
        private readonly CertificateAuthorizationSettings settings;

        public CertificateAuthorizationStrategy(CertificateAuthorizationSettings settings)
        {

            this.settings = settings;
        }

        public void Apply(Hashtable mqSettings)
        {
            mqSettings.Add("CertificateLabel", settings.CertificateLabel);
            mqSettings.Add("SSLCipherSpec", "TLS_RSA_WITH_AES_256_CBC_SHA256");
        }
    }
}
