using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Globalization;

namespace DroidExplorer.Tools {
	public class ExplorerRegistrationUtility {
		/// <summary>
		/// Initializes a new instance of the <see cref="ExplorerRegistrationUtility"/> class.
		/// </summary>
		private ExplorerRegistrationUtility () {
			this.Guid = new Guid ( "{3888C606-01ED-498d-81E5-66F2A26D08B0}" );
		}

		/// <summary>
		/// Gets or sets the GUID.
		/// </summary>
		/// <value>The GUID.</value>
		private Guid Guid { get; set; }

		/// <summary>
		/// Registers this instance.
		/// </summary>
		public void Register () {
			try {
				string path = this.GetType ().Assembly.Location;
				using ( RegistryKey key = Registry.ClassesRoot.CreateSubKey ( string.Format ( CultureInfo.InvariantCulture, "CLSID\\{0}", this.Guid.ToString ( "B" ) ) ) ) {
					key.SetValue ( string.Empty, "Droid Explorer" );
					using ( RegistryKey skey = key.CreateSubKey ( "DefaultIcon" ) ) {
						skey.SetValue ( string.Empty, string.Format ( CultureInfo.InvariantCulture, "{0},0", path ) );
					}

					using ( RegistryKey skey = key.CreateSubKey ( "InProcServer32" ) ) {
						skey.SetValue ( string.Empty, "shell32.dll" );
						skey.SetValue ( "ThreadingModel", "Apartment" );
					}

					using ( RegistryKey skey = key.CreateSubKey ( "Shell\\Open\\Command" ) ) {
						skey.SetValue ( string.Empty, path );
					}
					using ( RegistryKey skey = key.CreateSubKey ( string.Format ( CultureInfo.InvariantCulture, "ShellEx\\PropertySheetHandlers\\{0}", this.Guid.ToString ( "B" ) ) ) ) {
					}
					using ( RegistryKey skey = key.CreateSubKey ( "ShellFolder" ) ) {
						skey.SetValue ( "Attributes", new byte[] { }, RegistryValueKind.Binary );
					}
				}

				using ( RegistryKey key = Registry.LocalMachine.CreateSubKey ( string.Format ( CultureInfo.InvariantCulture, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\MyComputer\\NameSpace\\{0}", this.Guid.ToString ( "B" ) ) ) ) {
				}
			} catch ( Exception ex ) {
				throw;
			}
		}

		/// <summary>
		/// Unregisters this instance.
		/// </summary>
		public void Unregister () {
			try {
				Registry.LocalMachine.DeleteSubKey( string.Format ( @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0}", this.Guid.ToString ( "B" ) ) );
				Registry.ClassesRoot.DeleteSubKeyTree ( string.Format ( "CLSID\\{0}", this.Guid.ToString ( "B" ) ) );
			} catch ( Exception ex ) {
				throw;
			}
		}

		private static ExplorerRegistrationUtility _explorerRegistrationUtility;
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static ExplorerRegistrationUtility Instance {
			get {
				if ( _explorerRegistrationUtility == null ) {
					_explorerRegistrationUtility = new ExplorerRegistrationUtility ();
				}
				return _explorerRegistrationUtility;
			}
		}
	}
}
