using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Camalot.Common.Extensions;
using DroidExplorer.Core.Exceptions;

namespace DroidExplorer.Core {
	public static class FolderManagement {
		private static string MinAdbVersion = "1.0.31";
		/// <summary>
		/// 
		/// </summary>
		public const string TEMP_DIRECTORY_NAME = "DroidExplorer";


		/// <summary>
		/// Gets the assembly directory.
		/// </summary>
		/// <value>The assembly directory.</value>
		public static string AssemblyDirectory {
			get {
				return System.IO.Path.GetDirectoryName ( typeof ( FolderManagement ).Assembly.Location );
			}
		}

		public static string TempFolder {
			get {
				return System.IO.Path.Combine ( System.IO.Path.GetTempPath ( ), TEMP_DIRECTORY_NAME );
      }
		}

		public static string UserDataFolder {
			get {
				return System.IO.Path.Combine ( Environment.GetFolderPath ( Environment.SpecialFolder.ApplicationData ), TEMP_DIRECTORY_NAME );
      }
		}

		public static string ProgramDataFolder {
			get {
				return System.IO.Path.Combine ( Environment.GetFolderPath ( Environment.SpecialFolder.CommonApplicationData ), TEMP_DIRECTORY_NAME );
      }
		}

		public static string BundledToolsFolder {
			get {
				return System.IO.Path.Combine ( AssemblyDirectory, "tools" );
			}
		}

		/// <summary>
		/// Temps the path cleanup.
		/// </summary>
		public static void TempPathCleanup ( ) {
			try {
				if ( System.IO.Directory.Exists ( TempFolder ) ) {
					System.IO.Directory.Delete ( TempFolder, true );
					System.IO.Directory.CreateDirectory ( TempFolder );
				}
			} catch ( Exception ) {
				throw;
			}
		}

		/// <summary>
		/// Gets the SDK tool.
		/// </summary>
		/// <param name="relativePath">The relative path.</param>
		/// <returns></returns>
		public static string GetSdkTool ( string relativePath ) {
			//return System.IO.Path.Combine ( SdkPath, relativePath );
			string rootTool = System.IO.Path.Combine ( CommandRunner.Settings.SystemSettings.SdkPath, relativePath );
			string sdkTool = System.IO.Path.Combine ( CommandRunner.Settings.SystemSettings.SdkToolsPath, relativePath );
			string sdkBuildTool = GetBuildTool ( relativePath );
			string platformTool = System.IO.Path.Combine ( CommandRunner.Settings.SystemSettings.PlatformToolsPath, relativePath );
			//this.LogDebug ( "Checking for {0}", relativePath );
			if ( System.IO.File.Exists ( rootTool ) ) {
				return rootTool;
			} else if ( System.IO.File.Exists ( sdkTool ) ) {
				return sdkTool;
			} else if ( System.IO.File.Exists ( platformTool ) ) {
				return platformTool;
			} else if ( !string.IsNullOrWhiteSpace ( sdkBuildTool ) && System.IO.File.Exists ( sdkBuildTool ) ) {
				return sdkBuildTool;
			} else {
				Logger.LogError ( typeof ( FolderManagement ), string.Format ( "Unable to locate {0} in the SDK path ({1}).", relativePath, CommandRunner.Settings.SystemSettings.SdkPath ) );
				throw new System.IO.FileNotFoundException ( string.Format ( "Unable to locate {0} in the SDK path ({1}).", relativePath, CommandRunner.Settings.SystemSettings.SdkPath ) );
			}
		}

		/// <summary>
		/// Checks if the tool is able to be located.
		/// </summary>
		/// <param name="toolRelativePath">The tool relative path.</param>
		/// <returns></returns>
		public static bool ToolExists ( string toolRelativePath ) {
			try {
				var tool = GetSdkTool ( toolRelativePath );
				return !string.IsNullOrWhiteSpace ( tool ) && System.IO.File.Exists ( tool );
			} catch ( System.IO.FileNotFoundException ) {
				return false;
			}
		}

