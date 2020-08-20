using System;
using System.Collections.Generic;
using Xunit;

namespace DotNetConsoleApp.Test {
  public class TipsTest : IDisposable {
    /// <summary>
    /// このクラスのテストが始まる前に実行される(setup)
    /// </summary>
    public TipsTest() {
      Environment.SetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE, "TEST");
    }

    /// <summary>
    /// このクラスのテストが終了した後に実行される(tearDown)
    /// </summary>
    public void Dispose() {

    }

    /// <summary>
    /// Equalsが正しく実装されていればオブジェクトのリスト同士を比較できるようになる  
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
