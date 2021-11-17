using ChickenBanana.IbmMq.Client.Authentication.Settings;
using IBM.WMQ;
using System.Collections;

namespace ChickenBanana.IbmMq.Client.Authorization.Strategies
{
    public class CredentialsAuthorizationStrategy : IMqAuthorizationStrategy
    {
        private readonly CredentialsAuthorizationSettings settings;

        public CredentialsAuthorizationStrategy(CredentialsAuthorizationSettings settings)
        {
            this.settings = settings;
        }

        public void Apply(Hashtable mqSettings)
        {
            mqSettings.Add(MQC.USER_ID_PROPERTY, settings.Username);
            mqSettings.Add(MQC.PASSWORD_PROPERTY, settings.Password);
            mqSettings.Add(MQC.USE_MQCSP_AUTHENTICATION_PROPERTY, true);
         }
    }
}
