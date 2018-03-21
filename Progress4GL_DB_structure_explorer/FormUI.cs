using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
	/// <summary>
	/// Main form.
	/// </summary>
	public class FormUI : Form
	{
		private Splitter splitterLeftRight;
		private Panel panelRight;
		private TextBox textBoxFieldDescription;
		private Panel panelLeft;
		private TextBox textBoxTableDescription;
		private Splitter splitterLefts;
		private Splitter splitterRights;
		private MenuItem menuItemFile;
		private MenuItem Open;
		private OpenFileDialog openFileDialogDFFile;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;
		private MenuItem menuItemAbout;

		private DatabaseInfo _dbInfo = null;
		private MainMenu mainMenu;

		private System.Windows.Forms.Panel panelLeftTop;
		private System.Windows.Forms.Label labelTables;
		private System.Windows.Forms.ListBox listBoxTables;
		private System.Windows.Forms.Panel panelRightTop;
		private System.Windows.Forms.Label labelFields;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.Panel panelGeneral;
		private System.Windows.Forms.Splitter splitterLeftTop;
		private System.Windows.Forms.Splitter splitterRightTop;
		private System.Windows.Forms.DataGrid dataGridFields;
		private System.Data.DataSet dataSet;
		private System.Data.DataTable dataTableFields;
		private System.Data.DataColumn dataColumnFieldName;
		private System.Data.DataColumn dataColumnShortDescription;
		private System.Windows.Forms.Splitter splitterStatusBar;

		public FormUI()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			panelGeneral.Dock = DockStyle.Fill;

			try
			{
				if ((openFileDialogDFFile.FileName != null) &&
					(openFileDialogDFFile.FileName.Length > 0))
				{
					ProcessDFFileName(openFileDialogDFFile.FileName);
				}
			}
			catch (IOException ioex)
			{
				MessageBox.Show(ioex.Message, "IO error");

				ClearVisually();
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				Exception innerEx = ex;

				while (innerEx.InnerException != null)
				{
					message += "\n\n" + innerEx.Message;
					innerEx = innerEx.InnerException;
				}

				MessageBox.Show(message, "Unexpected error");

				ClearVisually();
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitterLeftRight = new System.Windows.Forms.Splitter();
			this.panelRight = new System.Windows.Forms.Panel();
			this.panelRightTop = new System.Windows.Forms.Panel();
			this.dataGridFields = new System.Windows.Forms.DataGrid();
			this.dataTableFields = new System.Data.DataTable();
			this.dataColumnFieldName = new System.Data.DataColumn();
			this.dataColumnShortDescription = new System.Data.DataColumn();
			this.splitterRightTop = new System.Windows.Forms.Splitter();
			this.labelFields = new System.Windows.Forms.Label();
			this.splitterRights = new System.Windows.Forms.Splitter();
			this.textBoxFieldDescription = new System.Windows.Forms.TextBox();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.panelLeftTop = new System.Windows.Forms.Panel();
			this.listBoxTables = new System.Windows.Forms.ListBox();
			this.splitterLeftTop = new System.Windows.Forms.Splitter();
			this.labelTables = new System.Windows.Forms.Label();
			this.splitterLefts = new System.Windows.Forms.Splitter();
			this.textBoxTableDescription = new System.Windows.Forms.TextBox();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItemFile = new System.Windows.Forms.MenuItem();
			this.Open = new System.Windows.Forms.MenuItem();
			this.menuItemAbout = new System.Windows.Forms.MenuItem();
			this.openFileDialogDFFile = new System.Windows.Forms.OpenFileDialog();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.splitterStatusBar = new System.Windows.Forms.Splitter();
			this.panelGeneral = new System.Windows.Forms.Panel();
			this.dataSet = new System.Data.DataSet();
			this.panelRight.SuspendLayout();
			this.panelRightTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridFields)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTableFields)).BeginInit();
			this.panelLeft.SuspendLayout();
			this.panelLeftTop.SuspendLayout();
			this.panelGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
			this.SuspendLayout();
			// 
			// splitterLeftRight
			// 
			this.splitterLeftRight.Location = new System.Drawing.Point(144, 0);
			this.splitterLeftRight.Name = "splitterLeftRight";
			this.splitterLeftRight.Size = new System.Drawing.Size(3, 388);
			this.splitterLeftRight.TabIndex = 7;
			this.splitterLeftRight.TabStop = false;
			// 
			// panelRight
			// 
			this.panelRight.Controls.Add(this.panelRightTop);
			this.panelRight.Controls.Add(this.splitterRights);
			this.panelRight.Controls.Add(this.textBoxFieldDescription);
			this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelRight.Location = new System.Drawing.Point(144, 0);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = new System.Drawing.Size(388, 388);
			this.panelRight.TabIndex = 10;
			// 
			// panelRightTop
			// 
			this.panelRightTop.Controls.Add(this.dataGridFields);
			this.panelRightTop.Controls.Add(this.splitterRightTop);
			this.panelRightTop.Controls.Add(this.labelFields);
			this.panelRightTop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelRightTop.Location = new System.Drawing.Point(0, 0);
			this.panelRightTop.Name = "panelRightTop";
			this.panelRightTop.Size = new System.Drawing.Size(388, 265);
			this.panelRightTop.TabIndex = 3;
			// 
			// dataGridFields
			// 
			this.dataGridFields.AllowDrop = true;
			this.dataGridFields.CaptionVisible = false;
			this.dataGridFields.DataMember = "";
			this.dataGridFields.DataSource = this.dataTableFields;
			this.dataGridFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridFields.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridFields.Location = new System.Drawing.Point(0, 17);
			this.dataGridFields.Name = "dataGridFields";
			this.dataGridFields.ParentRowsVisible = false;
			this.dataGridFields.PreferredColumnWidth = 100;
			this.dataGridFields.ReadOnly = true;
			this.dataGridFields.RowHeadersVisible = false;
			this.dataGridFields.RowHeaderWidth = 50;
			this.dataGridFields.Size = new System.Drawing.Size(388, 248);
			this.dataGridFields.TabIndex = 3;
			this.dataGridFields.SizeChanged += new System.EventHandler(this.dataGridFields_SizeChanged);
			this.dataGridFields.CurrentCellChanged += new System.EventHandler(this.listBoxFields_SelectedIndexChanged);
			// 
			// dataTableFields
			// 
			this.dataTableFields.Columns.AddRange(new System.Data.DataColumn[] {
																				   this.dataColumnFieldName,
																				   this.dataColumnShortDescription});
			this.dataTableFields.TableName = "TableFields";
			// 
			// dataColumnFieldName
			// 
			this.dataColumnFieldName.Caption = "Name";
			this.dataColumnFieldName.ColumnName = "ColumnFieldName";
			this.dataColumnFieldName.DefaultValue = "";
			this.dataColumnFieldName.ReadOnly = true;
			// 
			// dataColumnShortDescription
			// 
			this.dataColumnShortDescription.Caption = "Short description";
			this.dataColumnShortDescription.ColumnName = "ColumnFieldShortDescription";
			this.dataColumnShortDescription.DefaultValue = "";
			this.dataColumnShortDescription.ReadOnly = true;
			// 
			// splitterRightTop
			// 
			this.splitterRightTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitterRightTop.Location = new System.Drawing.Point(0, 16);
			this.splitterRightTop.Name = "splitterRightTop";
			this.splitterRightTop.Size = new System.Drawing.Size(388, 1);
			this.splitterRightTop.TabIndex = 2;
			this.splitterRightTop.TabStop = false;
			// 
			// labelFields
			// 
			this.labelFields.AutoSize = true;
			this.labelFields.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelFields.Location = new System.Drawing.Point(0, 0);
			this.labelFields.Name = "labelFields";
			this.labelFields.Size = new System.Drawing.Size(35, 16);
			this.labelFields.TabIndex = 1;
			this.labelFields.Text = "Fields";
			// 
			// splitterRights
			// 
			this.splitterRights.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterRights.Location = new System.Drawing.Point(0, 265);
			this.splitterRights.Name = "splitterRights";
			this.splitterRights.Size = new System.Drawing.Size(388, 3);
			this.splitterRights.TabIndex = 2;
			this.splitterRights.TabStop = false;
			// 
			// textBoxFieldDescription
			// 
			this.textBoxFieldDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBoxFieldDescription.Location = new System.Drawing.Point(0, 268);
			this.textBoxFieldDescription.Multiline = true;
			this.textBoxFieldDescription.Name = "textBoxFieldDescription";
			this.textBoxFieldDescription.ReadOnly = true;
			this.textBoxFieldDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxFieldDescription.Size = new System.Drawing.Size(388, 120);
			this.textBoxFieldDescription.TabIndex = 1;
			this.textBoxFieldDescription.Text = "";
			// 
			// panelLeft
			// 
			this.panelLeft.Controls.Add(this.panelLeftTop);
			this.panelLeft.Controls.Add(this.splitterLefts);
			this.panelLeft.Controls.Add(this.textBoxTableDescription);
			this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelLeft.Location = new System.Drawing.Point(0, 0);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(144, 388);
			this.panelLeft.TabIndex = 6;
			// 
			// panelLeftTop
			// 
			this.panelLeftTop.Controls.Add(this.listBoxTables);
			this.panelLeftTop.Controls.Add(this.splitterLeftTop);
			this.panelLeftTop.Controls.Add(this.labelTables);
			this.panelLeftTop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelLeftTop.Location = new System.Drawing.Point(0, 0);
			this.panelLeftTop.Name = "panelLeftTop";
			this.panelLeftTop.Size = new System.Drawing.Size(144, 239);
			this.panelLeftTop.TabIndex = 3;
			// 
			// listBoxTables
			// 
			this.listBoxTables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxTables.Location = new System.Drawing.Point(0, 17);
			this.listBoxTables.Name = "listBoxTables";
			this.listBoxTables.Size = new System.Drawing.Size(144, 212);
			this.listBoxTables.TabIndex = 1;
			this.listBoxTables.SelectedIndexChanged += new System.EventHandler(this.listBoxTables_SelectedIndexChanged);
			// 
			// splitterLeftTop
			// 
			this.splitterLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitterLeftTop.Location = new System.Drawing.Point(0, 16);
			this.splitterLeftTop.Name = "splitterLeftTop";
			this.splitterLeftTop.Size = new System.Drawing.Size(144, 1);
			this.splitterLeftTop.TabIndex = 0;
			this.splitterLeftTop.TabStop = false;
			// 
			// labelTables
			// 
			this.labelTables.AutoSize = true;
			this.labelTables.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelTables.Location = new System.Drawing.Point(0, 0);
			this.labelTables.Name = "labelTables";
			this.labelTables.Size = new System.Drawing.Size(38, 16);
			this.labelTables.TabIndex = 0;
			this.labelTables.Text = "Tables";
			// 
			// splitterLefts
			// 
			this.splitterLefts.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterLefts.Location = new System.Drawing.Point(0, 239);
			this.splitterLefts.Name = "splitterLefts";
			this.splitterLefts.Size = new System.Drawing.Size(144, 3);
			this.splitterLefts.TabIndex = 2;
			this.splitterLefts.TabStop = false;
			// 
			// textBoxTableDescription
			// 
			this.textBoxTableDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBoxTableDescription.Location = new System.Drawing.Point(0, 242);
			this.textBoxTableDescription.Multiline = true;
			this.textBoxTableDescription.Name = "textBoxTableDescription";
			this.textBoxTableDescription.ReadOnly = true;
			this.textBoxTableDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxTableDescription.Size = new System.Drawing.Size(144, 146);
			this.textBoxTableDescription.TabIndex = 1;
			this.textBoxTableDescription.Text = "";
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItemFile,
																					 this.menuItemAbout});
			// 
			// menuItemFile
			// 
			this.menuItemFile.Index = 0;
			this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.Open});
			this.menuItemFile.Text = "File";
			// 
			// Open
			// 
			this.Open.Index = 0;
			this.Open.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.Open.Text = "Open";
			this.Open.Click += new System.EventHandler(this.Open_Click);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 1;
			this.menuItemAbout.Text = "About";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// openFileDialogDFFile
			// 
			this.openFileDialogDFFile.DefaultExt = "*.df";
			this.openFileDialogDFFile.Filter = "Progress Openedge 4GL database definition files|*.df|All files|*.*";
			this.openFileDialogDFFile.Title = "Open a Progress Openedge 4GL database definition file";
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 537);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(768, 16);
			this.statusBar.TabIndex = 0;
			// 
			// splitterStatusBar
			// 
			this.splitterStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterStatusBar.Location = new System.Drawing.Point(0, 536);
			this.splitterStatusBar.Name = "splitterStatusBar";
			this.splitterStatusBar.Size = new System.Drawing.Size(768, 1);
			this.splitterStatusBar.TabIndex = 0;
			this.splitterStatusBar.TabStop = false;
			// 
			// panelGeneral
			// 
			this.panelGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelGeneral.Controls.Add(this.splitterLeftRight);
			this.panelGeneral.Controls.Add(this.panelRight);
			this.panelGeneral.Controls.Add(this.panelLeft);
			this.panelGeneral.Location = new System.Drawing.Point(24, 16);
			this.panelGeneral.Name = "panelGeneral";
			this.panelGeneral.Size = new System.Drawing.Size(536, 392);
			this.panelGeneral.TabIndex = 2;
			// 
			// dataSet
			// 
			this.dataSet.DataSetName = "dataSet";
			this.dataSet.Locale = new System.Globalization.CultureInfo("en-US");
			this.dataSet.Tables.AddRange(new System.Data.DataTable[] {
																		 this.dataTableFields});
			// 
			// FormUI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(768, 553);
			this.Controls.Add(this.panelGeneral);
			this.Controls.Add(this.splitterStatusBar);
			this.Controls.Add(this.statusBar);
			this.Menu = this.mainMenu;
			this.Name = "FormUI";
			this.Text = "Progress Openedge 4GL database structure explorer";
			this.panelRight.ResumeLayout(false);
			this.panelRightTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridFields)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTableFields)).EndInit();
			this.panelLeft.ResumeLayout(false);
			this.panelLeftTop.ResumeLayout(false);
			this.panelGeneral.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
		{
			string tableNameSelected = listBoxTables.SelectedItem as string;

			//// listBoxFields.Items.Clear();
			dataTableFields.Rows.Clear();

			TableInfo tableInfo = _dbInfo.Tables[tableNameSelected] as TableInfo;
			textBoxTableDescription.Lines = tableInfo.TableDescription.Split(new char[] {'\n'});
			IDictionary fields = tableInfo.Fields;
			foreach (string fieldName in fields.Keys)
			{
				//// listBoxFields.Items.Add(fieldName);
				DataRow dataRow = dataTableFields.NewRow();
				dataRow["ColumnFieldName"] = fieldName;
				dataRow["ColumnFieldShortDescription"] = 
					(fields[fieldName] as FieldInfo).ShortFieldDescription;
				dataTableFields.Rows.Add(dataRow);
			}

			textBoxFieldDescription.Clear();
		}

		private void Open_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult;

			bool wasException = false;

			do
			{
				wasException = false;

				dialogResult = openFileDialogDFFile.ShowDialog();

				if (dialogResult == DialogResult.Cancel)
					return;

				if (dialogResult == DialogResult.OK)
				{
					if (! File.Exists(openFileDialogDFFile.FileName))
						MessageBox.Show(string.Format("The {0} file does not exist"));

					try
					{
						ProcessDFFileName(openFileDialogDFFile.FileName);
					}
					catch (IOException ioex)
					{
						wasException = true;
					
						MessageBox.Show(ioex.Message, "IO error");

						ClearVisually();
					}
					catch (Exception ex)
					{
						wasException = true;

						string message = ex.Message;
						Exception innerEx = ex;

						while (innerEx.InnerException != null)
						{
							message += "\n\n" + innerEx.Message;
							innerEx = innerEx.InnerException;
						}

						MessageBox.Show(message, "Unexpected error");

						ClearVisually();
					}
				}
			}
			while (! 
					((dialogResult == DialogResult.OK) && 
					File.Exists(openFileDialogDFFile.FileName) &&
					(! wasException))
				);
		}

		/// <summary>
		/// Processed parsed .df file.
		/// Since this method id private and is free from any dummy calls,
		/// its input parameter is not checked for correctness.
		/// </summary>
		/// <param name="fileName"></param>
		private void ProcessDFFileName(string fileName)
		{
			try
			{
				_dbInfo = (new DFFileParser(fileName)).DatabaseInfo;

				UpdateFromDatabaseDefinition();

				statusBar.Text = fileName;
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Clears table and field list boxes, description texts and
		/// status bar.
		/// </summary>
		private void ClearVisually()
		{
			listBoxTables.Items.Clear();
			textBoxTableDescription.Text = "";

			//// listBoxFields.Items.Clear();
			dataTableFields.Rows.Clear();
			dataGridFields.CurrentRowIndex = 0;

			textBoxFieldDescription.Text = "";

			statusBar.Text = "";
		}

		/// <summary>
		/// Clears table and field list boxes, description texts with
		/// information from parsed .df file.
		/// Puts .df file name into the status bar.
		/// </summary>
		private void UpdateFromDatabaseDefinition()
		{
			if (_dbInfo == null)
				return;

			try
			{
				ClearVisually();

				foreach (string tableName in _dbInfo.Tables.Keys)
				{
					listBoxTables.Items.Add(tableName);
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Unknown error during updating from parsed .df file");
				ClearVisually();
				throw;
			}
		}

		private void listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxTables.SelectedItem == null)
			{
				textBoxFieldDescription.Text = "";
				return;
			}

			if (dataGridFields.CurrentRowIndex == -1)
			{
				textBoxFieldDescription.Text = "";
				return;
			}

			try
			{
				string tableNameSelected = listBoxTables.SelectedItem as string;
				TableInfo tableInfo = _dbInfo.Tables[tableNameSelected] as TableInfo;

				////string FieldNameSelected = listBoxFields.SelectedItem as string;
				try
				{
					string FieldNameSelected = 
						dataTableFields.Rows[dataGridFields.CurrentRowIndex]["ColumnFieldName"] as string;

					FieldInfo fieldInfo = tableInfo.Fields[FieldNameSelected] as FieldInfo;
					textBoxFieldDescription.Lines = fieldInfo.FieldDescription.Split(new char[] {'\n'});
				}
				catch (IndexOutOfRangeException)
				{
					// Just ignore
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,
					"Unexpected error when processing selection changes event");
			}
		}

		/// <summary>
		/// Shows window with the About information.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemAbout_Click(object sender, EventArgs e)
		{
			string about = "The Progress Openedge 4GL database structure (.df files) explorer\n\n" +
				"Author:\nDmytro P";

			MessageBox.Show(about, "About", MessageBoxButtons.OK);
		}

		private void dataGridFields_SizeChanged(object sender, System.EventArgs e)
		{
			// To prevent evil exception, I check if width is non-0
			//
			if (dataGridFields.Width != 0)
				dataGridFields.PreferredColumnWidth = dataGridFields.Width / 2 - 2;
		}
	}
}
