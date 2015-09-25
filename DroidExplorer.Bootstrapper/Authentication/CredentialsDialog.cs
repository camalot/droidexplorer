using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DroidExplorer.Bootstrapper.Authentication {

	public class CredentialsDialog {
		#region External Functions
		[DllImport ( "credui", CharSet = CharSet.Unicode )]
		static extern CredUIReturnCodes CredUIPromptForCredentials ( ref CredUIInfo uiInfo, string targetName, IntPtr reserved, int error, StringBuilder userName, int maxUserName, StringBuilder password, int maxPassword, ref int save, CredUIFlags flags );

		[DllImport ( "credui", CharSet = CharSet.Unicode )]
		static extern int CredUIConfirmCredentials ( string targetName, bool confirmed );

		[DllImport ( "advapi32", CharSet = CharSet.Unicode )]
		static extern bool CredDelete ( string targetName, CredType type, int flags );

		[DllImport ( "advapi32" )]
		static extern bool CredEnumerate ( string filter, int flags, out int count, out IntPtr credentials );

		[DllImport ( "advapi32" )]
		static extern void CredFree ( IntPtr buffer );

		[DllImport ( "advapi32" )]
		static extern void CredRead ( string targetName, CredType type, int flags, out IntPtr credential );

		[DllImport ( "user32.dll", CharSet = CharSet.Auto )]
		static extern IntPtr SendMessage ( IntPtr hWnd, WindowsMessages msg, IntPtr wParam, IntPtr lParam );

		enum WindowsMessages : uint {
			Close = 0x0010
		}

		[Flags]
		enum CredUIFlags {
			IncorrectPassword = 0x01,
			DoNotPersist = 0x02,
			RequestAdministration = 0x04,
			ExcludeCertificates = 0x08,
			RequireCertificate = 0x10,
			ShowSaveCheckBox = 0x40,
			AlwaysShowUI = 0x80,
			RequireSmartcard = 0x100,
			PasswordOnlyOK = 0x200,
			ValidateUsername = 0x400,
			CompleteUsername = 0x800,
			Persist = 0x1000,
			ServerCredential = 0x4000,
			ExpectConfirmation = 0x20000,
			GenericCredentials = 0x40000,
			UsernameTargetCredentials = 0x80000,
			KeepUsername = 0x100000
		}

		enum CredUIReturnCodes {
			NoError = 0x00,
			Cancelled = 1223,
			NoSuchLogonSession = 1312,
			NotFound = 1168,
			InvalidAccountName = 1315,
			InsufficientBuffer = 122,
			InvalidParameter = 87,
			InvalidFlags = 1004
		}

		enum CredType {
			Generic = 1,
			Password = 2,
			Certificate = 3,
			VisiblePassword = 4
		}

		struct CredUIInfo {
			public int size;
			public IntPtr hwndParent;

			[MarshalAs ( UnmanagedType.LPWStr )]
			public string messageText;

			[MarshalAs ( UnmanagedType.LPWStr )]
			public string captionText;

			public IntPtr hbmBanner;

			public CredUIInfo ( IWin32Window owner, string message, string caption ) {
				if ( owner == null )
					hwndParent = IntPtr.Zero;
				else
					hwndParent = ( owner as Control ).GetHandle ( );
				messageText = message;
				captionText = caption;
				hbmBanner = IntPtr.Zero;
				size = Marshal.SizeOf ( typeof ( CredUIInfo ) );
			}
		}

		struct Credential {
			public int Flags;
			public CredType Type;
			public long aLastWritten;
			public long bLastWritten;
			public long CredentialBlobSize;
			public IntPtr CredentialBlob;
			public long Persist;
			public string Username;

			Credential ( CredType type ) // I Hate Warnings so this gets rid of them
			{
				Flags = 0;
				Type = type;
				aLastWritten = 0;
				bLastWritten = 0;
				CredentialBlobSize = 0;
				CredentialBlob = IntPtr.Zero;
				Persist = 0;
				Username = null;
			}
		}
		#endregion

		#region Statics
		public static void ClearSavedCredentials ( string targetName ) {
			CredDelete ( targetName, CredType.Generic, 0 );
		}

		public static string GetSavedCredentialUsername ( string targetName ) {
			IntPtr ptr = IntPtr.Zero;
			CredRead ( targetName, CredType.Generic, 0, out ptr );
			if ( ptr == IntPtr.Zero )
				return null;
			Credential credential = (Credential)Marshal.PtrToStructure ( ptr, typeof ( Credential ) );
			string retVal = credential.Username;
			CredFree ( ptr );
			return retVal;
		}

		public static bool HasSavedCredentials ( string targetName ) {
			int count;
			IntPtr credentials;
			CredEnumerate ( targetName, 0, out count, out credentials );
			CredFree ( credentials );
			return count > 0;
		}
		#endregion

		const int MaxUsername = 256;
		const int MaxPassword = 256;

		#region Initialisation
		public CredentialsDialog ( ) {
		}

		public CredentialsDialog ( ExtensionPanel panel ) {
			panExtension = panel;
		}
		#endregion

		public DialogResult ShowDialog ( IWin32Window owner ) {
			return ShowDialog ( owner, false );
		}

		public DialogResult ShowDialog ( IWin32Window owner, bool retry ) {
			if ( !CanCallCredUI )
				return ShowPseudoDialog ( owner, retry );
			else {
				LocalCbtHook hook = new LocalCbtHook ( );
				try {
					hook.WindowActivated += new LocalCbtHook.CbtEventHandler ( hook_WindowActivated );
					hook.WindowCreated += new LocalCbtHook.CbtEventHandler ( hook_WindowCreated );
					hook.WindowDestroyed += new LocalCbtHook.CbtEventHandler ( hook_WindowDestroyed );
					hook.Install ( );

					CredUIInfo uiInfo = new CredUIInfo ( owner, text, caption );
					StringBuilder sbUsername = new StringBuilder ( username, MaxUsername );
					StringBuilder sbPassword = new StringBuilder ( password, MaxPassword );
					int nSave = Convert.ToInt32 ( save );
					CredUIFlags flags = CredUIFlags.GenericCredentials;
					if ( expectConfirmation )
						flags |= CredUIFlags.ExpectConfirmation;
					if ( forceUI )
						flags |= CredUIFlags.AlwaysShowUI;
					if ( retry )
						flags |= CredUIFlags.IncorrectPassword;
					programmaticCloseResult = DialogResult.None;
					CredUIReturnCodes retVal = CredUIPromptForCredentials ( ref uiInfo, targetName, IntPtr.Zero, 0, sbUsername, MaxUsername, sbPassword, MaxPassword, ref nSave, flags );
					save = Convert.ToBoolean ( nSave );

					if ( programmaticCloseResult != DialogResult.None )
						return programmaticCloseResult;
					switch ( retVal ) {
						case CredUIReturnCodes.NoError:
							username = sbUsername.ToString ( );
							password = sbPassword.ToString ( );
							save = Convert.ToBoolean ( nSave );
							return DialogResult.OK;
						case CredUIReturnCodes.Cancelled:
							return DialogResult.Cancel;
						default:
							return DialogResult.Abort;
					}
				} finally {
					hook.WindowActivated -= new LocalCbtHook.CbtEventHandler ( hook_WindowActivated );
					hook.WindowCreated -= new LocalCbtHook.CbtEventHandler ( hook_WindowCreated );
					hook.WindowDestroyed -= new LocalCbtHook.CbtEventHandler ( hook_WindowDestroyed );
					hook.Uninstall ( );
				}
			}
		}

		CredentialConfirmForm pseudoDialog;
		public DialogResult ShowPseudoDialog ( IWin32Window owner, bool retry ) {
			if ( pseudoDialog == null )
				pseudoDialog = new CredentialConfirmForm ( targetName, caption );
			try {
				pseudoDialog.Text = caption;
				pseudoDialog.Caption = ( text != null && text != "" ) ? text : string.Format ( "Welcome to {0}", targetName );
				;
				pseudoDialog.Target = targetName;
				pseudoDialog.ExpectConfirmation = expectConfirmation;

				DialogResult result;
				if ( pseudoDialog.LoadCredentials ( ) && !forceUI )
					result = DialogResult.OK;
				else {
					if ( panExtension != null ) {
						panExtension.Dialog = this;
						pseudoDialog.Controls.Add ( panExtension );
						//panExtension.LayoutExtension ( new PseudoCredentialsDialog.PseudoCredentialBounds ( pseudoDialog ) );
						panExtension.BringToFront ( );
						panExtension.Show ( );
					}

					programmaticCloseResult = DialogResult.None;
					result = pseudoDialog.ShowDialog ( owner );
					if ( programmaticCloseResult != DialogResult.None )
						return programmaticCloseResult;
				}
				if ( result != DialogResult.Cancel && result != DialogResult.Abort ) {
					username = pseudoDialog.Username;
					password = pseudoDialog.Password;
				}
				if ( panExtension != null )
					pseudoDialog.Controls.Remove ( panExtension );

				return result;
			} finally {
				if ( !expectConfirmation ) {
					pseudoDialog.Dispose ( );
					pseudoDialog = null;
				}
			}
		}

		public void Confirm ( bool confirmed ) {
			if ( CanCallCredUI ) {
				System.Diagnostics.Debug.Assert ( expectConfirmation );
				CredUIConfirmCredentials ( targetName, confirmed );
			} else if ( pseudoDialog != null ) {
				pseudoDialog.SaveCredentials ( );
				pseudoDialog = null;
			}
		}

		DialogResult programmaticCloseResult = DialogResult.None;
		public void Close ( DialogResult result ) {
			programmaticCloseResult = result;
			if ( pseudoDialog != null )
				pseudoDialog.Close ( );
			else if ( hwndCredDialog != IntPtr.Zero )
				SendMessage ( hwndCredDialog, WindowsMessages.Close, IntPtr.Zero, IntPtr.Zero );
		}

		bool CanCallCredUI {
			get {
				Version version = Environment.OSVersion.Version;
				return !forceLegacySupport && ( Environment.OSVersion.Platform == PlatformID.Win32NT && ( ( version.Major == 5 && version.Minor > 0 ) || version.Major > 5 ) );
			}
		}

		#region Properties
		IntPtr hwndCredDialog;
		public IntPtr Handle {
			get { return hwndCredDialog; }
		}

		string username;
		public string Username {
			get { return username; }
			set { username = value; }
		}

		string password;
		public string Password {
			get { return password; }
		}

		string text;
		public string Text {
			get { return text; }
			set { text = value; }
		}

		string caption = "";
		public string Caption {
			get { return caption; }
			set { caption = value; }
		}

		string targetName;
		public string TargetName {
			get { return targetName; }
			set {
				targetName = value;
			}
		}

		bool save;
		public bool Save {
			get { return save; }
			set { save = value; }
		}

		ExtensionPanel panExtension;
		public ExtensionPanel ExtensionPanel {
			get { return panExtension; }
			set { panExtension = value; }
		}

		bool expectConfirmation;
		public bool ExpectConfirmation {
			get { return expectConfirmation; }
			set { expectConfirmation = value; }
		}

		bool forceUI;
		public bool ForceUI {
			get { return forceUI; }
			set { forceUI = value; }
		}

		bool forceLegacySupport = false;
		public bool ForceLegacySupport {
			get { return forceLegacySupport; }
			set { forceLegacySupport = value; }
		}
		#endregion

		#region Hooks
		bool isSetup;
		private void hook_WindowActivated ( object sender, CbtEventArgs e ) {
			if ( e.Handle == hwndCredDialog && !isSetup ) {
				isSetup = true;

				InitialiseExtensionPanel ( );
			}
		}

		private void hook_WindowCreated ( object sender, CbtEventArgs e ) {
			if ( e.IsDialogWindow && hwndCredDialog == IntPtr.Zero )
				hwndCredDialog = e.Handle;
		}

		private void hook_WindowDestroyed ( object sender, CbtEventArgs e ) {
			if ( e.Handle == hwndCredDialog ) {
				isSetup = false;
				hwndCredDialog = IntPtr.Zero;

				if ( panExtension != null ) {
					panExtension.Hide ( );
					SetParent ( panExtension.Handle, IntPtr.Zero );
				}
			}
		}

		void InitialiseExtensionPanel ( ) {
			if ( panExtension != null ) {
				panExtension.Dialog = this;
				SetParent ( panExtension.Handle, hwndCredDialog );
				panExtension.LayoutExtension ( new CredentialBounds ( hwndCredDialog ) );
				panExtension.Show ( );
			}
		}
		#endregion

		#region Win32 Imports
		#region Methods
		[DllImport ( "user32.dll" )]
		public static extern IntPtr SetParent ( IntPtr hWndChild, IntPtr hWndNewParent );
		#endregion
		#endregion
	}

	#region CredentialBounds
	public interface ICredentialBounds {
		Rectangle DialogBounds { get; }
		Rectangle RememberBounds { get; }
		Rectangle OKBounds { get; }
	}

	internal class CredentialBounds : ICredentialBounds {
		#region Win32 Imports
		#region Methods
		[DllImport ( "user32.dll" )]
		protected static extern int ScreenToClient ( IntPtr hwnd, Point pt );

		[DllImport ( "user32.dll" )]
		protected static extern int GetWindowRect ( IntPtr hwnd, Rect rc );

		[DllImport ( "user32.dll" )]
		protected static extern IntPtr GetDlgItem ( IntPtr hwnd, int id );
		#endregion
		#region Structs
		[StructLayout ( LayoutKind.Sequential )]
		public class Point {
			public Point ( int x, int y ) {
				this.x = x;
				this.y = y;
			}

			public int x;
			public int y;
		}

		[StructLayout ( LayoutKind.Sequential )]
		public class Rect {
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		#endregion
		#endregion

		IntPtr hwndDialog;

		public CredentialBounds ( IntPtr hwndDialog ) {
			this.hwndDialog = hwndDialog;
		}

		public Rectangle DialogBounds {
			get {
				Rect retVal = new Rect ( );
				GetWindowRect ( hwndDialog, retVal );
				return ScreenToClient ( retVal );
				//return new System.Drawing.Rectangle(retVal.left, retVal.top, retVal.right - retVal.left, retVal.bottom - retVal.top);
			}
		}

		public Rectangle RememberBounds {
			get {
				IntPtr hwndSysCred = GetDlgItem ( hwndDialog, 0x03EA );
				IntPtr hwndRemember = GetDlgItem ( hwndSysCred, 0x03EF );
				Rect retVal = new Rect ( );
				GetWindowRect ( hwndRemember, retVal );
				return ScreenToClient ( retVal );
				//return new System.Drawing.Rectangle(retVal.left, retVal.top, retVal.right - retVal.left, retVal.bottom - retVal.top);
			}
		}

		public Rectangle OKBounds {
			get {
				IntPtr hwndOK = GetDlgItem ( hwndDialog, 0x0001 );
				Rect retVal = new Rect ( );
				GetWindowRect ( hwndOK, retVal );
				return ScreenToClient ( retVal );
				//return new System.Drawing.Rectangle(retVal.left, retVal.top, retVal.right - retVal.left, retVal.bottom - retVal.top);
			}
		}

		public Rectangle ScreenToClient ( Rect rect ) {
			Point topLeft = new Point ( rect.left, rect.top );
			Point bottomRight = new Point ( rect.right, rect.bottom );
			ScreenToClient ( hwndDialog, topLeft );
			ScreenToClient ( hwndDialog, bottomRight );
			return new Rectangle ( topLeft.x, topLeft.y, bottomRight.x - topLeft.x, bottomRight.y - topLeft.y );
		}
	}
	#endregion

	#region Enum CbtHookAction
	// CBT hook actions
	public enum CbtHookAction : int {
		HCBT_MOVESIZE = 0,
		HCBT_MINMAX = 1,
		HCBT_QS = 2,
		HCBT_CREATEWND = 3,
		HCBT_DESTROYWND = 4,
		HCBT_ACTIVATE = 5,
		HCBT_CLICKSKIPPED = 6,
		HCBT_KEYSKIPPED = 7,
		HCBT_SYSCOMMAND = 8,
		HCBT_SETFOCUS = 9
	}
	#endregion

	#region Class CbtEventArgs
	public class CbtEventArgs : EventArgs {
		public IntPtr Handle;			// Win32 handle of the window
		public string Title;			// caption of the window
		public string ClassName;		// class of the window
		public bool IsDialogWindow;		// whether is a popup dialog
	}
	#endregion

	#region Class LocalCbtHook
	public class LocalCbtHook : LocalWindowsHook {
		// ************************************************************************
		// Event delegate
		public delegate void CbtEventHandler ( object sender, CbtEventArgs e );
		// ************************************************************************

		// ************************************************************************
		// Events 
		public event CbtEventHandler WindowCreated;
		public event CbtEventHandler WindowDestroyed;
		public event CbtEventHandler WindowActivated;
		// ************************************************************************


		// ************************************************************************
		// Internal properties
		protected IntPtr m_hwnd = IntPtr.Zero;
		protected string m_title = "";
		protected string m_class = "";
		protected bool m_isDialog = false;
		// ************************************************************************


		// ************************************************************************
		// Class constructor(s)
		public LocalCbtHook ( )
			: base ( HookType.WH_CBT ) {
			this.HookInvoked += new HookEventHandler ( CbtHookInvoked );
		}
		public LocalCbtHook ( HookProc func )
			: base ( HookType.WH_CBT, func ) {
			this.HookInvoked += new HookEventHandler ( CbtHookInvoked );
		}
		// ************************************************************************


		// ************************************************************************
		// Handles the hook event
		private void CbtHookInvoked ( object sender, HookEventArgs e ) {
			CbtHookAction code = (CbtHookAction)e.HookCode;
			IntPtr wParam = e.wParam;
			IntPtr lParam = e.lParam;

			// Handle hook events (only a few of available actions)
			switch ( code ) {
				case CbtHookAction.HCBT_CREATEWND:
					HandleCreateWndEvent ( wParam, lParam );
					break;
				case CbtHookAction.HCBT_DESTROYWND:
					HandleDestroyWndEvent ( wParam, lParam );
					break;
				case CbtHookAction.HCBT_ACTIVATE:
					HandleActivateEvent ( wParam, lParam );
					break;
			}

			return;
		}
		// ************************************************************************


		// ************************************************************************
		// Handle the CREATEWND hook event
		private void HandleCreateWndEvent ( IntPtr wParam, IntPtr lParam ) {
			// Cache some information
			UpdateWindowData ( wParam );

			// raise event
			OnWindowCreated ( );
		}
		// ************************************************************************


		// ************************************************************************
		// Handle the DESTROYWND hook event
		private void HandleDestroyWndEvent ( IntPtr wParam, IntPtr lParam ) {
			// Cache some information
			UpdateWindowData ( wParam );

			// raise event
			OnWindowDestroyed ( );
		}
		// ************************************************************************


		// ************************************************************************
		// Handle the ACTIVATE hook event
		private void HandleActivateEvent ( IntPtr wParam, IntPtr lParam ) {
			// Cache some information
			UpdateWindowData ( wParam );

			// raise event
			OnWindowActivated ( );
		}
		// ************************************************************************


		// ************************************************************************
		// Read and store some information about the window
		private void UpdateWindowData ( IntPtr wParam ) {
			// Cache the window handle
			m_hwnd = wParam;

			// Cache the window's class name
			StringBuilder sb1 = new StringBuilder ( );
			sb1.Capacity = 40;
			GetClassName ( m_hwnd, sb1, 40 );
			m_class = sb1.ToString ( );

			// Cache the window's title bar
			StringBuilder sb2 = new StringBuilder ( );
			sb2.Capacity = 256;
			GetWindowText ( m_hwnd, sb2, 256 );
			m_title = sb2.ToString ( );

			// Cache the dialog flag
			m_isDialog = ( m_class == "#32770" );
		}
		// ************************************************************************


		// ************************************************************************
		// Helper functions that fire events by executing user code
		protected virtual void OnWindowCreated ( ) {
			if ( WindowCreated != null ) {
				CbtEventArgs e = new CbtEventArgs ( );
				PrepareEventData ( e );
				WindowCreated ( this, e );
			}
		}
		protected virtual void OnWindowDestroyed ( ) {
			if ( WindowDestroyed != null ) {
				CbtEventArgs e = new CbtEventArgs ( );
				PrepareEventData ( e );
				WindowDestroyed ( this, e );
			}
		}
		protected virtual void OnWindowActivated ( ) {
			if ( WindowActivated != null ) {
				CbtEventArgs e = new CbtEventArgs ( );
				PrepareEventData ( e );
				WindowActivated ( this, e );
			}
		}
		// ************************************************************************


		// ************************************************************************
		// Prepare the event data structure
		private void PrepareEventData ( CbtEventArgs e ) {
			e.Handle = m_hwnd;
			e.Title = m_title;
			e.ClassName = m_class;
			e.IsDialogWindow = m_isDialog;
		}
		// ************************************************************************



		#region Win32 Imports
		// ************************************************************************
		// Win32: GetClassName
		[DllImport ( "user32.dll" )]
		protected static extern int GetClassName ( IntPtr hwnd,
			StringBuilder lpClassName, int nMaxCount );
		// ************************************************************************

		// ************************************************************************
		// Win32: GetWindowText
		[DllImport ( "user32.dll" )]
		protected static extern int GetWindowText ( IntPtr hwnd,
			StringBuilder lpString, int nMaxCount );
		// ************************************************************************
		#endregion
	}
	#endregion

	#region Class HookEventArgs
	public class HookEventArgs : EventArgs {
		public int HookCode;	// Hook code
		public IntPtr wParam;	// WPARAM argument
		public IntPtr lParam;	// LPARAM argument
	}
	#endregion

	#region Enum HookType
	// Hook Types
	public enum HookType : int {
		WH_JOURNALRECORD = 0,
		WH_JOURNALPLAYBACK = 1,
		WH_KEYBOARD = 2,
		WH_GETMESSAGE = 3,
		WH_CALLWNDPROC = 4,
		WH_CBT = 5,
		WH_SYSMSGFILTER = 6,
		WH_MOUSE = 7,
		WH_HARDWARE = 8,
		WH_DEBUG = 9,
		WH_SHELL = 10,
		WH_FOREGROUNDIDLE = 11,
		WH_CALLWNDPROCRET = 12,
		WH_KEYBOARD_LL = 13,
		WH_MOUSE_LL = 14
	}
	#endregion

	#region Class LocalWindowsHook
	public class LocalWindowsHook {
		// ************************************************************************
		// Filter function delegate
		public delegate int HookProc ( int code, IntPtr wParam, IntPtr lParam );
		// ************************************************************************

		// ************************************************************************
		// Internal properties
		protected IntPtr m_hhook = IntPtr.Zero;
		protected HookProc m_filterFunc = null;
		protected HookType m_hookType;
		// ************************************************************************

		// ************************************************************************
		// Event delegate
		public delegate void HookEventHandler ( object sender, HookEventArgs e );
		// ************************************************************************

		// ************************************************************************
		// Event: HookInvoked 
		public event HookEventHandler HookInvoked;
		protected void OnHookInvoked ( HookEventArgs e ) {
			if ( HookInvoked != null )
				HookInvoked ( this, e );
		}
		// ************************************************************************

		// ************************************************************************
		// Class constructor(s)
		public LocalWindowsHook ( HookType hook ) {
			m_hookType = hook;
			m_filterFunc = new HookProc ( this.CoreHookProc );
		}
		public LocalWindowsHook ( HookType hook, HookProc func ) {
			m_hookType = hook;
			m_filterFunc = func;
		}
		// ************************************************************************

		// ************************************************************************
		// Default filter function
		protected int CoreHookProc ( int code, IntPtr wParam, IntPtr lParam ) {
			if ( code < 0 )
				return CallNextHookEx ( m_hhook, code, wParam, lParam );

			// Let clients determine what to do
			HookEventArgs e = new HookEventArgs ( );
			e.HookCode = code;
			e.wParam = wParam;
			e.lParam = lParam;
			OnHookInvoked ( e );

			// Yield to the next hook in the chain
			return CallNextHookEx ( m_hhook, code, wParam, lParam );
		}
		// ************************************************************************

		// ************************************************************************
		// Install the hook
		public void Install ( ) {
			m_hhook = SetWindowsHookEx (
				m_hookType,
				m_filterFunc,
				IntPtr.Zero,
				(int)AppDomain.GetCurrentThreadId ( ) );
		}
		// ************************************************************************

		// ************************************************************************
		// Uninstall the hook
		public void Uninstall ( ) {
			UnhookWindowsHookEx ( m_hhook );
		}
		// ************************************************************************


		#region Win32 Imports
		// ************************************************************************
		// Win32: SetWindowsHookEx()
		[DllImport ( "user32.dll" )]
		protected static extern IntPtr SetWindowsHookEx ( HookType code,
			HookProc func,
			IntPtr hInstance,
			int threadID );
		// ************************************************************************

		// ************************************************************************
		// Win32: UnhookWindowsHookEx()
		[DllImport ( "user32.dll" )]
		protected static extern int UnhookWindowsHookEx ( IntPtr hhook );
		// ************************************************************************

		// ************************************************************************
		// Win32: CallNextHookEx()
		[DllImport ( "user32.dll" )]
		protected static extern int CallNextHookEx ( IntPtr hhook,
			int code, IntPtr wParam, IntPtr lParam );
		// ************************************************************************
		#endregion
	}
	#endregion

}
