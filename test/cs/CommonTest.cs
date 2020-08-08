using System;
using System.Collections;
using System.Collections.Generic;
using Dapper;
using Xunit;

namespace DotNetConsoleApp.Test {
    public class CommonTest : IDisposable {
        /// <summary>
        /// このクラスのテストが始まる前に実行される(setup)
        /// </summary>
        public CommonTest() {
            Environment.SetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE, "TEST");
        }

        /// <summary>
        /// このクラスのテストが終了した後に実行される(tearDown)
        /// </summary>
        public void Dispose() {

        }

        [Fact]
        public void EnvrionmentVariableTest() {
            string stage = Environment.GetEnvironmentVariable(ComUtil.ENV_DNCA_STAGE);
            Assert.Equal("TEST", stage);
        }

        [Fact]
        public void EmbeddedFileTest() {
            Assert.Equal("SELECT\r\n    col\r\nFROM\r\n    tbl;", ComUtil.GetEmbeddedFileContent("sql/embedded.sql"));
        }

        [Fact]
        public void AppConfigTest() {
            Assert.Equal("DEV", ConfigUtil.AppConfig.Database[0].Profile);
        }

        [Fact]
        public void X64Test() {
            string msg = IntPtr.Size == 4 ? "32ビットで動作しています" : "64ビットで動作しています";
            Assert.Equal("64ビットで動作しています", msg);
        }

        [Fact]
        public void SqlTest() {
            SqlUtil.DefaultConnection.Query("select * from Database1.dbo.table1");
            // no exception
        }

        /// <summary>
        /// クラスにEqualsを実装する方法
        /// - 対象クラスのエディタ上でクラス名にカーソルを合わせて Ctrl + .
        /// - 「Equals 及び GetHashCode を生成する」を選択する
        /// </summary>
        [Fact]
        public void ObjectCollectionEqualsTest() {
            List<Dao.Table1> t1 = new List<Dao.Table1>();
            List<Dao.Table1> t2 = new List<Dao.Table1>();
            for (int i = 0; i < 5; i++) {
                Dao.Table1 r1 = new Dao.Table1(i, $"{i}_str");
                t1.Add(r1);
                Dao.Table1 r2 = new Dao.Table1(i, $"{i}_str");
                t2.Add(r2);
            }
            Assert.Equal(t1, t2);
        }

    }
}
