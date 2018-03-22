using System;
using System.Collections.Generic;

namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
    /// <summary>
    /// Encapsulates the Progress 4GL database information
    /// </summary>
    public class DatabaseInfo
    {
        // (string tableName, TableInfo tableInfo)
        private IDictionary<string, TableInfo> _tables = new SortedList<string, TableInfo>();

        public IDictionary<string, TableInfo> Tables { get => _tables; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tables">Tables of the database</param>
        /// <exception cref="ArgumentException">if tables is null.</exception>
        public DatabaseInfo(IDictionary<string, TableInfo> tables)
        {
            _tables = tables ?? throw new ArgumentNullException("Non-null tables dictionary is required for the DatabaseInfo");
        }

        /// <summary>
        /// TableInfo by the table name.
        /// </summary>
        public TableInfo this[string tableName] { get => _tables[tableName]; }
    }

}
