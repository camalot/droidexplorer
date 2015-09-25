namespace DroidExplorer.Plugins.UI {
	partial class ScreenRecordForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenRecordForm));
			this.resolutionList = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.bitrateList = new System.Windows.Forms.ComboBox();
			this.timeLimit = new System.Windows.Forms.TrackBar();
			this.label3 = new System.Windows.Forms.Label();
			this.displayTime = new System.Windows.Forms.Label();
			this.start = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.location = new System.Windows.Forms.TextBox();
			this.browse = new System.Windows.Forms.Button();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.copyToPC = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.remaining = new System.Windows.Forms.Label();
			this.rotateList = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.deleteAfterCopy = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.timeLimit)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// resolutionList
			// 
			this.resolutionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.resolutionList.FormattingEnabled = true;
			this.resolutionList.Location = new System.Drawing.Point(78, 10);
			this.resolutionList.Name = "resolutionList";
			this.resolutionList.Size = new System.Drawing.Size(208, 21);
			this.resolutionList.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Resolution:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Bitrate:";
			// 
			// bitrateList
			// 
			this.bitrateList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.bitrateList.FormattingEnabled = true;
			this.bitrateList.Location = new System.Drawing.Point(78, 49);
			this.bitrateList.Name = "bitrateList";
			this.bitrateList.Size = new System.Drawing.Size(208, 21);
			this.bitrateList.TabIndex = 3;
			// 
			// timeLimit
			// 
			this.timeLimit.LargeChange = 60;
			this.timeLimit.Location = new System.Drawing.Point(78, 85);
			this.timeLimit.Maximum = 180;
			this.timeLimit.Minimum = 1;
			this.timeLimit.Name = "timeLimit";
			this.timeLimit.Size = new System.Drawing.Size(423, 45);
			this.timeLimit.TabIndex = 4;
			this.timeLimit.TickFrequency = 15;
			this.timeLimit.Value = 90;
			this.timeLimit.ValueChanged += new System.EventHandler(this.timeLimit_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 85);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Time Limit:";
			// 
			// displayTime
			// 
			this.displayTime.AutoSize = true;
			this.displayTime.Location = new System.Drawing.Point(12, 107);
			this.displayTime.Name = "displayTime";
			this.displayTime.Size = new System.Drawing.Size(49, 13);
			this.displayTime.TabIndex = 6;
			this.displayTime.Text = "00:01:30";
			// 
			// start
			// 
			this.start.Enabled = false;
			this.start.Location = new System.Drawing.Point(397, 214);
			this.start.Name = "start";
			this.start.Size = new System.Drawing.Size(104, 31);
			this.start.TabIndex = 8;
			this.start.Text = "&Start";
			this.start.UseVisualStyleBackColor = true;
			this.start.Click += new System.EventHandler(this.start_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 133);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Location:";
			// 
			// location
			// 
			this.location.Location = new System.Drawing.Point(78, 130);
			this.location.Name = "location";
			this.location.ReadOnly = true;
			this.location.Size = new System.Drawing.Size(365, 20);
			this.location.TabIndex = 10;
			this.location.TextChanged += new System.EventHandler(this.location_TextChanged);
			// 
			// browse
			// 
			this.browse.Location = new System.Drawing.Point(447, 129);
			this.browse.Name = "browse";
			this.browse.Size = new System.Drawing.Size(32, 22);
			this.browse.TabIndex = 11;
			this.browse.Text = "...";
			this.browse.UseVisualStyleBackColor = true;
			this.browse.Click += new System.EventHandler(this.browse_Click);
			// 
			// progress
			// 
			this.progress.Location = new System.Drawing.Point(15, 287);
			this.progress.Maximum = 180;
			this.progress.Minimum = 1;
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(486, 23);
			this.progress.Step = 1;
			this.progress.TabIndex = 12;
			this.progress.Value = 1;
			// 
			// copyToPC
			// 
			this.copyToPC.AutoSize = true;
			this.copyToPC.Location = new System.Drawing.Point(11, 0);
			this.copyToPC.Name = "copyToPC";
			this.copyToPC.Size = new System.Drawing.Size(178, 17);
			this.copyToPC.TabIndex = 13;
			this.copyToPC.Text = "Copy to computer after capture?";
			this.copyToPC.UseVisualStyleBackColor = true;
			this.copyToPC.CheckedChanged += new System.EventHandler(this.copyToPC_CheckedChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 268);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Remaining:";
			// 
			// remaining
			// 
			this.remaining.AutoSize = true;
			this.remaining.Location = new System.Drawing.Point(81, 268);
			this.remaining.Name = "remaining";
			this.remaining.Size = new System.Drawing.Size(49, 13);
			this.remaining.TabIndex = 15;
			this.remaining.Text = "00:01:30";
			// 
			// rotateList
			// 
			this.rotateList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.rotateList.Enabled = false;
			this.rotateList.FormattingEnabled = true;
			this.rotateList.Location = new System.Drawing.Point(84, 51);
			this.rotateList.Name = "rotateList";
			this.rotateList.Size = new System.Drawing.Size(144, 21);
			this.rotateList.TabIndex = 16;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(37, 54);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(42, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Rotate:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.deleteAfterCopy);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.copyToPC);
			this.groupBox1.Controls.Add(this.rotateList);
			this.groupBox1.Location = new System.Drawing.Point(78, 156);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(298, 89);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			// 
			// deleteAfterCopy
			// 
			this.deleteAfterCopy.AutoSize = true;
			this.deleteAfterCopy.Location = new System.Drawing.Point(40, 23);
			this.deleteAfterCopy.Name = "deleteAfterCopy";
			this.deleteAfterCopy.Size = new System.Drawing.Size(173, 17);
			this.deleteAfterCopy.Enabled = false;
      this.deleteAfterCopy.TabIndex = 18;
			this.deleteAfterCopy.Text = "Delete file off device after copy";
			this.deleteAfterCopy.UseVisualStyleBackColor = true;
			// 
			// ScreenRecordForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(513, 254);
			this.Controls.Add(this.remaining);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.progress);
			this.Controls.Add(this.browse);
			this.Controls.Add(this.location);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.start);
			this.Controls.Add(this.displayTime);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.timeLimit);
			this.Controls.Add(this.bitrateList);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.resolutionList);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ScreenRecordForm";
			this.ShowInTaskbar = false;
			this.Text = "Device Screen Capture";
			((System.ComponentModel.ISupportInitialize)(this.timeLimit)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox resolutionList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox bitrateList;
		private System.Windows.Forms.TrackBar timeLimit;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label displayTime;
		private System.Windows.Forms.Button start;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox location;
		private System.Windows.Forms.Button browse;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.CheckBox copyToPC;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label remaining;
		private System.Windows.Forms.ComboBox rotateList;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox deleteAfterCopy;
	}
}