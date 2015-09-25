using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Plugins.UI;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using Camalot.Common.Extensions;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
	public class SqliteExplorer : BasePlugin, IFileTypeHandler {
		private readonly string ToolPath;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqliteExplorer"/> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public SqliteExplorer(IPluginHost host)
			: base(host) {

			ToolPath = FolderManagement.GetBundledTool ( "sqlitebrowser/sqlitebrowser.exe" );

			if (host != null) {
				this.PluginHost.RegisterFileTypeIcon(".db", DroidExplorer.Resources.Images.database_16xLG, DroidExplorer.Resources.Images.database_32xLG);
				this.PluginHost.RegisterFileTypeIcon(".sqlite", DroidExplorer.Resources.Images.database_16xLG, DroidExplorer.Resources.Images.database_32xLG);
				this.PluginHost.RegisterFileTypeIcon(".database", DroidExplorer.Resources.Images.database_16xLG, DroidExplorer.Resources.Images.database_32xLG);

				this.PluginHost.RegisterFileTypeHandler(".db", this);
				this.PluginHost.RegisterFileTypeHandler(".sqlite", this);
				this.PluginHost.RegisterFileTypeHandler(".database", this);
			}
		}
		#region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name {
			get { return "SqliteBrowser"; }
		}
		public override string Group { get { return "Tools"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public override string Description {
			get { return "DB Browser for SQLite is a high quality, visual, open source tool to create, design, and edit database files compatible with SQLite."; }
		}

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>The image.</value>
		public override System.Drawing.Image Image {
			get { return DroidExplorer.Resources.Images.database_16xLG; }
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public override string Text {
			get { return "SQLite Database Browser"; }
		}



		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute(IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args) {
			var ofd = new Core.UI.OpenFileDialog ( ) {
				Title = "Open Database File",
				DefaultExt = "db",
				Filter = "Database Files|*.db;*.database;*.sqlite|All Files|*.*",
				FilterIndex = 0,
				ValidateNames = true,
				CheckFileExists = true
			};

			ofd.CustomPlaces.Add ( new Core.UI.FileDialogCustomPlace ( "/data/data/", "data", Resources.Images.database_32xLG, delegate ( object s, EventArgs ea ) {
				ofd.Navigate ( new Core.IO.LinuxDirectoryInfo ( "/data/data/" ) );
			} ) );

			if (ofd.ShowDialog(pluginHost.GetHostWindow()) == DialogResult.OK ) {
				Open ( ofd.FileName );
			}
		}

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value><c>true</c> if [create tool button]; otherwise, <c>false</c>.</value>
		public override bool CreateToolButton {
			get { return !string.IsNullOrWhiteSpace( ToolPath ) && System.IO.File.Exists( ToolPath ); }
		}

		#endregion

		#region IPluginExtendedInfo Members

		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>The author.</value>
		public override string Author {
			get { return "Ryan Conrad"; }
		}

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>The URL.</value>
		public override string Url {
			get { return "http://sqlitebrowser.org/"; }
    }

		/// <summary>
		/// Gets the contact.
		/// </summary>
		/// <value>The contact.</value>
		public override string Contact {
			get { return string.Empty; }
		}

		#endregion

		#region IFileTypeHandler Members

		/// <summary>
		/// Opens the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		public void Open(DroidExplorer.Core.IO.FileInfo file) {
			Open ( file.FullPath );
		}

		private void Open(string file) {
			if ( string.IsNullOrWhiteSpace ( ToolPath ) || !System.IO.File.Exists ( ToolPath ) ) {
				this.LogWarn ( "Unable to launch SqliteBrowser: Unable to locate path: '{0}'", ToolPath );
				return;
			}

			var temp = this.PluginHost.CommandRunner.PullFile ( this.PluginHost.Device, file );

			var psi = new ProcessStartInfo ( ToolPath, temp.FullName );
			var process = new Process {
				StartInfo = psi,
				EnableRaisingEvents = true
			};
			process.Exited += (s, e) => {
				// push the changes back???
				if(process.ExitCode != 0 ) {
					this.LogWarn ( "Sqlite Browser Exited with code: {0}", process.ExitCode );
					return;
				}


				this.LogDebug ( "Sqlite Browser has exited: {0}", process.ExitCode );
				var question = this.PluginHost.ShowCommandBox ( "Push Database Changes?", 
					"Do you want to push the changes back to {0}?".With(this.PluginHost.Device), 
					"", "",
					"You can cause damage to applications if you alter the database files with invalid information.", "", 
					"No, I do not want to push the changes|Yes, push the changes", 
					true, // show cancel
					MessageBoxIcon.Question, 
					MessageBoxIcon.Warning );
        if ( question == 1 ) {
					var result = this.PluginHost.CommandRunner.PushFile ( temp.FullName, file );
					this.LogDebug ( "Push the changes from {0} -> {1}", temp.FullName, file );
				}
				try {
					this.LogDebug ( "Deleting: '{0}'", temp.FullName );
					temp.Delete ( );
				} catch ( Exception ex ) {
					this.LogError ( ex.Message, ex );
				}
			};
			process.Start ( );
		}
		#endregion



	}
}
