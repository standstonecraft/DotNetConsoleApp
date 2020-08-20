
namespace DotNetConsoleApp {

  /// <summary>
  /// メイン処理を実装するクラス。
  /// </summary>
  class Program {

    /// <summary>
    /// メイン処理
    /// </summary>
    /// <returns></returns>
    static void Main() {
      NLog.Logger logger = LogUtil.Log;

      logger.Info("Hello World!");
      logger.Info($"Stage: {ComUtil.GetStage()}");
      logger.Info($"Current directory: {ComUtil.GetCurrentDir()}");
    }

  }
}
