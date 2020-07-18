using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using Dapper;
namespace DotNetConsoleApp {
    class SqlUtil {
        static SqlUtil() {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        /// <summary>
        /// <see cref="GenerateTableDao(string, string, DynamicParameters)" />で使用するクラス
        /// </summary>
        class TableColumn {
            public string Name { get; set; }
            public string SystemTypeName { get; set; }
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
                string conStr = config.Database.Find(p => p.Profile == ComUtil.GetStage()).ConnectionString;
                connection = new SqlConnection(conStr);
                connection.Open();
            }
            return connection;
        }

        /// <summary>
        /// SQL ファイルを読み込みます。
        /// </summary>
        /// <param name="name">SQL ファイルの名前(拡張子なし)</param>
        /// <returns>SQL 文字列</returns>
        public static string LoadSqlFile(string name) {
            return ComUtil.GetEmbeddedFileContent($"sql/{name}.sql");
        }

        public static string GenerateTableDao(string sqlFileName) {
            return GenerateTableDao(sqlFileName, LoadSqlFile(sqlFileName), null);
        }

        /// <summary>
        /// SQL ファイルから DAO を生成します。
        /// </summary>
        /// <param name="sqlFileName">sql フォルダ内の SQL の名前(フォルダ名及び拡張子なし)</param>
        /// <param name="sqlParams">SQL のバインド変数</param>
        /// <returns>DAO の文字列</returns>
        public static string GenerateTableDao(string sqlFileName, DynamicParameters sqlParams) {
            return GenerateTableDao(sqlFileName, LoadSqlFile(sqlFileName), sqlParams);
        }

        /// <summary>
        /// SELECT 文から DAO を生成します。
        /// </summary>
        /// <param name="daoName">生成するクラスの名前</param>
        /// <param name="query">SELECT文</param>
        /// <param name="sqlParams">SQLのバインド変数</param>
        /// <returns>DAO の文字列</returns>
        public static string GenerateTableDao(string daoName, string query, DynamicParameters sqlParams) {
            string sql = $"DECLARE @query nvarchar(max) = '{query.Replace("'","''")}';EXEC sp_describe_first_result_set @query, null, 0;";

            SqlConnection con = GetConnection();
            IEnumerable < TableColumn > enumerable;
            if (sqlParams is null) {
                enumerable = con.Query < TableColumn > (sql);
            } else {
                enumerable = con.Query < TableColumn > (sql, sqlParams);
            }

            string pascalDaoName = ToPascalCase(daoName);
            StringBuilder paramSb = new StringBuilder();
            StringBuilder setSb = new StringBuilder();
            StringBuilder memberSb = new StringBuilder();
            int memberCount = 0;
            foreach (TableColumn col in enumerable) {
                string csType = "";
                csType = ConvertTypeName(col.SystemTypeName);
                string pascalColName = ToPascalCase(col.Name);
                memberSb.AppendLine($"        /// <summary>{col.Name}:{col.SystemTypeName}</summary>");
                memberSb.AppendLine($"        public {csType} {pascalColName} {{get;}}");
                memberSb.AppendLine();
                paramSb.Append($"{csType} {col.Name}, ");
                if (memberCount % 4 == 3) {
                    paramSb.AppendLine();
                    paramSb.Append("                        ");
                }
                memberCount++;
                setSb.AppendLine($"            this.{pascalColName} = {col.Name};");
            }
            paramSb.Remove(paramSb.Length - 2, 2);
            paramSb.AppendLine(")");

            StringBuilder result = new StringBuilder();
            result.AppendLine("using System;");
            result.AppendLine("namespace DotNetConsoleApp.Dao");
            result.AppendLine("{");
            result.AppendLine("    sealed class " + pascalDaoName);
            result.AppendLine("    {");
            result.Append(memberSb);
            result.Append($"        public {pascalDaoName}(");
            result.Append(paramSb);
            result.AppendLine("        {");
            result.Append(setSb);
            result.AppendLine("        }");
            result.AppendLine("    }");
            result.AppendLine("}");

            return result.ToString();
        }

        /// <summary>
        /// "THE_String_VALUE => TheStringValue
        /// </summary>
        /// <param name="str">変換する文字列</param>
        /// <returns>パスカルケースの文字列</returns>
        private static string ToPascalCase(string str) {
            var newStr = str.ToLower().Replace("_", " ");
            TextInfo info = CultureInfo.CurrentCulture.TextInfo;
            newStr = info.ToTitleCase(newStr).Replace(" ", string.Empty);
            return newStr;
        }

        /// <summary>
        /// nvarchar(50) => "string"
        /// </summary>
        /// <param name="typeName">SQL Server の型の名前</param>
        /// <seealso href="https://docs.microsoft.com/ja-jp/dotnet/framework/data/adonet/sql-server-data-type-mappings">XML コメントによるコードの文書化 | Microsoft Docs</seealso>
        /// <returns>C# の型名</returns>
        private static string ConvertTypeName(string typeName) {
            string csType;
            switch (typeName.Split('(') [0]) {
                case "bit":
                    csType = "bool";
                    break;
                case "tinyint":
                    csType = "byte";
                    break;
                case "binary":
                case "image":
                case "rowversion":
                case "timestamp":
                case "varbinary":
                    csType = "byte[]";
                    break;
                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    csType = "DateTime";
                    break;
                case "datetimeoffset":
                    csType = "DateTimeOffset";
                    break;
                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    csType = "decimal";
                    break;
                case "float":
                    csType = "double";
                    break;
                case "uniqueidentifier":
                    csType = "Guid";
                    break;
                case "smallint":
                case "int":
                    csType = "int";
                    break;
                case "bigint":
                    // Int64
                    csType = "long";
                    break;
                case "sql_variant":
                    csType = "object";
                    break;
                case "real":
                    // Single  
                    csType = "float";
                    break;
                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                    csType = "string";
                    break;
                case "time":
                    csType = "TimeSpan";
                    break;
                case "xml":
                    csType = "Xml";
                    break;
                default:
                    csType = "undefined";
                    break;
            }

            return csType;
        }
    }
}
