using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
	public abstract class FileSystemInfo {
		internal FileSystemInfo ( string name, long size, Permission userPermission, Permission groupPermission, Permission otherPermission, DateTime lastMod, bool isExec, string fullPath ) {
			this.Name = name;
			this.Size = size;
			this.UserPermissions = userPermission;
			this.GroupPermissions = groupPermission;
			this.OtherPermissions = otherPermission;
			this.LastModificationDateTime = lastMod;
			this.IsExecutable = isExec;
			this.FullPath = fullPath;
		}
		private long _size;
		private DateTime _lastModDate;
		private string _name;
		private Permission _userPermission;
		private Permission _otherPermission;
		private Permission _groupPermission;
		private bool _isLink;
		private bool _isDirectory;
		private bool _isExecutable;
		private bool _isPipe;
		private bool _isSocket;
		private string _fullPath;

		public string FullPath {
			get { return _fullPath; }
			protected set { _fullPath = value; }
		}
			
		public bool IsSocket {
			get { return _isSocket; }
			protected set { _isSocket = value; }
		}

		public bool IsPipe {
			get { return _isPipe; }
			protected set { _isPipe = value; }
		}
		
		public bool IsExecutable {
			get { return _isExecutable; }
			protected set { _isExecutable = value; }
		}
		
		public virtual bool IsDirectory {
			get { return _isDirectory; }
			protected set { _isDirectory = value; }
		}
		
		public virtual bool IsLink {
			get { return _isLink; }
			protected set { _isLink = value; }
		}
		
		public Permission GroupPermissions {
			get { return _groupPermission; }
			protected set { _groupPermission = value; }
		}
		
		public Permission OtherPermissions {
			get { return _otherPermission; }
			protected set { _otherPermission = value; }
		}
		
		public Permission UserPermissions {
			get { return _userPermission; }
			protected set { _userPermission = value; }
		}

		public string Name {
			get { return _name; }
			protected set { _name = value; }
		}

		public DateTime LastModificationDateTime {
			get { return _lastModDate; }
			protected set { _lastModDate = value; }
		}

		public long Size {
			get { return _size; }
			protected set { _size = value; }
		}

	}
}
