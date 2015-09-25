namespace DroidExplorer.UI {
	partial class SdkInstallDialog {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( SdkInstallDialog ) );
      this.panel1 = new System.Windows.Forms.Panel ( );
      this.finishedPanel = new System.Windows.Forms.Panel ( );
      this.label1 = new System.Windows.Forms.Label ( );
      this.installPanel = new System.Windows.Forms.Panel ( );
      this.stepsPanel = new System.Windows.Forms.Panel ( );
      this.stepImage = new System.Windows.Forms.PictureBox ( );
      this.step3 = new System.Windows.Forms.Label ( );
      this.step1 = new System.Windows.Forms.Label ( );
      this.step4 = new System.Windows.Forms.Label ( );
      this.step2 = new System.Windows.Forms.Label ( );
      this.information = new System.Windows.Forms.Label ( );
      this.errorPanel = new System.Windows.Forms.Panel ( );
      this.errorLabel = new System.Windows.Forms.Label ( );
      this.errorImage = new System.Windows.Forms.PictureBox ( );
      this.selectSourcePanel = new System.Windows.Forms.Panel ( );
      this.panel5 = new System.Windows.Forms.Panel ( );
      this.sdkInstalled = new System.Windows.Forms.RadioButton ( );
      this.androidTools = new System.Windows.Forms.RadioButton ( );
      this.label6 = new System.Windows.Forms.Label ( );
      this.statusLabel = new System.Windows.Forms.Label ( );
      this.progress = new System.Windows.Forms.ProgressBar ( );
      this.title = new System.Windows.Forms.Label ( );
      this.pictureBox1 = new System.Windows.Forms.PictureBox ( );
      this.panel2 = new System.Windows.Forms.Panel ( );
      this.next = new System.Windows.Forms.Button ( );
      this.perform = new System.Windows.Forms.Button ( );
      this.cancel = new System.Windows.Forms.Button ( );
      this.finish = new System.Windows.Forms.Button ( );
      this.usbDriverVersion = new System.Windows.Forms.Label ( );
      this.panel1.SuspendLayout ( );
      this.finishedPanel.SuspendLayout ( );
      this.installPanel.SuspendLayout ( );
      this.stepsPanel.SuspendLayout ( );
      ( ( System.ComponentModel.ISupportInitialize )( this.stepImage ) ).BeginInit ( );
      this.errorPanel.SuspendLayout ( );
      ( ( System.ComponentModel.ISupportInitialize )( this.errorImage ) ).BeginInit ( );
      this.selectSourcePanel.SuspendLayout ( );
      this.panel5.SuspendLayout ( );
      ( ( System.ComponentModel.ISupportInitialize )( this.pictureBox1 ) ).BeginInit ( );
      this.panel2.SuspendLayout ( );
      this.SuspendLayout ( );
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Window;
      this.panel1.Controls.Add ( this.finishedPanel );
      this.panel1.Controls.Add ( this.installPanel );
      this.panel1.Controls.Add ( this.errorPanel );
      this.panel1.Controls.Add ( this.selectSourcePanel );
      this.panel1.Controls.Add ( this.statusLabel );
      this.panel1.Controls.Add ( this.progress );
      this.panel1.Controls.Add ( this.title );
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point ( 166, 0 );
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size ( 397, 669 );
      this.panel1.TabIndex = 4;
      // 
      // finishedPanel
      // 
      this.finishedPanel.Controls.Add ( this.label1 );
      this.finishedPanel.Location = new System.Drawing.Point ( 6, 619 );
      this.finishedPanel.Name = "finishedPanel";
      this.finishedPanel.Size = new System.Drawing.Size ( 383, 47 );
      this.finishedPanel.TabIndex = 13;
      this.finishedPanel.Visible = false;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point ( 0, 0 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size ( 383, 47 );
      this.label1.TabIndex = 0;
      this.label1.Text = "Android SDK Tools setup completed. Click \'Finish\' below to launch Droid Explorer";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // installPanel
      // 
      this.installPanel.Controls.Add ( this.stepsPanel );
      this.installPanel.Controls.Add ( this.information );
      this.installPanel.Location = new System.Drawing.Point ( 6, 422 );
      this.installPanel.Name = "installPanel";
      this.installPanel.Size = new System.Drawing.Size ( 381, 184 );
      this.installPanel.TabIndex = 11;
      this.installPanel.Visible = false;
      // 
      // stepsPanel
      // 
      this.stepsPanel.Controls.Add ( this.stepImage );
      this.stepsPanel.Controls.Add ( this.step3 );
      this.stepsPanel.Controls.Add ( this.step1 );
      this.stepsPanel.Controls.Add ( this.step4 );
      this.stepsPanel.Controls.Add ( this.step2 );
      this.stepsPanel.Location = new System.Drawing.Point ( 13, 57 );
      this.stepsPanel.Name = "stepsPanel";
      this.stepsPanel.Size = new System.Drawing.Size ( 368, 107 );
      this.stepsPanel.TabIndex = 9;
      // 
      // stepImage
      // 
			this.stepImage.Image = global::DroidExplorer.Resources.Images.green_arrow1;
      this.stepImage.Location = new System.Drawing.Point ( 23, 10 );
      this.stepImage.Name = "stepImage";
      this.stepImage.Size = new System.Drawing.Size ( 13, 13 );
      this.stepImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.stepImage.TabIndex = 8;
      this.stepImage.TabStop = false;
      this.stepImage.Visible = false;
      // 
      // step3
      // 
      this.step3.AutoSize = true;
      this.step3.Location = new System.Drawing.Point ( 42, 57 );
      this.step3.Name = "step3";
      this.step3.Size = new System.Drawing.Size ( 162, 13 );
      this.step3.TabIndex = 6;
      this.step3.Text = "Register tools with Droid Explorer";
      // 
      // step1
      // 
      this.step1.AutoSize = true;
      this.step1.Location = new System.Drawing.Point ( 42, 10 );
      this.step1.Name = "step1";
      this.step1.Size = new System.Drawing.Size ( 80, 13 );
      this.step1.TabIndex = 4;
      this.step1.Text = "Download tools";
      // 
      // step4
      // 
      this.step4.AutoSize = true;
      this.step4.Location = new System.Drawing.Point ( 42, 79 );
      this.step4.Name = "step4";
      this.step4.Size = new System.Drawing.Size ( 108, 13 );
      this.step4.TabIndex = 7;
      this.step4.Text = "Delete temporary files";
      // 
      // step2
      // 
      this.step2.AutoSize = true;
      this.step2.Location = new System.Drawing.Point ( 42, 33 );
      this.step2.Name = "step2";
      this.step2.Size = new System.Drawing.Size ( 65, 13 );
      this.step2.TabIndex = 5;
      this.step2.Text = "Extract tools";
      // 
      // information
      // 
      this.information.Dock = System.Windows.Forms.DockStyle.Top;
      this.information.Location = new System.Drawing.Point ( 0, 0 );
      this.information.Name = "information";
      this.information.Size = new System.Drawing.Size ( 381, 54 );
      this.information.TabIndex = 2;
      this.information.Text = "Droid Explorer requires tools that are part of the Android SDK be present in orde" +
          "r to use Droid Explorer. This wizard will download and setup these tools for use" +
          " within Droid Explorer.";
      // 
      // errorPanel
      // 
      this.errorPanel.Controls.Add ( this.errorLabel );
      this.errorPanel.Controls.Add ( this.errorImage );
      this.errorPanel.Location = new System.Drawing.Point ( 10, 285 );
      this.errorPanel.Name = "errorPanel";
      this.errorPanel.Size = new System.Drawing.Size ( 372, 113 );
      this.errorPanel.TabIndex = 10;
      this.errorPanel.Visible = false;
      // 
      // errorLabel
      // 
      this.errorLabel.Location = new System.Drawing.Point ( 56, 3 );
      this.errorLabel.Name = "errorLabel";
      this.errorLabel.Size = new System.Drawing.Size ( 310, 105 );
      this.errorLabel.TabIndex = 10;
      this.errorLabel.Text = "error message";
      // 
      // errorImage
      // 
			this.errorImage.Image = global::DroidExplorer.Resources.Images.error;
      this.errorImage.Location = new System.Drawing.Point ( 4, 3 );
      this.errorImage.Name = "errorImage";
      this.errorImage.Size = new System.Drawing.Size ( 48, 48 );
      this.errorImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.errorImage.TabIndex = 9;
      this.errorImage.TabStop = false;
      // 
      // selectSourcePanel
      // 
      this.selectSourcePanel.Controls.Add ( this.panel5 );
      this.selectSourcePanel.Controls.Add ( this.label6 );
      this.selectSourcePanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.selectSourcePanel.Location = new System.Drawing.Point ( 0, 63 );
      this.selectSourcePanel.Name = "selectSourcePanel";
      this.selectSourcePanel.Size = new System.Drawing.Size ( 397, 184 );
      this.selectSourcePanel.TabIndex = 12;
      // 
      // panel5
      // 
      this.panel5.Controls.Add ( this.sdkInstalled );
      this.panel5.Controls.Add ( this.androidTools );
      this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel5.Location = new System.Drawing.Point ( 0, 54 );
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size ( 397, 130 );
      this.panel5.TabIndex = 10;
      // 
      // sdkInstalled
      // 
      this.sdkInstalled.AutoSize = true;
      this.sdkInstalled.Location = new System.Drawing.Point ( 31, 76 );
      this.sdkInstalled.Name = "sdkInstalled";
      this.sdkInstalled.Size = new System.Drawing.Size ( 283, 17 );
      this.sdkInstalled.TabIndex = 1;
      this.sdkInstalled.Text = "I already have the SDK installed, let me pick where it is";
      this.sdkInstalled.UseVisualStyleBackColor = true;
      // 
      // androidTools
      // 
      this.androidTools.AutoSize = true;
      this.androidTools.Checked = true;
      this.androidTools.Location = new System.Drawing.Point ( 31, 53 );
      this.androidTools.Name = "androidTools";
      this.androidTools.Size = new System.Drawing.Size ( 322, 17 );
      this.androidTools.TabIndex = 0;
      this.androidTools.TabStop = true;
      this.androidTools.Text = "Use Android Tools for Droid Explorer (Requires internet access)";
      this.androidTools.UseVisualStyleBackColor = true;
      // 
      // label6
      // 
      this.label6.Dock = System.Windows.Forms.DockStyle.Top;
      this.label6.Location = new System.Drawing.Point ( 0, 0 );
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size ( 397, 54 );
      this.label6.TabIndex = 2;
      this.label6.Text = resources.GetString ( "label6.Text" );
      // 
      // statusLabel
      // 
      this.statusLabel.AutoEllipsis = true;
      this.statusLabel.Location = new System.Drawing.Point ( 10, 256 );
      this.statusLabel.Name = "statusLabel";
      this.statusLabel.Size = new System.Drawing.Size ( 379, 16 );
      this.statusLabel.TabIndex = 3;
      // 
      // progress
      // 
      this.progress.Location = new System.Drawing.Point ( 10, 276 );
      this.progress.Name = "progress";
      this.progress.Size = new System.Drawing.Size ( 379, 23 );
      this.progress.TabIndex = 1;
      this.progress.Visible = false;
      // 
      // title
      // 
      this.title.Dock = System.Windows.Forms.DockStyle.Top;
      this.title.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.title.Location = new System.Drawing.Point ( 0, 0 );
      this.title.Name = "title";
      this.title.Size = new System.Drawing.Size ( 397, 63 );
      this.title.TabIndex = 0;
      this.title.Text = "Welcome to the Android Tools Setup Wizard";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBox1.Image = global::DroidExplorer.Resources.Images.installmain1;
      this.pictureBox1.Location = new System.Drawing.Point ( 0, 0 );
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size ( 166, 669 );
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 3;
      this.pictureBox1.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add ( this.usbDriverVersion );
      this.panel2.Controls.Add ( this.next );
      this.panel2.Controls.Add ( this.perform );
      this.panel2.Controls.Add ( this.cancel );
      this.panel2.Controls.Add ( this.finish );
      this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel2.Location = new System.Drawing.Point ( 0, 669 );
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size ( 563, 49 );
      this.panel2.TabIndex = 5;
      // 
      // next
      // 
      this.next.Location = new System.Drawing.Point ( 355, 10 );
      this.next.Name = "next";
      this.next.Size = new System.Drawing.Size ( 95, 31 );
      this.next.TabIndex = 7;
      this.next.Text = "&Next";
      this.next.UseVisualStyleBackColor = true;
      this.next.Click += new System.EventHandler ( this.next_Click );
      // 
      // perform
      // 
      this.perform.Location = new System.Drawing.Point ( 355, 10 );
      this.perform.Name = "perform";
      this.perform.Size = new System.Drawing.Size ( 95, 31 );
      this.perform.TabIndex = 5;
      this.perform.Text = "&Install";
      this.perform.UseVisualStyleBackColor = true;
      this.perform.Visible = false;
      this.perform.Click += new System.EventHandler ( this.perform_Click );
      // 
      // cancel
      // 
      this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancel.Location = new System.Drawing.Point ( 460, 10 );
      this.cancel.Name = "cancel";
      this.cancel.Size = new System.Drawing.Size ( 95, 31 );
      this.cancel.TabIndex = 4;
      this.cancel.Text = "&Cancel";
      this.cancel.UseVisualStyleBackColor = true;
      this.cancel.Click += new System.EventHandler ( this.cancel_Click );
      // 
      // finish
      // 
      this.finish.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.finish.Location = new System.Drawing.Point ( 460, 10 );
      this.finish.Name = "finish";
      this.finish.Size = new System.Drawing.Size ( 95, 31 );
      this.finish.TabIndex = 6;
      this.finish.Text = "&Finish";
      this.finish.UseVisualStyleBackColor = true;
      this.finish.Visible = false;
      this.finish.Click += new System.EventHandler ( this.finish_Click );
      // 
      // usbDriverVersion
      // 
      this.usbDriverVersion.Location = new System.Drawing.Point ( 12, 18 );
      this.usbDriverVersion.Name = "usbDriverVersion";
      this.usbDriverVersion.Size = new System.Drawing.Size ( 180, 15 );
      this.usbDriverVersion.TabIndex = 2;
      this.usbDriverVersion.Text = "USB Driver Version: {0}";
      this.usbDriverVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // SdkInstallDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size ( 563, 718 );
      this.Controls.Add ( this.panel1 );
      this.Controls.Add ( this.pictureBox1 );
      this.Controls.Add ( this.panel2 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ( ( System.Drawing.Icon )( resources.GetObject ( "$this.Icon" ) ) );
      this.MaximizeBox = false;
      this.Name = "SdkInstallDialog";
      this.Text = "Android Tools Setup Wizard";
      this.panel1.ResumeLayout ( false );
      this.finishedPanel.ResumeLayout ( false );
      this.installPanel.ResumeLayout ( false );
      this.stepsPanel.ResumeLayout ( false );
      this.stepsPanel.PerformLayout ( );
      ( ( System.ComponentModel.ISupportInitialize )( this.stepImage ) ).EndInit ( );
      this.errorPanel.ResumeLayout ( false );
      this.errorPanel.PerformLayout ( );
      ( ( System.ComponentModel.ISupportInitialize )( this.errorImage ) ).EndInit ( );
      this.selectSourcePanel.ResumeLayout ( false );
      this.panel5.ResumeLayout ( false );
      this.panel5.PerformLayout ( );
      ( ( System.ComponentModel.ISupportInitialize )( this.pictureBox1 ) ).EndInit ( );
      this.panel2.ResumeLayout ( false );
      this.ResumeLayout ( false );
      this.PerformLayout ( );

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label information;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.Label title;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button perform;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.Label step1;
		private System.Windows.Forms.PictureBox stepImage;
		private System.Windows.Forms.Label step4;
		private System.Windows.Forms.Label step3;
		private System.Windows.Forms.Label step2;
		private System.Windows.Forms.Button finish;
    private System.Windows.Forms.PictureBox errorImage;
    private System.Windows.Forms.Panel stepsPanel;
    private System.Windows.Forms.Panel errorPanel;
    private System.Windows.Forms.Label errorLabel;
    private System.Windows.Forms.Panel installPanel;
    private System.Windows.Forms.Button next;
    private System.Windows.Forms.Panel selectSourcePanel;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.RadioButton sdkInstalled;
    private System.Windows.Forms.RadioButton androidTools;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Panel finishedPanel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label usbDriverVersion;
	}
}