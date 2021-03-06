﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
    /// <summary>
    /// Encapsulates the Progress 4GL table information
    /// </summary>
    public class TableInfo
    {
        private readonly string _tableDescription;
        private IDictionary<string, FieldInfo> _fields = new SortedList<string, FieldInfo>();

        public string TableDescription { get => _tableDescription; }

        public IDictionary<string, FieldInfo> Fields { get => _fields; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="description">Table sbTableDescription.</param>
        /// <param name="fields">Table fields.</param>
        /// <exception cref="ArgumentException">if tableName or fields is null.</exception>
        public TableInfo(StringBuilder sbTableDescription, IDictionary<string, FieldInfo> fields)
        {
            _fields = fields ?? throw new ArgumentNullException("Non-null fields are required for the TableInfo");
            _tableDescription = Utils.AdjustNewLine(sbTableDescription).ToString() ?? "";
        }

        /// <summary>
        /// FieldInfo by the field name.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns>Field info by the field name.</returns>
        public FieldInfo this[string fieldName] { get => _fields[fieldName]; }
    }
}
