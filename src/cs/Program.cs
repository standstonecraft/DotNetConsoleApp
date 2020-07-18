using System;
using System.Text.RegularExpressions;
using Dapper;

namespace DotNetConsoleApp {
    class Program {
        static void Main() {

            Console.WriteLine("Hello World!");
            // 環境変数からステージを取得（launch.json や tasks.json で設定）
            string stage = ComUtil.GetStage();
            Console.WriteLine($"[env]stage: {stage}");
            // プロジェクトに埋め込まれたファイルの取得
            string content = ComUtil.GetEmbeddedFileContent("sql/embedded.sql");
            Console.WriteLine("[embedded file]embedded.sql: " + Regex.Replace(content, @"\r?\n", " "));
            // 設定ファイルの取得
            string configContent = ConfigUtil.AppConfig.Database.Find(d => d.Profile == "DEV").ConnectionString;
            Console.WriteLine("[AppConfig.json]connection string of profile 'DEV': " + configContent);
            string msg = IntPtr.Size == 4 ? "32ビットで動作しています" : "64ビットで動作しています";
            Console.WriteLine(msg);
            Console.WriteLine("table1 count: " + SqlUtil.DefaultConnection.Query("select * from Database1.dbo.table1").AsList().Count);
        }

    }
}
