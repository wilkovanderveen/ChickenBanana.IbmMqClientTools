using System.Text;

namespace ChickenBanana.IbmMq.Tools.Mqsc
{
    public class SslCertificateSectionBuilder
    {

        private string _label;

        public SslCertificateSectionBuilder()
        {

        }

        public SslCertificateSectionBuilder WithCertificateLabel(string label)
        {
            _label = label;
            return this;
        }

        internal string Build()
        {
            var stringBuilder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(_label))
            {
                stringBuilder.Append($"CERTLABL('{_label}') ");
                stringBuilder.Append($"SSLCIPH(TLS_RSA_WITH_AES_128_CBC_SHA256) ");
            }

            return stringBuilder.ToString();

        }
    }
}
