namespace ChickenBanana.IbmMq.Client.Authentication.Settings
{
    public class CertificateAuthorizationSettings : IMqAuthorizationSettings
    {       

        public string CertificateLabel { get; set; }
        public string ChannelLibraryFolder { get; set; }
        public string ChannelLibraryName { get; set; }
    }
}
