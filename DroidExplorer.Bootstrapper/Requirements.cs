using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DroidExplorer.Bootstrapper {
	public static class Requirements {

		/// <summary>
		/// Determines whether the machine has .net framework 3.5 sp1 or greater installed
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if the machine has .net framework 3.5 sp1 or greater installed; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasDotNet35SP1OrGreater ( ) {
			// todo: this needs to be "fixed", some people reporting it is saying they dont have 3.5sp1 when they
			// have win7, which ships with it.
			string[] keys = new string[] {
												@"SOFTWARE\Microsoft\NET Framework Setup\NDP\",
												@"SOFTWARE\WOW64Node32\Microsoft\NET Framework Setup\NDP\"
											};
			foreach ( string item in keys ) {
				using ( RegistryKey key = Registry.LocalMachine.OpenSubKey ( item ) ) {
					if ( key != null ) {
						foreach ( var verKey in key.GetSubKeyNames ( ) ) {
							string tstring = verKey.StartsWith ( "v" ) ? verKey.Substring ( 1 ) : "1";
							double dver = 1;
							double.TryParse ( tstring, out dver );
							if ( dver == 3.5 ) {
								using ( RegistryKey skey = key.OpenSubKey ( verKey ) ) {
									if ( skey != null ) {
										int spverion = (int)skey.GetValue ( "SP", 0 );
										if ( spverion >= 1 ) {
											Logger.LogDebug ( typeof ( Requirements ), "Found Service Pack Version: {0}", spverion );
											return true;
										}
									}
								}
							} else if ( dver > 3.5 ) {
								Logger.LogDebug ( typeof ( Requirements ), "Found .NET Framework Version: {0}", dver );
								return true;
							}
						}
					}
				}

			}

			return false;
		}

		/// <summary>
		/// Determines whether if the installer is the correct one for the platform.
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if the installer is the correct one for the platform; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsCorrectPlatform ( ) {
			if ( Is64Bit ( ) && ( Program.ApplicationArchitecture == ArchitectureTypes.x64 || Program.ApplicationArchitecture == ArchitectureTypes.ia64 ) ) {
				return true;
			} else if ( !Is64Bit ( ) && ( Program.ApplicationArchitecture == ArchitectureTypes.x86 ) ) {
				return true;
			} else {
				return false;
			}
		}


		[DllImport ( "kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi )]
		[return: MarshalAs ( UnmanagedType.Bool )]
		private static extern bool IsWow64Process ( [In] IntPtr hProcess, [Out] out bool lpSystemInfo );

		/// <summary>
		/// Checks if the running on 64 bit
		/// </summary>
		/// <returns></returns>
		public static bool Is64Bit ( ) {
			if ( IntPtr.Size == 8 || ( IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor ( ) ) ) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Checks if the process is 32 bit running on a 64 bit processor.
		/// </summary>
		/// <returns></returns>
		private static bool Is32BitProcessOn64BitProcessor ( ) {
			bool retVal;

			IsWow64Process ( Process.GetCurrentProcess ( ).Handle, out retVal );

			return retVal;
		}

	}
}
