using System;
using System.Collections;
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

        [Fact]
        public void QueryRealTableTest() {
            SqlUtil.DefaultConnection.Query("select * from SAMPLE_TABLE");
            // no exception
        }

        [Fact]
        public void TestName() {
            //Given

            //When

            //Then
        }
    }
}
