using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidExplorer.Core.UI.CConsole {
	partial class ConsoleControl {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			this.InternalDispose(disposing);
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.richTextBoxConsole = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richTextBoxConsole
			// 
			this.richTextBoxConsole.AcceptsTab = true;
			this.richTextBoxConsole.BackColor = System.Drawing.Color.Black;
			this.richTextBoxConsole.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxConsole.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBoxConsole.ForeColor = System.Drawing.Color.White;
			this.richTextBoxConsole.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxConsole.Name = "richTextBoxConsole";
			this.richTextBoxConsole.ReadOnly = true;
			this.richTextBoxConsole.Size = new System.Drawing.Size(150, 150);
			this.richTextBoxConsole.TabIndex = 0;
			this.richTextBoxConsole.Text = "";
			this.richTextBoxConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBoxConsole.ShowSelectionMargin = true;
			this.richTextBoxConsole.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
			this.richTextBoxConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			// 
			// ConsoleControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.richTextBoxConsole);
			this.Name = "ConsoleControl";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBoxConsole;
	}
}
