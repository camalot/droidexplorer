using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
  public class LinuxDirectoryInfo {
    public LinuxDirectoryInfo ( string path ) {
      this.FullName = FixUpFullName ( path );
    }

    public string FullName { get; private set; }
    public LinuxDirectoryInfo Parent {
      get {
        return GetParentDirectoryInfo ( );
      }
    }

    public string Name { get; set; }

    public override string ToString ( ) {
      return this.FullName;
    }

    private string FixUpFullName ( string path ) {
      string sb = path;
      sb = sb.Replace ( System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar );
      if ( !sb.StartsWith ( new String ( new char[ ] { System.IO.Path.AltDirectorySeparatorChar } ) ) ) {
        sb = string.Format ( ".{0}{1}", System.IO.Path.AltDirectorySeparatorChar, sb );
      }

      if ( !sb.EndsWith ( new String ( new char[ ] { System.IO.Path.AltDirectorySeparatorChar } ) ) ) {
        sb = string.Format ( "{0}{1}", sb, System.IO.Path.AltDirectorySeparatorChar );
      }

      return sb;
    }

    private LinuxDirectoryInfo GetParentDirectoryInfo ( ) {
      string tpath = this.FullName.Substring ( 0, this.FullName.Length - 1 );
      int lastIndex= tpath.LastIndexOf ( System.IO.Path.AltDirectorySeparatorChar ) + 1;
      if ( ( lastIndex <= 0 && tpath.StartsWith ( "." ) ) || tpath.Length == 0 || ( tpath.Length == 1 && string.Compare ( tpath, new string(new char[] { System.IO.Path.AltDirectorySeparatorChar } ), true ) == 0 ) ) {
        return null;
      }
      string parentPath = tpath.Substring ( 0, lastIndex );
      if ( string.Compare ( parentPath, ".", false ) == 0 ) {
        return null;
      }
      return new LinuxDirectoryInfo ( FixUpFullName ( parentPath ) );
    }
  }
}
