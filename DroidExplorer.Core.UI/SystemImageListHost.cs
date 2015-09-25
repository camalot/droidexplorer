using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DroidExplorer.Core.IO;

namespace DroidExplorer.Core.UI {
	public class SystemImageListHost {

		private static SystemImageListHost _instance = null;
		private Dictionary<string, int> _systemIcons = null;
		private ImageList _smallImageList = null;
		private ImageList _largeImageList = null;


		private SystemImageListHost() {
			SystemIcons = new Dictionary<string, int>();
			SmallSystemImageList = new SystemImageList(SystemImageListSize.SmallIcons);
			LargeSystemImageList = new SystemImageList(SystemImageListSize.LargeIcons);
		}

		public SystemImageList SmallSystemImageList { get; private set; }
		public SystemImageList LargeSystemImageList { get; private set; }

		public ImageList SmallImageList {
			get {
				if(_smallImageList == null) {
					_smallImageList = new ImageList();
					_smallImageList.ImageSize = new Size(16, 16);
					_smallImageList.ColorDepth = ColorDepth.Depth32Bit;
					LoadCachedApkIconsToImageList(_smallImageList);
				}
				return _smallImageList;
			}
		}

		public ImageList LargeImageList {
			get {
				if(_largeImageList == null) {
					_largeImageList = new ImageList();
					_largeImageList.ImageSize = new Size(32, 32);
					_largeImageList.ColorDepth = ColorDepth.Depth32Bit;
					LoadCachedApkIconsToImageList(_largeImageList);
				}
				return _largeImageList;
			}
		}

		public Dictionary<string, int> SystemIcons {
			get;
			private set;
		}

		public void AddFileTypeImage(string ext, Image smallImage, Image largeImage) {
			if(SystemIcons.ContainsKey(ext)) {
				SystemIcons.Remove(ext);
			}

			SystemIcons.Add(ext, SmallImageList.Images.Count);
			AlphaImageList.AddFromImage(smallImage, SmallImageList);
			AlphaImageList.AddFromImage(largeImage, LargeImageList);
		}


		private void LoadCachedApkIconsToImageList(ImageList il) {
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.folder_Closed_16xLG, SmallImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.folder_Closed_16xLG_Link, SmallImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.text_document_16, SmallImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.application_16xLG, SmallImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.text_document_16_link, SmallImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.application_16xLG_Link, SmallImageList);


			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.folder_Closed_32xLG, LargeImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.folder_Closed_32xLG_Link, LargeImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.text_document_32, LargeImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.application_32xLG, LargeImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.text_document_32_Link, LargeImageList);
			AlphaImageList.AddFromImage((Image)DroidExplorer.Resources.Images.application_32xLG_Link, LargeImageList);

			AddFileTypeImage(".txt", (Image)DroidExplorer.Resources.Images.text_document_16, (Image)DroidExplorer.Resources.Images.text_document_32);
			AddFileTypeImage(".prop", (Image)DroidExplorer.Resources.Images.text_document_16, (Image)DroidExplorer.Resources.Images.text_document_32);
			AddFileTypeImage(".log", (Image)DroidExplorer.Resources.Images.PickAxe_16xLG, (Image)DroidExplorer.Resources.Images.PickAxe_32xLG);
			AddFileTypeImage(".sh", (Image)DroidExplorer.Resources.Images.shellscript_16xLG, (Image)DroidExplorer.Resources.Images.shellscript_32xLG);
			AddFileTypeImage(".csv", (Image)DroidExplorer.Resources.Images.text_document_16, (Image)DroidExplorer.Resources.Images.text_document_32);
			AddFileTypeImage(".so", (Image)DroidExplorer.Resources.Images.library_16xLG, (Image)DroidExplorer.Resources.Images.library_32xLG);
			AddFileTypeImage(".dex", (Image)DroidExplorer.Resources.Images.package, (Image)DroidExplorer.Resources.Images.package32);
			AddFileTypeImage(".odex", (Image)DroidExplorer.Resources.Images.package, (Image)DroidExplorer.Resources.Images.package32);
			AddFileTypeImage(".sqlite", (Image)DroidExplorer.Resources.Images.database_16xLG, (Image)DroidExplorer.Resources.Images.database_32xLG);
			AddFileTypeImage(".db", (Image)DroidExplorer.Resources.Images.database_16xLG, (Image)DroidExplorer.Resources.Images.database_32xLG);
			AddFileTypeImage(".database", (Image)DroidExplorer.Resources.Images.database_16xLG, (Image)DroidExplorer.Resources.Images.database_32xLG);

			// apk cache icons
			var path = System.IO.Path.Combine(CommandRunner.Settings.UserDataDirectory, Cache.APK_IMAGE_CACHE);
			System.IO.DirectoryInfo apkCache = new System.IO.DirectoryInfo(path);
			if(!apkCache.Exists) {
				apkCache.Create();
			}

			foreach(System.IO.FileInfo fi in apkCache.GetFiles("*.png")) {
				Image image = Image.FromFile(fi.FullName);
				AddFileTypeImage(System.IO.Path.GetFileNameWithoutExtension(fi.Name).ToLower(), image, image);
			}

			// file cache icons
			path = System.IO.Path.Combine(CommandRunner.Settings.UserDataDirectory, Cache.ICON_IMAGE_CACHE);
			System.IO.DirectoryInfo cache = new System.IO.DirectoryInfo(path);
			if(!cache.Exists) {
				cache.Create();
			}

			foreach(System.IO.FileInfo fi in cache.GetFiles("*.png").Union(cache.GetFiles("*.jpg"))) {
				var image = Image.FromFile(fi.FullName);
				AddFileTypeImage(System.IO.Path.GetFileNameWithoutExtension(fi.Name).ToLower(), image, image);
			}
		}

		#region static methods / properties
		public static SystemImageListHost Instance {
			get {
				if(_instance == null) {
					_instance = new SystemImageListHost();
				}
				return _instance;
			}
		}
		#endregion
	}
}
