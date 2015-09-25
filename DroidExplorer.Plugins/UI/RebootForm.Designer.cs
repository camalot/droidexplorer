namespace DroidExplorer.Plugins.UI {
	partial class RebootForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RebootForm));
			this.rebootMode = new System.Windows.Forms.ComboBox();
			this.reboot = new System.Windows.Forms.Button();
			this.cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// rebootMode
			// 
			this.rebootMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.rebootMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rebootMode.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rebootMode.FormattingEnabled = true;
			this.rebootMode.Location = new System.Drawing.Point(12, 12);
			this.rebootMode.Name = "rebootMode";
			this.rebootMode.Size = new System.Drawing.Size(450, 41);
			this.rebootMode.TabIndex = 0;
			// 
			// reboot
			// 
			this.reboot.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.reboot.Location = new System.Drawing.Point(301, 60);
			this.reboot.Name = "reboot";
			this.reboot.Size = new System.Drawing.Size(161, 46);
			this.reboot.TabIndex = 1;
			this.reboot.Text = "&Reboot";
			this.reboot.UseVisualStyleBackColor = true;
			this.reboot.Click += new System.EventHandler(this.reboot_Click);
			// 
			// cancel
			// 
			this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancel.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cancel.Location = new System.Drawing.Point(12, 60);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(161, 46);
			this.cancel.TabIndex = 2;
			this.cancel.Text = "&Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.cancel_Click);
			// 
			// RebootForm
			// 
			this.AcceptButton = this.reboot;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancel;
			this.ClientSize = new System.Drawing.Size(474, 118);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.reboot);
			this.Controls.Add(this.rebootMode);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(0, 0);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RebootForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reboot Device";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox rebootMode;
		private System.Windows.Forms.Button reboot;
		private System.Windows.Forms.Button cancel;
	}
}