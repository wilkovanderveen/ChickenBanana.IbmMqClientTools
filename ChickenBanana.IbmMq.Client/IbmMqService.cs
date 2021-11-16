using IBM.WMQ;
using IbmMQ.ConnectionTest;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ChickenBanana.IbmMq.Client
{
    public class IbmMqService : IMqService
    {
        private readonly IMqAuthorizationStrategy authorizationStrategy;
        private readonly ILogger logger;
        private readonly MqServiceSettings settings;

        public IbmMqService(IMqAuthorizationStrategy authorizationStrategy, ILogger logger, MqServiceSettings settings)
        {
            this.authorizationStrategy = authorizationStrategy;
            this.logger = logger;
            this.settings = settings;

            Environment.SetEnvironmentVariable("MQCHLLIB", @"C:\Users\Wilko\source\repos\IbmMQ.ConnectionTest\IbmMQ.ConnectionTest");
            Environment.SetEnvironmentVariable("MQCHLTAB", "AMQCLCHL.TAB");
        }

        public void SendMessage(string queueName, string payload)
        {
            var queueManager = GetQueueManager();

            int openOptions = MQC.MQOO_INPUT_AS_Q_DEF | MQC.MQOO_OUTPUT;
            var queue = queueManager.AccessQueue(queueName, openOptions);
            var outgoingMessage = new MQMessage();
            outgoingMessage.WriteUTF(payload);

            queue.Put(outgoingMessage);

            queue.Close();
            queueManager.Close();
            queueManager.Disconnect();

            logger.LogDebug($"Done putting message on queue {queueName}");

        }

        public string RetrieveMessage(string queueName)
        {
            var queueManager = GetQueueManager();

            return RetrieveSingleMessage(queueManager, queueName);
        }

        private string RetrieveSingleMessage(MQQueueManager queueManager, string queueName)
        {

            var incomingMessage = new MQMessage();
            int openOptions = MQC.MQOO_INPUT_AS_Q_DEF | MQC.MQOO_OUTPUT | MQC.MQOO_INQUIRE;

            var queue = queueManager.AccessQueue(queueName, openOptions);

            if (queue.CurrentDepth == 0)
            {
                throw new NoMessagesLeftOnQueueException(queueName);
            }

            var message = ProcessIncomingMessage(incomingMessage, queue);

            queue.Close();
            queueManager.Close();
            queueManager.Disconnect();

            return message;

        }

        private static string ProcessIncomingMessage(MQMessage incomingMessage, MQQueue queue)
        {
            var options = new MQGetMessageOptions();
            queue.Get(incomingMessage, options);
            var payload = incomingMessage.ReadUTF();
            return payload;
        }

        public IEnumerable<string> RetrieveMessages(string queueName)
        {
            var queueManager = GetQueueManager();
            var incomingMessage = new MQMessage();
            int openOptions = MQC.MQOO_INPUT_AS_Q_DEF | MQC.MQOO_OUTPUT | MQC.MQOO_INQUIRE;

            var results = new List<string>();
            var queueDepth = 0;

            do
            {
                var queue = queueManager.AccessQueue(queueName, openOptions);
                queueDepth = queue.CurrentDepth;
                results.Add(ProcessIncomingMessage(incomingMessage, queue));
                queue.Close();
            }
            while (queueDepth > 0);

            queueManager.Disconnect();

            return results;
        }

        private MQQueueManager GetQueueManager()
        {
            return new MQQueueManager(settings.QueueManagerName, GetQueueManagerProperties());
        }

        private Hashtable GetQueueManagerProperties()
        {
            var optionsHashtable = new Hashtable
            {
                { MQC.CHANNEL_PROPERTY, settings.Channel },
                { MQC.HOST_NAME_PROPERTY, settings.Hostname }
            };

            authorizationStrategy.Apply(optionsHashtable);

            return optionsHashtable;
        }
    }
}
