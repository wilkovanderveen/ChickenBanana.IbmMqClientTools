using ChickenBanana.IbmMq.Tools.Certificates;
using Xunit;

namespace IbmMQ.ConnectionTest
{
    public class ScriptGeneratorTest
    {
        [Fact]
        public void DoSomething()
        {
            var scriptGenerator = new ScriptGenerator("QM1", (builder) =>
            {
                var myBuilder = new ScriptBuilder("QM1");
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

    public class ScriptBuilderTest
    {
        [Fact]
        public void BuildScript()
        {
            var scriptBuilder = new ScriptBuilder("QM1");
            scriptBuilder.AddClientConnectionChannel("DemoChannel", "DEV1")
                .WithDescription("Demo Channel Description")
                .WithDefaultReconnection()
                .WithSsl()
                    .WithCertificateLabel("My first label");


            var script = scriptBuilder.Build();
        }
    }
}
