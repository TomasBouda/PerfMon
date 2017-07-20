namespace PerfMon
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.ni = new System.Windows.Forms.NotifyIcon(this.components);
			this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openTaskmgrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cms.SuspendLayout();
			this.SuspendLayout();
			// 
			// ni
			// 
			this.ni.ContextMenuStrip = this.cms;
			this.ni.Visible = true;
			// 
			// cms
			// 
			this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTaskmgrToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.cms.Name = "cms";
			this.cms.Size = new System.Drawing.Size(150, 48);
			// 
			// openTaskmgrToolStripMenuItem
			// 
			this.openTaskmgrToolStripMenuItem.Name = "openTaskmgrToolStripMenuItem";
			this.openTaskmgrToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.openTaskmgrToolStripMenuItem.Text = "Open taskmgr";
			this.openTaskmgrToolStripMenuItem.Click += new System.EventHandler(this.openTaskmgrToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(120, 51);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Form1";
			this.cms.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.NotifyIcon ni;
		private System.Windows.Forms.ContextMenuStrip cms;
		private System.Windows.Forms.ToolStripMenuItem openTaskmgrToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
	}
}

