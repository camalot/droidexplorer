using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using System.IO;
using System.Collections;
using System.Linq;

using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace DroidExplorer.WixCA {
	public class AndroidSdkCustomActions {
		[CustomAction]
		public ActionResult VerifyAndroidSdkPath ( Session session ) {
			string path = session.CustomActionData["ANDROIDSDKPATH"];

			var toolsToCheck = new string[] { "adb.exe", "aapt.exe" };
      var valid = true;
			foreach ( var tool in toolsToCheck ) {
				var buildToolPath = System.IO.Path.Combine ( path, "build-tools" );

				var v = GetBuildToolsVersion ( buildToolPath );
				if ( v == null || v == new Version ( 0, 0, 0, 0 ) ) {
					valid = false;
				}


				var rootTool = System.IO.Path.Combine ( path, tool );
				var sdkTool = System.IO.Path.Combine ( System.IO.Path.Combine ( path, "tools" ), tool );
				var sdkBuildTool = GetBuildTool ( buildToolPath, tool );
				var platformTool = System.IO.Path.Combine ( System.IO.Path.Combine ( path, "platform-tools" ), tool );

				if ( System.IO.File.Exists ( rootTool ) ) {
					continue;
				} else if ( System.IO.File.Exists ( sdkTool ) ) {
					continue;
				} else if ( System.IO.File.Exists ( platformTool ) ) {
					continue;
				} else if ( !string.IsNullOrEmpty ( sdkBuildTool ) && System.IO.File.Exists ( sdkBuildTool ) ) {
					continue;
				} else {
					valid = false;
				}


			}


			return valid ? ActionResult.Success : ActionResult.Failure;

		}

		//[CustomAction]
		//public ActionResult GetLatestSdkPlatform ( Session session ) {
		//	string[] propValue = session.CustomActionData["ANDROIDSDKPATH"].Split ( ';' );
		//	string path = propValue[0];
		//	string regPath = propValue[1];
		//	DirectoryInfo sdk = new DirectoryInfo ( path );
		//	if ( !sdk.Exists ) {
		//		return ActionResult.Failure;
		//	} else {
		//		DirectoryInfo platforms = new DirectoryInfo ( Path.Combine ( path, "platforms" ) );
		//		String platformBaseName = "android-";
		//		List<String> versions = new List<String> ( );
		//		foreach ( var item in platforms.GetDirectories ( platformBaseName + "*" ) ) {
		//			try {
		//				String version = item.Name.Substring ( platformBaseName.Length );
		//				versions.Add ( version );
		//			} catch ( Exception ) {
		//				throw;
		//			}
		//		}

		//		if ( versions.Count == 0 ) {
		//			return ActionResult.Failure;
		//		}

		//		versions.Sort ( );


		//		using ( RegistryKey key = Registry.LocalMachine.CreateSubKey ( regPath ) ) {
		//			if ( key != null ) {
		//				key.SetValue ( "Platform", versions[versions.Count - 1] );
		//				return ActionResult.Success;
		//			}
		//		}
		//		return ActionResult.Failure;
		//	}
		//}

		private string GetBuildTool ( string toolPath, string relativePath ) {
			// get the max version in build tools
			var dir = new System.IO.DirectoryInfo ( toolPath );
			var roptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			if ( dir.Exists ) {
				var maxVer = dir.GetDirectories ( )
					.Where ( m => Regex.IsMatch ( m.Name, @"\d+\.\d+\.\d+", roptions ) )
					.Select ( m => new Version ( Regex.Replace ( m.Name, "^android-", "", roptions ) ) ).Max ( );
				if ( maxVer == null ) {
					return String.Empty;
				}

				var path = System.IO.Path.Combine ( dir.FullName, maxVer.ToString ( 3 ) );
				if ( !System.IO.Directory.Exists ( path ) ) {
					// older versions of the sdk put "android-" in front
					path = System.IO.Path.Combine ( dir.FullName, string.Format ( "android-{0}", maxVer.ToString ( 3 ) ) );
				}
				//this.LogDebug ( "Using build tools version {0}", maxVer.ToString ( 3 ) );
				return System.IO.Path.Combine ( path, relativePath );
			} else {
				return string.Empty;
			}
		}

		private Version GetBuildToolsVersion ( string buildToolPath ) {
			var dir = new System.IO.DirectoryInfo ( buildToolPath );
			var roptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			if ( dir.Exists ) {
				var maxVer = dir.GetDirectories ( )
					.Where ( m => Regex.IsMatch ( m.Name, @"\d+\.\d+\.\d+" ) )
						.Select ( m =>
								new Version ( Regex.Replace ( m.Name, "^android-", "", roptions ) )
						).Max ( );
				if ( maxVer == null ) {
					return new Version ( 0, 0, 0, 0 );
				}

				var path = System.IO.Path.Combine ( dir.FullName, maxVer.ToString ( 3 ) );
				if ( !System.IO.Directory.Exists ( path ) ) {
					// older versions of the sdk put "android-" in front
					path = System.IO.Path.Combine ( dir.FullName, string.Format ( "android-{0}", maxVer.ToString ( 3 ) ) );
				}

				return maxVer;
			}

			return new Version ( 0, 0, 0, 0 );
		}
	}
}
