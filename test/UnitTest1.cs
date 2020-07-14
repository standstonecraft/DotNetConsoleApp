using System;
using Xunit;

namespace DotNetConsoleApp.Test {
    public class UnitTest1 {
        [Fact]
        public void EnvVarTest() {
            string stage = Environment.GetEnvironmentVariable("DNCA_STAGE");
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