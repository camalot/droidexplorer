using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Configuration;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;
using System.IO;
using DroidExplorer.Plugins.UI;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
	public class BackupPacker : BasePlugin {

		/// <summary>
		/// Initializes a new instance of the <see cref="BackupPacker" /> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public BackupPacker ( IPluginHost host ) : base ( host ) {

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
			get { return DroidExplorer.Resources.Strings.ApplicationWebsiteUrl; }
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
			get { return "Backup Unpacker"; }
		}
		public override string Group { get { return "Applications and Data"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
			get { return "Ability to extract Android Backups"; }
		}


		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public override bool CreateToolButton {
			get { return false; }
		}


		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			var arguments = new Arguments ( args );

			if ( !HasRequiredArguments ( arguments ) ) {
				this.LogError ( "Missing required arguments" );
			}

			// get password


			StringBuilder argumentBuilder = new StringBuilder ( );
			var abe = System.IO.Path.Combine ( Settings.Instance.ToolsDirectory, "abe.jar" );
			argumentBuilder.AppendFormat ( "-jar {0} ", abe.QuoteIfHasSpace ( ) );

			bool isPack = false;
			var tar = String.Empty;
			var backup = String.Empty;
			var workingDir = String.Empty;
			if ( arguments.Contains ( "p", "pack" ) ) {
				argumentBuilder.Append ( "pack " );
				tar = Path.GetFullPath ( arguments["tar"] );
				workingDir = Path.GetFullPath ( Path.GetDirectoryName ( tar ) );
				backup = Path.GetFullPath ( System.IO.Path.Combine ( workingDir, System.IO.Path.GetFileNameWithoutExtension ( tar ) + ".ab" ) );
				isPack = true;
			} else if ( arguments.Contains ( "u", "unpack" ) ) {
				backup = Path.GetFullPath ( arguments["b", "backup"] );
				var bc = new BackupConverter ( this.PluginHost );
				if ( bc.IsExtendedBackup ( backup ) ) {
					argumentBuilder.Append ( "unpackex " );
				} else {
					argumentBuilder.Append ( "unpack " );
				}
				workingDir = Path.GetFullPath ( Path.GetDirectoryName ( backup ) );
				tar = Path.GetFullPath ( System.IO.Path.Combine ( workingDir, System.IO.Path.GetFileNameWithoutExtension ( backup ) + ".tar" ) );
				isPack = false;
			}

			argumentBuilder.AppendFormat ( "{0} {1} ",
				( isPack ? tar : backup ).QuoteIfHasSpace ( ),
				( isPack ? backup : tar ).QuoteIfHasSpace ( ) );

			var password = String.Empty;
			if ( arguments.Contains ( "pass", "password" ) ) {
				password = arguments["pass", "password"];
			} else {
				// show password dialog
				var ppf  = new PackPasswordForm() {
					Text = isPack ? "Pack Password" : "Unpack Password"
				};
				if ( ppf.ShowDialog() == DialogResult.OK ) {
					password = ppf.Password;
				}
			}

			if ( !String.IsNullOrEmpty ( password ) ) {
				argumentBuilder.Append( password );
			}

			this.LogDebug ( "running: java.exe {0}", argumentBuilder.ToString ( ) );

			var proc = new Process {
				StartInfo = new ProcessStartInfo {
					Arguments = argumentBuilder.ToString ( ).Trim ( ),
					WindowStyle = ProcessWindowStyle.Hidden,
					FileName = "java.exe",
					WorkingDirectory = Environment.CurrentDirectory,
					CreateNoWindow = true
				}
			};

			proc.Start ( );
		}

		/// <summary>
		/// Indicates the minimum SDK Tools Version that is required for this plugin. If no requirement, then default the value to 0.
		/// </summary>
		public override int MinimumSDKToolsVersion {
			get { return 13; }
		}
		/// <summary>
		/// Indicates the minimum SDK Platform Tools Version that is required for this plugin. If no requirement, then default the value to 0.
		/// </summary>
		public override int MinimumSDKPlatformToolsVersion {
			get { return 13; }
		}

		#endregion

		private bool HasRequiredArguments ( Arguments args ) {
			return ( args.Contains ( "u", "unpack" ) && args.Contains ( "b", "backup" ) ) ||
				( args.Contains ( "p", "pack" ) && args.Contains ( "tar" ) );

		}
	}
}
