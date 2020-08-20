using System;
using Xunit;

namespace DotNetConsoleApp.Test {
  /// <summary>
  /// 起動時の状態の確認
  /// </summary>
  public class LaunchTest : IDisposable {
    /// <summary>
    /// このクラスのテストが始まる前に実行される(setup)
    /// </summary>
    public LaunchTest() {
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

  }
}
