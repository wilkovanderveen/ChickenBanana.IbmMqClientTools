using ChickenBanana.IbmMq.Tools.Certificates;
using ChickenBanana.IbmMq.Tools.Mqsc;
using Xunit;

namespace IbmMQ.ConnectionTest
{
    public class ScriptGeneratorTest
    {
        [Fact]
        public void GenerateScript_QM1_DEV()
        {
            var scriptGenerator = new MqscScriptGenerator("QM1", (builder) =>
            {
                var myBuilder = new MqscScriptBuilder("QM1");
                myBuilder
                    .AddClientConnectionChannel("DEV.APP.SVRCONN", "DEV.ClientConnection")
                    .WithDescription("Demo TAB")
                    .WithDefaultReconnection()
                    .WithSsl()
                    .WithCertificateLabel("QueueManagerCertificate");
                return myBuilder.Build();
                 
            });

            scriptGenerator.WriteToFile(@"C:\TEMP\CHANNELSCRIPT");
        }
    }
}
