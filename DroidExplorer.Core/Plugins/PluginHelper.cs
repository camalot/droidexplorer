using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DroidExplorer.Core;
using System.Security.Cryptography;

namespace DroidExplorer.Core.Plugins {
	public static class PluginHelper {

		public static string GetPaidPluginApkSignature ( string device, string apk ) {
			try {
				MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider ( );

				System.IO.FileInfo localApk = System.IO.File.Exists ( apk ) ?
						new System.IO.FileInfo ( apk ) :
						CommandRunner.Instance.PullFile ( device, apk );

				if ( localApk.Exists ) {
					byte[] data = new byte[ 0 ];
					using ( System.IO.FileStream fs = new System.IO.FileStream ( localApk.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read ) ) {
						using ( System.IO.MemoryStream ms = new System.IO.MemoryStream ( ) ) {
							byte[] buffer = new byte[ 1024 ];
							int bytesRead = 0;
							while ( ( bytesRead = fs.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
								ms.Write ( buffer, 0, bytesRead );
					}
							data = ms.ToArray ( );
						}
					}
					byte[] bhash = md5.ComputeHash ( data );
					StringBuilder outHash = new StringBuilder ( );
					foreach ( byte b in bhash ) {
						outHash.Append ( String.Format ( "{0:x2}", b ) );
					}
					string hash = string.Format ( "{0}{1}", device, outHash.ToString() );
					return hash;
				} else {
					throw new System.IO.FileNotFoundException ( string.Format ( "Unable to create signature for {0}", apk ) );
				}
			} catch ( Exception ex ) {
				Logger.LogError ( typeof ( PluginHelper ), ex.Message, ex );
				throw;
			}
		}

		/// <summary>
		/// Creates the tool strip menu item for plugin.
		/// </summary>
		/// <param name="plugin">The plugin.</param>
		/// <returns></returns>
		public static ToolStripMenuItem CreateToolStripMenuItemForPlugin ( IPlugin plugin ) {
			Image img = plugin.Image == null ? DroidExplorer.Resources.Images.android : plugin.Image;
			ToolStripMenuItem tsmi = new ToolStripMenuItem ( plugin.Text, plugin.Image, delegate ( object sender, EventArgs e ) {
				try {
					plugin.Execute ( plugin.PluginHost, plugin.PluginHost.CurrentDirectory );
				} catch ( Exception ex ) {
					//throw;
					sender.LogError ( ex.Message, ex );
				}
			}, plugin.Name );

			// check for min tools version (there has to be a better way to check this)
			var minPT = plugin.MinimumSDKPlatformToolsVersion;
			var minT = plugin.MinimumSDKToolsVersion;
			var sdkv = 0;
			var sval = CommandRunner.Instance.GetProperty ( "ro.build.version.sdk" );
			int.TryParse ( sval, out sdkv );

			tsmi.Enabled = sdkv >= minT;

			return tsmi;
		}

		/// <summary>
		/// Creates the tool strip button for plugin.
		/// </summary>
		/// <param name="plugin">The plugin.</param>
		/// <returns></returns>
		public static ToolStripButton CreateToolStripButtonForPlugin ( IPlugin plugin ) {
			Image img = plugin.Image == null ? DroidExplorer.Resources.Images.android : plugin.Image;
			ToolStripButton tsb = new ToolStripButton ( plugin.Text, plugin.Image, delegate ( object sender, EventArgs e ) {
				
				try {
					plugin.Execute ( plugin.PluginHost, plugin.PluginHost.CurrentDirectory );
				} catch ( Exception ex ) {
					//throw;
					sender.LogError ( ex.Message, ex );
				}
			}, plugin.Name );
			tsb.DisplayStyle = plugin.DisplayStyle;

			// check for min tools version (there has to be a better way to check this)
			var minPT = plugin.MinimumSDKPlatformToolsVersion;
			var minT = plugin.MinimumSDKToolsVersion;
			var sdkv = 0;
			var sval = CommandRunner.Instance.GetProperty ( "ro.build.version.sdk" );
			int.TryParse ( sval, out sdkv );

			tsb.Enabled = sdkv >= minT;

			return tsb;
		}


		public static bool CanExecute ( IPlugin plugin ) {
			var minPT = plugin.MinimumSDKPlatformToolsVersion;
			var minT = plugin.MinimumSDKToolsVersion;
			var sdkv = 0;
			var sval = CommandRunner.Instance.GetProperty ("ro.build.version.sdk" );
			int.TryParse ( sval, out sdkv );
			return sdkv >= minPT;
		}
	}
}
