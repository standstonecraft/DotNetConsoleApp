using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;

namespace DotNetConsoleApp {
    public sealed class ConfigUtil {
        private static AppConfig config = null;
        public static AppConfig GetAppConfig() {
            if (config == null) {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AppConfig));
                string configFilePath = ComUtil.GetCurrentDir() + "/resource/config/Appconfig.json";
                using(Stream stream = new FileStream(configFilePath, FileMode.Open)) {
                    config = (AppConfig) serializer.ReadObject(stream);
                }
            }
            return config;
        }
    }
}
