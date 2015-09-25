using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroidExplorer.Components;
using DroidExplorer.Core;
using DroidExplorer.Core.IO;
using DroidExplorer.Core.UI;

namespace DroidExplorer {
	public class ApkCacheBuilder {
		public async Task RunAsync() {
			var apks = CommandRunner.Instance.GetInstalledPackagesApkInformation ( );
			this.LogDebug("Initializing APK Cache");
			foreach(var item in apks) {
				var lvi = new ApkPackageListViewItem(item);

				// cant uninstall if we dont know the package
				if(string.IsNullOrEmpty(lvi.ApkInformation.Package)) {
					continue;
				}

				string keyName = lvi.ApkInformation.DevicePath;
				if(keyName.StartsWith("/")) {
					keyName = keyName.Substring(1);
				}
				keyName = keyName.Replace("/", ".");

				if(!Program.SystemIcons.ContainsKey(keyName)) {
					// get apk and extract the app icon
					var img = CommandRunner.Instance.GetLocalApkIconImage(item.LocalApk);

					if(img == null) {
						img = DroidExplorer.Resources.Images.package32;
					} else {
						using(System.IO.MemoryStream stream = new System.IO.MemoryStream()) {
							string fileName = System.IO.Path.Combine(System.IO.Path.Combine(CommandRunner.Settings.UserDataDirectory, Cache.APK_IMAGE_CACHE), string.Format("{0}.png", keyName));
							img.Save(stream, ImageFormat.Png);
							stream.Position = 0;
							using(System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)) {
								byte[] buffer = new byte[2048];
								int readBytes = 0;
								while((readBytes = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0) {
									await fs.WriteAsync(buffer, 0, readBytes);
								}
							}
						}

					}
					//SystemImageListHost.Instance.AddFileTypeImage ( keyName, img, img );

				}
			}
		}
	}
}
