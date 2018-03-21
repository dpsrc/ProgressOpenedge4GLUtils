using System;
using System.Collections;
using System.IO;

namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
	/// <summary>
	/// Encapsulates the field information
	/// </summary>
	public class FieldInfo
	{
		private static readonly string _signatureDescription = "DESCRIPTION \"";
		private static readonly int _signatureDescriptionLen = _signatureDescription.Length;
        private string _fieldDescription = "";

        public string FieldDescription { get => _fieldDescription; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fieldDescription"></param>
        /// <exception cref="ArgumentException">if fieldName is null.</exception>
        public FieldInfo(string fieldDescription)
		{
			_fieldDescription = fieldDescription ?? "";
		}

		/// <summary>
		/// Returns so called short description:
		/// the text from the
		/// DESCRIPTION "This is returned by this property"
		/// line.
		/// </summary>
		public string ShortFieldDescription
		{
			get
			{
				if (FieldDescription == null)
					return "";

				string[] lines = FieldDescription.Split(new char[] {'\n'});
				foreach (string line in lines)
				{
					string lineModified = line.Trim();
					if (lineModified.StartsWith(_signatureDescription))
						return lineModified.Split(new char[] {'\"'})[1];
				}

				return "";
			}
		}
    }

	/// <summary>
	/// Encapsulates the table information
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
            Fields = fields ?? throw new ArgumentNullException("Non-null fields are required for the DatabaseInfo");
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

	/// <summary>
	/// Encapsulates the database information
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
            Tables = tables ?? throw new ArgumentNullException("Non-null dictionary is required for the DatabaseInfo");
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

	/// <summary>
	/// Parser of the .df file (INFO database definition).
	/// </summary>
	public class DFFileParser
	{
		private static readonly string _signatureAddTable = "ADD TABLE \"";
		private static readonly int _signatureAddTableLen = _signatureAddTable.Length;

		private static readonly string _signatureAddField = "ADD FIELD \"";
		private static readonly int _signatureAddFieldLen = _signatureAddField.Length;

		private static readonly string _signatureAddIndex = "ADD INDEX \"";
		private static readonly int _signatureAddIndexLen = _signatureAddIndex.Length;

		private DatabaseInfo _dbInfo;

		/// <summary>
		/// Returns the DatabaseInfo filled in with parsed values.
		/// </summary>
		public DatabaseInfo DatabaseInfo
		{
			get
			{
				return _dbInfo;
			}
		}

		/// <summary>
		/// Constructor. Parses the .df file.
		/// </summary>
		/// <param name="fileName">.df file name.</param>
		/// <exception cref="ArgumentException">if fileName is null.</exception>
		/// <exception cref="FileNotFoundException">if the .df file does not exist etc.</exception>
		/// <exception cref="IOException">other IO exception</exception>
		public DFFileParser(string fileName)
		{
			if (fileName == null)
				throw new ArgumentBullException("Non-null .df file name is required for DFFileParser");

			if (! File.Exists(fileName))
				throw new IOException(string.Format("The {0} file does not exist"));

			try
			{
				// Reading from file up to first non-empty line
				//
				using (StreamReader sr = new StreamReader(fileName)) 
				{
					String line;

					IDictionary tables = new SortedList();

					// Read and process data
					//
					line = sr.ReadLine();
					while (line != null) 
					{
						if (line.StartsWith(_signatureAddTable))
						{
							// Table name
							string tableName = line.Substring(_signatureAddTableLen).Trim(new char[] {'\"'});

							line = sr.ReadLine();

							string tableDescription = "";

							while ((line != null) && (! line.StartsWith(_signatureAddField)) && (! line.StartsWith(_signatureAddTable)))
							{
								if (line.Trim().Length > 0)
									tableDescription += line + "\n";

								line = sr.ReadLine();
							}

							IDictionary fields = ParseFields(sr, line, ref line, ref tableDescription);

							TableInfo tableInfo = new TableInfo(tableDescription, fields);

							tables.Add(tableName, tableInfo);
						}
						else
							line = sr.ReadLine();
					}

					_dbInfo = new DatabaseInfo(tables);
				}
			}
			catch (FileNotFoundException)
			{
				throw;
			}
			catch (IOException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new Exception("Unknown exception", e);
			}
		}

		/// <summary>
		/// Parses fields for a current table.
		/// Since this method id private and is free from any dummy calls,
		/// its input parameters are not checked for correctness.
		/// </summary>
		/// <param name="sr"></param>
		/// <param name="addFieldLine"></param>
		/// <param name="line"></param>
		/// <param name="tableDescription"></param>
		/// <returns></returns>
		private IDictionary ParseFields(StreamReader sr, string addFieldLine,
			ref string line, ref string tableDescription)
		{
			string fieldDescription;
			line = addFieldLine;

			IDictionary fields = new SortedList();

			do
			{
				string fieldName = "";

				fieldDescription = "";

				if (line.StartsWith(_signatureAddField))
				{
					fieldName = line.Substring(_signatureAddFieldLen).Split(new char[] {'\"'})[0];

					fieldDescription += line + "\n";

					line = sr.ReadLine();
				}

				while ((line != null) && (! line.StartsWith(_signatureAddField)) 
					&& (! line.StartsWith(_signatureAddTable))
					&& (! line.StartsWith(_signatureAddIndex)))
				{
					fieldDescription += line + "\n";

					line = sr.ReadLine();
				}

				// Indexes go to a table description
				//
				if (line.StartsWith(_signatureAddIndex))
				{
					tableDescription += "\n";

					while ((line != null) && (! line.StartsWith(_signatureAddField)) 
						&& (! line.StartsWith(_signatureAddTable)))
					{
						tableDescription += line + "\n";

						line = sr.ReadLine();
					}
				}

				fields.Add(fieldName, new FieldInfo(fieldDescription));
			}
			while ((line != null) && (! line.StartsWith(_signatureAddTable)));

			return fields;
		}
	}
}
