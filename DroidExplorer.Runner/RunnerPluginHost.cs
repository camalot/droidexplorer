using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Configuration;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI;

namespace DroidExplorer.Runner {
	public class RunnerPluginHost : IPluginHost {

		public RunnerPluginHost ( String device ) {
			if ( String.IsNullOrEmpty(device) ){
				device = Core.CommandRunner.Instance.DefaultDevice;
			}
			Device = device;

			CommandRunner.DefaultDevice = device;
		}
		#region IPluginHost Members

		public void Pull ( Core.IO.FileInfo remoteFile, System.IO.FileInfo destFile ) {
			var transfer = new TransferDialog ( );
			transfer.PullDialog ( remoteFile, destFile );
		}

		public void Pull ( List<string> files, System.IO.DirectoryInfo destPath ) {
			var transfer = new TransferDialog ( );
			transfer.PullDialog ( files, destPath );
		}

		public void Pull ( List<Core.IO.FileInfo> files, System.IO.DirectoryInfo destPath ) {
			var transfer = new TransferDialog ( );
			transfer.PullDialog ( files, destPath );
		}

		public void Push ( List<System.IO.FileInfo> files, string destPath ) {
			var transfer = new TransferDialog ( );
			transfer.PushDialog ( files, destPath );
		}

		public void Push ( System.IO.FileInfo file, string remote ) {
			var transfer = new TransferDialog ( );
			transfer.PushDialog ( file, remote);
		}

		public void Navigate ( Core.IO.LinuxDirectoryInfo path ) {
			// does nothing
		}

		public void Install ( System.IO.FileInfo file ) {
			this.CommandRunner.InstallApk ( this.Device, file.FullName );
		}

		public void Uninstall ( string package ) {
			this.CommandRunner.UninstallApk ( this.Device, package );
		}

		public int Top { get; set; }

		public int Left { get; set; }

		public int Height { get; set; }

		public int Width { get; set; }

		public System.Drawing.Rectangle Bounds {
			get { return new System.Drawing.Rectangle ( 0, 0, 0, 0 ); }
		}

		public int Right {
			get { return 0; }
		}

		public int Bottom {
			get { return 0; }
		}

		public int ShowCommandBox ( string title, string main, string content, string expandedInfo, string footer, string verification, string buttons, bool showCancel, System.Windows.Forms.MessageBoxIcon icon, System.Windows.Forms.MessageBoxIcon footerIcon ) {
			return 0;
		}

		public System.Windows.Forms.DialogResult MessageBox ( string title, string main, string content, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon ) {
			return System.Windows.Forms.DialogResult.OK;
		}

		public Core.CommandRunner CommandRunner {
			get { return Core.CommandRunner.Instance; }
		}

		public void RegisterFileTypeHandler ( string ext, IFileTypeHandler handler ) {
			// does nothing
		}

		public void UnregisterFileTypeHandler ( string ext, IFileTypeHandler handler ) {
			// does nothing
		}

		public void RegisterFileTypeIcon ( string ext, System.Drawing.Image smallImage, System.Drawing.Image largeImage ) {
			// does nothing
		}

		public void RegisterFileTypeIconHandler(string ext, IFileTypeIconHandler handler) {
			// does nothing
		}


		public System.Windows.Forms.IWin32Window GetHostWindow ( ) {
			return null;
		}

		public System.Windows.Forms.Control GetHostControl ( ) {
			return null;
		}

		public string GetDeviceFriendlyName ( string device ) {
			return KnownDeviceManager.Instance.GetDeviceFriendlyName ( device );
		}

		public void SetDeviceFriendlyName ( string device, string name ) {
			KnownDeviceManager.Instance.SetDeviceFriendlyName ( device, name );
		}

		public Core.IO.LinuxDirectoryInfo CurrentDirectory {
			get { return new Core.IO.LinuxDirectoryInfo ( "/" ); }
		}

		public string Device {get; private set; }

		#endregion
	}
}
