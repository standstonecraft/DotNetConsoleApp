using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Dapper;
namespace DotNetConsoleApp {
    class SqlUtil {
        class TableColumn {
            public string ColumnName { get; set; }
            public string TypeName { get; set; }
        }
        /// <summary>シングルトンな接続オブジェクト</summary>
        private static SqlConnection connection = null;
        /// <summary>
        /// データベースへの接続オブジェクトを取得します。  
        /// シングルトンオブジェクトを使いまわすため、オープンやクローズを都度実行しないでください。
        /// </summary>
        public static SqlConnection GetConnection() {
            // なければ作る
            if (connection == null) {
                AppConfig config = ConfigUtil.GetAppConfig();
                String conStr = config.Database.Find(p => p.Profile == ComUtil.GetStage()).ConnectionString;
                connection = new SqlConnection(conStr);
                connection.Open();
            }
            return connection;
        }
        /// <summary>
        /// SQLファイルを読み込みます。
        /// </summary>
        /// <param name="name">SQLファイルの名前(拡張子なし)</param>
        /// <returns>SQL文字列</returns>
        public static String LoadSqlFile(String name) {
            return ComUtil.GetEmbeddedFileContent($"sql/{name}.sql");
        }

        public static void PrintTableDao(String tableName) {
            String sql = SqlUtil.LoadSqlFile("columnList");
            DynamicParameters sqlParams = new DynamicParameters();
            sqlParams.Add("tableName", tableName);
            IEnumerable<TableColumn> enumerable = connection.Query<TableColumn>(sql, sqlParams);

            StringBuilder constSb = new StringBuilder();

            StringBuilder setSb = new StringBuilder();
            StringBuilder memberSb = new StringBuilder();
            int memberCount = 0;
            foreach (TableColumn col in enumerable) {
                String csType = "";
                switch (col.TypeName) {
                    case "bigint":
                        csType = "Int64";
                        break;
                    case "char":
                    case "nchar":
                    case "varchar":
                    case "nvarchar":
                        csType = "String";
                        break;
                    case "datetime":
                        csType = "DateTime";
                        break;
                    case "decimal":
                        csType = "Decimal";
                        break;
                    case "int":
                    case "smallint":
                        csType = "int";
                        break;
                    case "varbinary":
                        csType = "Byte[]";
                        break;
                    default:
                        csType = "undefined";
                        break;
                }
                memberSb.AppendLine($"        public {csType} {col.ColumnName} {{get;}}");
                constSb.Append($"{csType} {col.ColumnName}, ");
                if (memberCount % 4 == 3) {
                    constSb.AppendLine();
                    constSb.Append("                        ");
                }
                memberCount++;
                setSb.AppendLine($"            this.{col.ColumnName} = {col.ColumnName};");
            }
            constSb.Remove(constSb.Length - 2, 2);
            constSb.AppendLine(")");

            Console.WriteLine("using System;");
            Console.WriteLine("namespace DotNetConsoleApp.Dao");
            Console.WriteLine("{");
            Console.WriteLine("    sealed class " + tableName);
            Console.WriteLine("    {");
            Console.Write(memberSb);
            Console.Write($"        public {tableName}(");
            Console.Write(constSb);
            Console.WriteLine("        {");
            Console.Write(setSb);
            Console.WriteLine("        }");
            Console.WriteLine("    }");
            Console.WriteLine("}");
        }
    }
}
