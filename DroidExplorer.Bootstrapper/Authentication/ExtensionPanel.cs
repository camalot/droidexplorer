using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace DroidExplorer.Bootstrapper.Authentication {
	public class ExtensionPanel : Panel {
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

		public ExtensionPanel ( ) {
		}

		#region Properties
		CredentialsDialog dia;
		public CredentialsDialog Dialog {
			get { return dia; }
			set { dia = value; }
		}
		#endregion

		public virtual void LayoutExtension ( ICredentialBounds bounds ) {
			Rectangle rcCredDialog = bounds.DialogBounds;
			Rectangle rcRemember = bounds.RememberBounds;
			Rectangle rcOK = bounds.OKBounds;

			Point topLeft = new Point ( rcRemember.Left, rcRemember.Bottom + 10 );
			Point bottomRight = new Point ( rcCredDialog.Right - 10, rcOK.Top - 10 );
			SetBounds ( topLeft.x, topLeft.y, bottomRight.x - topLeft.x, bottomRight.y - topLeft.y );
		}

		protected Rect GetDialogBounds ( ) {
			Rect retVal = new Rect ( );
			GetWindowRect ( dia.Handle, retVal );
			return retVal;
		}

		protected Rect GetRememberBounds ( ) {
			IntPtr hwndSysCred = GetDlgItem ( dia.Handle, 0x03EA );
			IntPtr hwndRemember = GetDlgItem ( hwndSysCred, 0x03EF );
			Rect retVal = new Rect ( );
			GetWindowRect ( hwndRemember, retVal );
			return retVal;
		}

		protected Rect GetOKBounds ( ) {
			IntPtr hwndOK = GetDlgItem ( dia.Handle, 0x0001 );
			Rect retVal = new Rect ( );
			GetWindowRect ( hwndOK, retVal );
			return retVal;
		}
	}
}
