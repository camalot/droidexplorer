namespace DroidExplorer.Core.UI {
	partial class TransferDialog {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferDialog));
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.from = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.copyStatus = new System.Windows.Forms.Label();
			this.timeRemaining = new System.Windows.Forms.Label();
			this.itemsRemaining = new System.Windows.Forms.Label();
			this.stop = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(12, 134);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(466, 18);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar.TabIndex = 1;
			// 
			// from
			// 
			this.from.AutoEllipsis = true;
			this.from.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.from.Location = new System.Drawing.Point(13, 65);
			this.from.Name = "from";
			this.from.Size = new System.Drawing.Size(465, 14);
			this.from.TabIndex = 2;
			this.from.Text = "from PHONE-SERIAL (/file/path/file) to DirectoryName (c:\\dir\\path\\file)";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panel1.Controls.Add(this.copyStatus);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(490, 53);
			this.panel1.TabIndex = 3;
			// 
			// copyStatus
			// 
			this.copyStatus.AutoSize = true;
			this.copyStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.copyStatus.Location = new System.Drawing.Point(12, 14);
			this.copyStatus.Name = "copyStatus";
			this.copyStatus.Size = new System.Drawing.Size(256, 24);
			this.copyStatus.TabIndex = 0;
			this.copyStatus.Text = "Copying 0 items (0.0bytes)";
			// 
			// timeRemaining
			// 
			this.timeRemaining.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timeRemaining.Location = new System.Drawing.Point(13, 85);
			this.timeRemaining.Name = "timeRemaining";
			this.timeRemaining.Size = new System.Drawing.Size(351, 16);
			this.timeRemaining.TabIndex = 4;
			this.timeRemaining.Text = "Time remaining: {0}";
			// 
			// itemsRemaining
			// 
			this.itemsRemaining.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.itemsRemaining.Location = new System.Drawing.Point(13, 106);
			this.itemsRemaining.Name = "itemsRemaining";
			this.itemsRemaining.Size = new System.Drawing.Size(351, 16);
			this.itemsRemaining.TabIndex = 5;
			this.itemsRemaining.Text = "Items remaining: {0}";
			// 
			// stop
			// 
			this.stop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.stop.Location = new System.Drawing.Point(374, 176);
			this.stop.Name = "stop";
			this.stop.Size = new System.Drawing.Size(104, 31);
			this.stop.TabIndex = 6;
			this.stop.Text = "&Stop";
			this.stop.UseVisualStyleBackColor = true;
			// 
			// TransferDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(490, 219);
			this.Controls.Add(this.stop);
			this.Controls.Add(this.itemsRemaining);
			this.Controls.Add(this.timeRemaining);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.from);
			this.Controls.Add(this.progressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "TransferDialog";
			this.Text = "Transfering Files...";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label from;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label copyStatus;
		private System.Windows.Forms.Label timeRemaining;
		private System.Windows.Forms.Label itemsRemaining;
		private System.Windows.Forms.Button stop;
	}
}