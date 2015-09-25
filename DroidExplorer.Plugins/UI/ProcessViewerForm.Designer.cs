namespace DroidExplorer.Plugins.UI {
	partial class ProcessViewerForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessViewerForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.listViewEx1 = new DroidExplorer.Core.UI.ListViewEx();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.cpuInfo = new DroidExplorer.Core.UI.ListViewEx();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.chPid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(537, 409);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.listViewEx1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(529, 383);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Processes";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// listViewEx1
			// 
			this.listViewEx1.ColumnsOrder = ((System.Collections.Generic.Dictionary<string, int>)(resources.GetObject("listViewEx1.ColumnsOrder")));
			this.listViewEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewEx1.Location = new System.Drawing.Point(3, 3);
			this.listViewEx1.Name = "listViewEx1";
			this.listViewEx1.SelectedSortColumn = -1;
			this.listViewEx1.Size = new System.Drawing.Size(523, 377);
			this.listViewEx1.SortStyle = DroidExplorer.Core.UI.ListViewEx.SortStyles.SortDefault;
			this.listViewEx1.TabIndex = 0;
			this.listViewEx1.UseCompatibleStateImageBehavior = false;
			this.listViewEx1.WatermarkImage = null;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.cpuInfo);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(529, 383);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "CPU";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// cpuInfo
			// 
			this.cpuInfo.AllowColumnReorder = true;
			this.cpuInfo.ColumnsOrder = ((System.Collections.Generic.Dictionary<string, int>)(resources.GetObject("cpuInfo.ColumnsOrder")));
			this.cpuInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cpuInfo.FullRowSelect = true;
			this.cpuInfo.HideSelection = false;
			this.cpuInfo.Location = new System.Drawing.Point(0, 0);
			this.cpuInfo.MultiSelect = false;
			this.cpuInfo.Name = "cpuInfo";
			this.cpuInfo.SelectedSortColumn = -1;
			this.cpuInfo.Size = new System.Drawing.Size(529, 383);
			this.cpuInfo.SortStyle = DroidExplorer.Core.UI.ListViewEx.SortStyles.SortSelectedColumn;
			this.cpuInfo.TabIndex = 0;
			this.cpuInfo.UseCompatibleStateImageBehavior = false;
			this.cpuInfo.View = System.Windows.Forms.View.Details;
			this.cpuInfo.WatermarkImage = ((System.Drawing.Image)(resources.GetObject("cpuInfo.WatermarkImage")));
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(529, 383);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Network";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// chPid
			// 
			this.chPid.Text = "PID";
			// 
			// chName
			// 
			this.chName.Text = "Name";
			this.chName.Width = 150;
			// 
			// chSize
			// 
			this.chSize.Text = "Size";
			this.chSize.Width = 105;
			// 
			// chUser
			// 
			this.chUser.Text = "User";
			this.chUser.Width = 101;
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(529, 383);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Memory";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// ProcessViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(537, 409);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "ProcessViewerForm";
			this.ShowInTaskbar = false;
			this.Text = "Process Explore";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.ListView listView1;
    private System.Windows.Forms.ColumnHeader chPid;
    private System.Windows.Forms.ColumnHeader chName;
    private System.Windows.Forms.ColumnHeader chSize;
    private System.Windows.Forms.ColumnHeader chUser;
    private DroidExplorer.Core.UI.ListViewEx cpuInfo;
    private DroidExplorer.Core.UI.ListViewEx listViewEx1;
		private System.Windows.Forms.TabPage tabPage4;
	}
}