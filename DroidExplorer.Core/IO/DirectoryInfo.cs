using System;
using System.Collections.Generic;
using System.Text;
using Managed.Adb;

namespace DroidExplorer.Core.IO {
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="DroidExplorer.Core.IO.FileSystemInfo" />
	public class DirectoryInfo : FileSystemInfo {
		internal DirectoryInfo ( string name, long size, FilePermission userPermission, FilePermission groupPermission, FilePermission otherPermission, DateTime lastMod, string fullPath )
			: base ( name, size, userPermission, groupPermission, otherPermission, lastMod, false,fullPath ) {
		}


		/// <summary>
		/// Gets or sets a value indicating whether this instance is directory.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is directory; otherwise, <c>false</c>.
		/// </value>
		public override bool IsDirectory {
			get { return true; }
			protected set { return; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is link.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is link; otherwise, <c>false</c>.
		/// </value>
		public override bool IsLink {
			get { return false; }
			protected set { return; }
		}


		/// <summary>
		/// Creates the root.
		/// </summary>
		/// <returns></returns>
		public static DirectoryInfo CreateRoot ( ) {
			return new DirectoryInfo ( "/", 0, new FilePermission ( "rwx" ), new FilePermission ( "rwx" ), new FilePermission ( "rwx" ), DateTime.Now, "/" );
		}
	}
}
