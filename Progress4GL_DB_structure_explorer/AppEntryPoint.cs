using System;
using System.Windows.Forms;

namespace DP.ProgressOpenedge4GL.Utils.Progress_DB_structure_explorer
{
	/// <summary>
	/// Summary description for AppEntryPoint.
	/// </summary>
	public class AppEntryPoint
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try
			{
				Application.Run(new FormUI());
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Error in " + Application.ProductName);
			}
		}
	}
}
