using System;
using Xunit;

namespace DotNetConsoleApp.Test {
  public class UtilTest : IDisposable {
    /// <summary>
    /// このクラスのテストが始まる前に実行される(setup)
    /// </summary>
    public UtilTest() {
      Environment.SetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE, "TEST");
    }

    /// <summary>
    /// このクラスのテストが終了した後に実行される(tearDown)
    /// </summary>
    public void Dispose() {

    }

    [Fact]
    public void EmbeddedFileTest() {
      string expected = "SELECT\r\n    COL1\r\nFROM\r\n    SAMPLE_TABLE;";
      string actual = ComUtil.GetEmbeddedFileContent("sql/sampleSql.sql");
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void AppConfigTest() {
      Assert.Equal("DEV", ConfigUtil.AppConfig.Database[0].Profile);
    }

  }
}
