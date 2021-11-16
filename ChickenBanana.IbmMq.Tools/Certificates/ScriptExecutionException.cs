using System.Runtime.Serialization;

namespace ChickenBanana.IbmMq.Tools.Certificates
{
    [System.Serializable]
    internal class ScriptExecutionException : System.Exception
    {
        public ScriptExecutionException(int exitcode) : base($"Process exited with unexpected exitcode ({exitcode})")
        {
        }

        public ScriptExecutionException(string message) : base(message)
        {
        }

        public ScriptExecutionException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ScriptExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
