﻿using ChickenBanana.IbmMq.Tools.Mqsc;
using Xunit;

namespace IbmMQ.ConnectionTest
{
    public class ScriptBuilderTest
    {
        [Fact]
        public void BuildScript_With_Channel_And_Ssl()
        {
            var scriptBuilder = new MqscScriptBuilder("QM1");
            scriptBuilder.AddClientConnectionChannel("DemoChannel", "DEV1")
                .WithDescription("Demo Channel Description")
                .WithDefaultReconnection()
                .WithSsl()
                    .WithCertificateLabel("My first label");

            var script = scriptBuilder.Build();
            Assert.NotNull(script);
        }
    }
}
