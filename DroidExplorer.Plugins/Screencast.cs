using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.Plugins {
	public class Screencast : BasePlugin {

		public Screencast(IPluginHost host) : base(host) {

		}

		public override System.Drawing.Image Image { get { return DroidExplorer.Resources.Images.screencast; } }

		public override string Author {
			get { return "adakoda"; }
		}

		public override string Url {
			get { return "https://github.com/camalot/android-screen-monitor"; }
		}

		public override string Contact {
			get { return "https://github.com/adakoda"; }
		}

		public override string Name {
			get { return "Screen Monitor"; }
		}

		public override string Group {
			get { return "Tools"; }
		}

		public override string Description {
			get { return "A tool to monitor screen on the device"; }
		}

		public override bool CreateToolButton {
			get { return true; }
		}

		public override void Execute(IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args) {
			var jar = FolderManagement.GetBundledTool ( "asm.jar" );
			this.LogDebug ( "Executing: java.exe -jar \"{0}\" \"{1}\" {2}", jar, this.PluginHost.CommandRunner.SdkPath, this.PluginHost.Device );
			this.PluginHost.CommandRunner.LaunchProcessWindow("java.exe", "-jar \"{0}\" \"{1}\" {2}".With( jar, this.PluginHost.CommandRunner.SdkPath, this.PluginHost.Device ), false);
		}

		public override int MinimumSDKPlatformToolsVersion {
			get {
				return 19;
			}
		}
	}
}
