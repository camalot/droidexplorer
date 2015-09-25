namespace DroidExplorer.Plugins.UI {
	partial class BackupDeviceForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupDeviceForm));
			this.apk = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.unlock = new System.Windows.Forms.Label();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.backup = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.shared = new System.Windows.Forms.CheckBox();
			this.system = new System.Windows.Forms.CheckBox();
			this.extended = new System.Windows.Forms.CheckBox();
			this.readMore = new System.Windows.Forms.LinkLabel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// apk
			// 
			this.apk.AutoSize = true;
			this.apk.Location = new System.Drawing.Point(23, 71);
			this.apk.Name = "apk";
			this.apk.Size = new System.Drawing.Size(188, 17);
			this.apk.TabIndex = 0;
			this.apk.Text = "Include Application Files (*.apk)";
			this.apk.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.readMore);
			this.panel1.Controls.Add(this.extended);
			this.panel1.Controls.Add(this.unlock);
			this.panel1.Controls.Add(this.progress);
			this.panel1.Controls.Add(this.backup);
			this.panel1.Controls.Add(this.cancel);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.shared);
			this.panel1.Controls.Add(this.system);
			this.panel1.Controls.Add(this.apk);
			this.panel1.Location = new System.Drawing.Point(156, -1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(338, 317);
			this.panel1.TabIndex = 1;
			// 
			// unlock
			// 
			this.unlock.AutoSize = true;
			this.unlock.Location = new System.Drawing.Point(16, 197);
			this.unlock.Name = "unlock";
			this.unlock.Size = new System.Drawing.Size(287, 13);
			this.unlock.TabIndex = 7;
			this.unlock.Text = "Unlock your device, enter password then start backup.";
			this.unlock.Visible = false;
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point(14, 215);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(311, 23);
			this.progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progress.TabIndex = 6;
			this.progress.Visible = false;
			// 
			// backup
			// 
			this.backup.Location = new System.Drawing.Point(133, 271);
			this.backup.Name = "backup";
			this.backup.Size = new System.Drawing.Size(89, 33);
			this.backup.TabIndex = 5;
			this.backup.Text = "&Backup";
			this.backup.UseVisualStyleBackColor = true;
			this.backup.Click += new System.EventHandler(this.backup_Click);
			// 
			// cancel
			// 
			this.cancel.Location = new System.Drawing.Point(236, 271);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(89, 33);
			this.cancel.TabIndex = 4;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(217, 40);
			this.label1.TabIndex = 3;
			this.label1.Text = "Device Backup";
			// 
			// shared
			// 
			this.shared.AutoSize = true;
			this.shared.Location = new System.Drawing.Point(23, 137);
			this.shared.Name = "shared";
			this.shared.Size = new System.Drawing.Size(135, 17);
			this.shared.TabIndex = 2;
			this.shared.Text = "Include /sdcard/ data";
			this.shared.UseVisualStyleBackColor = true;
			// 
			// system
			// 
			this.system.AutoSize = true;
			this.system.Location = new System.Drawing.Point(23, 104);
			this.system.Name = "system";
			this.system.Size = new System.Drawing.Size(127, 17);
			this.system.TabIndex = 1;
			this.system.Text = "Include system data";
			this.system.UseVisualStyleBackColor = true;
			// 
			// extended
			// 
			this.extended.AutoSize = true;
			this.extended.Location = new System.Drawing.Point(23, 168);
			this.extended.Name = "extended";
			this.extended.Size = new System.Drawing.Size(185, 17);
			this.extended.TabIndex = 8;
			this.extended.Text = "Use Enhanced Android Backup";
			this.extended.UseVisualStyleBackColor = true;
			// 
			// readMore
			// 
			this.readMore.AutoSize = true;
			this.readMore.Location = new System.Drawing.Point(240, 169);
			this.readMore.Name = "readMore";
			this.readMore.Size = new System.Drawing.Size(63, 13);
			this.readMore.TabIndex = 9;
			this.readMore.TabStop = true;
			this.readMore.Text = "Read More";
			this.readMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.readMore_LinkClicked);
			// 
			// BackupDeviceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(493, 315);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(509, 353);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(509, 353);
			this.Name = "BackupDeviceForm";
			this.Text = "Device Backup";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox apk;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label unlock;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.Button backup;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox shared;
		private System.Windows.Forms.CheckBox system;
		private System.Windows.Forms.LinkLabel readMore;
		private System.Windows.Forms.CheckBox extended;
	}
}