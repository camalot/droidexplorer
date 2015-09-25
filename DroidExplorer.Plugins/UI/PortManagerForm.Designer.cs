namespace DroidExplorer.Plugins.UI {
	partial class PortManagerForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortManagerForm));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.forwardAdd = new System.Windows.Forms.ToolStripButton();
			this.forwardDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.reverseAdd = new System.Windows.Forms.ToolStripButton();
			this.reverseDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(12, 7);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(202, 384);
			this.treeView1.TabIndex = 0;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.treeView1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(662, 403);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(662, 428);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forwardAdd,
            this.forwardDelete,
            this.toolStripSeparator1,
            this.reverseAdd,
            this.reverseDelete});
			this.toolStrip1.Location = new System.Drawing.Point(3, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(141, 25);
			this.toolStrip1.TabIndex = 0;
			// 
			// forwardAdd
			// 
			this.forwardAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.forwardAdd.Image = ((System.Drawing.Image)(resources.GetObject("forwardAdd.Image")));
			this.forwardAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.forwardAdd.Name = "forwardAdd";
			this.forwardAdd.Size = new System.Drawing.Size(23, 22);
			this.forwardAdd.Text = "Add Port Forward";
			// 
			// forwardDelete
			// 
			this.forwardDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.forwardDelete.Image = ((System.Drawing.Image)(resources.GetObject("forwardDelete.Image")));
			this.forwardDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.forwardDelete.Name = "forwardDelete";
			this.forwardDelete.Size = new System.Drawing.Size(23, 22);
			this.forwardDelete.Text = "Remove Port Forward";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// reverseAdd
			// 
			this.reverseAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.reverseAdd.Image = ((System.Drawing.Image)(resources.GetObject("reverseAdd.Image")));
			this.reverseAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.reverseAdd.Name = "reverseAdd";
			this.reverseAdd.Size = new System.Drawing.Size(23, 22);
			this.reverseAdd.Text = "Add Port Reverse";
			// 
			// reverseDelete
			// 
			this.reverseDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.reverseDelete.Image = ((System.Drawing.Image)(resources.GetObject("reverseDelete.Image")));
			this.reverseDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.reverseDelete.Name = "reverseDelete";
			this.reverseDelete.Size = new System.Drawing.Size(23, 22);
			this.reverseDelete.Text = "Remove Port Reverse";
			// 
			// PortManagerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(662, 428);
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "PortManagerForm";
			this.Text = "PortManager";
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton forwardAdd;
		private System.Windows.Forms.ToolStripButton forwardDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton reverseAdd;
		private System.Windows.Forms.ToolStripButton reverseDelete;
	}
}