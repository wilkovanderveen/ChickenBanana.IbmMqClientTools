using System.Collections.Generic;
using System.Text;

namespace ChickenBanana.IbmMq.Tools.Certificates
{
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
