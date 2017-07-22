using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PerfMon
{
	public class AppContext : ApplicationContext
	{
		// https://msdn.microsoft.com/en-us/library/system.drawing.icon.fromhandle(v=vs.110).aspx
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		extern static bool DestroyIcon(IntPtr handle);

		public PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		public PerformanceCounter ram = new PerformanceCounter("Memory", "% Committed Bytes In Use", null);
		/// <summary>
		/// https://support.microsoft.com/en-us/help/310067/-disk-time-may-exceed-100-percent-in-the-performance-monitor-mmc
		/// </summary>
		public PerformanceCounter disc = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");

		private NotifyIcon TrayIcon { get; set; }
		private ContextMenuStrip TrayIconContextMenu { get; set; }
		private Timer _timer = new Timer();

		public AppContext()
		{
			InitializeComponent();

			_timer.Tick += Timer_Tick;
			_timer.Interval = 750;
			_timer.Enabled = true;
		}

		private void InitializeComponent()
		{
			TrayIcon = new NotifyIcon();
			TrayIcon.Visible = true;

			TrayIconContextMenu = new ContextMenuStrip();
			var _closeMenuItem = new ToolStripMenuItem();
			var _opemTMMenuItem = new ToolStripMenuItem();
			TrayIconContextMenu.SuspendLayout();

			this.TrayIconContextMenu.Items.AddRange(new ToolStripItem[] { _opemTMMenuItem, _closeMenuItem });
			this.TrayIconContextMenu.Name = "cm";
			this.TrayIconContextMenu.Size = new Size(153, 70);

			_closeMenuItem.Name = "miClose";
			_closeMenuItem.Size = new Size(152, 22);
			_closeMenuItem.Text = "Exit";
			_closeMenuItem.Click += new EventHandler(this.CloseMenuItem_Click);

			_opemTMMenuItem.Name = "miOpenTM";
			_opemTMMenuItem.Size = new Size(152, 22);
			_opemTMMenuItem.Text = "Open taskmgr";
			_opemTMMenuItem.Click += new EventHandler((o, ea) => { Process.Start("taskmgr"); });

			TrayIconContextMenu.ResumeLayout(false);
			TrayIcon.ContextMenuStrip = TrayIconContextMenu;
		}

		#region Main Methods
		public void ShowVals()
		{
			int cpuVal = Convert.ToInt32(cpu.NextValue());
			int ramVal = Convert.ToInt32(ram.NextValue());
			int discVal = Convert.ToInt32(disc.NextValue()).Clamp(0, 100);


			TrayIcon.Text = $"CPU: {cpuVal}% {Environment.NewLine}RAM: {ramVal}% {Environment.NewLine}DISC: {discVal}%";
			DrawIco(cpuVal, ramVal, discVal);
			//DrawIco(70, 60, 50);
		}

		public void DrawIco(int cpu, int ram, int disc)
		{
			int width = 100;
			int height = 100;
			int border = 5;
			int barWidth = (width - border * 2) / 3;

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
					using (var icon = Icon.FromHandle(handle))
					{
						TrayIcon.Icon = icon;
					}
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

		private void Close()
		{
			_timer.Stop();
			TrayIcon.Visible = false;
			TrayIcon.Dispose();

			Application.Exit();
		}

		#endregion

		#region Event Handlers

		public void Timer_Tick(System.Object sender, EventArgs e)
		{
			ShowVals();
		}

		private void CloseMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion
	}
}
