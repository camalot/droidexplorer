using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DroidExplorer.Core.IO;

namespace DroidExplorer.Core.Plugins {
  public interface IPluginHost {
    void Pull ( DroidExplorer.Core.IO.FileInfo remoteFile, System.IO.FileInfo destFile );
    void Pull ( List<string> files, System.IO.DirectoryInfo destPath );
    void Pull ( List<DroidExplorer.Core.IO.FileInfo> files, System.IO.DirectoryInfo destPath );

    void Push ( List<System.IO.FileInfo> files, string destPath );
    void Push ( System.IO.FileInfo file, string remote );

    void Navigate ( DroidExplorer.Core.IO.LinuxDirectoryInfo path );

    void Install ( System.IO.FileInfo file );
    void Uninstall ( string package );

    int Top { get; set; }
    int Left { get; set; }
    int Height { get; set; }
    int Width { get; set; }
    Rectangle Bounds { get; }
    int Right { get; }
    int Bottom { get; }
    int ShowCommandBox ( string title, string main, string content, string expandedInfo, string footer, string verification, string buttons, bool showCancel, MessageBoxIcon icon, MessageBoxIcon footerIcon );
    DialogResult MessageBox ( string title, string main, string content, MessageBoxButtons buttons, MessageBoxIcon icon );

    CommandRunner CommandRunner { get; }

    void RegisterFileTypeHandler ( string ext, IFileTypeHandler handler );
    void UnregisterFileTypeHandler ( string ext, IFileTypeHandler handler );
    void RegisterFileTypeIcon ( string ext, Image smallImage, Image largeImage );
		void RegisterFileTypeIconHandler(string ext, IFileTypeIconHandler handler);

    IWin32Window GetHostWindow ( );
    Control GetHostControl ( );

    string GetDeviceFriendlyName ( string device );
    void SetDeviceFriendlyName ( string device, string name );
    LinuxDirectoryInfo CurrentDirectory { get;}
		String Device { get; }
  }
}
