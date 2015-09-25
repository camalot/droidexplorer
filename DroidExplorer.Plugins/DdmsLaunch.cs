using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.Plugins {
	public class DdmsLaunch : BasePlugin {
		public DdmsLaunch(IPluginHost host)
			: base(host) {

		}

		#region IPluginExtendedInfo Members

		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		public override string Author {
			get { return "Google"; }
		}

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		public override string Url {
			get { return "http://developer.android.com/tools/debugging/ddms.html"; }
		}

		/// <summary>
		/// Gets the contact.
		/// </summary>
		/// <value>
		/// The contact.
		/// </value>
		public override string Contact {
			get { return String.Empty; }
		}

		#endregion

		#region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public override string Name {
			get { return "DDMS Launch"; }
		}

		/// <summary>
		/// Gets the group.
		/// </summary>
		/// <value>
		/// The group.
		/// </value>
		public override string Group { get { return "SDK Tools"; } }


		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
			get { return "Launch Dalvik Debug Monitor Server (DDMS)"; }
		}


		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public override bool CreateToolButton {
			get { return FolderManagement.ToolExists (CommandRunner.DDMS_COMMAND); }
		}

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public override System.Drawing.Image Image { get { return DroidExplorer.Resources.Images.ddms; } }

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute(IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args) {
			CommandRunner.Instance.LaunchDalvikDebugMonitor();
		}
		#endregion
	}
}
