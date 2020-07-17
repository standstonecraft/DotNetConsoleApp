using System;
using Xunit;

namespace DotNetConsoleApp.Test {
    public class UnitTest1 : IDisposable {
        /// <summary>
        /// このクラスのテストが始まる前に実行される(setup)
        /// </summary>
        public UnitTest1() {
            Environment.SetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE, "TEST");
        }

        /// <summary>
        /// このクラスのテストが終了した後に実行される(tearDown)
        /// </summary>
        public void Dispose() {

        }

        [Fact]
        public void EnvVarTest() {
            string stage = Environment.GetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE);
            Assert.Equal("TEST", stage);
        }

        [Fact]
        public void EmbeddedFileTest() {
            Assert.Equal("SELECT\r\n    col\r\nFROM\r\n    tbl;", ComUtil.GetEmbeddedFileContent("sql/embedded.sql"));
        }

        [Fact]
        public void AppConfigTest() {
            Assert.Equal("DEV", ConfigUtil.GetAppConfig().Database[0].Profile);
        }
    }
}
