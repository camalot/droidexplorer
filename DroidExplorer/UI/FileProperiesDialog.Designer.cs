namespace DroidExplorer.UI {
	partial class FileProperiesDialog {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( FileProperiesDialog ) );
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem ( "User" );
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem ( "Group" );
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem ( "Other" );
			this.tabControl1 = new System.Windows.Forms.TabControl ( );
			this.tabPage1 = new System.Windows.Forms.TabPage ( );
			this.panel2 = new System.Windows.Forms.Panel ( );
			this.isInstalled = new System.Windows.Forms.CheckBox ( );
			this.isSocket = new System.Windows.Forms.CheckBox ( );
			this.isPipe = new System.Windows.Forms.CheckBox ( );
			this.label7 = new System.Windows.Forms.Label ( );
			this.isLink = new System.Windows.Forms.CheckBox ( );
			this.isExecutable = new System.Windows.Forms.CheckBox ( );
			this.panel1 = new System.Windows.Forms.Panel ( );
			this.modifiedLabel = new System.Windows.Forms.Label ( );
			this.label6 = new System.Windows.Forms.Label ( );
			this.diskInfoPanel = new System.Windows.Forms.Panel ( );
			this.filePathLabel = new System.Windows.Forms.TextBox ( );
			this.fileSizeLabel = new System.Windows.Forms.Label ( );
			this.label4 = new System.Windows.Forms.Label ( );
			this.label3 = new System.Windows.Forms.Label ( );
			this.openInfoPanel = new System.Windows.Forms.Panel ( );
			this.fileTypeLabel = new System.Windows.Forms.Label ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.iconFileNamePanel = new System.Windows.Forms.Panel ( );
			this.filename = new System.Windows.Forms.TextBox ( );
			this.fileIcon = new System.Windows.Forms.PictureBox ( );
			this.tabPage2 = new System.Windows.Forms.TabPage ( );
			this.label8 = new System.Windows.Forms.Label ( );
			this.label5 = new System.Windows.Forms.Label ( );
			this.label2 = new System.Windows.Forms.Label ( );
			this.panel3 = new System.Windows.Forms.Panel ( );
			this.canExecute = new System.Windows.Forms.CheckBox ( );
			this.canWrite = new System.Windows.Forms.CheckBox ( );
			this.canRead = new System.Windows.Forms.CheckBox ( );
			this.label12 = new System.Windows.Forms.Label ( );
			this.label11 = new System.Windows.Forms.Label ( );
			this.label10 = new System.Windows.Forms.Label ( );
			this.permissionTypes = new DroidExplorer.Core.UI.ListViewEx ( );
			this.ok = new System.Windows.Forms.Button ( );
			this.cancel = new System.Windows.Forms.Button ( );
			this.apply = new System.Windows.Forms.Button ( );
			this.imageList1 = new System.Windows.Forms.ImageList ( this.components );
			this.isReadOnly = new System.Windows.Forms.CheckBox ( );
			this.tabControl1.SuspendLayout ( );
			this.tabPage1.SuspendLayout ( );
			this.panel2.SuspendLayout ( );
			this.panel1.SuspendLayout ( );
			this.diskInfoPanel.SuspendLayout ( );
			this.openInfoPanel.SuspendLayout ( );
			this.iconFileNamePanel.SuspendLayout ( );
			( (System.ComponentModel.ISupportInitialize)( this.fileIcon ) ).BeginInit ( );
			this.tabPage2.SuspendLayout ( );
			this.panel3.SuspendLayout ( );
			this.SuspendLayout ( );
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
									| System.Windows.Forms.AnchorStyles.Left )
									| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.tabControl1.Controls.Add ( this.tabPage1 );
			this.tabControl1.Controls.Add ( this.tabPage2 );
			this.tabControl1.Location = new System.Drawing.Point ( 12, 8 );
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size ( 340, 352 );
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add ( this.panel2 );
			this.tabPage1.Controls.Add ( this.panel1 );
			this.tabPage1.Controls.Add ( this.diskInfoPanel );
			this.tabPage1.Controls.Add ( this.openInfoPanel );
			this.tabPage1.Controls.Add ( this.iconFileNamePanel );
			this.tabPage1.Location = new System.Drawing.Point ( 4, 22 );
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding ( 3 );
			this.tabPage1.Size = new System.Drawing.Size ( 332, 326 );
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.Controls.Add ( this.isReadOnly );
			this.panel2.Controls.Add ( this.isInstalled );
			this.panel2.Controls.Add ( this.isSocket );
			this.panel2.Controls.Add ( this.isPipe );
			this.panel2.Controls.Add ( this.label7 );
			this.panel2.Controls.Add ( this.isLink );
			this.panel2.Controls.Add ( this.isExecutable );
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point ( 3, 218 );
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size ( 326, 105 );
			this.panel2.TabIndex = 4;
			// 
			// isInstalled
			// 
			this.isInstalled.AutoSize = true;
			this.isInstalled.Enabled = false;
			this.isInstalled.Location = new System.Drawing.Point ( 83, 65 );
			this.isInstalled.Name = "isInstalled";
			this.isInstalled.Size = new System.Drawing.Size ( 65, 17 );
			this.isInstalled.TabIndex = 6;
			this.isInstalled.Text = "Installed";
			this.isInstalled.UseVisualStyleBackColor = true;
			// 
			// isSocket
			// 
			this.isSocket.AutoSize = true;
			this.isSocket.Enabled = false;
			this.isSocket.Location = new System.Drawing.Point ( 206, 41 );
			this.isSocket.Name = "isSocket";
			this.isSocket.Size = new System.Drawing.Size ( 60, 17 );
			this.isSocket.TabIndex = 5;
			this.isSocket.Text = "Socket";
			this.isSocket.UseVisualStyleBackColor = true;
			// 
			// isPipe
			// 
			this.isPipe.AutoSize = true;
			this.isPipe.Enabled = false;
			this.isPipe.Location = new System.Drawing.Point ( 83, 41 );
			this.isPipe.Name = "isPipe";
			this.isPipe.Size = new System.Drawing.Size ( 47, 17 );
			this.isPipe.TabIndex = 4;
			this.isPipe.Text = "Pipe";
			this.isPipe.UseVisualStyleBackColor = true;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point ( 8, 19 );
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size ( 54, 13 );
			this.label7.TabIndex = 3;
			this.label7.Text = "Attributes:";
			// 
			// isLink
			// 
			this.isLink.AutoSize = true;
			this.isLink.Enabled = false;
			this.isLink.Location = new System.Drawing.Point ( 206, 18 );
			this.isLink.Name = "isLink";
			this.isLink.Size = new System.Drawing.Size ( 46, 17 );
			this.isLink.TabIndex = 1;
			this.isLink.Text = "Link";
			this.isLink.UseVisualStyleBackColor = true;
			// 
			// isExecutable
			// 
			this.isExecutable.AutoSize = true;
			this.isExecutable.Location = new System.Drawing.Point ( 83, 18 );
			this.isExecutable.Name = "isExecutable";
			this.isExecutable.Size = new System.Drawing.Size ( 79, 17 );
			this.isExecutable.TabIndex = 0;
			this.isExecutable.Text = "Executable";
			this.isExecutable.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add ( this.modifiedLabel );
			this.panel1.Controls.Add ( this.label6 );
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point ( 3, 176 );
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size ( 326, 42 );
			this.panel1.TabIndex = 3;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler ( this.panel1_Paint );
			// 
			// modifiedLabel
			// 
			this.modifiedLabel.AutoEllipsis = true;
			this.modifiedLabel.Location = new System.Drawing.Point ( 80, 12 );
			this.modifiedLabel.Name = "modifiedLabel";
			this.modifiedLabel.Size = new System.Drawing.Size ( 236, 18 );
			this.modifiedLabel.TabIndex = 5;
			this.modifiedLabel.Text = "Monday, November 09, 2009, 5:50:16 AM";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point ( 9, 12 );
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size ( 50, 13 );
			this.label6.TabIndex = 2;
			this.label6.Text = "Modified:";
			// 
			// diskInfoPanel
			// 
			this.diskInfoPanel.Controls.Add ( this.filePathLabel );
			this.diskInfoPanel.Controls.Add ( this.fileSizeLabel );
			this.diskInfoPanel.Controls.Add ( this.label4 );
			this.diskInfoPanel.Controls.Add ( this.label3 );
			this.diskInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.diskInfoPanel.Location = new System.Drawing.Point ( 3, 106 );
			this.diskInfoPanel.Name = "diskInfoPanel";
			this.diskInfoPanel.Size = new System.Drawing.Size ( 326, 70 );
			this.diskInfoPanel.TabIndex = 2;
			this.diskInfoPanel.Paint += new System.Windows.Forms.PaintEventHandler ( this.diskInfoPanel_Paint );
			// 
			// filePathLabel
			// 
			this.filePathLabel.BackColor = System.Drawing.SystemColors.Window;
			this.filePathLabel.Location = new System.Drawing.Point ( 83, 9 );
			this.filePathLabel.Name = "filePathLabel";
			this.filePathLabel.ReadOnly = true;
			this.filePathLabel.Size = new System.Drawing.Size ( 233, 20 );
			this.filePathLabel.TabIndex = 5;
			// 
			// fileSizeLabel
			// 
			this.fileSizeLabel.AutoEllipsis = true;
			this.fileSizeLabel.Location = new System.Drawing.Point ( 80, 43 );
			this.fileSizeLabel.Name = "fileSizeLabel";
			this.fileSizeLabel.Size = new System.Drawing.Size ( 236, 18 );
			this.fileSizeLabel.TabIndex = 4;
			this.fileSizeLabel.Text = "0.00 KB (0 bytes)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point ( 9, 43 );
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size ( 30, 13 );
			this.label4.TabIndex = 2;
			this.label4.Text = "Size:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point ( 9, 12 );
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size ( 51, 13 );
			this.label3.TabIndex = 1;
			this.label3.Text = "Location:";
			// 
			// openInfoPanel
			// 
			this.openInfoPanel.Controls.Add ( this.fileTypeLabel );
			this.openInfoPanel.Controls.Add ( this.label1 );
			this.openInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.openInfoPanel.Location = new System.Drawing.Point ( 3, 69 );
			this.openInfoPanel.Name = "openInfoPanel";
			this.openInfoPanel.Size = new System.Drawing.Size ( 326, 37 );
			this.openInfoPanel.TabIndex = 1;
			this.openInfoPanel.Paint += new System.Windows.Forms.PaintEventHandler ( this.openInfoPanel_Paint );
			// 
			// fileTypeLabel
			// 
			this.fileTypeLabel.AutoEllipsis = true;
			this.fileTypeLabel.Location = new System.Drawing.Point ( 83, 10 );
			this.fileTypeLabel.Name = "fileTypeLabel";
			this.fileTypeLabel.Size = new System.Drawing.Size ( 233, 18 );
			this.fileTypeLabel.TabIndex = 2;
			this.fileTypeLabel.Text = "File Type or Content Type";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point ( 8, 10 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 62, 13 );
			this.label1.TabIndex = 0;
			this.label1.Text = "Type of file:";
			// 
			// iconFileNamePanel
			// 
			this.iconFileNamePanel.Controls.Add ( this.filename );
			this.iconFileNamePanel.Controls.Add ( this.fileIcon );
			this.iconFileNamePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.iconFileNamePanel.Location = new System.Drawing.Point ( 3, 3 );
			this.iconFileNamePanel.Name = "iconFileNamePanel";
			this.iconFileNamePanel.Size = new System.Drawing.Size ( 326, 66 );
			this.iconFileNamePanel.TabIndex = 0;
			this.iconFileNamePanel.Paint += new System.Windows.Forms.PaintEventHandler ( this.iconFileNamePanel_Paint );
			// 
			// filename
			// 
			this.filename.BackColor = System.Drawing.SystemColors.Window;
			this.filename.Location = new System.Drawing.Point ( 83, 22 );
			this.filename.Name = "filename";
			this.filename.ReadOnly = true;
			this.filename.Size = new System.Drawing.Size ( 233, 20 );
			this.filename.TabIndex = 1;
			this.filename.Text = "file.ext";
			// 
			// fileIcon
			// 
			this.fileIcon.Location = new System.Drawing.Point ( 8, 6 );
			this.fileIcon.Name = "fileIcon";
			this.fileIcon.Size = new System.Drawing.Size ( 48, 48 );
			this.fileIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.fileIcon.TabIndex = 0;
			this.fileIcon.TabStop = false;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add ( this.label8 );
			this.tabPage2.Controls.Add ( this.label5 );
			this.tabPage2.Controls.Add ( this.label2 );
			this.tabPage2.Controls.Add ( this.panel3 );
			this.tabPage2.Controls.Add ( this.permissionTypes );
			this.tabPage2.Location = new System.Drawing.Point ( 4, 22 );
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding ( 3 );
			this.tabPage2.Size = new System.Drawing.Size ( 332, 326 );
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Security";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point ( 262, 161 );
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size ( 32, 13 );
			this.label8.TabIndex = 4;
			this.label8.Text = "Allow";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point ( 18, 161 );
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size ( 62, 13 );
			this.label5.TabIndex = 3;
			this.label5.Text = "Permissions";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point ( 15, 13 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size ( 39, 13 );
			this.label2.TabIndex = 2;
			this.label2.Text = "Types:";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.Window;
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel3.Controls.Add ( this.canExecute );
			this.panel3.Controls.Add ( this.canWrite );
			this.panel3.Controls.Add ( this.canRead );
			this.panel3.Controls.Add ( this.label12 );
			this.panel3.Controls.Add ( this.label11 );
			this.panel3.Controls.Add ( this.label10 );
			this.panel3.Location = new System.Drawing.Point ( 18, 180 );
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size ( 293, 99 );
			this.panel3.TabIndex = 1;
			// 
			// canExecute
			// 
			this.canExecute.AutoSize = true;
			this.canExecute.Location = new System.Drawing.Point ( 250, 47 );
			this.canExecute.Name = "canExecute";
			this.canExecute.Size = new System.Drawing.Size ( 15, 14 );
			this.canExecute.TabIndex = 11;
			this.canExecute.UseVisualStyleBackColor = true;
			// 
			// canWrite
			// 
			this.canWrite.AutoSize = true;
			this.canWrite.Location = new System.Drawing.Point ( 250, 27 );
			this.canWrite.Name = "canWrite";
			this.canWrite.Size = new System.Drawing.Size ( 15, 14 );
			this.canWrite.TabIndex = 9;
			this.canWrite.UseVisualStyleBackColor = true;
			// 
			// canRead
			// 
			this.canRead.AutoSize = true;
			this.canRead.Location = new System.Drawing.Point ( 250, 8 );
			this.canRead.Name = "canRead";
			this.canRead.Size = new System.Drawing.Size ( 15, 14 );
			this.canRead.TabIndex = 7;
			this.canRead.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point ( 9, 47 );
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size ( 46, 13 );
			this.label12.TabIndex = 6;
			this.label12.Text = "Execute";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point ( 9, 27 );
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size ( 32, 13 );
			this.label11.TabIndex = 5;
			this.label11.Text = "Write";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point ( 8, 7 );
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size ( 33, 13 );
			this.label10.TabIndex = 0;
			this.label10.Text = "Read";
			// 
			// permissionTypes
			// 
			this.permissionTypes.ColumnsOrder = ( (System.Collections.Generic.Dictionary<string, int>)( resources.GetObject ( "permissionTypes.ColumnsOrder" ) ) );
			this.permissionTypes.HideSelection = false;
			this.permissionTypes.Items.AddRange ( new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3} );
			this.permissionTypes.Location = new System.Drawing.Point ( 18, 29 );
			this.permissionTypes.MultiSelect = false;
			this.permissionTypes.Name = "permissionTypes";
			this.permissionTypes.SelectedSortColumn = -1;
			this.permissionTypes.Size = new System.Drawing.Size ( 293, 103 );
			this.permissionTypes.SortStyle = DroidExplorer.Core.UI.ListViewEx.SortStyles.SortDefault;
			this.permissionTypes.TabIndex = 0;
			this.permissionTypes.UseCompatibleStateImageBehavior = false;
			this.permissionTypes.View = System.Windows.Forms.View.List;
			this.permissionTypes.WatermarkImage = null;
			this.permissionTypes.SelectedIndexChanged += new System.EventHandler ( this.permissionTypes_SelectedIndexChanged );
			// 
			// ok
			// 
			this.ok.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.ok.Location = new System.Drawing.Point ( 115, 366 );
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size ( 75, 23 );
			this.ok.TabIndex = 0;
			this.ok.Text = "&OK";
			this.ok.UseVisualStyleBackColor = true;
			// 
			// cancel
			// 
			this.cancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point ( 196, 366 );
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size ( 75, 23 );
			this.cancel.TabIndex = 1;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			// 
			// apply
			// 
			this.apply.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.apply.Enabled = false;
			this.apply.Location = new System.Drawing.Point ( 277, 366 );
			this.apply.Name = "apply";
			this.apply.Size = new System.Drawing.Size ( 75, 23 );
			this.apply.TabIndex = 2;
			this.apply.Text = "&Apply";
			this.apply.UseVisualStyleBackColor = true;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size ( 16, 16 );
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// isReadOnly
			// 
			this.isReadOnly.AutoSize = true;
			this.isReadOnly.Enabled = false;
			this.isReadOnly.Location = new System.Drawing.Point ( 206, 65 );
			this.isReadOnly.Name = "isReadOnly";
			this.isReadOnly.Size = new System.Drawing.Size ( 71, 17 );
			this.isReadOnly.TabIndex = 7;
			this.isReadOnly.Text = "Readonly";
			this.isReadOnly.UseVisualStyleBackColor = true;
			// 
			// FileProperiesDialog
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size ( 362, 397 );
			this.Controls.Add ( this.apply );
			this.Controls.Add ( this.cancel );
			this.Controls.Add ( this.ok );
			this.Controls.Add ( this.tabControl1 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FileProperiesDialog";
			this.ShowInTaskbar = false;
			this.Text = "Properties";
			this.tabControl1.ResumeLayout ( false );
			this.tabPage1.ResumeLayout ( false );
			this.panel2.ResumeLayout ( false );
			this.panel2.PerformLayout ( );
			this.panel1.ResumeLayout ( false );
			this.panel1.PerformLayout ( );
			this.diskInfoPanel.ResumeLayout ( false );
			this.diskInfoPanel.PerformLayout ( );
			this.openInfoPanel.ResumeLayout ( false );
			this.openInfoPanel.PerformLayout ( );
			this.iconFileNamePanel.ResumeLayout ( false );
			this.iconFileNamePanel.PerformLayout ( );
			( (System.ComponentModel.ISupportInitialize)( this.fileIcon ) ).EndInit ( );
			this.tabPage2.ResumeLayout ( false );
			this.tabPage2.PerformLayout ( );
			this.panel3.ResumeLayout ( false );
			this.panel3.PerformLayout ( );
			this.ResumeLayout ( false );

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Button apply;
		private System.Windows.Forms.Panel iconFileNamePanel;
		private System.Windows.Forms.TextBox filename;
		private System.Windows.Forms.PictureBox fileIcon;
		private System.Windows.Forms.Panel openInfoPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label fileTypeLabel;
		private System.Windows.Forms.Panel diskInfoPanel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label fileSizeLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label modifiedLabel;
		private System.Windows.Forms.CheckBox isExecutable;
		private System.Windows.Forms.CheckBox isSocket;
		private System.Windows.Forms.CheckBox isPipe;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox isLink;
		private System.Windows.Forms.CheckBox isInstalled;
		private System.Windows.Forms.TextBox filePathLabel;
		private System.Windows.Forms.Panel panel3;
		private DroidExplorer.Core.UI.ListViewEx permissionTypes;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox canRead;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.CheckBox canExecute;
		private System.Windows.Forms.CheckBox canWrite;
		private System.Windows.Forms.CheckBox isReadOnly;
	}
}