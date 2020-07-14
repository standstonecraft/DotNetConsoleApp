using System;
using Xunit;

namespace DotNetConsoleApp.Test {
    public class UnitTest1 {
        [Fact]
        public void envVarTest() {
            string stage = Environment.GetEnvironmentVariable("DNCA_STAGE");
            Assert.Equal("TEST", stage);
        }
    }
}