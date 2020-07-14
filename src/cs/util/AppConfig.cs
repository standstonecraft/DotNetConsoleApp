using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetConsoleApp {
    [DataContract]
    public sealed class AppConfig {
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