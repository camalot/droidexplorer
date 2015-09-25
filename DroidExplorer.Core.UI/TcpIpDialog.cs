using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using Camalot.Common.Extensions;
using DroidExplorer.Core.Net;

namespace DroidExplorer.Core.UI {
	public partial class TcpIpDialog : Form {

		public string ConnectionString { get; set; }

		public TcpIpDialog() {
			InitializeComponent();
			this.ActiveControl = this.hostName;
		}

		private void cancel_Click(object sender, EventArgs e) {
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void connect_Click(object sender, EventArgs e) {

			if(!string.IsNullOrEmpty(hostName.Text)) {
				this.ConnectionString = string.Format("{0}:{1}", hostName.Text, hostPort.Value);
				this.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
		}

		private void TcpIpDialog_Load(object sender, EventArgs e) {
		}

		private void howToConnectLink_LinkClicked ( object sender, LinkLabelLinkClickedEventArgs e ) {
			Process.Start ( Resources.Strings.WifiAdbHelpLink );
		}

		//private async void button1_Click(object sender, EventArgs e) {
		//	var addresses = await new AdbScanner().ScanAsync();
		//	foreach(var item in addresses) {
		//		textBox1.AppendText(item);
		//		textBox1.AppendText(Environment.NewLine);
		//	}
		//}
	}
}
