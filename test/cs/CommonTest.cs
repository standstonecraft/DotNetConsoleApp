using System;
using System.Collections.Generic;
using Xunit;

namespace DotNetConsoleApp.Test {
  public class CommonTest : IDisposable {
    /// <summary>
    /// このクラスのテストが始まる前に実行される(setup)
    /// </summary>
    public CommonTest() {
      Environment.SetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE, "TEST");
    }

    /// <summary>
    /// このクラスのテストが終了した後に実行される(tearDown)
    /// </summary>
    public void Dispose() {

    }

    [Fact]
    public void EnvrionmentVariableTest() {
      string stage = Environment.GetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE);
      Assert.Equal("TEST", stage);
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

    [Fact]
    public void X64Test() {
      string msg = IntPtr.Size == 4 ? "32ビットで動作しています" : "64ビットで動作しています";
      Assert.Equal("64ビットで動作しています", msg);
    }

    /// <summary>
    /// テスト実行時のターゲットフレームワークの確認
    /// </summary>
    [Fact]
    public void TestTargetFrameworkTest() {
#if NET47
      Assert.True(true);
#else
            Assert.True(false);
#endif
    }

    /// <summary>
    /// クラスにEqualsを実装する方法
    /// - 対象クラスのエディタ上でクラス名にカーソルを合わせて Ctrl + .
    /// - 「Equals 及び GetHashCode を生成する」を選択する
    /// </summary>
    [Fact]
    public void ObjectCollectionEqualsTest() {
      List<Dao.SampleTableDao> t1 = new List<Dao.SampleTableDao>();
      List<Dao.SampleTableDao> t2 = new List<Dao.SampleTableDao>();
      for (int i = 0; i < 5; i++) {
        Dao.SampleTableDao r1 = new Dao.SampleTableDao(i, $"{i}_str");
        t1.Add(r1);
        Dao.SampleTableDao r2 = new Dao.SampleTableDao(i, $"{i}_str");
        t2.Add(r2);
      }
      Assert.Equal(t1, t2);
    }

  }
}
