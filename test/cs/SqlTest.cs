using System;
using System.Collections.Generic;
using Dapper;
using Xunit;

namespace DotNetConsoleApp.Test {
    public class SqlTest : IDisposable {
        /// <summary>
        /// このクラスのテストが始まる前に実行される(setup)
        /// </summary>
        public SqlTest() {
            Environment.SetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE, "TEST");
        }

        /// <summary>
        /// このクラスのテストが終了した後に実行される(tearDown)
        /// </summary>
        public void Dispose() {

        }

        /// <summary>
        /// DB接続確認
        /// </summary>
        [Fact]
        public void QueryRealTableTest() {
            SqlUtil.DefaultConnection.Query("select * from SAMPLE_TABLE");
            // no exception
        }

        /// <summary>
        /// クエリーサンプル
        /// </summary>
        [Fact]
        public void SqlSampleTest() {

            string ddl = @"
            CREATE TABLE #TEMP_SAMPLE_TABLE
            (
                COL1 INT NOT NULL PRIMARY KEY,
                COL2 NVARCHAR(50)
            );
            ";
            int createReturn = SqlUtil.DefaultConnection.Execute(ddl);
            Assert.Equal(createReturn, -1);

            // INSERT
            List<Dao.SampleTable> items = new List<Dao.SampleTable>();
            for (int i = 0; i < 3; i++) {
                items.Add(new Dao.SampleTable(i, "row" + i));
            }
            string insSql = "INSERT INTO #TEMP_SAMPLE_TABLE (COL1, COL2) VALUES (@Col1, @Col2)";
            int insertReturn = SqlUtil.DefaultConnection.Execute(insSql, items);
            Assert.Equal(3, insertReturn);

            // SELECT バインド変数付き
            string selSql = "SELECT COL1, COL2 FROM #TEMP_SAMPLE_TABLE WHERE COL1 > @Cond ORDER BY COL1";
            List<Dao.SampleTable> selected = SqlUtil.DefaultConnection.Query<Dao.SampleTable>(selSql, new { Cond = 0 }).AsList();
            Assert.Equal(2, selected.Count);
            Assert.Equal("row2", selected[1].Col2);
        }
    }
}
