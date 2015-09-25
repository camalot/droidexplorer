using System;
using System.Collections.Generic;
using System.Text;
using DroidExplorer.Core;
using DroidExplorer.Core.UI.Components;

namespace DroidExplorer.Core.UI.Components {
	public class ApkFileSystemInfoListViewItem : FileSystemInfoListViewItem {
		private string _fileName;
		private AaptBrandingCommandResult _apkInfo;

		public ApkFileSystemInfoListViewItem ( DroidExplorer.Core.IO.FileSystemInfo fsi, AaptBrandingCommandResult apkInfo )
			: base ( fsi ) {
			this.ApkInfo = apkInfo;
			if ( !string.IsNullOrEmpty ( this.ApkInfo.Label ) ) {
				this.Text = ApkInfo.Label;
			} else {
				this.Text = fsi.Name;
			}
			this.FileName = fsi.Name;
		}

		public ApkFileSystemInfoListViewItem ( DroidExplorer.Core.IO.FileSystemInfo fsi, int imageIndex, AaptBrandingCommandResult apkInfo )
			: this ( fsi, apkInfo ) {
			this.ImageIndex = imageIndex;
		}

		public string FileName { get { return this._fileName; } private set { this._fileName = value; } }

		public string Version {
			get { return this.ApkInfo.Version; }
		}

		public string Label {
			get { return this.ApkInfo.Label; }
		}

		public string Package {
			get { return this.ApkInfo.Package; }
		}

		public string Icon {
			get { return this.ApkInfo.Icon; }
		}

		public string LocalApk {
			get { return this.ApkInfo.LocalApk; }
		}

		public AaptBrandingCommandResult ApkInfo {
			get { return this._apkInfo; }
			private set { this._apkInfo = value; }
		}
	}
}
