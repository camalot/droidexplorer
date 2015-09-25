using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection;
using Microsoft.Win32;

namespace DroidExplorer.ShellExtension {
	/// <summary>
	/// Based on apkshellext by allangoing: http://code.google.com/p/apkshellext/
	/// Portions are Copyright allangoing under the apache 2.0 license
	/// </summary>
	[Guid ( "2B688A81-C9F8-4b0e-A1F6-1CE9535614DD" ), ComVisible ( true )]
	public class ApkShellExtension : IExtractIcon, IPersistFile {
		//private readonly ILog Log = log4net.LogManager.GetLogger ( MethodBase.GetCurrentMethod ( ).DeclaringType );

		private const string GUID = "{2B688A81-C9F8-4b0e-A1F6-1CE9535614DD}";
		private const string KEYNAME = "DroidExplorerShellExtension";

		private const uint E_PENDING = 0x8000000A;
		private const uint E_NOTIMPL = 0x80004001;

		private const int S_OK = 0;
		private const int S_FALSE = 1;

		/// <summary>
		/// Initializes a new instance of the <see cref="ApkShellExtension"/> class.
		/// </summary>
		public ApkShellExtension ( ) {
			/*using ( Stream stream = this.GetType ( ).Assembly.GetManifestResourceStream ( "DroidExplorer.ShellExtension.DroidExplorer.ShellExtension.log4net" ) ) {
				XmlConfigurator.Configure ( stream );
			}*/

			Aapt = Path.Combine ( RegistrySettings.Instance.PlatformToolsPath, RegistrySettings.AAPT_COMMAND );


		}

		private string FileName { get; set; }
		private string Aapt { get; set; }

		#region IPersistFile Members

		public uint GetClassID ( out Guid pClassID ) {
			pClassID = new Guid ( GUID );
			return S_OK;
		}

		public uint IsDirty ( ) {
			throw new NotImplementedException ( );
		}

		public uint Load ( string pszFileName, uint dwMode ) {
			FileName = pszFileName;
			return S_OK;
		}

		public uint Save ( string pszFileName, bool fRemember ) {
			throw new NotImplementedException ( );
		}

		public uint SaveCompleted ( string pszFileName ) {
			throw new NotImplementedException ( );
		}

		public uint GetCurFile ( out string ppszFileName ) {
			throw new NotImplementedException ( );
		}

		#endregion

		#region IExtractIcon Members

		/// <summary>
		/// Get the icon location
		/// </summary>
		/// <param name="uFlags"></param>
		/// <param name="szIconFile"></param>
		/// <param name="cchMax"></param>
		/// <param name="piIndex"></param>
		/// <param name="pwFlags"></param>
		/// <returns></returns>
		public uint GetIconLocation ( ExtractIconOptions uFlags, IntPtr szIconFile, uint cchMax, out int piIndex, out ExtractIconFlags pwFlags ) {
			piIndex = -1;
			szIconFile = IntPtr.Zero;
			try {
				pwFlags = ExtractIconFlags.NotFilename | ExtractIconFlags.PerInstance | ExtractIconFlags.DontCache;
				return ( ( uFlags & ExtractIconOptions.Async ) != 0 ) ? E_PENDING : S_OK;
			} catch ( Exception ex ) {
				//Log.Error ( ex.Message, ex );
				pwFlags = ExtractIconFlags.None;
				return S_FALSE;
			}
		}

		/// <summary>
		/// Extracts the icon from the specified file.
		/// </summary>
		/// <param name="pszFile">The file.</param>
		/// <param name="nIconIndex">Index of the icon.</param>
		/// <param name="phiconLarge">The phicon large.</param>
		/// <param name="phiconSmall">The phicon small.</param>
		/// <param name="nIconSize">Size of the n icon.</param>
		/// <returns></returns>
		public uint Extract ( string pszFile, uint nIconIndex, out IntPtr phiconLarge, out IntPtr phiconSmall, uint nIconSize ) {
			try {
				Icon ico = ExtractApkIcon ( );
				int s_size = (int)nIconSize >> 16;
				int l_size = (int)nIconSize & 0xffff;
				phiconLarge = ( new Icon ( ico, l_size, l_size ) ).Handle;
				phiconSmall = ( new Icon ( ico, s_size, s_size ) ).Handle;
				return S_OK;
			} catch ( Exception ex ) {
				//Log.Error ( ex.Message, ex );
				phiconLarge = phiconSmall = IntPtr.Zero;
				return S_FALSE;
			}
		}

