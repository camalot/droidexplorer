namespace DroidExplorer.Plugins.UI {
	partial class BartForm {
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
			this.components = new System.ComponentModel.Container ( );
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( BartForm ) );
			this.tabs = new System.Windows.Forms.TabControl ( );
			this.tabPage1 = new System.Windows.Forms.TabPage ( );
			this.gotoCreate = new System.Windows.Forms.Button ( );
			this.groupBox1 = new System.Windows.Forms.GroupBox ( );
			this.groupBox4 = new System.Windows.Forms.GroupBox ( );
			this.restoreShutdown = new System.Windows.Forms.RadioButton ( );
			this.restoreReboot = new System.Windows.Forms.RadioButton ( );
			this.restoreNothing = new System.Windows.Forms.RadioButton ( );
			this.restore = new System.Windows.Forms.Button ( );
			this.restoreIncludeData = new System.Windows.Forms.CheckBox ( );
			this.restoreIncludeRecovery = new System.Windows.Forms.CheckBox ( );
			this.restoreIncludeSystem = new System.Windows.Forms.CheckBox ( );
			this.restoreVerbose = new System.Windows.Forms.CheckBox ( );
			this.restoreIncludeBoot = new System.Windows.Forms.CheckBox ( );
			this.delete = new System.Windows.Forms.Button ( );
			this.backups = new System.Windows.Forms.ListBox ( );
			this.tabPage2 = new System.Windows.Forms.TabPage ( );
			this.groupBox2 = new System.Windows.Forms.GroupBox ( );
			this.groupBox3 = new System.Windows.Forms.GroupBox ( );
			this.shutdown = new System.Windows.Forms.RadioButton ( );
			this.reboot = new System.Windows.Forms.RadioButton ( );
			this.nothing = new System.Windows.Forms.RadioButton ( );
			this.compress = new System.Windows.Forms.CheckBox ( );
			this.name = new System.Windows.Forms.TextBox ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.create = new System.Windows.Forms.Button ( );
			this.includeData = new System.Windows.Forms.CheckBox ( );
			this.includeRecovery = new System.Windows.Forms.CheckBox ( );
			this.includeSystem = new System.Windows.Forms.CheckBox ( );
			this.verbose = new System.Windows.Forms.CheckBox ( );
			this.includeBoot = new System.Windows.Forms.CheckBox ( );
			this.tabPage3 = new System.Windows.Forms.TabPage ( );
			this.console = new System.Windows.Forms.RichTextBox ( );
			this.close = new System.Windows.Forms.Button ( );
			this.errorProvider = new System.Windows.Forms.ErrorProvider ( this.components );
			this.tabs.SuspendLayout ( );
			this.tabPage1.SuspendLayout ( );
			this.groupBox1.SuspendLayout ( );
			this.groupBox4.SuspendLayout ( );
			this.tabPage2.SuspendLayout ( );
			this.groupBox2.SuspendLayout ( );
			this.groupBox3.SuspendLayout ( );
			this.tabPage3.SuspendLayout ( );
			( (System.ComponentModel.ISupportInitialize)( this.errorProvider ) ).BeginInit ( );
			this.SuspendLayout ( );
			// 
			// tabs
			// 
			this.tabs.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
									| System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.tabs.Controls.Add ( this.tabPage1 );
			this.tabs.Controls.Add ( this.tabPage2 );
			this.tabs.Controls.Add ( this.tabPage3 );
			this.tabs.Location = new System.Drawing.Point ( 2, 6 );
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size ( 567, 339 );
			this.tabs.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add ( this.gotoCreate );
			this.tabPage1.Controls.Add ( this.groupBox1 );
			this.tabPage1.Controls.Add ( this.delete );
			this.tabPage1.Controls.Add ( this.backups );
			this.tabPage1.Location = new System.Drawing.Point ( 4, 22 );
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding ( 3 );
			this.tabPage1.Size = new System.Drawing.Size ( 559, 313 );
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Manage";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// gotoCreate
			// 
			this.gotoCreate.Location = new System.Drawing.Point ( 430, 283 );
			this.gotoCreate.Name = "gotoCreate";
			this.gotoCreate.Size = new System.Drawing.Size ( 123, 23 );
			this.gotoCreate.TabIndex = 3;
			this.gotoCreate.Text = "Create Backup";
			this.gotoCreate.UseVisualStyleBackColor = true;
			this.gotoCreate.Click += new System.EventHandler ( this.gotoCreate_Click );
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.groupBox1.Controls.Add ( this.groupBox4 );
			this.groupBox1.Controls.Add ( this.restore );
			this.groupBox1.Controls.Add ( this.restoreIncludeData );
			this.groupBox1.Controls.Add ( this.restoreIncludeRecovery );
			this.groupBox1.Controls.Add ( this.restoreIncludeSystem );
			this.groupBox1.Controls.Add ( this.restoreVerbose );
			this.groupBox1.Controls.Add ( this.restoreIncludeBoot );
			this.groupBox1.Location = new System.Drawing.Point ( 183, 7 );
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size ( 364, 172 );
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Restore Options";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add ( this.restoreShutdown );
			this.groupBox4.Controls.Add ( this.restoreReboot );
			this.groupBox4.Controls.Add ( this.restoreNothing );
			this.groupBox4.Location = new System.Drawing.Point ( 158, 9 );
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size ( 200, 103 );
			this.groupBox4.TabIndex = 10;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "After Restore Complete";
			// 
			// restoreShutdown
			// 
			this.restoreShutdown.AutoSize = true;
			this.restoreShutdown.Location = new System.Drawing.Point ( 7, 66 );
			this.restoreShutdown.Name = "restoreShutdown";
			this.restoreShutdown.Size = new System.Drawing.Size ( 73, 17 );
			this.restoreShutdown.TabIndex = 2;
			this.restoreShutdown.Text = "Shutdown";
			this.restoreShutdown.UseVisualStyleBackColor = true;
			// 
			// restoreReboot
			// 
			this.restoreReboot.AutoSize = true;
			this.restoreReboot.Location = new System.Drawing.Point ( 7, 43 );
			this.restoreReboot.Name = "restoreReboot";
			this.restoreReboot.Size = new System.Drawing.Size ( 60, 17 );
			this.restoreReboot.TabIndex = 1;
			this.restoreReboot.Text = "Reboot";
			this.restoreReboot.UseVisualStyleBackColor = true;
			// 
			// restoreNothing
			// 
			this.restoreNothing.AutoSize = true;
			this.restoreNothing.Checked = true;
			this.restoreNothing.Location = new System.Drawing.Point ( 7, 20 );
			this.restoreNothing.Name = "restoreNothing";
			this.restoreNothing.Size = new System.Drawing.Size ( 79, 17 );
			this.restoreNothing.TabIndex = 0;
			this.restoreNothing.TabStop = true;
			this.restoreNothing.Text = "Do Nothing";
			this.restoreNothing.UseVisualStyleBackColor = true;
			// 
			// restore
			// 
			this.restore.Location = new System.Drawing.Point ( 264, 143 );
			this.restore.Name = "restore";
			this.restore.Size = new System.Drawing.Size ( 94, 23 );
			this.restore.TabIndex = 5;
			this.restore.Text = "&Restore";
			this.restore.UseVisualStyleBackColor = true;
			this.restore.Click += new System.EventHandler ( this.restore_Click );
			// 
			// restoreIncludeData
			// 
			this.restoreIncludeData.AutoSize = true;
			this.restoreIncludeData.Checked = true;
			this.restoreIncludeData.CheckState = System.Windows.Forms.CheckState.Checked;
			this.restoreIncludeData.Location = new System.Drawing.Point ( 6, 43 );
			this.restoreIncludeData.Name = "restoreIncludeData";
			this.restoreIncludeData.Size = new System.Drawing.Size ( 87, 17 );
			this.restoreIncludeData.TabIndex = 4;
			this.restoreIncludeData.Text = "Include Data";
			this.restoreIncludeData.UseVisualStyleBackColor = true;
			// 
			// restoreIncludeRecovery
			// 
			this.restoreIncludeRecovery.AutoSize = true;
			this.restoreIncludeRecovery.Location = new System.Drawing.Point ( 6, 66 );
			this.restoreIncludeRecovery.Name = "restoreIncludeRecovery";
			this.restoreIncludeRecovery.Size = new System.Drawing.Size ( 110, 17 );
			this.restoreIncludeRecovery.TabIndex = 3;
			this.restoreIncludeRecovery.Text = "Include Recovery";
			this.restoreIncludeRecovery.UseVisualStyleBackColor = true;
			// 
			// restoreIncludeSystem
			// 
			this.restoreIncludeSystem.AutoSize = true;
			this.restoreIncludeSystem.Checked = true;
			this.restoreIncludeSystem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.restoreIncludeSystem.Location = new System.Drawing.Point ( 6, 89 );
			this.restoreIncludeSystem.Name = "restoreIncludeSystem";
			this.restoreIncludeSystem.Size = new System.Drawing.Size ( 98, 17 );
			this.restoreIncludeSystem.TabIndex = 2;
			this.restoreIncludeSystem.Text = "Include System";
			this.restoreIncludeSystem.UseVisualStyleBackColor = true;
			// 
			// restoreVerbose
			// 
			this.restoreVerbose.AutoSize = true;
			this.restoreVerbose.Checked = true;
			this.restoreVerbose.CheckState = System.Windows.Forms.CheckState.Checked;
			this.restoreVerbose.Location = new System.Drawing.Point ( 6, 112 );
			this.restoreVerbose.Name = "restoreVerbose";
			this.restoreVerbose.Size = new System.Drawing.Size ( 65, 17 );
			this.restoreVerbose.TabIndex = 1;
			this.restoreVerbose.Text = "Verbose";
			this.restoreVerbose.UseVisualStyleBackColor = true;
			// 
			// restoreIncludeBoot
			// 
			this.restoreIncludeBoot.AutoSize = true;
			this.restoreIncludeBoot.Checked = true;
			this.restoreIncludeBoot.CheckState = System.Windows.Forms.CheckState.Checked;
			this.restoreIncludeBoot.Location = new System.Drawing.Point ( 6, 19 );
			this.restoreIncludeBoot.Name = "restoreIncludeBoot";
			this.restoreIncludeBoot.Size = new System.Drawing.Size ( 86, 17 );
			this.restoreIncludeBoot.TabIndex = 0;
			this.restoreIncludeBoot.Text = "Include Boot";
			this.restoreIncludeBoot.UseVisualStyleBackColor = true;
			// 
			// delete
			// 
			this.delete.Location = new System.Drawing.Point ( 182, 283 );
			this.delete.Name = "delete";
			this.delete.Size = new System.Drawing.Size ( 123, 23 );
			this.delete.TabIndex = 1;
			this.delete.Text = "Delete Backup";
			this.delete.UseVisualStyleBackColor = true;
			this.delete.Click += new System.EventHandler ( this.delete_Click );
			// 
			// backups
			// 
			this.backups.Dock = System.Windows.Forms.DockStyle.Left;
			this.backups.FormattingEnabled = true;
			this.backups.Location = new System.Drawing.Point ( 3, 3 );
			this.backups.Name = "backups";
			this.backups.Size = new System.Drawing.Size ( 173, 303 );
			this.backups.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add ( this.groupBox2 );
			this.tabPage2.Location = new System.Drawing.Point ( 4, 22 );
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding ( 3 );
			this.tabPage2.Size = new System.Drawing.Size ( 559, 313 );
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "New Backup";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.groupBox2.Controls.Add ( this.groupBox3 );
			this.groupBox2.Controls.Add ( this.compress );
			this.groupBox2.Controls.Add ( this.name );
			this.groupBox2.Controls.Add ( this.label1 );
			this.groupBox2.Controls.Add ( this.create );
			this.groupBox2.Controls.Add ( this.includeData );
			this.groupBox2.Controls.Add ( this.includeRecovery );
			this.groupBox2.Controls.Add ( this.includeSystem );
			this.groupBox2.Controls.Add ( this.verbose );
			this.groupBox2.Controls.Add ( this.includeBoot );
			this.groupBox2.Location = new System.Drawing.Point ( 6, 6 );
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size ( 547, 209 );
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Create Options";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add ( this.shutdown );
			this.groupBox3.Controls.Add ( this.reboot );
			this.groupBox3.Controls.Add ( this.nothing );
			this.groupBox3.Location = new System.Drawing.Point ( 258, 17 );
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size ( 200, 103 );
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "After Backup Complete";
			// 
			// shutdown
			// 
			this.shutdown.AutoSize = true;
			this.shutdown.Location = new System.Drawing.Point ( 7, 66 );
			this.shutdown.Name = "shutdown";
			this.shutdown.Size = new System.Drawing.Size ( 73, 17 );
			this.shutdown.TabIndex = 2;
			this.shutdown.Text = "Shutdown";
			this.shutdown.UseVisualStyleBackColor = true;
			// 
			// reboot
			// 
			this.reboot.AutoSize = true;
			this.reboot.Location = new System.Drawing.Point ( 7, 43 );
			this.reboot.Name = "reboot";
			this.reboot.Size = new System.Drawing.Size ( 60, 17 );
			this.reboot.TabIndex = 1;
			this.reboot.Text = "Reboot";
			this.reboot.UseVisualStyleBackColor = true;
			// 
			// nothing
			// 
			this.nothing.AutoSize = true;
			this.nothing.Checked = true;
			this.nothing.Location = new System.Drawing.Point ( 7, 20 );
			this.nothing.Name = "nothing";
			this.nothing.Size = new System.Drawing.Size ( 79, 17 );
			this.nothing.TabIndex = 0;
			this.nothing.TabStop = true;
			this.nothing.Text = "Do Nothing";
			this.nothing.UseVisualStyleBackColor = true;
			// 
			// compress
			// 
			this.compress.AutoSize = true;
			this.compress.Checked = true;
			this.compress.CheckState = System.Windows.Forms.CheckState.Checked;
			this.compress.Location = new System.Drawing.Point ( 156, 80 );
			this.compress.Name = "compress";
			this.compress.Size = new System.Drawing.Size ( 72, 17 );
			this.compress.TabIndex = 8;
			this.compress.Text = "Compress";
			this.compress.UseVisualStyleBackColor = true;
			// 
			// name
			// 
			this.name.Location = new System.Drawing.Point ( 50, 17 );
			this.name.Name = "name";
			this.name.Size = new System.Drawing.Size ( 171, 20 );
			this.name.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point ( 6, 20 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 38, 13 );
			this.label1.TabIndex = 6;
			this.label1.Text = "Name:";
			// 
			// create
			// 
			this.create.Location = new System.Drawing.Point ( 6, 180 );
			this.create.Name = "create";
			this.create.Size = new System.Drawing.Size ( 94, 23 );
			this.create.TabIndex = 5;
			this.create.Text = "&Create";
			this.create.UseVisualStyleBackColor = true;
			this.create.Click += new System.EventHandler ( this.create_Click );
			// 
			// includeData
			// 
			this.includeData.AutoSize = true;
			this.includeData.Checked = true;
			this.includeData.CheckState = System.Windows.Forms.CheckState.Checked;
			this.includeData.Location = new System.Drawing.Point ( 6, 80 );
			this.includeData.Name = "includeData";
			this.includeData.Size = new System.Drawing.Size ( 87, 17 );
			this.includeData.TabIndex = 4;
			this.includeData.Text = "Include Data";
			this.includeData.UseVisualStyleBackColor = true;
			// 
			// includeRecovery
			// 
			this.includeRecovery.AutoSize = true;
			this.includeRecovery.Location = new System.Drawing.Point ( 6, 103 );
			this.includeRecovery.Name = "includeRecovery";
			this.includeRecovery.Size = new System.Drawing.Size ( 110, 17 );
			this.includeRecovery.TabIndex = 3;
			this.includeRecovery.Text = "Include Recovery";
			this.includeRecovery.UseVisualStyleBackColor = true;
			// 
			// includeSystem
			// 
			this.includeSystem.AutoSize = true;
			this.includeSystem.Checked = true;
			this.includeSystem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.includeSystem.Location = new System.Drawing.Point ( 6, 126 );
			this.includeSystem.Name = "includeSystem";
			this.includeSystem.Size = new System.Drawing.Size ( 98, 17 );
			this.includeSystem.TabIndex = 2;
			this.includeSystem.Text = "Include System";
			this.includeSystem.UseVisualStyleBackColor = true;
			// 
			// verbose
			// 
			this.verbose.AutoSize = true;
			this.verbose.Checked = true;
			this.verbose.CheckState = System.Windows.Forms.CheckState.Checked;
			this.verbose.Location = new System.Drawing.Point ( 156, 56 );
			this.verbose.Name = "verbose";
			this.verbose.Size = new System.Drawing.Size ( 65, 17 );
			this.verbose.TabIndex = 1;
			this.verbose.Text = "Verbose";
			this.verbose.UseVisualStyleBackColor = true;
			// 
			// includeBoot
			// 
			this.includeBoot.AutoSize = true;
			this.includeBoot.Checked = true;
			this.includeBoot.CheckState = System.Windows.Forms.CheckState.Checked;
			this.includeBoot.Location = new System.Drawing.Point ( 6, 56 );
			this.includeBoot.Name = "includeBoot";
			this.includeBoot.Size = new System.Drawing.Size ( 86, 17 );
			this.includeBoot.TabIndex = 0;
			this.includeBoot.Text = "Include Boot";
			this.includeBoot.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add ( this.console );
			this.tabPage3.Location = new System.Drawing.Point ( 4, 22 );
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size ( 559, 313 );
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Console";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// console
			// 
			this.console.BackColor = System.Drawing.Color.Black;
			this.console.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.console.Dock = System.Windows.Forms.DockStyle.Fill;
			this.console.Font = new System.Drawing.Font ( "Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.console.Location = new System.Drawing.Point ( 0, 0 );
			this.console.Name = "console";
			this.console.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.console.Size = new System.Drawing.Size ( 559, 313 );
			this.console.TabIndex = 0;
			this.console.Text = "";
			// 
			// close
			// 
			this.close.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.close.Location = new System.Drawing.Point ( 480, 351 );
			this.close.Name = "close";
			this.close.Size = new System.Drawing.Size ( 75, 23 );
			this.close.TabIndex = 1;
			this.close.Text = "&Close";
			this.close.UseVisualStyleBackColor = true;
			this.close.Click += new System.EventHandler ( this.close_Click );
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// BartForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size ( 571, 386 );
			this.Controls.Add ( this.close );
			this.Controls.Add ( this.tabs );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject ( "$this.Icon" ) ) );
			this.MaximizeBox = false;
			this.Name = "BartForm";
			this.Text = "Bart Manager";
			this.tabs.ResumeLayout ( false );
			this.tabPage1.ResumeLayout ( false );
			this.groupBox1.ResumeLayout ( false );
			this.groupBox1.PerformLayout ( );
			this.groupBox4.ResumeLayout ( false );
			this.groupBox4.PerformLayout ( );
			this.tabPage2.ResumeLayout ( false );
			this.groupBox2.ResumeLayout ( false );
			this.groupBox2.PerformLayout ( );
			this.groupBox3.ResumeLayout ( false );
			this.groupBox3.PerformLayout ( );
			this.tabPage3.ResumeLayout ( false );
			( (System.ComponentModel.ISupportInitialize)( this.errorProvider ) ).EndInit ( );
			this.ResumeLayout ( false );

		}

		#endregion

		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListBox backups;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button close;
		private System.Windows.Forms.Button delete;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox restoreIncludeData;
		private System.Windows.Forms.CheckBox restoreIncludeRecovery;
		private System.Windows.Forms.CheckBox restoreIncludeSystem;
		private System.Windows.Forms.CheckBox restoreVerbose;
		private System.Windows.Forms.CheckBox restoreIncludeBoot;
		private System.Windows.Forms.Button restore;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.RichTextBox console;
		private System.Windows.Forms.Button gotoCreate;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox compress;
		private System.Windows.Forms.TextBox name;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button create;
		private System.Windows.Forms.CheckBox includeData;
		private System.Windows.Forms.CheckBox includeRecovery;
		private System.Windows.Forms.CheckBox includeSystem;
		private System.Windows.Forms.CheckBox verbose;
		private System.Windows.Forms.CheckBox includeBoot;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton shutdown;
		private System.Windows.Forms.RadioButton reboot;
		private System.Windows.Forms.RadioButton nothing;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RadioButton restoreShutdown;
		private System.Windows.Forms.RadioButton restoreReboot;
		private System.Windows.Forms.RadioButton restoreNothing;
		private System.Windows.Forms.ErrorProvider errorProvider;
	}
}