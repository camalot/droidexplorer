using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;

namespace DroidExplorer.WixCA {
	public class CreateSystemAndroidBackupDirectory {

		/// <summary>
		/// Sets the user backup directory as a system folder.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <returns></returns>
		[CustomAction("SetUserBackupDirectorySystemFolder")]
		public static ActionResult SetUserBackupDirectorySystemFolder ( Session session ) {
			try {
				var path = session["ANDROIDBACKUPDIR"];
				if ( string.IsNullOrEmpty ( path ) ) {
					session.Log ( "using default location for Backup path" );
					path = Path.Combine ( Environment.GetEnvironmentVariable ( "USERPROFILE" ), "Android Backups" );
				}
				session.Log ( "Path: {0}", path );
				var dir = new DirectoryInfo ( path );
				if ( !dir.Exists ) {
					try {
						Directory.CreateDirectory ( path );
					} catch ( Exception e ) {
						session.Log ( e.ToString ( ) );
						return ActionResult.Failure;
					}
				}

				dir.Attributes = System.IO.FileAttributes.System | System.IO.FileAttributes.ReadOnly;

				return ActionResult.Success;
			} catch ( Exception ex ) {
				session.Log ( ex.ToString ( ) );
				return ActionResult.Failure;
			}
		}

	}
}
