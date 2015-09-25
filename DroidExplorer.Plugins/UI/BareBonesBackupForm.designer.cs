namespace DroidExplorer.Plugins.UI {
  partial class BareBonesBackupForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( BareBonesBackupForm ) );
			this.pictureBox1 = new System.Windows.Forms.PictureBox ( );
			this.cancel = new System.Windows.Forms.Button ( );
			this.perform = new System.Windows.Forms.Button ( );
			this.finish = new System.Windows.Forms.Button ( );
			this.panel2 = new System.Windows.Forms.Panel ( );
			this.panel1 = new System.Windows.Forms.Panel ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.backupFromZip = new System.Windows.Forms.RadioButton ( );
			this.backupFromDevice = new System.Windows.Forms.RadioButton ( );
			this.backupComplete = new System.Windows.Forms.Label ( );
			this.status = new System.Windows.Forms.Label ( );
			this.progress = new System.Windows.Forms.ProgressBar ( );
			this.title = new System.Windows.Forms.Label ( );
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit ( );
			this.panel2.SuspendLayout ( );
			this.panel1.SuspendLayout ( );
			this.SuspendLayout ( );
			// 
			// pictureBox1
			// 
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.pictureBox1.Image = ( (System.Drawing.Image)( resources.GetObject ( "pictureBox1.Image" ) ) );
			this.pictureBox1.Location = new System.Drawing.Point ( 0, 0 );
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size ( 166, 320 );
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
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
			// perform
			// 
			this.perform.Location = new System.Drawing.Point ( 359, 10 );
			this.perform.Name = "perform";
			this.perform.Size = new System.Drawing.Size ( 95, 31 );
			this.perform.TabIndex = 5;
			this.perform.Text = "Backup";
			this.perform.UseVisualStyleBackColor = true;
			this.perform.Click += new System.EventHandler ( this.perform_Click );
			// 
			// finish
			// 
			this.finish.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.finish.Location = new System.Drawing.Point ( 460, 10 );
			this.finish.Name = "finish";
			this.finish.Size = new System.Drawing.Size ( 95, 31 );
			this.finish.TabIndex = 6;
			this.finish.Text = "&Finish";
			this.finish.UseVisualStyleBackColor = true;
			this.finish.Visible = false;
			this.finish.Click += new System.EventHandler ( this.finish_Click );
			// 
			// panel2
			// 
			this.panel2.Controls.Add ( this.perform );
			this.panel2.Controls.Add ( this.cancel );
			this.panel2.Controls.Add ( this.finish );
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point ( 0, 320 );
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size ( 563, 49 );
			this.panel2.TabIndex = 9;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add ( this.label1 );
			this.panel1.Controls.Add ( this.backupFromZip );
			this.panel1.Controls.Add ( this.backupFromDevice );
			this.panel1.Controls.Add ( this.backupComplete );
			this.panel1.Controls.Add ( this.status );
			this.panel1.Controls.Add ( this.progress );
			this.panel1.Controls.Add ( this.title );
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point ( 166, 0 );
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size ( 397, 320 );
			this.panel1.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point ( 6, 38 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 383, 44 );
			this.label1.TabIndex = 11;
			this.label1.Text = "Backup your \"Google Experience\" Applications for use in a \"Bare Bones\" ROM. Selec" +
					"t below the source you want to back up from. The signed update file will be copi" +
					"ed to your desktop.";
			// 
			// backupFromZip
			// 
			this.backupFromZip.Location = new System.Drawing.Point ( 44, 157 );
			this.backupFromZip.Name = "backupFromZip";
			this.backupFromZip.Size = new System.Drawing.Size ( 229, 24 );
			this.backupFromZip.TabIndex = 5;
			this.backupFromZip.Text = "Backup from existing update.zip";
			this.backupFromZip.UseVisualStyleBackColor = true;
			// 
			// backupFromDevice
			// 
			this.backupFromDevice.Checked = true;
			this.backupFromDevice.Location = new System.Drawing.Point ( 44, 129 );
			this.backupFromDevice.Name = "backupFromDevice";
			this.backupFromDevice.Size = new System.Drawing.Size ( 229, 22 );
			this.backupFromDevice.TabIndex = 4;
			this.backupFromDevice.TabStop = true;
			this.backupFromDevice.Text = "Backup from connected device";
			this.backupFromDevice.UseVisualStyleBackColor = true;
			// 
			// backupComplete
			// 
			this.backupComplete.AutoSize = true;
			this.backupComplete.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.backupComplete.Location = new System.Drawing.Point ( 81, 97 );
			this.backupComplete.Name = "backupComplete";
			this.backupComplete.Size = new System.Drawing.Size ( 229, 20 );
			this.backupComplete.TabIndex = 3;
			this.backupComplete.Text = "Backup Process Completed";
			this.backupComplete.Visible = false;
			// 
			// status
			// 
			this.status.AutoEllipsis = true;
			this.status.Location = new System.Drawing.Point ( 10, 239 );
			this.status.Name = "status";
			this.status.Size = new System.Drawing.Size ( 379, 13 );
			this.status.TabIndex = 2;
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point ( 10, 256 );
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size ( 379, 23 );
			this.progress.TabIndex = 1;
			this.progress.Visible = false;
			// 
			// title
			// 
			this.title.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.title.Location = new System.Drawing.Point ( 6, 9 );
			this.title.Name = "title";
			this.title.Size = new System.Drawing.Size ( 383, 42 );
			this.title.TabIndex = 0;
			this.title.Text = "Google Applications Backup";
			// 
			// BareBonesBackupForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size ( 563, 369 );
			this.Controls.Add ( this.panel1 );
			this.Controls.Add ( this.pictureBox1 );
			this.Controls.Add ( this.panel2 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject ( "$this.Icon" ) ) );
			this.Name = "BareBonesBackupForm";
			this.Text = "Google Applications Backup";
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit ( );
			this.panel2.ResumeLayout ( false );
			this.panel1.ResumeLayout ( false );
			this.panel1.PerformLayout ( );
			this.ResumeLayout ( false );
			this.PerformLayout ( );

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button cancel;
    private System.Windows.Forms.Button perform;
    private System.Windows.Forms.Button finish;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label status;
    private System.Windows.Forms.ProgressBar progress;
    private System.Windows.Forms.Label title;
    private System.Windows.Forms.Label backupComplete;
    private System.Windows.Forms.RadioButton backupFromZip;
    private System.Windows.Forms.RadioButton backupFromDevice;
    private System.Windows.Forms.Label label1;
  }
}