		#endregion

		/// <summary>
		/// Extract the icon from the apk file
		/// </summary>
		/// <returns></returns>
		private Icon ExtractApkIcon ( ) {
			//ExtractAaptTools ( );
			// default image
			Bitmap defaultBitmap = Bitmap.FromStream ( this.GetType ( ).Assembly.GetManifestResourceStream ( "DroidExplorer.ShellExtension.apk.png" ) ) as Bitmap;
			Bitmap apkBitmap = defaultBitmap;
			try {

				// get badging information
				Process proc = new Process ( );
				ProcessStartInfo psi = new ProcessStartInfo ( Aapt, string.Format ( CultureInfo.InvariantCulture, "dump badging \"{0}\"", FileName ) );
				psi.WindowStyle = ProcessWindowStyle.Hidden;
				psi.UseShellExecute = false;
				psi.WorkingDirectory = Path.GetTempPath ( );
				psi.CreateNoWindow = true;
				psi.RedirectStandardOutput = true;
				proc.StartInfo = psi;
				// start the process
				proc.Start ( );

				// regex to find the icon
				Regex re = new Regex ( @"icon='([^']+)'", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline );
				StringBuilder icon = new StringBuilder ( );
				Match m = re.Match ( proc.StandardOutput.ReadToEnd ( ) );
				if ( m.Success ) {
					// possible whitespace in filename fix
					Regex rews = new Regex ( @"\r|\n", RegexOptions.Compiled | RegexOptions.Singleline );
					string sout = rews.Replace ( m.Groups[1].Value, string.Empty );
					icon.Append ( sout );
				} else {
					// we did not find an icon, return the default now, no need to try anything else.
					return Icon.FromHandle ( defaultBitmap.GetHicon ( ) );
				}

				if ( icon.Length != 0 ) {
					//using ( FileStream fs = new FileStream ( FileName, FileMode.Open, FileAccess.Read, FileShare.Read ) ) {
					using ( Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile ( FileName ) ) {
						var entry = zip.Entries.FirstOrDefault ( e => String.Compare ( icon.ToString ( ), e.FileName, false ) == 0 );
						if ( entry != null ) {
							using ( var iconMS = new MemoryStream ( ) ) {
								entry.Extract ( iconMS );
								iconMS.Position = 0;
								apkBitmap = Bitmap.FromStream ( iconMS ) as Bitmap;
								/*using ( Stream inputStream = zip.GetInputStream ( entry ) ) {
									if ( inputStream != null ) {
										apkBitmap = Bitmap.FromStream ( inputStream ) as Bitmap;
									}
								}*/
							}
						}
						//}
					}
				}

				if ( apkBitmap == null ) {
					apkBitmap = defaultBitmap;
				}

			} catch ( Exception ex ) {
				Logger.Write ( ex.ToString ( ) );
				apkBitmap = defaultBitmap;
			}

			try {
				Icon ico = Icon.FromHandle ( apkBitmap.GetHicon ( ) );
				return ico;
			} catch ( Exception ex ) {
				Logger.Write ( ex.ToString ( ) );
				return Icon.FromHandle ( defaultBitmap.GetHicon ( ) );
			}
		}

		/// <summary>
		/// Registers this instance.
		/// </summary>
		/// <param name="s">The s.</param>
		[System.Runtime.InteropServices.ComRegisterFunctionAttribute ( )]
		static void Register ( string s ) {
			try {
				// actual registry changes done in the installer
			} catch {
			}
		}

		/// <summary>
		/// Unregisters this instance.
		/// </summary>
		/// <param name="s">The s.</param>
		[System.Runtime.InteropServices.ComUnregisterFunctionAttribute ( )]
		static void Unregister ( string s ) {
			try {
				// actual registry changes done in the installer
			} catch {
			}
		}
	}
}
