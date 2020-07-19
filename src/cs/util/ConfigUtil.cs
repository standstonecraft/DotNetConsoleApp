using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;

namespace DotNetConsoleApp {

    /// <summary>
    /// 設定ファイルに関する共通部品を実装するクラス。
    /// </summary>
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

        /// <summary>
        /// 必要な情報が設定ファイルになかった場合に使用する例外
        /// </summary>
        [System.Serializable]
        public class ConfigMissingException : System.Exception {

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public ConfigMissingException() {}

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="message"></param>
            public ConfigMissingException(string message) : base(message) {}

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="message"></param>
            /// <param name="inner"></param>
            public ConfigMissingException(string message, System.Exception inner) : base(message, inner) {}

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="info"></param>
            /// <param name="context"></param>
            protected ConfigMissingException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
        }
    }
}
