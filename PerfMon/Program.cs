using System;
using System.Windows.Forms;

namespace PerfMon
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			bool result;
			var mutex = new System.Threading.Mutex(true, "PerfMon_AppId", out result);

			if (!result)
			{
				MessageBox.Show("Another instance is already running.", "PerfMon");
				return;
			}
			else
			{
				var form = new MainForm();
				Application.Run();
				GC.KeepAlive(mutex);
			}
		}
	}
}