		/// <summary>
		/// Gets the bundled tool.
		/// </summary>
		/// <param name="relativePath">The relative path.</param>
		/// <returns></returns>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
		public static string GetBundledTool ( string relativePath ) {
			var ipath = string.IsNullOrWhiteSpace ( CommandRunner.Settings.SystemSettings.InstallPath ) ? AssemblyDirectory : CommandRunner.Settings.SystemSettings.InstallPath;

			string tool = System.IO.Path.Combine ( ipath, "Tools", relativePath );
			if ( System.IO.File.Exists ( tool ) ) {
				return tool;
			} else {
				var location = System.IO.Path.Combine ( CommandRunner.Settings.SystemSettings.InstallPath, "Tools" );
				Logger.LogError ( typeof ( FolderManagement ), string.Format ( "Unable to locate {0} in the bundled tools path ({1}).", relativePath, location ) );
				throw new System.IO.FileNotFoundException ( string.Format ( "Unable to locate {0} in the bundled tools path ({1}).", relativePath, location ) );
			}
		}

		/// <summary>
		/// Verifies the android SDK tools.
		/// </summary>
		/// <returns></returns>
		public static bool VerifyAndroidSdkTools ( ) {
			var adbExists = System.IO.File.Exists ( GetSdkTool ( CommandRunner.ADB_COMMAND ) );
			if ( adbExists ) {
				var ver = new Version ( CommandRunner.Instance.GetAdbVersion ( ) );
				var min = new Version ( MinAdbVersion );
				var validVersion = ver >= min;
				if ( !validVersion ) {
					throw new UnsupportedAdbVersionException ( ver.ToString ( ), min.ToString ( ) );
				}
			}
			return adbExists && System.IO.File.Exists ( GetSdkTool ( CommandRunner.AAPT_COMMAND ) );
		}


		/// <summary>
		/// Gets the build tool.
		/// </summary>
		/// <param name="relativePath">The relative path.</param>
		/// <returns></returns>
		public static string GetBuildTool ( String relativePath ) {
			// get the max version in build tools
			var dir = new System.IO.DirectoryInfo ( CommandRunner.Settings.SystemSettings.BuildToolsPath );
			var roptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			if ( dir.Exists ) {
				var maxVer = dir.GetDirectories ( )
					.Where ( m => m.Name.IsMatch ( @"\d+\.\d+\.\d+", roptions ) )
					.Select ( m => new Version ( m.Name.Replace ( "^android-", "", roptions ) ) ).Max ( );
				if ( maxVer == null ) {
					return String.Empty;
				}

				var path = System.IO.Path.Combine ( dir.FullName, maxVer.ToString ( 3 ) );
				if ( !System.IO.Directory.Exists ( path ) ) {
					// older versions of the sdk put "android-" in front
					path = System.IO.Path.Combine ( dir.FullName, "android-{0}".With ( maxVer.ToString ( 3 ) ) );
				}
				//this.LogDebug ( "Using build tools version {0}", maxVer.ToString ( 3 ) );
				return System.IO.Path.Combine ( path, relativePath );
			} else {
				return String.Empty;
			}
		}

		/// <summary>
		/// Gets the build tool version.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="AdbException">
		/// </exception>
		public static Version GetBuildToolVersion ( ) {
			var dir = new System.IO.DirectoryInfo ( CommandRunner.Settings.SystemSettings.BuildToolsPath );
			var roptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			if ( dir.Exists ) {
				var maxVer = dir.GetDirectories ( )
					.Where ( m => m.Name.IsMatch ( @"\d+\.\d+\.\d+", roptions ) )
						.Select ( m =>
								new Version ( m.Name.Replace ( "^android-", "", roptions ) )
						).Max ( );
				if ( maxVer == null ) {
					throw new AdbException ( DroidExplorer.Resources.Strings.NoBuildToolsMessage );
				}

				var path = System.IO.Path.Combine ( dir.FullName, maxVer.ToString ( 3 ) );
				if ( !System.IO.Directory.Exists ( path ) ) {
					// older versions of the sdk put "android-" in front
					path = System.IO.Path.Combine ( dir.FullName, "android-{0}".With ( maxVer.ToString ( 3 ) ) );
				}

				return maxVer;
			}

			throw new AdbException ( DroidExplorer.Resources.Strings.NoBuildToolsMessage );
		}
	}
}
