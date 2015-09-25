namespace DroidExplorer.Core.UI {
	partial class GenericDeviceSelectionForm {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenericDeviceSelectionForm));
			this.helpVideoLink = new System.Windows.Forms.LinkLabel();
			this.devicesList = new System.Windows.Forms.ListView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tcpipConnect = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.adbRestart = new System.Windows.Forms.ToolStripButton();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// helpVideoLink
			// 
			this.helpVideoLink.ActiveLinkColor = System.Drawing.Color.Blue;
			this.helpVideoLink.AutoSize = true;
			this.helpVideoLink.BackColor = System.Drawing.SystemColors.Control;
			this.helpVideoLink.Cursor = System.Windows.Forms.Cursors.Hand;
			this.helpVideoLink.Location = new System.Drawing.Point(540, 6);
			this.helpVideoLink.Name = "helpVideoLink";
			this.helpVideoLink.Size = new System.Drawing.Size(110, 13);
			this.helpVideoLink.TabIndex = 5;
			this.helpVideoLink.TabStop = true;
			this.helpVideoLink.Text = "I don\'t see my device!";
			this.helpVideoLink.VisitedLinkColor = System.Drawing.Color.Blue;
			this.helpVideoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpVideoLink_LinkClicked);
			// 
			// devicesList
			// 
			this.devicesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.devicesList.LargeImageList = this.imageList;
			this.devicesList.Location = new System.Drawing.Point(0, 25);
			this.devicesList.Name = "devicesList";
			this.devicesList.Size = new System.Drawing.Size(655, 273);
			this.devicesList.TabIndex = 6;
			this.devicesList.UseCompatibleStateImageBehavior = false;
			this.devicesList.DoubleClick += new System.EventHandler(this.devicesList_DoubleClick);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(128, 128);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripButton,
            this.toolStripSeparator1,
            this.tcpipConnect,
            this.toolStripSeparator2,
            this.adbRestart});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
			this.toolStrip.Size = new System.Drawing.Size(655, 25);
			this.toolStrip.TabIndex = 7;
			this.toolStrip.Text = "toolStrip1";
			// 
			// refreshToolStripButton
			// 
			this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.refreshToolStripButton.Image = global::DroidExplorer.Core.UI.Properties.Resources.generic;
			this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshToolStripButton.Name = "refreshToolStripButton";
			this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.refreshToolStripButton.Text = "Refresh";
			this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tcpipConnect
			// 
			this.tcpipConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tcpipConnect.Image = global::DroidExplorer.Core.UI.Properties.Resources.generic;
			this.tcpipConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tcpipConnect.Name = "tcpipConnect";
			this.tcpipConnect.Size = new System.Drawing.Size(23, 22);
			this.tcpipConnect.Text = "TCP/IP Connect";
			this.tcpipConnect.Click += new System.EventHandler(this.tcpipConnect_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// adbRestart
			// 
			this.adbRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.adbRestart.Image = ((System.Drawing.Image)(resources.GetObject("adbRestart.Image")));
			this.adbRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.adbRestart.Name = "adbRestart";
			this.adbRestart.Size = new System.Drawing.Size(23, 22);
			this.adbRestart.Text = "Restart ADB";
			this.adbRestart.Click += new System.EventHandler(this.adbRestart_Click);
			// 
			// GenericDeviceSelectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(655, 298);
			this.Controls.Add(this.helpVideoLink);
			this.Controls.Add(this.devicesList);
			this.Controls.Add(this.toolStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "GenericDeviceSelectionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Device";
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel helpVideoLink;
		private System.Windows.Forms.ListView devicesList;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton refreshToolStripButton;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolStripButton tcpipConnect;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton adbRestart;
	}
}