using System;

namespace DotNetConsoleApp {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            // 環境変数からステージを取得（launch.json や tasks.json で設定）
            string stage = ComUtil.GetStage();
            Console.WriteLine($"[env]stage: {stage}");
            // プロジェクトに埋め込まれたファイルの取得
            string content = ComUtil.GetEmbeddedFileContent("sql/embedded.sql");
            Console.WriteLine("[embedded file]embedded.sql: " + content.Replace("\r\n", " "));
            // 設定ファイルの取得
            string configContent = ConfigUtil.GetAppConfig().Database.Find(d => d.Profile == "DEV").ConnectionString;
            Console.WriteLine("[AppConfig.json]connection string of profile 'DEV': " + configContent);
        }

    }
}