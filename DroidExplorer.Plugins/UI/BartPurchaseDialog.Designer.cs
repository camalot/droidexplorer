namespace DroidExplorer.Plugins.UI {
	partial class BartPurchaseDialog {
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox ( );
			this.label1 = new System.Windows.Forms.Label ( );
			this.ok = new System.Windows.Forms.Button ( );
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit ( );
			this.SuspendLayout ( );
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DroidExplorer.Plugins.Properties.Resources.bartqr;
			this.pictureBox1.Location = new System.Drawing.Point ( 12, 12 );
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size ( 150, 150 );
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point ( 169, 13 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size ( 304, 33 );
			this.label1.TabIndex = 1;
			this.label1.Text = "License for Bart Manager not found. Scan the QR code to the left to purchase a li" +
					"cense.";
			// 
			// ok
			// 
			this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ok.Location = new System.Drawing.Point ( 397, 142 );
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size ( 75, 23 );
			this.ok.TabIndex = 2;
			this.ok.Text = "&OK";
			this.ok.UseVisualStyleBackColor = true;
			// 
			// BartPurchaseDialog
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ok;
			this.ClientSize = new System.Drawing.Size ( 485, 177 );
			this.Controls.Add ( this.ok );
			this.Controls.Add ( this.label1 );
			this.Controls.Add ( this.pictureBox1 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BartPurchaseDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "License not found";
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit ( );
			this.ResumeLayout ( false );

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ok;
	}
}