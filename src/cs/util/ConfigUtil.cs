using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;

namespace DotNetConsoleApp {
    public sealed class ConfigUtil {
        private static AppConfig config = null;
        public static AppConfig GetAppConfig() {
            if (config == null) {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AppConfig));
                string configFilePath = "";
                switch (ComUtil.GetStage()) {
                    case "DEV":
                        configFilePath = "../resource/config/Appconfig.json";
                        break;
                    case "TEST":
                        configFilePath = "../../../../resource/config/Appconfig.json";
                        break;
                    case "PROD":
                        configFilePath = "config/Appconfig.json";
                        break;
                    default:
                        break;
                }
                using(Stream stream = new FileStream(configFilePath, FileMode.Open)) {
                    config = (AppConfig) serializer.ReadObject(stream);
                }
            }
            return config;
        }
    }
}