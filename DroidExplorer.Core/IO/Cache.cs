using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.IO {
	public static class Cache {
		public const string APK_IMAGE_CACHE = @"assets\apk";
		public const string ICON_IMAGE_CACHE = @"assets\icons";

		public static string GetCacheKey(FileSystemInfo file) {
			return GetCacheKey(file.FullPath);
		}

		public static string GetCacheKey(string remoteFilePath) {
			var fullName = remoteFilePath.REReplace(@"\/|\:", ".");
			if(fullName.StartsWith(".")) {
				fullName = fullName.Substring(1);
			}
			return fullName;
		}

		public static System.IO.FileInfo MoveTo(string path, string localFile) {
			return MoveTo(path, new System.IO.FileInfo(localFile));
		}

		public static System.IO.FileInfo MoveTo(string path, System.IO.FileInfo file) {
			var root = CommandRunner.Settings.UserDataDirectory;
			var fullCachePath = System.IO.Path.Combine(root,path,file.Name);
			file.MoveTo(fullCachePath);
			return new System.IO.FileInfo(fullCachePath);
		}

		public static System.IO.FileInfo Save(string filePath, System.Drawing.Image image) {
			image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
			return new System.IO.FileInfo(filePath);
		}

		public static string GetPath(string path) {
			var root = CommandRunner.Settings.UserDataDirectory;
			return System.IO.Path.Combine(root, path);
		}



		//public static System.IO.FileInfo MoveTo(string path, FileInfo file) {
		//	var temp = CommandRunner.Instance.PullFile(file.FullPath);
		//	return MoveTo(path, temp);
		//}

		public static bool Exists(string path, System.IO.FileInfo file) {
			var root = CommandRunner.Settings.UserDataDirectory;
			var fullCachePath = System.IO.Path.Combine(root, path, file.Name);
			return System.IO.File.Exists(fullCachePath);
		}

		public static bool Exists(string path, string fileName) {
			var root = CommandRunner.Settings.UserDataDirectory;
			var fullCachePath = System.IO.Path.Combine(root, path, fileName);
			return System.IO.File.Exists(fullCachePath);
		}

		//public static bool Exists(string path, System.IO.FileInfo file) {

		//}

		public static System.IO.FileInfo Get(string path, string fileName) {
			if(Exists(path, fileName)) {
				var root = CommandRunner.Settings.UserDataDirectory;
				var fullCachePath = System.IO.Path.Combine(root, path, fileName);
				return new System.IO.FileInfo(fullCachePath);
			} else {
				throw new System.IO.FileNotFoundException();
			}
		}

		public static System.IO.FileInfo Get(string path, System.IO.FileInfo file) {
			if(Exists(path, file)) {
				var root = CommandRunner.Settings.UserDataDirectory;
				var fullCachePath = System.IO.Path.Combine(root, path, file.Name);
				return new System.IO.FileInfo(fullCachePath);
			} else {
				throw new System.IO.FileNotFoundException();
			}
		}
	}
}
