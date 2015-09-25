namespace DroidExplorer.UI {
	partial class SplashDialog {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashDialog));
			this.logo = new System.Windows.Forms.PictureBox();
			this.title = new System.Windows.Forms.PictureBox();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.status = new System.Windows.Forms.Label();
			this.version = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.title)).BeginInit();
			this.SuspendLayout();
			// 
			// logo
			// 
			this.logo.BackColor = System.Drawing.Color.Transparent;
			this.logo.Location = new System.Drawing.Point(12, 12);
			this.logo.Name = "logo";
			this.logo.Size = new System.Drawing.Size(192, 192);
			this.logo.TabIndex = 0;
			this.logo.TabStop = false;
			// 
			// title
			// 
			this.title.BackColor = System.Drawing.Color.Transparent;
			this.title.Location = new System.Drawing.Point(204, 12);
			this.title.Name = "title";
			this.title.Size = new System.Drawing.Size(483, 71);
			this.title.TabIndex = 1;
			this.title.TabStop = false;
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point(210, 181);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(470, 23);
			this.progress.TabIndex = 2;
			// 
			// status
			// 
			this.status.AutoSize = true;
			this.status.BackColor = System.Drawing.Color.Transparent;
			this.status.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.status.Location = new System.Drawing.Point(210, 165);
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size(116, 13);
			this.status.TabIndex = 3;
			this.status.Text = "Starting Droid Explorer";
			// 
			// version
			// 
			this.version.BackColor = System.Drawing.Color.Transparent;
			this.version.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.version.Location = new System.Drawing.Point(533, 86);
			this.version.Name = "version";
			this.version.Size = new System.Drawing.Size(147, 23);
			this.version.TabIndex = 4;
			this.version.Text = "[Version]";
			this.version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SplashDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(706, 225);
			this.Controls.Add(this.version);
			this.Controls.Add(this.status);
			this.Controls.Add(this.progress);
			this.Controls.Add(this.title);
			this.Controls.Add(this.logo);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SplashDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Droid Explorer";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.title)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox logo;
		private System.Windows.Forms.PictureBox title;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.Label status;
		private System.Windows.Forms.Label version;
	}
}