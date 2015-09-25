using System;
using System.Windows.Forms;
namespace DroidExplorer.Configuration {
	partial class DummyForm {
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
			this.label1 = new System.Windows.Forms.Label();
			this.browse = new System.Windows.Forms.Button();
			this.sdkPath = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.driverVersion = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.aaptVersion = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.adbVersion = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.adbBuildToolsVersion = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Android SDK Path:";
			// 
			// browse
			// 
			this.browse.Location = new System.Drawing.Point(323, 25);
			this.browse.Name = "browse";
			this.browse.Size = new System.Drawing.Size(75, 23);
			this.browse.TabIndex = 1;
			this.browse.Text = "&Browse";
			this.browse.UseVisualStyleBackColor = true;
			this.browse.Click += new System.EventHandler(this.browse_Click);
			// 
			// sdkPath
			// 
			this.sdkPath.Location = new System.Drawing.Point(13, 27);
			this.sdkPath.Name = "sdkPath";
			this.sdkPath.ReadOnly = true;
			this.sdkPath.Size = new System.Drawing.Size(305, 20);
			this.sdkPath.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.adbBuildToolsVersion);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.driverVersion);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.aaptVersion);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.adbVersion);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(13, 65);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(385, 161);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "SDK Tools Information";
			// 
			// driverVersion
			// 
			this.driverVersion.Location = new System.Drawing.Point(258, 97);
			this.driverVersion.Name = "driverVersion";
			this.driverVersion.Size = new System.Drawing.Size(121, 13);
			this.driverVersion.TabIndex = 8;
			this.driverVersion.Text = "{0} ({1})";
			this.driverVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(101, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "USB Driver Version:";
			// 
			// aaptVersion
			// 
			this.aaptVersion.Location = new System.Drawing.Point(279, 70);
			this.aaptVersion.Name = "aaptVersion";
			this.aaptVersion.Size = new System.Drawing.Size(100, 13);
			this.aaptVersion.TabIndex = 6;
			this.aaptVersion.Text = "0.0.0.0";
			this.aaptVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(191, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Android Asset Packaging Tool Version:";
			// 
			// adbVersion
			// 
			this.adbVersion.Location = new System.Drawing.Point(279, 44);
			this.adbVersion.Name = "adbVersion";
			this.adbVersion.Size = new System.Drawing.Size(100, 13);
			this.adbVersion.TabIndex = 4;
			this.adbVersion.Text = "0.0.0.0";
			this.adbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Android Debug Bridge Version:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 21);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(139, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Android Build Tools Version:";
			// 
			// adbBuildToolsVersion
			// 
			this.adbBuildToolsVersion.Location = new System.Drawing.Point(279, 21);
			this.adbBuildToolsVersion.Name = "adbBuildToolsVersion";
			this.adbBuildToolsVersion.Size = new System.Drawing.Size(100, 13);
			this.adbBuildToolsVersion.TabIndex = 12;
			this.adbBuildToolsVersion.Text = "0.0.0.0";
			this.adbBuildToolsVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// DummyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(403, 259);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.browse);
			this.Controls.Add(this.sdkPath);
			this.Controls.Add(this.groupBox1);
			this.Name = "DummyForm";
			this.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
			this.Text = "DummyForm";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label adbBuildToolsVersion;
		private Label label5;

	}
}