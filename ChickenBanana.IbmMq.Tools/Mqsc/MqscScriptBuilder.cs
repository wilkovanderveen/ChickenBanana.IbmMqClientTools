using System.Collections.Generic;
using System.Text;

namespace ChickenBanana.IbmMq.Tools.Mqsc
{
    /// <summary>
    /// Builder to simply creating <see cref="https://www.ibm.com/docs/en/ibm-mq/9.1?topic=administering-administration-using-mqsc-commands">MQSC commands</see>.
    /// </summary>
    public class MqscScriptBuilder
    {
        private readonly string _queueManager;
        private IList<ClientConnectionChannelScriptBuilder> _clientConnections;

        public MqscScriptBuilder(string queueManager)
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

        /// <summary>
        /// Adds a client connection channel to the MQ instance.
        /// </summary>
        /// <param name="serverConnectionName"></param>
        /// <param name="clientConnectionName"></param>
        /// <returns></returns>
        public ClientConnectionChannelScriptBuilder AddClientConnectionChannel(string serverConnectionName, string clientConnectionName)
        {
            var channelBuilder = new ClientConnectionChannelScriptBuilder(serverConnectionName, clientConnectionName, _queueManager);

            _clientConnections.Add(channelBuilder);
            return channelBuilder;
        }
    }
}
