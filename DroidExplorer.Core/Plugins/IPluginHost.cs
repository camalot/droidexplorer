using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DroidExplorer.Core.IO;

namespace DroidExplorer.Core.Plugins {
  public interface IPluginHost {
		/// <summary>
		/// Pulls the specified remote file.
		/// </summary>
		/// <param name="remoteFile">The remote file.</param>
		/// <param name="destFile">The dest file.</param>
		void Pull ( DroidExplorer.Core.IO.FileInfo remoteFile, System.IO.FileInfo destFile );
		/// <summary>
		/// Pulls the specified files.
		/// </summary>
		/// <param name="files">The files.</param>
		/// <param name="destPath">The dest path.</param>
		void Pull ( List<string> files, System.IO.DirectoryInfo destPath );
		/// <summary>
		/// Pulls the specified files.
		/// </summary>
		/// <param name="files">The files.</param>
		/// <param name="destPath">The dest path.</param>
		void Pull ( List<DroidExplorer.Core.IO.FileInfo> files, System.IO.DirectoryInfo destPath );

		/// <summary>
		/// Pushes the specified files.
		/// </summary>
		/// <param name="files">The files.</param>
		/// <param name="destPath">The dest path.</param>
		void Push ( List<System.IO.FileInfo> files, string destPath );
		/// <summary>
		/// Pushes the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="remote">The remote.</param>
		void Push ( System.IO.FileInfo file, string remote );

		/// <summary>
		/// Navigates the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		void Navigate ( DroidExplorer.Core.IO.LinuxDirectoryInfo path );

		/// <summary>
		/// Installs the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		void Install ( System.IO.FileInfo file );
		/// <summary>
		/// Uninstalls the specified package.
		/// </summary>
		/// <param name="package">The package.</param>
		void Uninstall ( string package );

		/// <summary>
		/// Gets or sets the top.
		/// </summary>
		/// <value>
		/// The top.
		/// </value>
		int Top { get; set; }
		/// <summary>
		/// Gets or sets the left.
		/// </summary>
		/// <value>
		/// The left.
		/// </value>
		int Left { get; set; }
		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>
		/// The height.
		/// </value>
		int Height { get; set; }
		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>
		/// The width.
		/// </value>
		int Width { get; set; }
		/// <summary>
		/// Gets the bounds.
		/// </summary>
		/// <value>
		/// The bounds.
		/// </value>
		Rectangle Bounds { get; }
		/// <summary>
		/// Gets the right.
		/// </summary>
		/// <value>
		/// The right.
		/// </value>
		int Right { get; }
		/// <summary>
		/// Gets the bottom.
		/// </summary>
		/// <value>
		/// The bottom.
		/// </value>
		int Bottom { get; }
		/// <summary>
		/// Shows the command box.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="main">The main.</param>
		/// <param name="content">The content.</param>
		/// <param name="expandedInfo">The expanded information.</param>
		/// <param name="footer">The footer.</param>
		/// <param name="verification">The verification.</param>
		/// <param name="buttons">The buttons.</param>
		/// <param name="showCancel">if set to <c>true</c> [show cancel].</param>
		/// <param name="icon">The icon.</param>
		/// <param name="footerIcon">The footer icon.</param>
		/// <returns></returns>
		int ShowCommandBox ( string title, string main, string content, string expandedInfo, string footer, string verification, string buttons, bool showCancel, MessageBoxIcon icon, MessageBoxIcon footerIcon );
		/// <summary>
		/// Messages the box.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="main">The main.</param>
		/// <param name="content">The content.</param>
		/// <param name="buttons">The buttons.</param>
		/// <param name="icon">The icon.</param>
		/// <returns></returns>
		DialogResult MessageBox ( string title, string main, string content, MessageBoxButtons buttons, MessageBoxIcon icon );

		/// <summary>
		/// Gets the command runner.
		/// </summary>
		/// <value>
		/// The command runner.
		/// </value>
		CommandRunner CommandRunner { get; }

		/// <summary>
		/// Registers the file type handler.
		/// </summary>
		/// <param name="ext">The ext.</param>
		/// <param name="handler">The handler.</param>
		void RegisterFileTypeHandler ( string ext, IFileTypeHandler handler );
		/// <summary>
		/// Unregisters the file type handler.
		/// </summary>
		/// <param name="ext">The ext.</param>
		/// <param name="handler">The handler.</param>
		void UnregisterFileTypeHandler ( string ext, IFileTypeHandler handler );
		/// <summary>
		/// Registers the file type icon.
		/// </summary>
		/// <param name="ext">The ext.</param>
		/// <param name="smallImage">The small image.</param>
		/// <param name="largeImage">The large image.</param>
		void RegisterFileTypeIcon ( string ext, Image smallImage, Image largeImage );
		/// <summary>
		/// Registers the file type icon handler.
		/// </summary>
		/// <param name="ext">The ext.</param>
		/// <param name="handler">The handler.</param>
		void RegisterFileTypeIconHandler (string ext, IFileTypeIconHandler handler);

		/// <summary>
		/// Gets the host window.
		/// </summary>
		/// <returns></returns>
		IWin32Window GetHostWindow ( );
		/// <summary>
		/// Gets the host control.
		/// </summary>
		/// <returns></returns>
		Control GetHostControl ( );

		/// <summary>
		/// Gets the name of the device friendly.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns></returns>
		string GetDeviceFriendlyName ( string device );
		/// <summary>
		/// Sets the name of the device friendly.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="name">The name.</param>
		void SetDeviceFriendlyName ( string device, string name );
		/// <summary>
		/// Gets the current directory.
		/// </summary>
		/// <value>
		/// The current directory.
		/// </value>
		LinuxDirectoryInfo CurrentDirectory { get;}
		/// <summary>
		/// Gets the device.
		/// </summary>
		/// <value>
		/// The device.
		/// </value>
		string Device { get; }
  }
}
