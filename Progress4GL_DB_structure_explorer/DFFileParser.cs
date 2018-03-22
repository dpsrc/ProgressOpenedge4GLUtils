using System;
using System.Collections.Generic;
using System.IO;

namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
	/// <summary>
	/// Parser of the .df file (Progress 4GL database definition).
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
		/// Returns the DbInfo filled in with parsed values.
		/// </summary>
		public DatabaseInfo DbInfo { get => _dbInfo; }

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
				throw new ArgumentNullException("Non-null .df file name is required for DFFileParser");

			if (! File.Exists(fileName))
				throw new IOException(string.Format("The {0} file does not exist", fileName));

			try
			{
				// Reading from file up to first non-empty line
				//
				using (StreamReader sr = new StreamReader(fileName)) 
				{
					string line;

					IDictionary<string, TableInfo> tables = new SortedList<string, TableInfo>();

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

							IDictionary<string, FieldInfo> fields = ParseFields(sr, line, ref line, ref tableDescription);

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
		/// <returns>The fields collection.</returns>
		private IDictionary<string, FieldInfo> ParseFields(StreamReader sr, string addFieldLine,
			ref string line, ref string tableDescription)
		{
			string fieldDescription;
			line = addFieldLine;

			IDictionary<string, FieldInfo> fields = new SortedList<string, FieldInfo>();

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
