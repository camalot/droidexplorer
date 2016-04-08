using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.IO {
	/// <summary>
	/// 
	/// </summary>
	public static class Cache {
		/// <summary>
		/// The ap k_ imag e_ cache
		/// </summary>
		public const string APK_IMAGE_CACHE = @"assets\apk";
		/// <summary>
		/// The icon n_ imag e_ cache
		/// </summary>
		public const string ICON_IMAGE_CACHE = @"assets\icons";

		/// <summary>
		/// Gets the cache key.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public static string GetCacheKey(FileSystemInfo file) {
			return GetCacheKey(file.FullPath);
		}

		/// <summary>
		/// Gets the cache key.
		/// </summary>
		/// <param name="remoteFilePath">The remote file path.</param>
		/// <returns></returns>
		public static string GetCacheKey(string remoteFilePath) {
			var fullName = remoteFilePath.REReplace(@"\/|\:", ".");
			if(fullName.StartsWith(".")) {
				fullName = fullName.Substring(1);
			}
			return fullName;
		}

		/// <summary>
		/// Moves to.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="localFile">The local file.</param>
		/// <returns></returns>
		public static System.IO.FileInfo MoveTo(string path, string localFile) {
			return MoveTo(path, new System.IO.FileInfo(localFile));
		}

		/// <summary>
		/// Moves to.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public static System.IO.FileInfo MoveTo(string path, System.IO.FileInfo file) {
			var root = CommandRunner.Settings.UserDataDirectory;
			var fullCachePath = System.IO.Path.Combine(root,path,file.Name);
			file.MoveTo(fullCachePath);
			return new System.IO.FileInfo(fullCachePath);
		}

		/// <summary>
		/// Saves the specified file path.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <param name="image">The image.</param>
		/// <returns></returns>
		public static System.IO.FileInfo Save(string filePath, System.Drawing.Image image) {
			image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
			return new System.IO.FileInfo(filePath);
		}

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static string GetPath(string path) {
			var root = CommandRunner.Settings.UserDataDirectory;
			return System.IO.Path.Combine(root, path);
		}



		//public static System.IO.FileInfo MoveTo(string path, FileInfo file) {
		//	var temp = CommandRunner.Instance.PullFile(file.FullPath);
		//	return MoveTo(path, temp);
		//}

		/// <summary>
		/// Existses the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public static bool Exists(string path, System.IO.FileInfo file) {
			var root = CommandRunner.Settings.UserDataDirectory;
			var fullCachePath = System.IO.Path.Combine(root, path, file.Name);
			return System.IO.File.Exists(fullCachePath);
		}

		/// <summary>
		/// Existses the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public static bool Exists(string path, string fileName) {
			var root = CommandRunner.Settings.UserDataDirectory;
			var fullCachePath = System.IO.Path.Combine(root, path, fileName);
			return System.IO.File.Exists(fullCachePath);
		}

		//public static bool Exists(string path, System.IO.FileInfo file) {

		//}

		/// <summary>
		/// Gets the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
		public static System.IO.FileInfo Get(string path, string fileName) {
			if(Exists(path, fileName)) {
				var root = CommandRunner.Settings.UserDataDirectory;
				var fullCachePath = System.IO.Path.Combine(root, path, fileName);
				return new System.IO.FileInfo(fullCachePath);
			} else {
				throw new System.IO.FileNotFoundException();
			}
		}

		/// <summary>
		/// Gets the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
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
