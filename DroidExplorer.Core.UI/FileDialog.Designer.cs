namespace DroidExplorer.Core.UI {
  partial class FileDialog {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( FileDialog ) );
      this.panel1 = new System.Windows.Forms.Panel ( );
      this.toolStrip1 = new System.Windows.Forms.ToolStrip ( );
      this.backToolStripButton = new System.Windows.Forms.ToolStripButton ( );
      this.parentToolStripButton = new System.Windows.Forms.ToolStripButton ( );
      this.newFolderToolStripButton = new System.Windows.Forms.ToolStripButton ( );
      this.viewModeToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton ( );
      this.iconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
      this.largeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
      this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
      this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
      this.selectedPath = new DroidExplorer.Core.UI.ComboBoxEx ( );
      this.label1 = new System.Windows.Forms.Label ( );
      this.panel2 = new System.Windows.Forms.Panel ( );
      this.fileTypes = new System.Windows.Forms.ComboBox ( );
      this.fileName = new System.Windows.Forms.TextBox ( );
      this.label3 = new System.Windows.Forms.Label ( );
      this.label2 = new System.Windows.Forms.Label ( );
      this.ok = new System.Windows.Forms.Button ( );
      this.cancel = new System.Windows.Forms.Button ( );
      this.pathTree = new DroidExplorer.Core.UI.TreeViewComboBox ( );
      this.panel3 = new System.Windows.Forms.Panel ( );
      this.files = new DroidExplorer.Core.UI.FileSystemListView ( );
      this.panel4 = new System.Windows.Forms.Panel ( );
      this.customPlacesPanel = new DroidExplorer.Core.UI.CustomPlacesPanel ( );
      this.panel1.SuspendLayout ( );
      this.toolStrip1.SuspendLayout ( );
      this.panel2.SuspendLayout ( );
      this.panel3.SuspendLayout ( );
      this.panel4.SuspendLayout ( );
      this.SuspendLayout ( );
      // 
      // panel1
      // 
      this.panel1.Controls.Add ( this.toolStrip1 );
      this.panel1.Controls.Add ( this.selectedPath );
      this.panel1.Controls.Add ( this.label1 );
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point ( 0, 0 );
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size ( 654, 33 );
      this.panel1.TabIndex = 0;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[ ] {
            this.backToolStripButton,
            this.parentToolStripButton,
            this.newFolderToolStripButton,
            this.viewModeToolStripDropDownButton} );
      this.toolStrip1.Location = new System.Drawing.Point ( 463, 5 );
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.toolStrip1.Size = new System.Drawing.Size ( 101, 25 );
      this.toolStrip1.Stretch = true;
      this.toolStrip1.TabIndex = 5;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // backToolStripButton
      // 
      this.backToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.backToolStripButton.Enabled = false;
			this.backToolStripButton.Image = global::DroidExplorer.Core.UI.Properties.Resources.generic;
      this.backToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.backToolStripButton.Name = "backToolStripButton";
      this.backToolStripButton.Size = new System.Drawing.Size ( 23, 22 );
      this.backToolStripButton.Text = "Back";
      // 
      // parentToolStripButton
      // 
      this.parentToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.parentToolStripButton.Enabled = false;
			this.parentToolStripButton.Image = global::DroidExplorer.Core.UI.Properties.Resources.generic;
      this.parentToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.parentToolStripButton.Name = "parentToolStripButton";
      this.parentToolStripButton.Size = new System.Drawing.Size ( 23, 22 );
      this.parentToolStripButton.Text = "Go To Parent";
      this.parentToolStripButton.Click += new System.EventHandler ( this.parentToolStripButton_Click );
      // 
      // newFolderToolStripButton
      // 
      this.newFolderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.newFolderToolStripButton.Enabled = false;
			this.newFolderToolStripButton.Image = global::DroidExplorer.Core.UI.Properties.Resources.generic;
      this.newFolderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.newFolderToolStripButton.Name = "newFolderToolStripButton";
      this.newFolderToolStripButton.Size = new System.Drawing.Size ( 23, 22 );
      this.newFolderToolStripButton.Text = "New Folder";
      // 
      // viewModeToolStripDropDownButton
      // 
      this.viewModeToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.viewModeToolStripDropDownButton.DropDownItems.AddRange ( new System.Windows.Forms.ToolStripItem[ ] {
            this.iconsToolStripMenuItem,
            this.largeIconsToolStripMenuItem,
            this.listToolStripMenuItem,
            this.detailsToolStripMenuItem} );
      this.viewModeToolStripDropDownButton.Image = global::DroidExplorer.Core.UI.Properties.Resources.generic;
      this.viewModeToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.viewModeToolStripDropDownButton.Name = "viewModeToolStripDropDownButton";
      this.viewModeToolStripDropDownButton.Size = new System.Drawing.Size ( 29, 22 );
      this.viewModeToolStripDropDownButton.Text = "Views";
      // 
      // iconsToolStripMenuItem
      // 
      this.iconsToolStripMenuItem.Name = "iconsToolStripMenuItem";
      this.iconsToolStripMenuItem.Size = new System.Drawing.Size ( 141, 22 );
      this.iconsToolStripMenuItem.Text = "Small &Icons";
      this.iconsToolStripMenuItem.Click += new System.EventHandler ( this.iconsToolStripMenuItem_Click );
      // 
      // largeIconsToolStripMenuItem
      // 
      this.largeIconsToolStripMenuItem.Name = "largeIconsToolStripMenuItem";
      this.largeIconsToolStripMenuItem.Size = new System.Drawing.Size ( 141, 22 );
      this.largeIconsToolStripMenuItem.Text = "Lar&ge Icons";
      this.largeIconsToolStripMenuItem.Click += new System.EventHandler ( this.largeIconsToolStripMenuItem_Click );
      // 
      // listToolStripMenuItem
      // 
      this.listToolStripMenuItem.Name = "listToolStripMenuItem";
      this.listToolStripMenuItem.Size = new System.Drawing.Size ( 141, 22 );
      this.listToolStripMenuItem.Text = "&List";
      this.listToolStripMenuItem.Click += new System.EventHandler ( this.listToolStripMenuItem_Click );
      // 
      // detailsToolStripMenuItem
      // 
      this.detailsToolStripMenuItem.Checked = true;
      this.detailsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
      this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
      this.detailsToolStripMenuItem.Size = new System.Drawing.Size ( 141, 22 );
      this.detailsToolStripMenuItem.Text = "&Details";
      this.detailsToolStripMenuItem.Click += new System.EventHandler ( this.detailsToolStripMenuItem_Click );
      // 
      // selectedPath
      // 
      this.selectedPath.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.selectedPath.FormattingEnabled = true;
      this.selectedPath.Location = new System.Drawing.Point ( 113, 6 );
      this.selectedPath.Name = "selectedPath";
      this.selectedPath.Size = new System.Drawing.Size ( 337, 21 );
      this.selectedPath.TabIndex = 4;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point ( 62, 9 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size ( 45, 13 );
      this.label1.TabIndex = 2;
      this.label1.Text = "Look in:";
      // 
      // panel2
      // 
      this.panel2.Controls.Add ( this.fileTypes );
      this.panel2.Controls.Add ( this.fileName );
      this.panel2.Controls.Add ( this.label3 );
      this.panel2.Controls.Add ( this.label2 );
      this.panel2.Controls.Add ( this.ok );
      this.panel2.Controls.Add ( this.cancel );
      this.panel2.Controls.Add ( this.pathTree );
      this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel2.Location = new System.Drawing.Point ( 94, 261 );
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size ( 560, 74 );
      this.panel2.TabIndex = 1;
      // 
      // fileTypes
      // 
      this.fileTypes.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.fileTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.fileTypes.FormattingEnabled = true;
      this.fileTypes.Location = new System.Drawing.Point ( 119, 43 );
      this.fileTypes.Name = "fileTypes";
      this.fileTypes.Size = new System.Drawing.Size ( 307, 21 );
      this.fileTypes.TabIndex = 6;
      // 
      // fileName
      // 
      this.fileName.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.fileName.Location = new System.Drawing.Point ( 119, 10 );
      this.fileName.Name = "fileName";
      this.fileName.Size = new System.Drawing.Size ( 307, 20 );
      this.fileName.TabIndex = 5;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point ( 12, 46 );
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size ( 66, 13 );
      this.label3.TabIndex = 4;
      this.label3.Text = "Files of type:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point ( 12, 13 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size ( 55, 13 );
      this.label2.TabIndex = 3;
      this.label2.Text = "File name:";
      // 
      // ok
      // 
      this.ok.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.ok.Location = new System.Drawing.Point ( 456, 6 );
      this.ok.Name = "ok";
      this.ok.Size = new System.Drawing.Size ( 89, 27 );
      this.ok.TabIndex = 1;
      this.ok.Text = "&Open";
      this.ok.UseVisualStyleBackColor = true;
      this.ok.Click += new System.EventHandler ( this.ok_Click );
      // 
      // cancel
      // 
      this.cancel.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancel.Location = new System.Drawing.Point ( 456, 39 );
      this.cancel.Name = "cancel";
      this.cancel.Size = new System.Drawing.Size ( 89, 27 );
      this.cancel.TabIndex = 2;
      this.cancel.Text = "&Cancel";
      this.cancel.UseVisualStyleBackColor = true;
      // 
      // pathTree
      // 
      this.pathTree.CloseComboBoxExtenderDelegate = null;
      this.pathTree.Location = new System.Drawing.Point ( 55, -2 );
      this.pathTree.Name = "pathTree";
      this.pathTree.PathSeparator = "/";
      this.pathTree.ShowPlusMinus = false;
      this.pathTree.ShowRootLines = false;
      this.pathTree.Size = new System.Drawing.Size ( 346, 247 );
      this.pathTree.TabIndex = 3;
      this.pathTree.Visible = false;
      // 
      // panel3
      // 
      this.panel3.Controls.Add ( this.files );
      this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel3.Location = new System.Drawing.Point ( 94, 33 );
      this.panel3.Name = "panel3";
      this.panel3.Padding = new System.Windows.Forms.Padding ( 8 );
      this.panel3.Size = new System.Drawing.Size ( 560, 228 );
      this.panel3.TabIndex = 0;
      // 
      // files
      // 
      this.files.ColumnsOrder = ( ( System.Collections.Generic.Dictionary<string, int> )( resources.GetObject ( "files.ColumnsOrder" ) ) );
      this.files.Dock = System.Windows.Forms.DockStyle.Fill;
      this.files.FullRowSelect = true;
      this.files.Location = new System.Drawing.Point ( 8, 8 );
      this.files.Name = "files";
      this.files.SelectedSortColumn = -1;
      this.files.Size = new System.Drawing.Size ( 544, 212 );
      this.files.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.files.SortStyle = DroidExplorer.Core.UI.ListViewEx.SortStyles.SortSelectedColumn;
      this.files.TabIndex = 0;
      this.files.UseCompatibleStateImageBehavior = false;
      this.files.View = System.Windows.Forms.View.Details;
      this.files.WatermarkImage = null;
      // 
      // panel4
      // 
      this.panel4.Controls.Add ( this.customPlacesPanel );
      this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel4.Location = new System.Drawing.Point ( 0, 33 );
      this.panel4.Name = "panel4";
      this.panel4.Padding = new System.Windows.Forms.Padding ( 4, 0, 0, 0 );
      this.panel4.Size = new System.Drawing.Size ( 94, 302 );
      this.panel4.TabIndex = 1;
      // 
      // customPlacesPanel
      // 
      this.customPlacesPanel.BackColor = System.Drawing.SystemColors.Control;
      this.customPlacesPanel.CanOverflow = false;
      this.customPlacesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.customPlacesPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.customPlacesPanel.ImageScalingSize = new System.Drawing.Size ( 32, 32 );
      this.customPlacesPanel.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
      this.customPlacesPanel.Location = new System.Drawing.Point ( 4, 0 );
      this.customPlacesPanel.Name = "customPlacesPanel";
      this.customPlacesPanel.Padding = new System.Windows.Forms.Padding ( 3, 2, 3, 2 );
      this.customPlacesPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.customPlacesPanel.Size = new System.Drawing.Size ( 90, 302 );
      this.customPlacesPanel.Stretch = true;
      this.customPlacesPanel.TabIndex = 0;
      // 
      // FileDialog
      // 
      this.AcceptButton = this.ok;
      this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancel;
      this.ClientSize = new System.Drawing.Size ( 654, 335 );
      this.Controls.Add ( this.panel3 );
      this.Controls.Add ( this.panel2 );
      this.Controls.Add ( this.panel4 );
      this.Controls.Add ( this.panel1 );
      this.Icon = ( ( System.Drawing.Icon )( resources.GetObject ( "$this.Icon" ) ) );
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size ( 510, 369 );
      this.Name = "FileDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "OpenFileDialog";
      this.panel1.ResumeLayout ( false );
      this.panel1.PerformLayout ( );
      this.toolStrip1.ResumeLayout ( false );
      this.toolStrip1.PerformLayout ( );
      this.panel2.ResumeLayout ( false );
      this.panel2.PerformLayout ( );
      this.panel3.ResumeLayout ( false );
      this.panel4.ResumeLayout ( false );
      this.panel4.PerformLayout ( );
      this.ResumeLayout ( false );

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Button ok;
    private System.Windows.Forms.Button cancel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox fileTypes;
    private System.Windows.Forms.TextBox fileName;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel4;
    private CustomPlacesPanel customPlacesPanel;
    private TreeViewComboBox pathTree;
    private ComboBoxEx selectedPath;
    private FileSystemListView files;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton backToolStripButton;
    private System.Windows.Forms.ToolStripButton parentToolStripButton;
    private System.Windows.Forms.ToolStripButton newFolderToolStripButton;
    private System.Windows.Forms.ToolStripDropDownButton viewModeToolStripDropDownButton;
    private System.Windows.Forms.ToolStripMenuItem iconsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem largeIconsToolStripMenuItem;
  }
}