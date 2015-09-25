using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Reflection;

namespace DroidExplorer.Core {
	public static class Architecture {
		/// <summary>
		/// Gets a value indicating whether this instance is running X64.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is running X64; otherwise, <c>false</c>.
		/// </value>
		public static bool IsRunningX64 {
			get {
				using ( RegistryKey key = Registry.LocalMachine.OpenSubKey ( @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment" ) ) {
					if ( key != null ) {
						return string.Compare ( key.GetValue ( "PROCESSOR_ARCHITECTURE", "x86" ).ToString ( ), "x86", true ) != 0;
					} else {
						return false;
					}
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is assembly X64.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is assembly X64; otherwise, <c>false</c>.
		/// </value>
		public static bool IsAssemblyX64 {
			get {
				AssemblyName asmName = typeof ( Architecture ).Assembly.GetName ( );

				return asmName.ProcessorArchitecture == System.Reflection.ProcessorArchitecture.Amd64 ||
					asmName.ProcessorArchitecture == ProcessorArchitecture.IA64 || IntPtr.Size == 8;

			}
		}
	}
}
