namespace DroidExplorer.UI {
	partial class OptionsForm {
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
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent () {
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode ( "General" );
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode ( "Android SDK" );
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode ( "Devices" );
      System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode ( "Environment", new System.Windows.Forms.TreeNode[ ] {
            treeNode1,
            treeNode2,
            treeNode3} );
      System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode ( "Plugins" );
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( OptionsForm ) );
      this.categories = new System.Windows.Forms.TreeView ( );
      this.bottomPanel = new System.Windows.Forms.Panel ( );
      this.cancel = new System.Windows.Forms.Button ( );
      this.ok = new System.Windows.Forms.Button ( );
      this.panel2 = new System.Windows.Forms.Panel ( );
      this.contentPanel = new System.Windows.Forms.Panel ( );
      this.bottomPanel.SuspendLayout ( );
      this.panel2.SuspendLayout ( );
      this.SuspendLayout ( );
      // 
      // categories
      // 
      this.categories.Dock = System.Windows.Forms.DockStyle.Fill;
      this.categories.Location = new System.Drawing.Point ( 5, 5 );
      this.categories.Name = "categories";
      treeNode1.Name = "generalNode";
      treeNode1.Text = "General";
      treeNode2.Name = "sdkNode";
      treeNode2.Text = "Android SDK";
      treeNode3.Name = "devicesNode";
      treeNode3.Text = "Devices";
      treeNode4.Name = "environmentNode";
      treeNode4.Text = "Environment";
      treeNode5.Name = "pluginsNode";
      treeNode5.Text = "Plugins";
      this.categories.Nodes.AddRange ( new System.Windows.Forms.TreeNode[ ] {
            treeNode4,
            treeNode5} );
      this.categories.Size = new System.Drawing.Size ( 206, 292 );
      this.categories.TabIndex = 0;
      // 
      // bottomPanel
      // 
      this.bottomPanel.Controls.Add ( this.cancel );
      this.bottomPanel.Controls.Add ( this.ok );
      this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.bottomPanel.Location = new System.Drawing.Point ( 0, 297 );
      this.bottomPanel.Name = "bottomPanel";
      this.bottomPanel.Size = new System.Drawing.Size ( 635, 50 );
      this.bottomPanel.TabIndex = 1;
      this.bottomPanel.Paint += new System.Windows.Forms.PaintEventHandler ( this.bottomPanel_Paint );
      // 
      // cancel
      // 
      this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancel.Location = new System.Drawing.Point ( 548, 15 );
      this.cancel.Name = "cancel";
      this.cancel.Size = new System.Drawing.Size ( 75, 23 );
      this.cancel.TabIndex = 1;
      this.cancel.Text = "&Cancel";
      this.cancel.UseVisualStyleBackColor = true;
      // 
      // ok
      // 
      this.ok.Location = new System.Drawing.Point ( 467, 15 );
      this.ok.Name = "ok";
      this.ok.Size = new System.Drawing.Size ( 75, 23 );
      this.ok.TabIndex = 0;
      this.ok.Text = "&OK";
      this.ok.UseVisualStyleBackColor = true;
      this.ok.Click += new System.EventHandler ( this.ok_Click );
      // 
      // panel2
      // 
      this.panel2.Controls.Add ( this.categories );
      this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel2.Location = new System.Drawing.Point ( 0, 0 );
      this.panel2.Name = "panel2";
      this.panel2.Padding = new System.Windows.Forms.Padding ( 5, 5, 5, 0 );
      this.panel2.Size = new System.Drawing.Size ( 216, 297 );
      this.panel2.TabIndex = 2;
      // 
      // contentPanel
      // 
      this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.contentPanel.Location = new System.Drawing.Point ( 216, 0 );
      this.contentPanel.Name = "contentPanel";
      this.contentPanel.Padding = new System.Windows.Forms.Padding ( 10, 5, 10, 5 );
      this.contentPanel.Size = new System.Drawing.Size ( 419, 297 );
      this.contentPanel.TabIndex = 3;
      // 
      // OptionsForm
      // 
      this.AcceptButton = this.ok;
      this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancel;
      this.ClientSize = new System.Drawing.Size ( 635, 347 );
      this.Controls.Add ( this.contentPanel );
      this.Controls.Add ( this.panel2 );
      this.Controls.Add ( this.bottomPanel );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ( ( System.Drawing.Icon )( resources.GetObject ( "$this.Icon" ) ) );
      this.MaximizeBox = false;
      this.Name = "OptionsForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Options";
      this.bottomPanel.ResumeLayout ( false );
      this.panel2.ResumeLayout ( false );
      this.ResumeLayout ( false );

		}

		#endregion

    private System.Windows.Forms.TreeView categories;
    private System.Windows.Forms.Panel bottomPanel;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel contentPanel;
    private System.Windows.Forms.Button cancel;
    private System.Windows.Forms.Button ok;

  }
}