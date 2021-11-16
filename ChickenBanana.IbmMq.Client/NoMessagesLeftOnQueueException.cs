using System;
using System.Runtime.Serialization;

namespace IbmMQ.ConnectionTest
{
    [Serializable]
    internal class NoMessagesLeftOnQueueException : Exception
    {
        public NoMessagesLeftOnQueueException()
        {
        }

        public NoMessagesLeftOnQueueException(string queueName) : base($"No messages are left on queue {queueName}")
        {
        }

        public NoMessagesLeftOnQueueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoMessagesLeftOnQueueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
