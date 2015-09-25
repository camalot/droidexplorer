namespace DroidExplorer.Plugins.UI {
	partial class RestoreDeviceForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreDeviceForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.unlock = new System.Windows.Forms.Label();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.cancel = new System.Windows.Forms.Button();
			this.restore = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.device = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.backupName = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.backupName);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.device);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.unlock);
			this.panel1.Controls.Add(this.progress);
			this.panel1.Controls.Add(this.cancel);
			this.panel1.Controls.Add(this.restore);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(156, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(338, 317);
			this.panel1.TabIndex = 1;
			// 
			// unlock
			// 
			this.unlock.AutoSize = true;
			this.unlock.Location = new System.Drawing.Point(16, 190);
			this.unlock.Name = "unlock";
			this.unlock.Size = new System.Drawing.Size(272, 13);
			this.unlock.TabIndex = 7;
			this.unlock.Text = "Unlock your device, enter password to start restore.";
			this.unlock.Visible = false;
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point(14, 212);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(311, 23);
			this.progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progress.TabIndex = 6;
			this.progress.Visible = false;
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
			// restore
			// 
			this.restore.Location = new System.Drawing.Point(133, 271);
			this.restore.Name = "restore";
			this.restore.Size = new System.Drawing.Size(89, 33);
			this.restore.TabIndex = 5;
			this.restore.Text = "&Restore";
			this.restore.UseVisualStyleBackColor = true;
			this.restore.Click += new System.EventHandler(this.restore_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(221, 40);
			this.label1.TabIndex = 3;
			this.label1.Text = "Device Restore";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 90);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Restoring Device:";
			// 
			// device
			// 
			this.device.AutoEllipsis = true;
			this.device.Location = new System.Drawing.Point(117, 90);
			this.device.Name = "device";
			this.device.Size = new System.Drawing.Size(208, 13);
			this.device.TabIndex = 9;
			this.device.Text = "[Device Name]";
			this.device.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 117);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(51, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Backup: ";
			// 
			// backupName
			// 
			this.backupName.AutoEllipsis = true;
			this.backupName.Location = new System.Drawing.Point(117, 117);
			this.backupName.Name = "backupName";
			this.backupName.Size = new System.Drawing.Size(208, 13);
			this.backupName.TabIndex = 11;
			this.backupName.Text = "[Backup Name]";
			this.backupName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RestoreDeviceForm
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
			this.Name = "RestoreDeviceForm";
			this.Text = "Device Restore";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Button restore;
		private System.Windows.Forms.Label unlock;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.Label backupName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label device;
		private System.Windows.Forms.Label label2;
	}
}