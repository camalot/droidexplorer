namespace DroidExplorer.Runner {
	partial class SelectPluginDialog {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( SelectPluginDialog ) );
			this.plugins = new System.Windows.Forms.ListView ( );
			this.cancel = new System.Windows.Forms.Button ( );
			this.ok = new System.Windows.Forms.Button ( );
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader ( );
			this.imageList = new System.Windows.Forms.ImageList ( this.components );
			this.label1 = new System.Windows.Forms.Label ( );
			this.arguments = new System.Windows.Forms.TextBox ( );
			this.SuspendLayout ( );
			// 
			// plugins
			// 
			this.plugins.Columns.AddRange ( new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1} );
			this.plugins.FullRowSelect = true;
			this.plugins.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.plugins.HideSelection = false;
			this.plugins.Location = new System.Drawing.Point ( 12, 12 );
			this.plugins.MultiSelect = false;
			this.plugins.Name = "plugins";
			this.plugins.Size = new System.Drawing.Size ( 355, 259 );
			this.plugins.SmallImageList = this.imageList;
			this.plugins.TabIndex = 0;
			this.plugins.UseCompatibleStateImageBehavior = false;
			this.plugins.View = System.Windows.Forms.View.Details;
			this.plugins.DoubleClick += new System.EventHandler ( this.plugins_DoubleClick );
			// 
			// cancel
			// 
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point ( 292, 329 );
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size ( 75, 23 );
			this.cancel.TabIndex = 1;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			// 
			// ok
			// 
			this.ok.Location = new System.Drawing.Point ( 211, 329 );
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size ( 75, 23 );
			this.ok.TabIndex = 2;
			this.ok.Text = "&OK";
			this.ok.UseVisualStyleBackColor = true;
			this.ok.Click += new System.EventHandler ( this.ok_Click );
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 351;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size ( 16, 16 );
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point ( 12, 283 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 129, 13 );
			this.label1.TabIndex = 3;
			this.label1.Text = "Command line Arguments:";
			// 
			// arguments
			// 
			this.arguments.Location = new System.Drawing.Point ( 12, 299 );
			this.arguments.Name = "arguments";
			this.arguments.Size = new System.Drawing.Size ( 355, 20 );
			this.arguments.TabIndex = 4;
			// 
			// SelectPluginDialog
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size ( 379, 364 );
			this.Controls.Add ( this.arguments );
			this.Controls.Add ( this.label1 );
			this.Controls.Add ( this.ok );
			this.Controls.Add ( this.cancel );
			this.Controls.Add ( this.plugins );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ( (System.Drawing.Icon)( resources.GetObject ( "$this.Icon" ) ) );
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectPluginDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Plugin to Launch";
			this.ResumeLayout ( false );
			this.PerformLayout ( );

		}

		#endregion

		private System.Windows.Forms.ListView plugins;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox arguments;
	}
}