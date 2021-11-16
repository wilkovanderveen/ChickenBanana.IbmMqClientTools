using System.Text;

namespace ChickenBanana.IbmMq.Tools.Mqsc
{
    /// <summary>
    /// Builder for creating the <see cref="https://www.ibm.com/docs/en/ibm-mq/9.2?topic=reference-define-channel-define-new-channel">Define channel</see> command.
    /// </summary>
    /// <seealso cref="https://www.ibm.com/docs/en/ibm-mq/9.2?topic=reference-runmqsc-run-mqsc-commands"/>
    public class ClientConnectionChannelScriptBuilder
    {
        private readonly string _name;
        private readonly string _connectionName;
        private readonly string _queueManager;
        private string _description;
        private bool _defaultReconnection;
        private SslCertificateSectionBuilder _ssl;
        private StringBuilder _scriptStringBuilder;
        private long _maximumMessageLength;
        private bool _replace;

        public ClientConnectionChannelScriptBuilder(string name, string connectionName, string queueManager)
        {
            _name = name;
            _connectionName = connectionName;
            _queueManager = queueManager;
            _defaultReconnection = false;
            _maximumMessageLength = 4194304;
        }

        public ClientConnectionChannelScriptBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ClientConnectionChannelScriptBuilder WithMaximumMessageLength(long messageLength)
        {
            _maximumMessageLength = messageLength;
            return this;
        }

        private string CreateParameter(string name, string value)
        {
            return $"{name}('{value}') ";
        }

        private string CreateParameter(string name, long value)
        {
            return $"{name}({value}) ";
        }

        private string CreateParameter(string name, bool value)
        {
            return $"{name}({(value == true ? "YES" : "NO")}) ";
        }

        private string CreateEnumParameter(string name, string value)
        {
            return $"{name}({value}) ";
        }

        public SslCertificateSectionBuilder WithSsl()
        {
            _ssl = new SslCertificateSectionBuilder();
            return _ssl;
        }

        public ClientConnectionChannelScriptBuilder WithReplace(bool shouldReplace = true)
        {
            _replace = shouldReplace;
            return this;
        }

        public ClientConnectionChannelScriptBuilder WithDefaultReconnection()
        {
            _defaultReconnection = true;
            return this;
        }

        internal string Build()
        {
            _scriptStringBuilder = new StringBuilder();
            _scriptStringBuilder.Append($"DEFINE CHANNEL('{_name}') ");
            _scriptStringBuilder.Append(CreateEnumParameter("CHLTYPE", "CLNTCONN"));
            _scriptStringBuilder.Append(CreateEnumParameter("TRPTYPE", "TCP")); // TODO: allow all supported types.
            _scriptStringBuilder.Append(CreateParameter("DEFRECON", _defaultReconnection));
            _scriptStringBuilder.Append(CreateParameter("QMNAME", _queueManager));
            _scriptStringBuilder.Append(CreateParameter("CONNAME", _connectionName));
            _scriptStringBuilder.Append(CreateParameter("MAXMSGL", _maximumMessageLength));

            if (!string.IsNullOrEmpty(_description))
            {
                _scriptStringBuilder.Append(CreateParameter("DESCR", _description));
            }

            if (_ssl != null)
            {
                _scriptStringBuilder.Append(_ssl.Build());
            }

            if (_replace)
            {
                _scriptStringBuilder.Append(" REPLACE");
            }

            return _scriptStringBuilder.ToString();
        }
    }
}
