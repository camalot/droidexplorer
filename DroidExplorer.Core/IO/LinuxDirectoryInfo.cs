using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
	/// <summary>
	/// 
	/// </summary>
	public class LinuxDirectoryInfo {
		/// <summary>
		/// Initializes a new instance of the <see cref="LinuxDirectoryInfo"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		public LinuxDirectoryInfo ( string path ) {
      this.FullName = FixUpFullName ( path );
    }

		/// <summary>
		/// Gets the full name.
		/// </summary>
		/// <value>
		/// The full name.
		/// </value>
		public string FullName { get; private set; }
		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <value>
		/// The parent.
		/// </value>
		public LinuxDirectoryInfo Parent {
      get {
        return GetParentDirectoryInfo ( );
      }
    }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString ( ) {
      return this.FullName;
    }

		/// <summary>
		/// Fixes up full name.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
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

		/// <summary>
		/// Gets the parent directory information.
		/// </summary>
		/// <returns></returns>
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
