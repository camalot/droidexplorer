using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DroidExplorer.Configuration.UI {
	public class EnvironmentGeneralUIEditor : Control, IUIEditor{
		private System.ComponentModel.IContainer components = null;

		public EnvironmentGeneralUIEditor() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			// 
			// this
			// 
			this.ClientSize = new System.Drawing.Size(411, 263);
			this.Dock = DockStyle.Fill;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

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

		public void SetSourceObject(object obj) {
		}
	}
}
