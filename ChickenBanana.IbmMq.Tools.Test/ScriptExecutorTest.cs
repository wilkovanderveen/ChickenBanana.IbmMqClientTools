using ChickenBanana.IbmMq.Tools.Certificates;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;
using Xunit;

namespace ChickenBanana.IbmMq.Tools.Test
{
    public class ScriptExecutorTest
    {
        [Fact]
        public async Task CreateAndExecuteScript()
        {
            var queueManager = "QM1";

            var scriptBuilder = new ScriptBuilder(queueManager);
            scriptBuilder
                .AddClientConnectionChannel("DemoChannel", "DEV.APP.SVRCONN")
                    .WithReplace()
                    .WithDescription("Demo Channel Description")
                    .WithDefaultReconnection()
                    .WithSsl()
                        .WithCertificateLabel("My first label");

            var script = scriptBuilder.Build();

            var options = new ScriptExecutorOptions
            {
                IbmMqClientFolder = @"C:\Program Files\IBM\MQ\bin"
            };

            var logger = NullLoggerFactory.Instance.CreateLogger("MyLogger");

            var scriptExecutor = new ScriptExecutor(options, logger);
            await scriptExecutor.ExecuteScriptAsync(queueManager, script, default).ConfigureAwait(false);
        }
    }
}
