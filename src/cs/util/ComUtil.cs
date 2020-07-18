using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DotNetConsoleApp {
    public static class ComUtil {
        public const string ENV_DNCA_STAGE = "DNCA_STAGE";
        public static string GetEmbeddedFileContent(string filePath) {
            string content;
            string asbName = Assembly.GetExecutingAssembly().GetName().Name;
            using(Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(asbName + "." + Regex.Replace(filePath, @"[/\\]", "."))) {
                content = new StreamReader(stream).ReadToEnd();
            }

            return content;
        }

        public static string GetStage() {
            return Environment.GetEnvironmentVariable(ENV_DNCA_STAGE);
        }

        public static string GetCurrentDir() {
            string dir;
            switch (GetStage()) {
                case "DEV":
                    dir = Directory.GetCurrentDirectory() + "./";
                    break;
                case "TEST":
                    dir = Directory.GetCurrentDirectory() + "../../../../../";
                    break;
                case "PROD":
                    dir = Directory.GetCurrentDirectory() + "./";
                    break;
                default:
                    // test in contextコマンドでは環境変数がセットされていないためTESTとみなす
                    dir = Directory.GetCurrentDirectory() + "../../../../../";
                    break;
            }
            return dir;
        }
    }
}
