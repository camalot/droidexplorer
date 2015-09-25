namespace DroidExplorer.Core.UI {
	partial class NewVersionForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewVersionForm));
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.changelog = new System.Windows.Forms.TextBox();
			this.close = new System.Windows.Forms.Button();
			this.name = new System.Windows.Forms.Label();
			this.downloadUpdate = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(417, 71);
			this.panel1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(52, 37);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(157, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "A new version is available";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(15, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 25);
			this.label1.TabIndex = 0;
			this.label1.Text = "Update Available";
			// 
			// changelog
			// 
			this.changelog.Location = new System.Drawing.Point(12, 109);
			this.changelog.Multiline = true;
			this.changelog.Name = "changelog";
			this.changelog.ReadOnly = true;
			this.changelog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.changelog.Size = new System.Drawing.Size(393, 127);
			this.changelog.TabIndex = 2;
			// 
			// close
			// 
			this.close.Location = new System.Drawing.Point(12, 242);
			this.close.Name = "close";
			this.close.Size = new System.Drawing.Size(106, 31);
			this.close.TabIndex = 3;
			this.close.Text = "&Close";
			this.close.UseVisualStyleBackColor = true;
			this.close.Click += new System.EventHandler(this.close_Click);
			// 
			// name
			// 
			this.name.AutoEllipsis = true;
			this.name.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.name.Location = new System.Drawing.Point(12, 74);
			this.name.Name = "name";
			this.name.Size = new System.Drawing.Size(393, 32);
			this.name.TabIndex = 4;
			this.name.Text = "[Title]";
			// 
			// downloadUpdate
			// 
			this.downloadUpdate.Location = new System.Drawing.Point(299, 242);
			this.downloadUpdate.Name = "downloadUpdate";
			this.downloadUpdate.Size = new System.Drawing.Size(106, 31);
			this.downloadUpdate.TabIndex = 5;
			this.downloadUpdate.Text = "&Download Update";
			this.downloadUpdate.UseVisualStyleBackColor = true;
			this.downloadUpdate.Click += new System.EventHandler(this.downloadUpdate_Click);
			// 
			// NewVersionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(417, 287);
			this.Controls.Add(this.downloadUpdate);
			this.Controls.Add(this.name);
			this.Controls.Add(this.close);
			this.Controls.Add(this.changelog);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewVersionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Version Available";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox changelog;
		private System.Windows.Forms.Button close;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label name;
		private System.Windows.Forms.Button downloadUpdate;
	}
}