using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DroidExplorer.Core;
using DroidExplorer.Core.IO;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Plugins.UI;

namespace DroidExplorer.Plugins {
	public class ScreenRecording : BasePlugin {
		private ScreenRecordForm _screenRecord;

		private const string SR_PATH = "/system/bin/screenrecord";

		public ScreenRecording ( IPluginHost host) : base( host ) {
			
		}
		#region IPluginExtendedInfo Members

		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		public override string Author {
			get { return "Ryan Conrad"; }
		}

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		public override string Url {
			get { return Resources.Strings.SupportUrl; }
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
			get { return "Screen Recording"; }
		}

		public override string Group { get { return "Tools"; } }


		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
			get { return "Record device screen"; }
		}


		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public override bool CreateToolButton {
			get {
				return Core.IO.File.Exists ( SR_PATH );
			}
		}

		public override System.Drawing.Image Image { get { return DroidExplorer.Resources.Images.camera_16; } }

		internal ScreenRecordForm ScreenRecord {
			get {
				if ( _screenRecord == null || _screenRecord.IsDisposed ) {
					_screenRecord = new ScreenRecordForm ( this.PluginHost );
				}
				return _screenRecord;
			}
		}


		public override void Initialize ( IPluginHost pluginHost ) {
			base.Initialize ( pluginHost );
		}

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			ScreenRecord.StartPosition = FormStartPosition.Manual;
			if (pluginHost.GetHostWindow ( ) != null ) {
				if ( !this.ScreenRecord.Visible ) {
					ScreenRecord.Show ( pluginHost.GetHostWindow ( ) );
				}
			} else {
				ScreenRecord.ShowInTaskbar = true;
				ScreenRecord.ShowDialog ( );
			};
		}


		#endregion

	}
}
