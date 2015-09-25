namespace DroidExplorer.UI {
  partial class AboutDialog {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.productName = new System.Windows.Forms.Label();
			this.version = new System.Windows.Forms.Label();
			this.copyright = new System.Windows.Forms.Label();
			this.ok = new System.Windows.Forms.Button();
			this.plugins = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.dontate = new System.Windows.Forms.PictureBox();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dontate)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 96);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// productName
			// 
			this.productName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.productName.Location = new System.Drawing.Point(102, 9);
			this.productName.Name = "productName";
			this.productName.Size = new System.Drawing.Size(500, 23);
			this.productName.TabIndex = 1;
			this.productName.Text = "productName";
			// 
			// version
			// 
			this.version.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.version.Location = new System.Drawing.Point(105, 32);
			this.version.Name = "version";
			this.version.Size = new System.Drawing.Size(497, 23);
			this.version.TabIndex = 2;
			this.version.Text = "version";
			this.version.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// copyright
			// 
			this.copyright.Location = new System.Drawing.Point(102, 55);
			this.copyright.Name = "copyright";
			this.copyright.Size = new System.Drawing.Size(497, 18);
			this.copyright.TabIndex = 3;
			this.copyright.Text = "copyright";
			// 
			// ok
			// 
			this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ok.Location = new System.Drawing.Point(505, 227);
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size(97, 35);
			this.ok.TabIndex = 4;
			this.ok.Text = "&OK";
			this.ok.UseVisualStyleBackColor = true;
			// 
			// plugins
			// 
			this.plugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.plugins.ForeColor = System.Drawing.SystemColors.WindowText;
			this.plugins.FullRowSelect = true;
			this.plugins.Location = new System.Drawing.Point(12, 118);
			this.plugins.Name = "plugins";
			this.plugins.Size = new System.Drawing.Size(590, 101);
			this.plugins.SmallImageList = this.imageList1;
			this.plugins.TabIndex = 5;
			this.plugins.UseCompatibleStateImageBehavior = false;
			this.plugins.View = System.Windows.Forms.View.Details;
			this.plugins.DoubleClick += new System.EventHandler(this.plugins_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 126;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Author";
			this.columnHeader2.Width = 72;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Url";
			this.columnHeader3.Width = 268;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Copyright";
			this.columnHeader4.Width = 118;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 99);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Plugins:";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(102, 83);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(82, 13);
			this.linkLabel1.TabIndex = 7;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Project Website";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// dontate
			// 
			this.dontate.Cursor = System.Windows.Forms.Cursors.Hand;
			this.dontate.Image = ((System.Drawing.Image)(resources.GetObject("dontate.Image")));
			this.dontate.Location = new System.Drawing.Point(15, 236);
			this.dontate.Name = "dontate";
			this.dontate.Size = new System.Drawing.Size(92, 26);
			this.dontate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.dontate.TabIndex = 8;
			this.dontate.TabStop = false;
			this.dontate.Click += new System.EventHandler(this.dontate_Click);
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(486, 83);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(116, 13);
			this.linkLabel2.TabIndex = 9;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "XDA-Developers Topic";
			this.linkLabel2.Visible = false;
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// AboutDialog
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(614, 274);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.dontate);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.plugins);
			this.Controls.Add(this.ok);
			this.Controls.Add(this.copyright);
			this.Controls.Add(this.version);
			this.Controls.Add(this.productName);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.Padding = new System.Windows.Forms.Padding(9);
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AboutDialog";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dontate)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label productName;
    private System.Windows.Forms.Label version;
    private System.Windows.Forms.Label copyright;
    private System.Windows.Forms.Button ok;
    private System.Windows.Forms.ListView plugins;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.PictureBox dontate;
    private System.Windows.Forms.LinkLabel linkLabel2;

  }
}
