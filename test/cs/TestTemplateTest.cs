using System;
using Xunit;
namespace Name {
  public class TestTemplateTest : IDisposable {

    /// <summary>
    /// このクラスのテストが始まる前に実行される(setup)
    /// </summary>
    public TestTemplateTest() {
      // Environment.SetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE, "TEST");
    }

    /// <summary>
    /// このクラスのテストが終了した後に実行される(tearDown)
    /// </summary>
    public void Dispose() {

    }

    /// <summary>
    /// テストメソッド  
    /// メソッドの外で `Fact` とタイプすればテンプレートがサジェストされる
    /// </summary>
    [Fact]
    public void AdditionTest() {
      //Given
      int numA = 1;
      int numB = 2;
      //When
      int actual = numA + numB;
      //Then
      Assert.Equal(3, actual);
    }
  }
}