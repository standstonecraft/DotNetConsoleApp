using System;
using System.Collections.Generic;

namespace DotNetConsoleApp.Dao {
    public sealed class Table1 {
        /// <summary>col1:int</summary>
        public int Col1 { get; }

        /// <summary>col2:nvarchar(50)</summary>
        public string Col2 { get; }

        public Table1(int col1, string col2) {
            this.Col1 = col1;
            this.Col2 = col2;
        }

        public override bool Equals(object obj) {
            return obj is Table1 table &&
                Col1 == table.Col1 &&
                Col2 == table.Col2;
        }

        public override int GetHashCode() {
            int hashCode = 1638584441;
            hashCode = hashCode * -1521134295 + Col1.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Col2);
            return hashCode;
        }
    }
}
