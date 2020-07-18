using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetConsoleApp {
    /// <summary>
    /// 設定ファイルの内容をマッピングするオブジェクト
    /// </summary>
    [DataContract]
    public sealed class AppConfigJson {
        [DataMember]
        public List<DatabaseConfig> Database { get; set; }

        [DataContract]
        public sealed class DatabaseConfig {
            [DataMember]
            public string Profile { get; set; }

            [DataMember]
            public string ConnectionString { get; set; }
        }
    }

}
