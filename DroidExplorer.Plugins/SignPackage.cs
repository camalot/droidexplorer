using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core.Plugins;
using System.Diagnostics;
using System.Windows.Forms;
using DroidExplorer.Core;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Threading;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
	public class SignPackage : BasePlugin {
		/// <summary>
		/// Initializes a new instance of the <see cref="SignPackage"/> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public SignPackage ( IPluginHost host ) : base(host) {

		}

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
			get { return DroidExplorer.Resources.Strings.ApplicationWebsiteUrl; }
		}

		/// <summary>
		/// Gets the contact.
		/// </summary>
		/// <value>The contact.</value>
		public override string Contact {
			get { return string.Empty; }
		}

		#endregion

		#region IPlugin Members

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public override string Name {
			get { return "SignPackage"; }
		}
		public override string Group { get { return "Tools"; } }

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>The description.</value>
		public override string Description {
			get { return "Signs a zip file with the test keys"; }
		}

		/// <summary>
		/// Gets the image.
		/// </summary>
		/// <value>The image.</value>
		public override System.Drawing.Image Image {
			get { return DroidExplorer.Resources.Images.LockControls_322; }
		}

		/// <summary>
		/// Gets the text.
		/// </summary>
		/// <value>The text.</value>
		public override string Text {
			get { return "Sign Package"; }
		}

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value><c>true</c> if [create tool button]; otherwise, <c>false</c>.</value>
		public override bool CreateToolButton {
			get { return true; }
		}
		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, DroidExplorer.Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			if ( RunningThread != null && RunningThread.IsAlive ) {
				return;
			}

			OpenFileDialog ofd = new OpenFileDialog ( );
			ofd.Title = "Select zip file to sign";
			ofd.Filter = "Zip files|*.zip|All Files (*.*)|*.*";
			ofd.FilterIndex = 0;
			ofd.CheckFileExists = true;
			if ( ofd.ShowDialog ( ) == DialogResult.OK ) {
				RunningThread = new Thread ( new ParameterizedThreadStart ( delegate ( object obj ) {
					if ( obj is string ) {
						SignZip ( ( string )obj );
					}
				} ) );
				RunningThread.Start ( ofd.FileName );
			}

		}
		#endregion

		/// <summary>
		/// Gets or sets the running thread.
		/// </summary>
		/// <value>The running thread.</value>
		private Thread RunningThread { get; set; }

		/// <summary>
		/// Signs the zip.
		/// </summary>
		/// <param name="file">The file.</param>
		private void SignZip ( string file ) {
			try {
				ExtractTools ( );

				FileInfo zipFile = new FileInfo ( file );
				zipFile.CopyTo ( Path.Combine ( FolderManagement.TempFolder, zipFile.Name ), true );

				FileInfo zipOutput = new FileInfo ( Path.Combine ( zipFile.Directory.FullName, string.Format ( CultureInfo.InvariantCulture, "{0}-signed.zip", Path.GetFileNameWithoutExtension ( zipFile.Name ) ) ) );
				FileInfo tempSigned = new FileInfo ( Path.Combine ( FolderManagement.TempFolder, zipOutput.Name ) );

				Process proc = new Process ( );
				ProcessStartInfo psi = new ProcessStartInfo ( "java.exe", string.Format ( CultureInfo.InvariantCulture, "-jar signapk.jar testkey.x509.pem testkey.pk8 {1} {0}", tempSigned.Name, zipFile.Name ) );
				psi.WorkingDirectory = FolderManagement.TempFolder;
				psi.CreateNoWindow = true;
				psi.WindowStyle = ProcessWindowStyle.Hidden;
				proc.StartInfo = psi;
				proc.Start ( );
				proc.WaitForExit ( );

				if ( tempSigned.Exists ) {
					tempSigned.CopyTo ( zipOutput.FullName, true );
				}
				string message = string.Format ( CultureInfo.InvariantCulture, "{0} has been signed successfully and saved as {1} in the same directory as the source", zipFile.Name, zipOutput.Name );
				MessageBox.Show ( message, "Signing Complete", MessageBoxButtons.OK, MessageBoxIcon.Information );
			} catch ( Exception ex ) {
				MessageBox.Show ( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
				this.LogError ( ex.Message, ex );
			}
		}

		/// <summary>
		/// Extracts the tools.
		/// </summary>
		private void ExtractTools ( ) {
			try {
				string zipFile = System.IO.Path.Combine ( FolderManagement.TempFolder, "SignApk.zip" );
				using ( System.IO.FileStream fs = new System.IO.FileStream ( zipFile, System.IO.FileMode.Create, System.IO.FileAccess.Write ) ) {
					fs.Write ( Properties.Resources.SignApk_zip, 0, Properties.Resources.SignApk_zip.Length );
				}
				FastZip zip = new FastZip ( );
				zip.ExtractZip ( zipFile, FolderManagement.TempFolder, string.Empty );
			} catch ( Exception ) {
				throw;
			}
		}
	}
}
