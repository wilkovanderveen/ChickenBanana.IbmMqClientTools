using ChickenBanana.IbmMq.Tools.Certificates;
using ChickenBanana.IbmMq.Tools.Mqsc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
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

            var scriptBuilder = new MqscScriptBuilder(queueManager);
            scriptBuilder
                .AddClientConnectionChannel("DEV.APP.SVRCONN", "DEV.APP.CLIENTCONN")
                    .WithReplace()
                    .WithDescription("Client Connection Channel")
                    .WithDefaultReconnection()
                    .WithSsl()
                        .WithCertificateLabel("ibmwebspheremqqm1");

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
