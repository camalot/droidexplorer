using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroidExplorer.Core.UI;
using System.Linq;
using DroidExplorer.UI;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.Plugins.UI {
	public partial class RebootForm : PluginForm {
		public RebootForm(IPluginHost host) : base( host)  {
			InitializeComponent();

			this.rebootMode.Items.AddRange(new RebootModeItem[] {
				new RebootModeItem { Name = "Normal Reboot", Value = "" }, 
				new RebootModeItem { Name = "Recovery", Value = "recovery" }, 
				new RebootModeItem { Name = "Bootloader", Value = "bootloader" }, 
				new RebootModeItem { Name = "Download", Value = "download" }
			});
			
			this.rebootMode.DisplayMember = "Name";
			this.rebootMode.ValueMember = "Value";

			this.rebootMode.SelectedIndex = 0;
		}

		private void reboot_Click(object sender, EventArgs e) {
			var cmd = ((RebootModeItem)this.rebootMode.SelectedItem).Value;
			CommandRunner.Instance.Reboot(CommandRunner.Instance.DefaultDevice, cmd);
			if(this.InvokeRequired) {
				Invoke((Action)(() => {
					this.DialogResult = System.Windows.Forms.DialogResult.OK;
					this.Close();
				}));
			} else {
				this.DialogResult = System.Windows.Forms.DialogResult.OK;
				this.Close();
			}
		}

		private void cancel_Click(object sender, EventArgs e) {
			if(this.InvokeRequired) {
				Invoke((Action)(() => {
					this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
					this.Close();
				}));
			} else {
				this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
				this.Close();
			}
		}

		public class RebootModeItem {
			public string Name { get; set; }
			public string Value { get; set; }
		}


	}
}
