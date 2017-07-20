using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PerfMon
{
	public partial class MainForm : Form
	{
		// https://msdn.microsoft.com/en-us/library/system.drawing.icon.fromhandle(v=vs.110).aspx
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		extern static bool DestroyIcon(IntPtr handle);

		public PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		public PerformanceCounter ram = new PerformanceCounter("Memory", "% Committed Bytes In Use", null);
		public PerformanceCounter disc = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");

		private Timer timer = new Timer();

		public MainForm()
		{
			InitializeComponent();

			timer.Tick += Timer_Tick;
			timer.Interval = 500;
			timer.Enabled = true;
		}

		#region Main Methods
		public void ShowVals()
		{
			int discVal = Convert.ToInt32(disc.NextValue());
			int cpuVal = Convert.ToInt32(cpu.NextValue());
			int ramVal = Convert.ToInt32(ram.NextValue());

			ni.Text = $"CPU: {cpuVal}% {Environment.NewLine}RAM: {ramVal}% {Environment.NewLine}DISC: {discVal}%";
			DrawIco(cpuVal, ramVal, discVal);
			//DrawIco(70, 60, 50);
		}

		public void DrawIco(int cpu, int ram, int disc)
		{
			int width = 100;
			int height = 100;
			int border = 5;
			int barWidth = (width - border) / 3;

			using (Bitmap bmp = new Bitmap(width, height))
			{
				using (Graphics graphic = Graphics.FromImage(bmp))
				{
					graphic.FillRectangle(Brushes.DimGray, 0, 0, width, height);

					graphic.FillRectangle(Brushes.OrangeRed, border, (height - cpu), barWidth, cpu);
					graphic.FillRectangle(Brushes.DodgerBlue, barWidth + border, (height - ram), barWidth, ram);
					graphic.FillRectangle(Brushes.LimeGreen, (barWidth * 2) + border, (height - disc), barWidth, disc);

					DrawIconBorder(graphic, width, height, border);

					var handle = bmp.GetHicon();
					ni.Icon = Icon.FromHandle(handle);
					DestroyIcon(handle);
				}
			}
		}

		private void DrawIconBorder(Graphics graphic, int width, int height, int borderThickness = 4)
		{
			graphic.FillRectangle(Brushes.Gray, 0, 0, width, borderThickness);
			graphic.FillRectangle(Brushes.White, width - borderThickness, 0, borderThickness, height);
			graphic.FillRectangle(Brushes.White, 0, height - borderThickness, width, borderThickness);
			graphic.FillRectangle(Brushes.Gray, 0, 0, borderThickness, height);
		}
		#endregion

		#region Event Handlers
		public void Timer_Tick(System.Object sender, EventArgs e)
		{
			ShowVals();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			timer.Stop();
			ni.Dispose();

			Application.Exit();
		}

		private void openTaskmgrToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("taskmgr");
		}
		#endregion
	}
}
