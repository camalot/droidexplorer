namespace DroidExplorer.Core.UI {
	partial class TcpIpDialog {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TcpIpDialog));
			this.connect = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.hostName = new System.Windows.Forms.TextBox();
			this.hostPort = new System.Windows.Forms.NumericUpDown();
			this.howToConnectLink = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.hostPort)).BeginInit();
			this.SuspendLayout();
			// 
			// connect
			// 
			this.connect.Location = new System.Drawing.Point(354, 101);
			this.connect.Name = "connect";
			this.connect.Size = new System.Drawing.Size(84, 28);
			this.connect.TabIndex = 2;
			this.connect.Text = "Connec&t";
			this.connect.UseVisualStyleBackColor = true;
			this.connect.Click += new System.EventHandler(this.connect_Click);
			// 
			// cancel
			// 
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point(264, 101);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(84, 28);
			this.cancel.TabIndex = 3;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Host Name:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 68);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Host Port:";
			// 
			// hostName
			// 
			this.hostName.Location = new System.Drawing.Point(114, 33);
			this.hostName.Name = "hostName";
			this.hostName.Size = new System.Drawing.Size(324, 20);
			this.hostName.TabIndex = 0;
			// 
			// hostPort
			// 
			this.hostPort.Location = new System.Drawing.Point(114, 66);
			this.hostPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.hostPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.hostPort.Name = "hostPort";
			this.hostPort.Size = new System.Drawing.Size(324, 20);
			this.hostPort.TabIndex = 1;
			this.hostPort.Value = new decimal(new int[] {
            5555,
            0,
            0,
            0});
			// 
			// howToConnectLink
			// 
			this.howToConnectLink.AutoSize = true;
			this.howToConnectLink.Location = new System.Drawing.Point(305, 9);
			this.howToConnectLink.Name = "howToConnectLink";
			this.howToConnectLink.Size = new System.Drawing.Size(133, 13);
			this.howToConnectLink.TabIndex = 6;
			this.howToConnectLink.TabStop = true;
			this.howToConnectLink.Text = "Help Connecting via wi-fi...";
			this.howToConnectLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.howToConnectLink_LinkClicked);
			// 
			// TcpIpDialog
			// 
			this.AcceptButton = this.connect;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(450, 140);
			this.Controls.Add(this.howToConnectLink);
			this.Controls.Add(this.hostPort);
			this.Controls.Add(this.hostName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.connect);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TcpIpDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Remote Connect";
			this.Load += new System.EventHandler(this.TcpIpDialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.hostPort)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button connect;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox hostName;
		private System.Windows.Forms.NumericUpDown hostPort;
		private System.Windows.Forms.LinkLabel howToConnectLink;
	}
}