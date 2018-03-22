using System;
using System.Collections;

namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
    /// <summary>
    /// Encapsulates the Progress 4GL database information
    /// </summary>
    public class DatabaseInfo
    {
        // (string tableName, TableInfo tableInfo)
        public IDictionary Tables = new SortedList();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tables">Tables of the database</param>
        /// <exception cref="ArgumentException">if tables is null.</exception>
        public DatabaseInfo(IDictionary tables)
        {
            Tables = tables ?? throw new ArgumentNullException("Non-null dictionary is required for the DbInfo");
        }

        /// <summary>
        /// TableInfo by the table name.
        /// </summary>
        public TableInfo this[string tableName]
        {
            get
            {
                return Tables[tableName] as TableInfo;
            }
        }
    }

}
