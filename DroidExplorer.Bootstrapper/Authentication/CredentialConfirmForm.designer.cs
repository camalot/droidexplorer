namespace DroidExplorer.Bootstrapper.Authentication {
	partial class CredentialConfirmForm {
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
			this.banner = new System.Windows.Forms.PictureBox ( );
			this.welcome = new System.Windows.Forms.Label ( );
			this.label2 = new System.Windows.Forms.Label ( );
			this.label3 = new System.Windows.Forms.Label ( );
			this.username = new System.Windows.Forms.TextBox ( );
			this.password = new System.Windows.Forms.TextBox ( );
			this.save = new System.Windows.Forms.CheckBox ( );
			this.ok = new System.Windows.Forms.Button ( );
			this.cancel = new System.Windows.Forms.Button ( );
			this.pictureBox1 = new System.Windows.Forms.PictureBox ( );
			( (System.ComponentModel.ISupportInitialize)( this.banner ) ).BeginInit ( );
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).BeginInit ( );
			this.SuspendLayout ( );
			// 
			// banner
			// 
			this.banner.Dock = System.Windows.Forms.DockStyle.Top;
			this.banner.Image = global::DroidExplorer.Bootstrapper.Properties.Resources.security_background;
			this.banner.Location = new System.Drawing.Point ( 0, 0 );
			this.banner.Name = "banner";
			this.banner.Size = new System.Drawing.Size ( 320, 60 );
			this.banner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.banner.TabIndex = 0;
			this.banner.TabStop = false;
			// 
			// welcome
			// 
			this.welcome.AutoSize = true;
			this.welcome.Location = new System.Drawing.Point ( 16, 78 );
			this.welcome.Name = "welcome";
			this.welcome.Size = new System.Drawing.Size ( 81, 13 );
			this.welcome.TabIndex = 1;
			this.welcome.Text = "Welcome to {0}";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point ( 16, 122 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size ( 61, 13 );
			this.label2.TabIndex = 2;
			this.label2.Text = "&User name:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point ( 16, 156 );
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size ( 56, 13 );
			this.label3.TabIndex = 3;
			this.label3.Text = "&Password:";
			// 
			// username
			// 
			this.username.Location = new System.Drawing.Point ( 120, 119 );
			this.username.Margin = new System.Windows.Forms.Padding ( 23, 3, 3, 3 );
			this.username.Name = "username";
			this.username.Size = new System.Drawing.Size ( 189, 20 );
			this.username.TabIndex = 4;
			// 
			// password
			// 
			this.password.Font = new System.Drawing.Font ( "Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 2 ) ) );
			this.password.Location = new System.Drawing.Point ( 120, 153 );
			this.password.Name = "password";
			this.password.PasswordChar = 'l';
			this.password.Size = new System.Drawing.Size ( 189, 20 );
			this.password.TabIndex = 5;
			// 
			// save
			// 
			this.save.AutoSize = true;
			this.save.Location = new System.Drawing.Point ( 120, 185 );
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size ( 141, 17 );
			this.save.TabIndex = 6;
			this.save.Text = "&Remember my password";
			this.save.UseVisualStyleBackColor = true;
			// 
			// ok
			// 
			this.ok.Location = new System.Drawing.Point ( 153, 230 );
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size ( 75, 23 );
			this.ok.TabIndex = 7;
			this.ok.Text = "&OK";
			this.ok.UseVisualStyleBackColor = true;
			this.ok.Click += new System.EventHandler ( this.ok_Click );
			// 
			// cancel
			// 
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point ( 234, 230 );
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size ( 75, 23 );
			this.cancel.TabIndex = 8;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::DroidExplorer.Bootstrapper.Properties.Resources.user;
			this.pictureBox1.Location = new System.Drawing.Point ( 104, 121 );
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size ( 11, 14 );
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 9;
			this.pictureBox1.TabStop = false;
			// 
			// CredentialConfirmForm
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size ( 320, 265 );
			this.Controls.Add ( this.pictureBox1 );
			this.Controls.Add ( this.cancel );
			this.Controls.Add ( this.ok );
			this.Controls.Add ( this.save );
			this.Controls.Add ( this.password );
			this.Controls.Add ( this.username );
			this.Controls.Add ( this.label3 );
			this.Controls.Add ( this.label2 );
			this.Controls.Add ( this.welcome );
			this.Controls.Add ( this.banner );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CredentialConfirmForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Connect to {0}";
			( (System.ComponentModel.ISupportInitialize)( this.banner ) ).EndInit ( );
			( (System.ComponentModel.ISupportInitialize)( this.pictureBox1 ) ).EndInit ( );
			this.ResumeLayout ( false );
			this.PerformLayout ( );

		}

		#endregion

		private System.Windows.Forms.PictureBox banner;
		private System.Windows.Forms.Label welcome;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox username;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.CheckBox save;
		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}