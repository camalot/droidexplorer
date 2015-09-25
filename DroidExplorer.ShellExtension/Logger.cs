using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DroidExplorer.ShellExtension {
	public static class Logger {
		static Logger ( ) {

		}

		public static void Write ( string message ) {
			Write ( message, null );
		}

		public static void Write ( string message, params object[] args ) {
			FileInfo logFile = new FileInfo ( @"c:\logs\deshell.log" );
			if ( !logFile.Directory.Exists ) {
				logFile.Directory.Create ( );
			}
			using ( FileStream fs = new FileStream ( logFile.FullName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite ) ) {
				using ( StreamWriter sw = new StreamWriter ( fs ) ) {
					sw.WriteLine ( message, args );
				}
			}
		}
	}
}
