using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using DroidExplorer.Core;
using DroidExplorer.Core.IO;
using System.Globalization;
using System.Security.AccessControl;
using System.Reflection;
using DroidExplorer.Configuration;
using DroidExplorer.Configuration.Net;

namespace DroidExplorer.Service {
	class DeviceExplorerRegistration : KnownDeviceManager {
		[Flags]
		public enum SFGAO : uint {
			/// <summary>
			/// The specified items can be browsed in place. 
			/// </summary>
			BROWSABLE = 0x8000000,
			/// <summary>
			/// The specified items can be copied (same value as the DROPEFFECT_COPY flag). 
			/// </summary>
			CANCOPY = 1,
			/// <summary>
			/// The specified items can be deleted by selecting Delete from their context menus. 
			/// </summary>
			CANDELETE = 0x20,
			/// <summary>
			/// Shortcuts can be created for the specified items. 
			/// </summary>
			CANLINK = 4,
			/// <summary>
			/// It is possible to create monikers for the specified items. 
			/// </summary>
			CANMONIKER = 0x400000,
			/// <summary>
			/// The specified items can be moved (same value as the DROPEFFECT_MOVE flag). 
			/// </summary>
			CANMOVE = 2,
			/// <summary>
			/// The specified items can be renamed. 
			/// </summary>
			CANRENAME = 0x10,
			/// <summary>
			/// Do not use. 
			/// </summary>
			CAPABILITYMASK = 0x177,
			/// <summary>
			/// The specified items are compressed. 
			/// </summary>
			COMPRESSED = 0x4000000,
			/// <summary>
			/// Do not use.
			/// </summary>
			CONTENTSMASK = 0x80000000,
			/// <summary>
			/// Do not use. 
			/// </summary>
			DISPLAYATTRMASK = 0xfc000,
			/// <summary>
			/// The specified items are drop targets. 
			/// </summary>
			DROPTARGET = 0x100,
			/// <summary>
			/// The item is encrypted and may require special presentation. 
			/// </summary>
			ENCRYPTED = 0x2000,
			/// <summary>
			/// The specified folder objects are either file system folders or have at least one descendant (child, grandchild, or later) that is a file system folder.
			/// </summary>
			FILESYSANCESTOR = 0x10000000,
			/// <summary>
			/// The specified items are part of the file system (that is, they are files, directories, or root directories). 
			/// </summary>
			FILESYSTEM = 0x40000000,
			/// <summary>
			/// The specified items are folders. 
			/// </summary>
			FOLDER = 0x20000000,
			/// <summary>
			/// The specified items should be displayed using a ghosted icon. 
			/// </summary>
			GHOSTED = 0x8000,
			/// <summary>
			/// The specified items have property sheets. 
			/// </summary>
			HASPROPSHEET = 0x40,
			/// <summary>
			/// Not supported. 
			/// </summary>
			HASSTORAGE = 0x400000,
			/// <summary>
			/// The specified folder objects may have subfolders and are, therefore, expandable in the left pane of Windows Explorer. 
			/// </summary>
			HASSUBFOLDER = 0x80000000,
			/// <summary>
			/// The item is hidden and should not be displayed unless the Show hidden files and folders option is enabled in Folder Settings. 
			/// </summary>
			HIDDEN = 0x80000,
			/// <summary>
			/// Indicates that accessing the object (through IStream or other storage interfaces) is a slow operation. 
			/// </summary>
			ISSLOW = 0x4000,
			/// <summary>
			/// The specified items are shortcuts. 
			/// </summary>
			LINK = 0x10000,
			/// <summary>
			/// The specified items contain new content. 
			/// </summary>
			NEWCONTENT = 0x200000,
			/// <summary>
			/// The specified items are nonenumerated items.
			/// </summary>
			NONENUMERATED = 0x100000,
			/// <summary>
			/// The specified items are read-only. 
			/// </summary>
			READONLY = 0x40000,
			/// <summary>
			/// The specified items are on removable media or are themselves removable devices. 
			/// </summary>
			REMOVABLE = 0x2000000,
			/// <summary>
			/// The specified folder objects are shared. 
			/// </summary>
			SHARE = 0x20000,
			/// <summary>
			/// The item can be bound to an IStorage interface through IShellFolder::BindToObject. 
			/// </summary>
			STORAGE = 8,
			/// <summary>
			/// Children of this item are accessible through IStream or IStorage. 
			/// </summary>
			STORAGEANCESTOR = 0x800000,
			/// <summary>
			/// This flag is a mask for the storage capability attributes. 
			/// </summary>
			STORAGECAPMASK = 0x70c50008,
			/// <summary>
			/// Indicates that the item has a stream associated with it that can be accessed by a call to IShellFolder::BindToObject with IID_IStream in the riid parameter.
			/// </summary>
			STREAM = 0x400000,
			/// <summary>
			/// When specified as input, SFGAO_VALIDATE instructs the folder to validate that the items pointed to by the contents of apidl exist. 
			/// </summary>
			VALIDATE = 0x1000000
		}

