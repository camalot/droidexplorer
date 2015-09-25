using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroidExplorer.Core;
using DroidExplorer.Core.IO;
using DroidExplorer.Core.Plugins;
using System.Drawing;
using DroidExplorer.Core.UI.Components;

namespace DroidExplorer.Plugins {
	public class ApkExtension : BasePlugin, IFileTypeIconHandler {
		/// <summary>
		/// Initializes a new instance of the <see cref="Shell"/> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public ApkExtension(IPluginHost host)
			: base(host) {

			if ( host != null ) {
				this.PluginHost.RegisterFileTypeIconHandler(".apk", this);
			}
		}


		public void Open(Core.IO.FileInfo file) {
			
		}

		public override string Author {
			get { return "Ryan Conrad"; }
		}

		public override string Url {
			get { return DroidExplorer.Resources.Strings.ApplicationWebsiteUrl; }
		}

		public override string Contact {
			get { return string.Empty; }
		}

		public override string Name {
			get { return "Apk Extension"; }
		}

		public override string Group {
			get { return "Images"; }
		}

		public override string Description {
			get { return "Handles apk icons"; }
		}

		public override bool CreateToolButton {
			get { return false; }
		}

		public override void Execute(IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args) {
			throw new NotImplementedException();
		}

		public override bool Runnable { get { return false; } }


		public System.Drawing.Image GetLargeImage(Core.IO.FileSystemInfo file) {


			/*String cacheDir = System.IO.Path.Combine(CommandRunner.Settings.UserDataDirectory, Cache.APK_IMAGE_CACHE);

				AaptBrandingCommandResult result = CommandRunner.Instance.GetApkInformationFromLocalCache(item.FullPath, cacheDir);
				lvi = new ApkFileSystemInfoListViewItem(item, result);

				keyName = lvi.FileSystemInfo.FullPath;
				if(keyName.StartsWith("/")) {
					keyName = keyName.Substring(1);
				}
				keyName = keyName.Replace("/", ".");

				if(!SystemImageListHost.Instance.SystemIcons.ContainsKey(keyName)) {
					// get apk and extract the app icon
					Image img = CommandRunner.Instance.GetApkIconImageFromCache(lvi.FileSystemInfo.FullPath, cacheDir);

					if(img == null) {
						img = DroidExplorer.Resources.Images.apk32;
					}
					SystemImageListHost.Instance.AddFileTypeImage(keyName, img, img);
				}

				if(this.InvokeRequired) {
					this.Invoke(new SetListViewItemImageIndexDelegate(this.SetListViewItemImageIndex), new object[] { lvi, Program.SystemIcons[keyName] });
				} else {
					SetListViewItemImageIndex(lvi, Program.SystemIcons[keyName]);
				}
			 */



			var key = GetKeyName(file);
			if(!Cache.Exists(Cache.APK_IMAGE_CACHE, key)) {
				var img = CommandRunner.Instance.GetApkIconImage(file.FullPath);
				if(img == null) {
					return DefaultLargeImage;
				}
				var path = Cache.GetPath(Cache.APK_IMAGE_CACHE);
				var cacheFile = Cache.Save(System.IO.Path.Combine(path, key), img);
				return Image.FromFile(cacheFile.FullName).Resize(32, 32);
			} else {
				return System.Drawing.Image.FromFile(Cache.Get(Cache.APK_IMAGE_CACHE, key).FullName);
			}
		}

		public System.Drawing.Image GetSmallImage(Core.IO.FileSystemInfo file) {
			return GetLargeImage(file).Resize(16, 16);
		}

		public string GetKeyName(Core.IO.FileSystemInfo file) {
			return Cache.GetCacheKey(file);
		}


		private Image DefaultLargeImage {
			get {
				return DroidExplorer.Resources.Images.apk32;
			}
		}

		private Image DefaultSmallImage {
			get {
				return DroidExplorer.Resources.Images.apk16;
			}
		}

		public System.Windows.Forms.ListViewItem GetListViewItem(FileSystemInfo fsi) {
			var path = Cache.GetPath(Cache.APK_IMAGE_CACHE);
			var result = CommandRunner.Instance.GetApkInformationFromLocalCache(fsi.FullPath, path);
			return new ApkFileSystemInfoListViewItem(fsi, result);
		}
	}
}
