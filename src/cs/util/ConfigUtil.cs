using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;

namespace DotNetConsoleApp {
    public sealed class ConfigUtil {
        /// <summary>シングルトンな設定オブジェクト</summary>
        public static readonly AppConfigJson AppConfig;

        /// <summary>
        /// スタティックイニシャライザ(このクラスへの初めてのアクセス時に実行される)
        /// </summary>
        static ConfigUtil() {
            if (AppConfig == null) {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AppConfigJson));
                string configFilePath = ComUtil.GetCurrentDir() + "/resource/config/Appconfig.json";
                using(Stream stream = new FileStream(configFilePath, FileMode.Open)) {
                    AppConfig = (AppConfigJson) serializer.ReadObject(stream);
                }
            }
        }
    }
}
