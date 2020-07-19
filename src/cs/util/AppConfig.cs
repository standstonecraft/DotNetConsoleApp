using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetConsoleApp {

    /// <summary>
    /// 設定ファイルの内容がマッピングされるクラス。
    /// </summary>
    [DataContract]
    public sealed class AppConfigJson {

        /// <summary>
        /// データベースの接続情報
        /// </summary>
        /// <value></value>
        [DataMember]
        public List<DatabaseConfig> Database { get; set; }

        /// <summary>
        /// データベースの接続情報
        /// </summary>
        [DataContract]
        public sealed class DatabaseConfig {

            /// <summary>
            /// 接続設定を特定する一意な名前
            /// </summary>
            /// <value></value>
            [DataMember]
            public string Profile { get; set; }

            /// <summary>
            /// 接続文字列
            /// </summary>
            /// <value></value>
            [DataMember]
            public string ConnectionString { get; set; }
        }
    }

}
