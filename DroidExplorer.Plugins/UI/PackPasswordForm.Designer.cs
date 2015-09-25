namespace DroidExplorer.Plugins.UI {
	partial class PackPasswordForm {
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
			this.ok = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.password = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ok
			// 
			this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ok.Location = new System.Drawing.Point(196, 64);
			this.ok.Name = "ok";
			this.ok.Size = new System.Drawing.Size(75, 32);
			this.ok.TabIndex = 0;
			this.ok.Text = "&OK";
			this.ok.UseVisualStyleBackColor = true;
			this.ok.Click += new System.EventHandler(this.ok_Click);
			// 
			// cancel
			// 
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Location = new System.Drawing.Point(277, 64);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(75, 32);
			this.cancel.TabIndex = 1;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Password:";
			// 
			// password
			// 
			this.password.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
			this.password.Location = new System.Drawing.Point(74, 23);
			this.password.Name = "password";
			this.password.PasswordChar = '|';
			this.password.Size = new System.Drawing.Size(278, 20);
			this.password.TabIndex = 3;
			// 
			// PackPasswordForm
			// 
			this.AcceptButton = this.ok;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(364, 108);
			this.ControlBox = false;
			this.Controls.Add(this.password);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.ok);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "PackPasswordForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Password";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox password;
	}
}