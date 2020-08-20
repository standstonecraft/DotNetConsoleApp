using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DotNetConsoleApp {

  /// <summary>
  /// 共通部品を実装するクラス。特定の関心事に関する関数が複数ある場合は別のクラスに切り出すこと。
  /// </summary>
  public static class ComUtil {

    /// <summary>
    /// ステージングを指定する環境変数の名前
    /// </summary>
    public const string ENV_DNCA_STAGE = "DNCA_STAGE";

    /// <summary>
    /// プログラムに埋め込まれたファイルの内容を取得します。  
    /// ファイルを埋め込むには .csproj で ItemGroup 要素に EmbeddedResource 要素を追加します。
    /// </summary>
    /// <param name="filePath">プログラムに埋め込まれたファイルのパス</param>
    /// <returns>ファイルの内容</returns>
    public static string GetEmbeddedFileContent(string filePath) {
      string content;
      string asbName = Assembly.GetExecutingAssembly().GetName().Name;
      using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(asbName + "." + Regex.Replace(filePath, @"[/\\]", "."))) {
        content = new StreamReader(stream).ReadToEnd();
      }

      return content;
    }

    /// <summary>
    /// 環境変数で指定されたステージングを取得します。
    /// </summary>
    /// <returns>環境変数で指定されたステージング</returns>
    public static string GetStage() {
      return Environment.GetEnvironmentVariable(ENV_DNCA_STAGE);
    }

    /// <summary>
    /// ステージングに対応したカレントディレクトリを取得します。  
    /// (ステージング及び実行方法によって .exe の位置が異なるため)
    /// </summary>
    /// <returns>カレントディレクトリ</returns>
    public static string GetCurrentDir() {
      return GetCurrentDir(GetStage());
    }

    /// <summary>
    /// ステージングに対応したカレントディレクトリを取得します。  
    /// (ステージング及び実行方法によって .exe の位置が異なるため)
    /// </summary>
    /// <param name="stage"></param>
    /// <returns>カレントディレクトリ</returns>
    public static string GetCurrentDir(string stage) {
      string dir;
      switch (stage) {
        case "DEV":
          dir = Directory.GetCurrentDirectory() + "/./";
          break;
        case "TEST":
          dir = Directory.GetCurrentDirectory() + "/../../../../";
          break;
        case "PROD":
          dir = Directory.GetCurrentDirectory() + "/./";
          break;
        default:
          // test in contextコマンドでは環境変数がセットされていないためTESTとみなす
          dir = Directory.GetCurrentDirectory() + "/../../../../";
          break;
      }
      return Path.GetFullPath(dir);
    }
  }
}
