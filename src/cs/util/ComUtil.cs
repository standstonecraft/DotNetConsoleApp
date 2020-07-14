using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DotNetConsoleApp {
    public static class ComUtil {
        public static string GetEmbeddedFileContent(string filePath) {
            string content;
            string asbName = Assembly.GetExecutingAssembly().GetName().Name;
            using(Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(asbName + "." + Regex.Replace(filePath, @"[/\\]", "."))) {
                content = new StreamReader(stream).ReadToEnd();
            }

            return content;
        }

        public static string GetStage() {
            return Environment.GetEnvironmentVariable("DNCA_STAGE");
        }
    }
}