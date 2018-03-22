using System;
using System.Collections;

namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
    /// <summary>
    /// Encapsulates the Progress 4GL table information
    /// </summary>
    public class TableInfo
    {
        private string _tableDescription = "";

        // (string fieldName, FieldInfo fieldInfo)
        public IDictionary Fields = new SortedList();

        public string TableDescription { get => _tableDescription; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="description">Table description.</param>
        /// <param name="fields">Table fields.</param>
        /// <exception cref="ArgumentException">if tableName or fields is null.</exception>
        public TableInfo(string description, IDictionary fields)
        {
            Fields = fields ?? throw new ArgumentNullException("Non-null fields are required for the TableInfo");
            _tableDescription = description ?? "";
        }

        /// <summary>
        /// FieldInfo by the field name.
        /// </summary>
        public FieldInfo this[string fieldName]
        {
            get
            {
                return Fields[fieldName] as FieldInfo;
            }
        }
    }
}
