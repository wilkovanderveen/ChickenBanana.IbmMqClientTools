using System.Collections.Generic;
using System.Text;

namespace ChickenBanana.IbmMq.Tools.Certificates
{
    /// <summary>
    /// Builder to simply creating <see cref="https://www.ibm.com/docs/en/ibm-mq/9.1?topic=administering-administration-using-mqsc-commands">MQSC commands</see>.
    /// </summary>
    public class ScriptBuilder
    {
        private readonly string _queueManager;
        private IList<ClientConnectionChannelScriptBuilder> _clientConnections;

        public ScriptBuilder(string queueManager)
        {
            _clientConnections = new List<ClientConnectionChannelScriptBuilder>();
            _queueManager = queueManager;
        }

        public string Build()
        {
            var scriptStringBuilder = new StringBuilder();
            foreach (var clientConnection in _clientConnections)
            {
                scriptStringBuilder.Append(clientConnection.Build());
            }

            return scriptStringBuilder.ToString();
        }

        public ClientConnectionChannelScriptBuilder AddClientConnectionChannel(string name, string connectionName)
        {
            var channelBuilder = new ClientConnectionChannelScriptBuilder(name, connectionName, _queueManager);

            _clientConnections.Add(channelBuilder);
            return channelBuilder;
        }
    }
}
