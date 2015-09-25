namespace DroidExplorer.UI {
  partial class MainForm {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.objectsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.sizeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.directoryTree = new DroidExplorer.Core.UI.DirectoryTreeView();
			this.treeImages = new System.Windows.Forms.ImageList(this.components);
			this.itemsList = new DroidExplorer.Core.UI.FileSystemListView();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.installToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uninstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.managePackagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.packageManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.installPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.applyROMUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutDroidExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.reportABugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.askForHelpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.askForHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageLink = new DroidExplorer.Controls.ImageLinkToolStripItem();
			this.connectToDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.folderUpToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.foldersToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.viewStyleToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.largeIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.smallIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStrip = new System.Windows.Forms.ToolStrip();
			this.copyToLocalToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.copyToDeviceToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolsToolStrip = new System.Windows.Forms.ToolStrip();
			this.updateRomToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.installToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.fileListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openWithContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.installContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uninstallContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newFolderContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newFileContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.copyContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.glassArea = new System.Windows.Forms.Panel();
			this.explorerNavigation = new Vista.Controls.ExplorerNavigation();
			this.breadcrumbBar = new Vista.Controls.BreadcrumbBar();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.copyToolStrip.SuspendLayout();
			this.toolsToolStrip.SuspendLayout();
			this.fileListContextMenuStrip.SuspendLayout();
			this.glassArea.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.BackColor = System.Drawing.SystemColors.Control;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(768, 312);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 42);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(768, 383);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.mainToolStrip);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.copyToolStrip);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolsToolStrip);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectsToolStripStatusLabel,
            this.sizeToolStripStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(768, 22);
			this.statusStrip1.TabIndex = 0;
			// 
			// objectsToolStripStatusLabel
			// 
			this.objectsToolStripStatusLabel.Name = "objectsToolStripStatusLabel";
			this.objectsToolStripStatusLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.objectsToolStripStatusLabel.Size = new System.Drawing.Size(733, 17);
			this.objectsToolStripStatusLabel.Spring = true;
			this.objectsToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// sizeToolStripStatusLabel
			// 
			this.sizeToolStripStatusLabel.Name = "sizeToolStripStatusLabel";
			this.sizeToolStripStatusLabel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.sizeToolStripStatusLabel.Size = new System.Drawing.Size(20, 17);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.directoryTree);
			this.splitContainer1.Panel1MinSize = 0;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.itemsList);
			this.splitContainer1.Size = new System.Drawing.Size(768, 312);
			this.splitContainer1.SplitterDistance = 185;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 0;
			// 
			// directoryTree
			// 
			this.directoryTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.directoryTree.HideSelection = false;
			this.directoryTree.ImageIndex = 0;
			this.directoryTree.ImageList = this.treeImages;
			this.directoryTree.Location = new System.Drawing.Point(0, 0);
			this.directoryTree.Name = "directoryTree";
			this.directoryTree.PathSeparator = "/";
			this.directoryTree.SelectedImageIndex = 0;
			this.directoryTree.Size = new System.Drawing.Size(185, 312);
			this.directoryTree.TabIndex = 0;
			// 
			// treeImages
			// 
			this.treeImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.treeImages.ImageSize = new System.Drawing.Size(16, 16);
			this.treeImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// itemsList
			// 
			this.itemsList.AllowColumnReorder = true;
			this.itemsList.AllowDrop = true;
			this.itemsList.ColumnsOrder = ((System.Collections.Generic.Dictionary<string, int>)(resources.GetObject("itemsList.ColumnsOrder")));
			this.itemsList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.itemsList.FullRowSelect = true;
			this.itemsList.LabelEdit = true;
			this.itemsList.Location = new System.Drawing.Point(0, 0);
			this.itemsList.Name = "itemsList";
			this.itemsList.SelectedSortColumn = -1;
			this.itemsList.Size = new System.Drawing.Size(577, 312);
			this.itemsList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.itemsList.SortStyle = DroidExplorer.Core.UI.ListViewEx.SortStyles.SortSelectedColumn;
			this.itemsList.TabIndex = 0;
			this.itemsList.UseCompatibleStateImageBehavior = false;
			this.itemsList.View = System.Windows.Forms.View.Details;
			this.itemsList.WatermarkImage = ((System.Drawing.Image)(resources.GetObject("itemsList.WatermarkImage")));
			// 
			// menuStrip
			// 
			this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.askForHelpToolStripMenuItem,
            this.imageLink,
            this.connectToDeviceToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(768, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "Menu";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			this.fileToolStripMenuItem.Visible = false;
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openWithToolStripMenuItem,
            this.installToolStripMenuItem,
            this.uninstallToolStripMenuItem,
            this.newFolderToolStripMenuItem,
            this.newFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem2,
            this.deleteToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Visible = false;
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// openWithToolStripMenuItem
			// 
			this.openWithToolStripMenuItem.Name = "openWithToolStripMenuItem";
			this.openWithToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
			this.openWithToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.openWithToolStripMenuItem.Text = "Open &With...";
			this.openWithToolStripMenuItem.Visible = false;
			this.openWithToolStripMenuItem.Click += new System.EventHandler(this.openWithToolStripMenuItem_Click);
			// 
			// installToolStripMenuItem
			// 
			this.installToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("installToolStripMenuItem.Image")));
			this.installToolStripMenuItem.Name = "installToolStripMenuItem";
			this.installToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Insert";
			this.installToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)));
			this.installToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.installToolStripMenuItem.Text = "&Install";
			this.installToolStripMenuItem.Visible = false;
			this.installToolStripMenuItem.Click += new System.EventHandler(this.installToolStripButton_Click);
			// 
			// uninstallToolStripMenuItem
			// 
			this.uninstallToolStripMenuItem.Name = "uninstallToolStripMenuItem";
			this.uninstallToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+Insert";
			this.uninstallToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Insert)));
			this.uninstallToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.uninstallToolStripMenuItem.Text = "&Uninstall";
			this.uninstallToolStripMenuItem.Visible = false;
			this.uninstallToolStripMenuItem.Click += new System.EventHandler(this.uninstallToolStripMenuItem_Click);
			// 
			// newFolderToolStripMenuItem
			// 
			this.newFolderToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newFolderToolStripMenuItem.Image")));
			this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
			this.newFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
			this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.newFolderToolStripMenuItem.Text = "New &Folder";
			this.newFolderToolStripMenuItem.Visible = false;
			this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
			// 
			// newFileToolStripMenuItem
			// 
			this.newFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newFileToolStripMenuItem.Image")));
			this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
			this.newFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newFileToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.newFileToolStripMenuItem.Text = "New Fi&le";
			this.newFileToolStripMenuItem.Visible = false;
			this.newFileToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(212, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.cutToolStripMenuItem.Text = "Cu&t";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.selectAllToolStripMenuItem.Text = "&Select All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(212, 6);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Delete";
			this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.deleteToolStripMenuItem.Text = "&Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			this.viewToolStripMenuItem.Visible = false;
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginsToolStripMenuItem,
            this.managePackagesToolStripMenuItem,
            this.toolStripMenuItem3,
            this.applyROMUpdateToolStripMenuItem,
            this.toolStripMenuItem4,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// pluginsToolStripMenuItem
			// 
			this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
			this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.pluginsToolStripMenuItem.Text = "Plugins";
			// 
			// managePackagesToolStripMenuItem
			// 
			this.managePackagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.packageManagerToolStripMenuItem,
            this.toolStripSeparator6,
            this.installPackageToolStripMenuItem});
			this.managePackagesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("managePackagesToolStripMenuItem.Image")));
			this.managePackagesToolStripMenuItem.Name = "managePackagesToolStripMenuItem";
			this.managePackagesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.managePackagesToolStripMenuItem.Text = "Manage &Packages";
			// 
			// packageManagerToolStripMenuItem
			// 
			this.packageManagerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("packageManagerToolStripMenuItem.Image")));
			this.packageManagerToolStripMenuItem.Name = "packageManagerToolStripMenuItem";
			this.packageManagerToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.packageManagerToolStripMenuItem.Text = "&Package Manager";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(165, 6);
			// 
			// installPackageToolStripMenuItem
			// 
			this.installPackageToolStripMenuItem.Name = "installPackageToolStripMenuItem";
			this.installPackageToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.installPackageToolStripMenuItem.Text = "&Install Package";
			this.installPackageToolStripMenuItem.Click += new System.EventHandler(this.installPackageToolStripMenuItem_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(173, 6);
			// 
			// applyROMUpdateToolStripMenuItem
			// 
			this.applyROMUpdateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("applyROMUpdateToolStripMenuItem.Image")));
			this.applyROMUpdateToolStripMenuItem.Name = "applyROMUpdateToolStripMenuItem";
			this.applyROMUpdateToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.applyROMUpdateToolStripMenuItem.Text = "Apply ROM &Update";
			this.applyROMUpdateToolStripMenuItem.Click += new System.EventHandler(this.applyROMUpdateToolStripMenuItem_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(173, 6);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
			this.optionsToolStripMenuItem.Text = "&Options...";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutDroidExplorerToolStripMenuItem,
            this.toolStripSeparator3,
            this.reportABugToolStripMenuItem,
            this.askForHelpToolStripMenuItem1});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutDroidExplorerToolStripMenuItem
			// 
			this.aboutDroidExplorerToolStripMenuItem.Name = "aboutDroidExplorerToolStripMenuItem";
			this.aboutDroidExplorerToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.aboutDroidExplorerToolStripMenuItem.Text = "&About Droid Explorer";
			this.aboutDroidExplorerToolStripMenuItem.Click += new System.EventHandler(this.aboutDroidExplorerToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
			// 
			// reportABugToolStripMenuItem
			// 
			this.reportABugToolStripMenuItem.Name = "reportABugToolStripMenuItem";
			this.reportABugToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.reportABugToolStripMenuItem.Text = "Report a Bug";
			this.reportABugToolStripMenuItem.Click += new System.EventHandler(this.reportABugToolStripMenuItem_Click);
			// 
			// askForHelpToolStripMenuItem1
			// 
			this.askForHelpToolStripMenuItem1.Name = "askForHelpToolStripMenuItem1";
			this.askForHelpToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
			this.askForHelpToolStripMenuItem1.Text = "Ask for Help";
			this.askForHelpToolStripMenuItem1.Click += new System.EventHandler(this.askForHelpToolStripMenuItem1_Click);
			// 
			// askForHelpToolStripMenuItem
			// 
			this.askForHelpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.askForHelpToolStripMenuItem.Name = "askForHelpToolStripMenuItem";
			this.askForHelpToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
			this.askForHelpToolStripMenuItem.Text = "Ask for Help";
			this.askForHelpToolStripMenuItem.ToolTipText = "Get help on Android Enthusiasts";
			this.askForHelpToolStripMenuItem.Click += new System.EventHandler(this.askForHelpToolStripMenuItem1_Click);
			// 
			// imageLink
			// 
			this.imageLink.AccessibleName = "image";
			this.imageLink.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.imageLink.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.imageLink.Name = "image";
			this.imageLink.Size = new System.Drawing.Size(71, 20);
			this.imageLink.Text = "Dontate";
			// 
			// connectToDeviceToolStripMenuItem
			// 
			this.connectToDeviceToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.connectToDeviceToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("connectToDeviceToolStripMenuItem.Image")));
			this.connectToDeviceToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.connectToDeviceToolStripMenuItem.Name = "connectToDeviceToolStripMenuItem";
			this.connectToDeviceToolStripMenuItem.Size = new System.Drawing.Size(132, 20);
			this.connectToDeviceToolStripMenuItem.Text = "Connect to Device";
			this.connectToDeviceToolStripMenuItem.Click += new System.EventHandler(this.connectToDeviceToolStripMenuItem_Click);
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folderUpToolStripButton,
            this.toolStripSeparator1,
            this.foldersToolStripButton,
            this.toolStripSeparator2,
            this.viewStyleToolStripButton});
			this.mainToolStrip.Location = new System.Drawing.Point(3, 24);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(99, 25);
			this.mainToolStrip.TabIndex = 1;
			// 
			// folderUpToolStripButton
			// 
			this.folderUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.folderUpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("folderUpToolStripButton.Image")));
			this.folderUpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.folderUpToolStripButton.Name = "folderUpToolStripButton";
			this.folderUpToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.folderUpToolStripButton.Text = "Go To Parent";
			this.folderUpToolStripButton.Click += new System.EventHandler(this.folderUpToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// foldersToolStripButton
			// 
			this.foldersToolStripButton.Checked = true;
			this.foldersToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
			this.foldersToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.foldersToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("foldersToolStripButton.Image")));
			this.foldersToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.foldersToolStripButton.Name = "foldersToolStripButton";
			this.foldersToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.foldersToolStripButton.Text = "Folders";
			this.foldersToolStripButton.Click += new System.EventHandler(this.foldersToolStripButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// viewStyleToolStripButton
			// 
			this.viewStyleToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.viewStyleToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.largeIconToolStripMenuItem,
            this.smallIconToolStripMenuItem,
            this.listToolStripMenuItem,
            this.tileToolStripMenuItem,
            this.detailsToolStripMenuItem});
			this.viewStyleToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("viewStyleToolStripButton.Image")));
			this.viewStyleToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.viewStyleToolStripButton.Name = "viewStyleToolStripButton";
			this.viewStyleToolStripButton.Size = new System.Drawing.Size(29, 22);
			this.viewStyleToolStripButton.Text = "Change View";
			// 
			// largeIconToolStripMenuItem
			// 
			this.largeIconToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("largeIconToolStripMenuItem.Image")));
			this.largeIconToolStripMenuItem.Name = "largeIconToolStripMenuItem";
			this.largeIconToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.largeIconToolStripMenuItem.Text = "&Large Icon";
			this.largeIconToolStripMenuItem.Click += new System.EventHandler(this.largeIconToolStripMenuItem_Click);
			// 
			// smallIconToolStripMenuItem
			// 
			this.smallIconToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("smallIconToolStripMenuItem.Image")));
			this.smallIconToolStripMenuItem.Name = "smallIconToolStripMenuItem";
			this.smallIconToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.smallIconToolStripMenuItem.Text = "&Small Icon";
			this.smallIconToolStripMenuItem.Click += new System.EventHandler(this.smallIconToolStripMenuItem_Click);
			// 
			// listToolStripMenuItem
			// 
			this.listToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("listToolStripMenuItem.Image")));
			this.listToolStripMenuItem.Name = "listToolStripMenuItem";
			this.listToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.listToolStripMenuItem.Text = "L&ist";
			this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
			// 
			// tileToolStripMenuItem
			// 
			this.tileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tileToolStripMenuItem.Image")));
			this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
			this.tileToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.tileToolStripMenuItem.Text = "&Tile";
			this.tileToolStripMenuItem.Visible = false;
			this.tileToolStripMenuItem.Click += new System.EventHandler(this.tileToolStripMenuItem_Click);
			// 
			// detailsToolStripMenuItem
			// 
			this.detailsToolStripMenuItem.Checked = true;
			this.detailsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.detailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("detailsToolStripMenuItem.Image")));
			this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
			this.detailsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.detailsToolStripMenuItem.Text = "&Details";
			this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
			// 
			// copyToolStrip
			// 
			this.copyToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.copyToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToLocalToolStripButton,
            this.copyToDeviceToolStripButton});
			this.copyToolStrip.Location = new System.Drawing.Point(102, 24);
			this.copyToolStrip.Name = "copyToolStrip";
			this.copyToolStrip.Size = new System.Drawing.Size(58, 25);
			this.copyToolStrip.TabIndex = 4;
			// 
			// copyToLocalToolStripButton
			// 
			this.copyToLocalToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToLocalToolStripButton.Enabled = false;
			this.copyToLocalToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToLocalToolStripButton.Image")));
			this.copyToLocalToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToLocalToolStripButton.Name = "copyToLocalToolStripButton";
			this.copyToLocalToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.copyToLocalToolStripButton.Text = "Copy to Local Computer";
			this.copyToLocalToolStripButton.Click += new System.EventHandler(this.copyToLocalToolStripButton_Click);
			// 
			// copyToDeviceToolStripButton
			// 
			this.copyToDeviceToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToDeviceToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToDeviceToolStripButton.Image")));
			this.copyToDeviceToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToDeviceToolStripButton.Name = "copyToDeviceToolStripButton";
			this.copyToDeviceToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.copyToDeviceToolStripButton.Text = "Copy to device";
			this.copyToDeviceToolStripButton.Click += new System.EventHandler(this.copyToDeviceToolStripButton_Click);
			// 
			// toolsToolStrip
			// 
			this.toolsToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateRomToolStripButton,
            this.toolStripSeparator4,
            this.installToolStripButton});
			this.toolsToolStrip.Location = new System.Drawing.Point(160, 24);
			this.toolsToolStrip.Name = "toolsToolStrip";
			this.toolsToolStrip.Size = new System.Drawing.Size(95, 25);
			this.toolsToolStrip.TabIndex = 2;
			// 
			// updateRomToolStripButton
			// 
			this.updateRomToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.updateRomToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("updateRomToolStripButton.Image")));
			this.updateRomToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.updateRomToolStripButton.Name = "updateRomToolStripButton";
			this.updateRomToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.updateRomToolStripButton.Text = "Install Update";
			this.updateRomToolStripButton.Click += new System.EventHandler(this.updateRomToolStripButton_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// installToolStripButton
			// 
			this.installToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.installToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("installToolStripButton.Image")));
			this.installToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.installToolStripButton.Name = "installToolStripButton";
			this.installToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.installToolStripButton.Text = "Install Application on device";
			this.installToolStripButton.Click += new System.EventHandler(this.installToolStripButton_Click);
			// 
			// fileListContextMenuStrip
			// 
			this.fileListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openContextToolStripMenuItem,
            this.openWithContextToolStripMenuItem,
            this.installContextToolStripMenuItem,
            this.uninstallContextToolStripMenuItem,
            this.newFolderContextToolStripMenuItem,
            this.newFileContextToolStripMenuItem,
            this.toolStripSeparator7,
            this.copyContextToolStripMenuItem,
            this.cutContextToolStripMenuItem,
            this.pasteContextToolStripMenuItem,
            this.selectAllContextToolStripMenuItem,
            this.toolStripSeparator8,
            this.deleteContextToolStripMenuItem,
            this.toolStripMenuItem5,
            this.propertiesToolStripMenuItem});
			this.fileListContextMenuStrip.Name = "fileListContextMenuStrip";
			this.fileListContextMenuStrip.Size = new System.Drawing.Size(216, 286);
			this.fileListContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.fileListContextMenuStrip_Opening);
			// 
			// openContextToolStripMenuItem
			// 
			this.openContextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openContextToolStripMenuItem.Image")));
			this.openContextToolStripMenuItem.Name = "openContextToolStripMenuItem";
			this.openContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
			this.openContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.openContextToolStripMenuItem.Text = "&Open";
			this.openContextToolStripMenuItem.Visible = false;
			this.openContextToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// openWithContextToolStripMenuItem
			// 
			this.openWithContextToolStripMenuItem.Name = "openWithContextToolStripMenuItem";
			this.openWithContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+O";
			this.openWithContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.openWithContextToolStripMenuItem.Text = "Open &With...";
			this.openWithContextToolStripMenuItem.Visible = false;
			this.openWithContextToolStripMenuItem.Click += new System.EventHandler(this.openWithToolStripMenuItem_Click);
			// 
			// installContextToolStripMenuItem
			// 
			this.installContextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("installContextToolStripMenuItem.Image")));
			this.installContextToolStripMenuItem.Name = "installContextToolStripMenuItem";
			this.installContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Insert";
			this.installContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.installContextToolStripMenuItem.Text = "&Install";
			this.installContextToolStripMenuItem.Visible = false;
			this.installContextToolStripMenuItem.Click += new System.EventHandler(this.installToolStripMenuItem_Click);
			// 
			// uninstallContextToolStripMenuItem
			// 
			this.uninstallContextToolStripMenuItem.Name = "uninstallContextToolStripMenuItem";
			this.uninstallContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+Insert";
			this.uninstallContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.uninstallContextToolStripMenuItem.Text = "&Uninstall";
			this.uninstallContextToolStripMenuItem.Visible = false;
			this.uninstallContextToolStripMenuItem.Click += new System.EventHandler(this.uninstallToolStripMenuItem_Click);
			// 
			// newFolderContextToolStripMenuItem
			// 
			this.newFolderContextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newFolderContextToolStripMenuItem.Image")));
			this.newFolderContextToolStripMenuItem.Name = "newFolderContextToolStripMenuItem";
			this.newFolderContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+N";
			this.newFolderContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.newFolderContextToolStripMenuItem.Text = "New &Folder";
			this.newFolderContextToolStripMenuItem.Visible = false;
			this.newFolderContextToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
			// 
			// newFileContextToolStripMenuItem
			// 
			this.newFileContextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newFileContextToolStripMenuItem.Image")));
			this.newFileContextToolStripMenuItem.Name = "newFileContextToolStripMenuItem";
			this.newFileContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
			this.newFileContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.newFileContextToolStripMenuItem.Text = "New Fi&le";
			this.newFileContextToolStripMenuItem.Visible = false;
			this.newFileContextToolStripMenuItem.Click += new System.EventHandler(this.newFileToolStripMenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(212, 6);
			// 
			// copyContextToolStripMenuItem
			// 
			this.copyContextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyContextToolStripMenuItem.Image")));
			this.copyContextToolStripMenuItem.Name = "copyContextToolStripMenuItem";
			this.copyContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+C";
			this.copyContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.copyContextToolStripMenuItem.Text = "&Copy";
			this.copyContextToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// cutContextToolStripMenuItem
			// 
			this.cutContextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutContextToolStripMenuItem.Image")));
			this.cutContextToolStripMenuItem.Name = "cutContextToolStripMenuItem";
			this.cutContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+X";
			this.cutContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.cutContextToolStripMenuItem.Text = "Cu&t";
			this.cutContextToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// pasteContextToolStripMenuItem
			// 
			this.pasteContextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteContextToolStripMenuItem.Image")));
			this.pasteContextToolStripMenuItem.Name = "pasteContextToolStripMenuItem";
			this.pasteContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+V";
			this.pasteContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.pasteContextToolStripMenuItem.Text = "&Paste";
			this.pasteContextToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// selectAllContextToolStripMenuItem
			// 
			this.selectAllContextToolStripMenuItem.Name = "selectAllContextToolStripMenuItem";
			this.selectAllContextToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+A";
			this.selectAllContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.selectAllContextToolStripMenuItem.Text = "&Select All";
			this.selectAllContextToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(212, 6);
			// 
			// deleteContextToolStripMenuItem
			// 
			this.deleteContextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteContextToolStripMenuItem.Image")));
			this.deleteContextToolStripMenuItem.Name = "deleteContextToolStripMenuItem";
			this.deleteContextToolStripMenuItem.ShortcutKeyDisplayString = "Delete";
			this.deleteContextToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.deleteContextToolStripMenuItem.Text = "&Delete";
			this.deleteContextToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(212, 6);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
			this.propertiesToolStripMenuItem.Text = "&Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// glassArea
			// 
			this.glassArea.BackColor = System.Drawing.SystemColors.Control;
			this.glassArea.Controls.Add(this.explorerNavigation);
			this.glassArea.Controls.Add(this.breadcrumbBar);
			this.glassArea.Dock = System.Windows.Forms.DockStyle.Top;
			this.glassArea.Location = new System.Drawing.Point(0, 0);
			this.glassArea.Name = "glassArea";
			this.glassArea.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
			this.glassArea.Size = new System.Drawing.Size(768, 42);
			this.glassArea.TabIndex = 2;
			// 
			// explorerNavigation
			// 
			this.explorerNavigation.BackColor = System.Drawing.Color.Transparent;
			this.explorerNavigation.Location = new System.Drawing.Point(0, 6);
			this.explorerNavigation.Name = "explorerNavigation";
			this.explorerNavigation.PaintForGlass = true;
			this.explorerNavigation.Size = new System.Drawing.Size(74, 29);
			this.explorerNavigation.TabIndex = 1;
			this.explorerNavigation.Text = "explorerNavigation1";
			// 
			// breadcrumbBar
			// 
			this.breadcrumbBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.breadcrumbBar.BackColor = System.Drawing.SystemColors.Window;
			this.breadcrumbBar.BackgroundAlpha = ((byte)(255));
			this.breadcrumbBar.ForeColor = System.Drawing.SystemColors.WindowText;
			this.breadcrumbBar.FullPath = "";
			this.breadcrumbBar.HoverBackColor = System.Drawing.SystemColors.Window;
			this.breadcrumbBar.Location = new System.Drawing.Point(75, 10);
			this.breadcrumbBar.Name = "breadcrumbBar";
			this.breadcrumbBar.PathSeparator = "/";
			this.breadcrumbBar.Size = new System.Drawing.Size(690, 22);
			this.breadcrumbBar.TabIndex = 0;
			this.breadcrumbBar.Text = "breadcrumbBar1";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(768, 425);
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.glassArea);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "Droid Explorer";
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.copyToolStrip.ResumeLayout(false);
			this.copyToolStrip.PerformLayout();
			this.toolsToolStrip.ResumeLayout(false);
			this.toolsToolStrip.PerformLayout();
			this.fileListContextMenuStrip.ResumeLayout(false);
			this.glassArea.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStrip mainToolStrip;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private DroidExplorer.Core.UI.DirectoryTreeView directoryTree;
    private System.Windows.Forms.ImageList treeImages;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;

    private System.Windows.Forms.ToolStripDropDownButton viewStyleToolStripButton;
    private System.Windows.Forms.ToolStripMenuItem largeIconToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem smallIconToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem tileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripButton folderUpToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton foldersToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStrip toolsToolStrip;
    private System.Windows.Forms.ToolStripStatusLabel objectsToolStripStatusLabel;
    private System.Windows.Forms.ToolStripStatusLabel sizeToolStripStatusLabel;
    private System.Windows.Forms.ToolStrip copyToolStrip;
    private System.Windows.Forms.ToolStripButton copyToLocalToolStripButton;
    private System.Windows.Forms.ToolStripMenuItem connectToDeviceToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton updateRomToolStripButton;
    private System.Windows.Forms.ToolStripButton copyToDeviceToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton installToolStripButton;
    private System.Windows.Forms.ContextMenuStrip fileListContextMenuStrip;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem managePackagesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem packageManagerToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem installPackageToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem applyROMUpdateToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openWithToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem installToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem uninstallToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openWithContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem installContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem uninstallContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newFolderContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newFileContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripMenuItem copyContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cutContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pasteContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectAllContextToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem deleteContextToolStripMenuItem;
    private System.Windows.Forms.Panel glassArea;
    private Vista.Controls.BreadcrumbBar breadcrumbBar;
    private Vista.Controls.ExplorerNavigation explorerNavigation;
		private DroidExplorer.Core.UI.FileSystemListView itemsList;
    private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutDroidExplorerToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
    private  DroidExplorer.Controls.ImageLinkToolStripItem imageLink;
		private System.Windows.Forms.ToolStripMenuItem askForHelpToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem reportABugToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem askForHelpToolStripMenuItem1;
		private System.Windows.Forms.ToolStripManager toolstripManager;
  }
}

