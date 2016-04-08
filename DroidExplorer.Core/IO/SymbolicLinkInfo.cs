using System;
using System.Collections.Generic;
using System.Text;
using Managed.Adb;

namespace DroidExplorer.Core.IO {
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="DroidExplorer.Core.IO.FileSystemInfo" />
	public class SymbolicLinkInfo : FileSystemInfo {
		private string _link;

		/// <summary>
		/// Initializes a new instance of the <see cref="SymbolicLinkInfo"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="link">The link.</param>
		/// <param name="size">The size.</param>
		/// <param name="userPermission">The user permission.</param>
		/// <param name="groupPermission">The group permission.</param>
		/// <param name="otherPermission">The other permission.</param>
		/// <param name="lastMod">The last mod.</param>
		/// <param name="isDirectory">if set to <c>true</c> [is directory].</param>
		/// <param name="isExec">if set to <c>true</c> [is execute].</param>
		/// <param name="fullPath">The full path.</param>
		internal SymbolicLinkInfo ( string name, string link, long size, FilePermission userPermission, FilePermission groupPermission, FilePermission otherPermission, DateTime lastMod, bool isDirectory, bool isExec, string fullPath )
			: base ( name, size, userPermission, groupPermission, otherPermission, lastMod, isExec,fullPath ) {
			this.IsDirectory = isDirectory;
			this.Link = link;
		}


		/// <summary>
		/// Gets or sets the link.
		/// </summary>
		/// <value>
		/// The link.
		/// </value>
		public string Link {
			get { return _link; }
			set { _link = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is link.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is link; otherwise, <c>false</c>.
		/// </value>
		public override bool IsLink {
			get { return true; }
			protected set { return; }
		}
	}
}
