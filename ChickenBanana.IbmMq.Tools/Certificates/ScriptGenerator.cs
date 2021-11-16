using ChickenBanana.IbmMq.Tools.Mqsc;
using System;
using System.IO;

namespace ChickenBanana.IbmMq.Tools.Certificates
{
    public class ScriptGenerator
    {
        private string _script;

        public ScriptGenerator(string queueManager, Func<MqscScriptBuilder, string> scriptGenerationFunction)
        {
            _script = scriptGenerationFunction.Invoke(new MqscScriptBuilder(queueManager));

        }

        public void WriteToFile(string filename)
        {
            File.WriteAllText(filename, _script);
        }
    }
}
