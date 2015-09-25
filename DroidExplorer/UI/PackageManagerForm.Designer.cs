namespace DroidExplorer.UI {
  partial class PackageManagerForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( PackageManagerForm ) );
			this.packagesList = new DroidExplorer.Core.UI.ListViewEx ();
			this.chName = new System.Windows.Forms.ColumnHeader ();
			this.chPackage = new System.Windows.Forms.ColumnHeader ();
			this.chPath = new System.Windows.Forms.ColumnHeader ();
			this.chVersion = new System.Windows.Forms.ColumnHeader ();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip ();
			this.installToolStripButton = new System.Windows.Forms.ToolStripButton ();
			this.uninstallToolStripButton = new System.Windows.Forms.ToolStripButton ();
			this.loadingPanel = new System.Windows.Forms.Panel ();
			this.label1 = new System.Windows.Forms.Label ();
			this.pictureBox1 = new System.Windows.Forms.PictureBox ();
			this.toolStrip1.SuspendLayout ();
			this.loadingPanel.SuspendLayout ();
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit ();
			this.SuspendLayout ();
			// 
			// packagesList
			// 
			this.packagesList.Columns.AddRange ( new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chPackage,
            this.chPath,
            this.chVersion} );
			this.packagesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.packagesList.FullRowSelect = true;
			this.packagesList.Location = new System.Drawing.Point ( 0, 25 );
			this.packagesList.MultiSelect = false;
			this.packagesList.Name = "packagesList";
			this.packagesList.Size = new System.Drawing.Size ( 659, 428 );
			this.packagesList.TabIndex = 0;
			this.packagesList.UseCompatibleStateImageBehavior = false;
			this.packagesList.View = System.Windows.Forms.View.Details;
			// 
			// chName
			// 
			this.chName.Text = "Name";
			this.chName.Width = 199;
			// 
			// chPackage
			// 
			this.chPackage.Text = "Package";
			this.chPackage.Width = 158;
			// 
			// chPath
			// 
			this.chPath.Text = "Path";
			this.chPath.Width = 162;
			// 
			// chVersion
			// 
			this.chVersion.Text = "Version";
			this.chVersion.Width = 127;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.installToolStripButton,
            this.uninstallToolStripButton} );
			this.toolStrip1.Location = new System.Drawing.Point ( 0, 0 );
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size ( 659, 25 );
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// installToolStripButton
			// 
			this.installToolStripButton.Image = global::DroidExplorer.Resources.Images.install;
			this.installToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.installToolStripButton.Name = "installToolStripButton";
			this.installToolStripButton.Size = new System.Drawing.Size ( 58, 22 );
			this.installToolStripButton.Text = "Install";
			this.installToolStripButton.Click += new System.EventHandler ( this.installToolStripButton_Click );
			// 
			// uninstallToolStripButton
			// 
			this.uninstallToolStripButton.Enabled = false;
			this.uninstallToolStripButton.Image = global::DroidExplorer.Resources.Images.Symbols_Critical_16xLG;
			this.uninstallToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uninstallToolStripButton.Name = "uninstallToolStripButton";
			this.uninstallToolStripButton.Size = new System.Drawing.Size ( 73, 22 );
			this.uninstallToolStripButton.Text = "Uninstall";
			this.uninstallToolStripButton.Click += new System.EventHandler ( this.uninstallToolStripButton_Click );
			// 
			// loadingPanel
			// 
			this.loadingPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.loadingPanel.Controls.Add ( this.label1 );
			this.loadingPanel.Controls.Add ( this.pictureBox1 );
			this.loadingPanel.Location = new System.Drawing.Point ( 176, 192 );
			this.loadingPanel.Name = "loadingPanel";
			this.loadingPanel.Size = new System.Drawing.Size ( 298, 57 );
			this.loadingPanel.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
			this.label1.Location = new System.Drawing.Point ( 121, 13 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 134, 31 );
			this.label1.TabIndex = 1;
			this.label1.Text = "Loading...";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DroidExplorer.Resources.Images.loading;
			this.pictureBox1.Location = new System.Drawing.Point ( 53, 3 );
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size ( 50, 50 );
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// PackageManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size ( 659, 453 );
			this.Controls.Add ( this.loadingPanel );
			this.Controls.Add ( this.packagesList );
			this.Controls.Add ( this.toolStrip1 );
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject ( "$this.Icon" ) ) );
			this.Name = "PackageManagerForm";
			this.Text = "Package Manager";
			this.toolStrip1.ResumeLayout ( false );
			this.toolStrip1.PerformLayout ();
			this.loadingPanel.ResumeLayout ( false );
			this.loadingPanel.PerformLayout ();
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit ();
			this.ResumeLayout ( false );
			this.PerformLayout ();

    }

    #endregion

    private DroidExplorer.Core.UI.ListViewEx packagesList;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ColumnHeader chName;
    private System.Windows.Forms.ColumnHeader chPackage;
    private System.Windows.Forms.ColumnHeader chPath;
    private System.Windows.Forms.ColumnHeader chVersion;
    private System.Windows.Forms.Panel loadingPanel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.ToolStripButton installToolStripButton;
    private System.Windows.Forms.ToolStripButton uninstallToolStripButton;
  }
}