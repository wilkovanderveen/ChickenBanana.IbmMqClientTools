using System;
using System.IO;

namespace ChickenBanana.IbmMq.Tools.Certificates
{
    public class ScriptGenerator
    {
        private string _script;

        public ScriptGenerator(string queueManager, Func<ScriptBuilder, string> scriptGenerationFunction)
        {
            _script = scriptGenerationFunction.Invoke(new ScriptBuilder(queueManager));

        }

        public void WriteToFile(string filename)
        {
            File.WriteAllText(filename, _script);
        }
    }
}
