using System.Windows.Forms;
namespace DroidExplorer.Plugins.UI {
	/// <summary>
	/// 
	/// </summary>
	partial class ScreenShotForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing ) {
			if ( disposing && ( components != null ) ) {
				components.Dispose ( );
			}
			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ( ) {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenShotForm));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.PLModeToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.screen = new System.Windows.Forms.PictureBox();
			this.openInDefault = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripButton,
            this.copyToolStripButton,
            this.openInDefault,
            this.toolStripSeparator1,
            this.refreshToolStripButton,
            this.PLModeToolStripButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(3, 0, 1, 0);
			this.toolStrip1.Size = new System.Drawing.Size(263, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Enabled = false;
			this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "&Save";
			this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
			// 
			// copyToolStripButton
			// 
			this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToolStripButton.Enabled = false;
			this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
			this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripButton.Name = "copyToolStripButton";
			this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.copyToolStripButton.Text = "&Copy";
			this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// refreshToolStripButton
			// 
			this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.refreshToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripButton.Image")));
			this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshToolStripButton.Name = "refreshToolStripButton";
			this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.refreshToolStripButton.Text = "&Refresh";
			this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
			// 
			// PLModeToolStripButton
			// 
			this.PLModeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PLModeToolStripButton.Enabled = false;
			this.PLModeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PLModeToolStripButton.Image")));
			this.PLModeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PLModeToolStripButton.Name = "PLModeToolStripButton";
			this.PLModeToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.PLModeToolStripButton.Text = "Portrait / Landscape";
			this.PLModeToolStripButton.CheckedChanged += new System.EventHandler(this.PLModeToolStripButton_CheckedChanged);
			this.PLModeToolStripButton.Click += new System.EventHandler(this.PLModeToolStripButton_Click);
			// 
			// screen
			// 
			this.screen.Dock = System.Windows.Forms.DockStyle.Fill;
			this.screen.Location = new System.Drawing.Point(0, 25);
			this.screen.Name = "screen";
			this.screen.Padding = new System.Windows.Forms.Padding(5);
			this.screen.Size = new System.Drawing.Size(263, 99);
			this.screen.TabIndex = 1;
			this.screen.TabStop = false;
			this.screen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// openInDefault
			// 
			this.openInDefault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openInDefault.Enabled = false;
			this.openInDefault.Image = ((System.Drawing.Image)(resources.GetObject("openInDefault.Image")));
			this.openInDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openInDefault.Name = "openInDefault";
			this.openInDefault.Size = new System.Drawing.Size(23, 22);
			this.openInDefault.Text = "Edit in default application";
			this.openInDefault.Click += new System.EventHandler(this.openInDefault_Click);
			// 
			// ScreenShotForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(263, 124);
			this.Controls.Add(this.screen);
			this.Controls.Add(this.toolStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ScreenShotForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Screenshot";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private System.Windows.Forms.ToolStripButton copyToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton refreshToolStripButton;
		private System.Windows.Forms.ToolStripButton PLModeToolStripButton;
		private System.Windows.Forms.PictureBox screen;
		private ToolStripButton openInDefault;
	}
}

