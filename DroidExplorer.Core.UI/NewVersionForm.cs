using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core.Net;
using System.Diagnostics;

namespace DroidExplorer.Core.UI {
	public partial class NewVersionForm : Form {

		public NewVersionForm ( SoftwareRelease release ) {
			InitializeComponent ( );

			Release = release;
			name.Text = release.Name;
			changelog.Text = release.Description;
			
		}

		public SoftwareRelease Release { get; private set; }

		private void close_Click ( object sender, EventArgs e ) {
			this.Close ( );
		}

		private void downloadUpdate_Click(object sender, EventArgs e) {
			if(!string.IsNullOrWhiteSpace(Release.Url)) {
				var proc = new Process();
				proc.StartInfo = new ProcessStartInfo(Release.Url);
				proc.Start();
			} else {
				MessageBox.Show("Unable to get update url.");
			}
		}
	}
}
