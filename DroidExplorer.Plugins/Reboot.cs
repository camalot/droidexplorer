using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Plugins.UI;

namespace DroidExplorer.Plugins {
	public class Reboot : BasePlugin {
		public Reboot(IPluginHost host)
			: base(host) {

		}
		public override string Author {
			get { return "Ryan Conrad"; }
		}

		public override string Url {
			get { return Resources.Strings.ApplicationWebsiteUrl; }
		}

		public override string Contact {
			get { return string.Empty; }
		}

		public override string Name {
			get { return "Reboot"; }
		}
		public override string Group { get { return "Tools"; } }

		public override System.Drawing.Image Image {
			get { return Resources.Images.WorkflowActivity_16xLG; }
		}

		public override string Description {
			get { return "Reboot your device in to different states"; }
		}

		public override bool CreateToolButton {
			get { return true; }
		}

		public override void Execute(IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args) {
			Arguments arguments = new Arguments(args ?? new string[] { });
			string mode = string.Empty;
			if(arguments.Contains("mode")) {
				mode = arguments["mode"];
			}
			var form = new RebootForm( pluginHost );

			form.Top = Screen.PrimaryScreen.WorkingArea.Top;
			form.Left = Screen.PrimaryScreen.WorkingArea.Left;

			if(pluginHost.GetHostWindow() != null) {
				form.Show();
			} else {
				form.ShowInTaskbar = true;
				form.ShowDialog();
			}
		}
	}
}