		private DeviceExplorerRegistration ( ) {

		}

		public void Register ( string device ) {
			try {
				string deviceGuid = GetDeviceGuid ( device ).ToString ( "B" );
				string friendlyName = GetDeviceFriendlyName ( device );
				string path = System.IO.Path.GetDirectoryName ( this.GetType ( ).Assembly.Location );

				using ( RegistryKey key = Registry.ClassesRoot.CreateSubKey ( string.Format ( CultureInfo.InvariantCulture, @"CLSID\{0}", deviceGuid ) ) ) {
					key.SetValue ( string.Empty, friendlyName );
					key.SetValue ( "SerialNumber", device );
					using ( RegistryKey skey = key.CreateSubKey ( "DefaultIcon" ) ) {
						skey.SetValue ( string.Empty, GetDeviceIcon ( device ) );
					}

					using ( RegistryKey skey = key.CreateSubKey ( "InProcServer32" ) ) {
						skey.SetValue ( string.Empty, "shell32.dll" );
						skey.SetValue ( "ThreadingModel", "Apartment" );
					}

					using ( RegistryKey skey = key.CreateSubKey ( @"Shell\Open\Command" ) ) {
						string exe = string.Format ( CultureInfo.InvariantCulture, "\"{0}\" /d={1}", System.IO.Path.Combine ( path, "DroidExplorer.exe" ), device );
						skey.SetValue ( string.Empty, exe );
					}

					using ( RegistryKey skey = key.CreateSubKey ( string.Format ( CultureInfo.InvariantCulture, @"ShellEx\PropertySheetHandlers\{0}", deviceGuid ) ) ) {
					}

					using ( RegistryKey skey = key.CreateSubKey ( "ShellFolder" ) ) {
						skey.SetValue ( "Attributes", SFGAO.REMOVABLE, RegistryValueKind.DWord );
					}
				}

				using ( RegistryKey key = Registry.LocalMachine.CreateSubKey ( string.Format ( CultureInfo.InvariantCulture, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0}", deviceGuid ) ) ) {
				}
			} catch ( Exception ex ) {
				//throw;
				this.LogError ( ex.Message, ex );
			}
		}

		public void Unregister ( string device ) {
			try {
				string deviceGuid = GetDeviceGuid ( device ).ToString ( "B" );
				Registry.LocalMachine.DeleteSubKey ( string.Format ( @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace\{0}", deviceGuid ), false);
				var keyName = string.Format ( @"CLSID\{0}", deviceGuid );
				var key = Registry.ClassesRoot.OpenSubKey ( keyName );
				if ( key != null ) {
					Registry.ClassesRoot.DeleteSubKeyTree (keyName);
				}
				
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
			}
		}

		private string GetDeviceIcon ( string device ) {

			var ico = new System.IO.FileInfo(Path.Combine(Settings.Instance.ProgramDataDirectory, "assets/[DEFAULT].ico"));
			if ( !ico.Exists ) {
				ico = CloudImage.Instance.GetIcon ( "[DEFAULT]" );
			}

			var info = CommandRunner.Instance.GetDevices().SingleOrDefault(m => m.SerialNumber == device);
			if(info != null) {
				// cache the device icon and images
				if(!String.IsNullOrEmpty(info.DeviceName)) {
					var icoPath = Path.Combine(Settings.Instance.ProgramDataDirectory, String.Format("assets/{0}.ico", info.DeviceName));
					if(!System.IO.File.Exists(icoPath)) {
						ico = CloudImage.Instance.GetIcon(info.DeviceName);
						CloudImage.Instance.GetImage(info.DeviceName);
					}
				}
			}
			return ico.FullName;
		}


		private static DeviceExplorerRegistration _deviceExplorerRegistration;
		public new static DeviceExplorerRegistration Instance {
			get {
				if ( _deviceExplorerRegistration == null ) {
					_deviceExplorerRegistration = new DeviceExplorerRegistration ( );
				}
				return _deviceExplorerRegistration;
			}
		}
	}
}