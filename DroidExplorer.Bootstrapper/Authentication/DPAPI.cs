using System;
using System.Runtime.InteropServices;
namespace DroidExplorer.Bootstrapper.Authentication {
	public enum DataProtectionKeyType {
		UserKey = 1,
		MachineKey
	}

	public class DPAPI {
		#region Native Imports
		[DllImport ( "crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto )]
		private static extern bool CryptProtectData ( ref DataBlob pPlainText, string szDescription, ref DataBlob pEntropy, IntPtr pReserved, IntPtr pPrompt, CryptProtectFlags dwFlags, ref DataBlob pCipherText );

		[DllImport ( "crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto )]
		private static extern bool CryptUnprotectData ( ref DataBlob pCipherText, ref string pszDescription, ref DataBlob pEntropy, IntPtr pReserved, IntPtr pPrompt, CryptProtectFlags dwFlags, ref DataBlob pPlainText );

		[StructLayout ( LayoutKind.Sequential, CharSet = CharSet.Unicode )]
		struct DataBlob {
			public int cbData;
			public IntPtr pbData;

			public DataBlob ( byte[] data ) {
				if ( data == null )
					data = new byte[ 0 ];

				pbData = Marshal.AllocHGlobal ( data.Length );

				if ( pbData == IntPtr.Zero )
					throw new Exception ( "Unable to allocate data buffer for BLOB structure." );

				cbData = data.Length;

				Marshal.Copy ( data, 0, pbData, data.Length );
			}
		}
		#endregion

		public static bool CanUseDPAPI {
			get {
				Version version = Environment.OSVersion.Version;
				return ( Environment.OSVersion.Platform == PlatformID.Win32NT && version.Major >= 5 );
			}
		}

		enum CryptProtectFlags {
			UIForbidden = 0x1,
			LocalMachine = 0x4
		}

		#region Properties
		string description;
		public string Description {
			get { return description; }
			set { description = value; }
		}

		DataProtectionKeyType keyType = DataProtectionKeyType.UserKey;
		public DataProtectionKeyType KeyType {
			get { return keyType; }
			set { keyType = value; }
		}

		byte[] entropy;
		public byte[] Entropy {
			get { return entropy; }
			set { entropy = value; }
		}
		#endregion

		public byte[] Encrypt ( byte[] plainTextBytes ) {
			if ( description == null )
				description = "";

			DataBlob plainTextBlob = new DataBlob ( plainTextBytes );
			DataBlob cipherTextBlob = new DataBlob ( );
			DataBlob entropyBlob = new DataBlob ( entropy );

			try {
				CryptProtectFlags flags = CryptProtectFlags.UIForbidden;

				if ( keyType == DataProtectionKeyType.MachineKey )
					flags |= CryptProtectFlags.LocalMachine;

				if ( !CryptProtectData ( ref plainTextBlob, description, ref entropyBlob, IntPtr.Zero, IntPtr.Zero, flags, ref cipherTextBlob ) )
					throw new COMException ( "CryptProtectData failed." + Marshal.GetLastWin32Error ( ) );

				byte[] cipherTextBytes = new byte[ cipherTextBlob.cbData ];

				Marshal.Copy ( cipherTextBlob.pbData, cipherTextBytes, 0, cipherTextBlob.cbData );

				return cipherTextBytes;
			} catch ( Exception ex ) {
				throw new Exception ( "DPAPI was unable to encrypt data. " + ex.Message );
			} finally {
				if ( plainTextBlob.pbData != IntPtr.Zero )
					Marshal.FreeHGlobal ( plainTextBlob.pbData );

				if ( cipherTextBlob.pbData != IntPtr.Zero )
					Marshal.FreeHGlobal ( cipherTextBlob.pbData );

				if ( entropyBlob.pbData != IntPtr.Zero )
					Marshal.FreeHGlobal ( entropyBlob.pbData );
			}
		}

		public byte[] Decrypt ( byte[] cipherTextBytes ) {
			DataBlob plainTextBlob = new DataBlob ( );
			DataBlob cipherTextBlob = new DataBlob ( cipherTextBytes );
			DataBlob entropyBlob = new DataBlob ( entropy );

			description = "";

			try {
				CryptProtectFlags flags = CryptProtectFlags.UIForbidden;

				if ( !CryptUnprotectData ( ref cipherTextBlob, ref description, ref entropyBlob, IntPtr.Zero, IntPtr.Zero, flags, ref plainTextBlob ) )
					throw new COMException ( "CryptUnprotectData failed. ", Marshal.GetLastWin32Error ( ) );

				byte[] plainTextBytes = new byte[ plainTextBlob.cbData ];

				Marshal.Copy ( plainTextBlob.pbData, plainTextBytes, 0, plainTextBlob.cbData );
				return plainTextBytes;
			} catch ( Exception ex ) {
				throw new Exception ( "DPAPI was unable to decrypt data. " + ex.Message );
			} finally {
				if ( plainTextBlob.pbData != IntPtr.Zero )
					Marshal.FreeHGlobal ( plainTextBlob.pbData );

				if ( cipherTextBlob.pbData != IntPtr.Zero )
					Marshal.FreeHGlobal ( cipherTextBlob.pbData );

				if ( entropyBlob.pbData != IntPtr.Zero )
					Marshal.FreeHGlobal ( entropyBlob.pbData );
			}
		}
	}
}
