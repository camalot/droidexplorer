using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DroidExplorer.Core.UI {
	public static class NativeApi {

		[StructLayout ( LayoutKind.Sequential )]
		public class LVBKIMAGE {
			public int ulFlags;
			public IntPtr hbm;
			public string pszImage;
			public int cchImageMax;
			public int xOffsetPercent;
			public int yOffsetPercent;
		}

		[StructLayout ( LayoutKind.Sequential )]
		public struct LVCOLUMN {
			public Int32 mask;
			public Int32 cx;
			[MarshalAs ( UnmanagedType.LPTStr )]
			public string pszText;
			public IntPtr hbm;
			public Int32 cchTextMax;
			public Int32 fmt;
			public Int32 iSubItem;
			public Int32 iImage;
			public Int32 iOrder;
		}

		internal const int WM_MOUSEMOVE = 0x0200;
		internal const int WM_NCMOUSEMOVE = 0x00A0;
		internal const int WM_NCLBUTTONDOWN = 0x00A1;
		internal const int WM_NCLBUTTONUP = 0x00A2;
		internal const int WM_NCLBUTTONDBLCLK = 0x00A3;
		internal const int WM_LBUTTONDOWN = 0x0201;
		internal const int WM_LBUTTONUP = 0x0202;
		internal const int WM_KEYDOWN = 0x0100;

		internal const int HTERROR = ( -2 );
		internal const int HTTRANSPARENT = ( -1 );
		internal const int HTNOWHERE = 0;
		internal const int HTCLIENT = 1;
		internal const int HTCAPTION = 2;
		internal const int HTSYSMENU = 3;
		internal const int HTGROWBOX = 4;
		internal const int HTSIZE = HTGROWBOX;
		internal const int HTMENU = 5;
		internal const int HTHSCROLL = 6;
		internal const int HTVSCROLL = 7;
		internal const int HTMINBUTTON = 8;
		internal const int HTMAXBUTTON = 9;
		internal const int HTLEFT = 10;
		internal const int HTRIGHT = 11;
		internal const int HTTOP = 12;
		internal const int HTTOPLEFT = 13;
		internal const int HTTOPRIGHT = 14;
		internal const int HTBOTTOM = 15;
		internal const int HTBOTTOMLEFT = 16;
		internal const int HTBOTTOMRIGHT = 17;
		internal const int HTBORDER = 18;
		internal const int HTREDUCE = HTMINBUTTON;
		internal const int HTZOOM = HTMAXBUTTON;
		internal const int HTSIZEFIRST = HTLEFT;
		internal const int HTSIZELAST = HTBOTTOMRIGHT;

		internal const int HTOBJECT = 19;
		internal const int HTCLOSE = 20;
		internal const int HTHELP = 21;

		internal const int NOERROR = 0x0;
		internal const int S_OK = 0x0;
		internal const int S_FALSE = 0x1;
		internal const int LVM_FIRST = 0x1000;
		internal const int LVM_SETBKIMAGE = LVM_FIRST + 68;
		internal const int LVM_SETTEXTBKCOLOR = LVM_FIRST + 38;
		internal const int LVBKIF_SOURCE_URL = 0x02;
		internal const int LVBKIF_STYLE_TILE = 0x10;
		internal const Int32 LVM_SETBKIMAGEW = ( LVM_FIRST + 138 );
		internal const Int32 LVBKIF_TYPE_WATERMARK = 0x10000000;
		internal const int LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1000 + 54;
		internal const int LVS_EX_HEADERDRAGDROP = 0x00000010;


		internal const uint CLR_NONE = 0xFFFFFFFF;



		internal const string UX_EXPLORER = "explorer";
		internal const int TV_FIRST = 0x1100;
		internal const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
		internal const int TVM_GETEXTENDEDSTYLE = TV_FIRST + 45;
		internal const int TVM_SETAUTOSCROLLINFO = TV_FIRST + 59;
		internal const int TVS_NOHSCROLL = 0x8000;
		internal const int TVS_EX_AUTOHSCROLL = 0x0020;
		internal const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;
		internal const int GWL_STYLE = -16;

		internal const Int32 HDI_FORMAT = 0x4;
		internal const Int32 HDF_SORTUP = 0x400;
		internal const Int32 HDF_SORTDOWN = 0x200;
		internal const Int32 LVM_GETHEADER = 0x101f;
		internal const Int32 HDM_GETITEM = 0x120b;
		internal const Int32 HDM_SETITEM = 0x120c;

		[DllImport ( "ole32.dll" )]
		internal extern static void CoUninitialize ( );

		[DllImport ( "ole32.dll" )]
		internal extern static int CoInitialize ( int pReserved );

		[DllImport ( "user32.dll" )]
		internal extern static int SendMessage ( IntPtr hwnd, uint msg, uint wParam, uint lParam );

		[DllImport ( "user32.dll" )]
		internal extern static int SendMessage ( IntPtr hwnd, uint msg, uint wParam, LVBKIMAGE lParam );

		[DllImport ( "user32.dll" )]
		internal static extern IntPtr SendMessage ( IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam );

		[DllImport ( "user32.dll", CharSet = CharSet.Unicode )]
		internal static extern int SendMessage ( IntPtr hWnd, UInt32 Msg, int wParam, IntPtr lParam );

		[DllImport ( "user32.dll" )]
		internal static extern IntPtr SendMessage ( IntPtr hwnd, Int32 msg, IntPtr wParam, ref LVCOLUMN lParam );

		[DllImport ( "user32.dll", CharSet = CharSet.Unicode )]
		internal static extern int SendMessage ( IntPtr hWnd, UInt32 Msg, int wParam, int lParam );

		[DllImport ( "uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true )]
		internal static extern int SetWindowTheme ( IntPtr hWnd, string appName, string partList );

		[DllImport ( "user32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl )]
		internal static extern short GetAsyncKeyState ( int vKey );

		[DllImport ( "user32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl )]
		internal static extern IntPtr GetDesktopWindow ( );


		[DllImport ( "user32.dll", CharSet = CharSet.Unicode )]
		internal static extern void SetWindowLong ( IntPtr hWnd, int nIndex, int dwNewLong );

		[DllImport ( "user32.dll", CharSet = CharSet.Unicode )]
		internal static extern int GetWindowLong ( IntPtr hWnd, int nIndex );


		public static bool IsWindow7OrLater {
			get {
				return ( Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1 ) || Environment.OSVersion.Version.Major > 6;
			}
		}
		public static bool IsWindowsVistaOrLater {
			get {
				return Environment.OSVersion.Version.Major >= 6;
			}
		}

		public static void SetVistaExplorerStyle ( this TreeView tv, bool fadeoutExpandos, bool showTreeLines ) {
			if ( IsWindowsVistaOrLater ) {
				NativeApi.SetWindowTheme ( tv.Handle, NativeApi.UX_EXPLORER, null );
				if ( fadeoutExpandos ) {
					NativeApi.SendMessage ( tv.Handle, NativeApi.TVM_SETEXTENDEDSTYLE, 0, NativeApi.TVS_EX_FADEINOUTEXPANDOS );
				}
				tv.ShowLines = showTreeLines;
			}
		}

		public static void SetVistaExplorerStyle ( this ListView lv ) {
			if ( IsWindowsVistaOrLater ) {
				NativeApi.SetWindowTheme ( lv.Handle, NativeApi.UX_EXPLORER, null );
			}
		}

		// listview extension method
		public static void SetAllowDraggableColumns ( this ListView lv, bool enabled ) {
			// Send the message to the listview control
			SendMessage ( lv.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_HEADERDRAGDROP, enabled ? LVS_EX_HEADERDRAGDROP : 0 );
		}

		public static void SetSortIcon ( this ListView lv, int columnIndex, SortOrder order ) {
			IntPtr columnHeader = SendMessage ( lv.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero );

			for ( int columnNumber = 0; columnNumber <= lv.Columns.Count - 1; columnNumber++ ) {
				IntPtr columnPtr = new IntPtr ( columnNumber );
				LVCOLUMN lvColumn = new LVCOLUMN ( );
				lvColumn.mask = HDI_FORMAT;
				SendMessage ( columnHeader, HDM_GETITEM, columnPtr, ref lvColumn );

				if ( !( order == System.Windows.Forms.SortOrder.None ) && columnNumber == columnIndex ) {
					switch ( order ) {
						case System.Windows.Forms.SortOrder.Ascending:
							lvColumn.fmt &= ~HDF_SORTDOWN;
							lvColumn.fmt |= HDF_SORTUP;
							break;
						case System.Windows.Forms.SortOrder.Descending:
							lvColumn.fmt &= ~HDF_SORTUP;
							lvColumn.fmt |= HDF_SORTDOWN;
							break;
					}
				} else {
					lvColumn.fmt &= ~HDF_SORTDOWN & ~HDF_SORTUP;
				}

				SendMessage ( columnHeader, HDM_SETITEM, columnPtr, ref lvColumn );
			}
		}

		/// <summary>
		/// Determines whether Recent Document Tracking is Enabled.
		/// </summary>
		/// <returns></returns>
		public static bool IsRecentDocumentTrackingEnabled ( ) {
			var userDisabled = false;
			var policyDisabled = false;
			using ( var key = Registry.CurrentUser.OpenSubKey ( @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced" ) ) {
				if ( key != null ) {
					userDisabled = (int)key.GetValue ( "Start_TrackDocs", 0 ) == 0;
				}
			}

			using ( var key = Registry.CurrentUser.OpenSubKey ( @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer" ) ) {
				if ( key != null ) {
					policyDisabled = (int)key.GetValue ( "NoRecentDocsHistory", 0 ) == 1;
				}
			}

			return !( userDisabled || policyDisabled );
		}

		/// <summary>
		/// VK is just a placeholder for VK (VirtualKey) general definitions
		/// </summary>
		public class VK {
			public const int VK_SHIFT = 0x10;
			public const int VK_CONTROL = 0x11;
			public const int VK_MENU = 0x12;
			public const int VK_ESCAPE = 0x1B;

			public static bool IsKeyPressed ( int KeyCode ) {
				return ( GetAsyncKeyState ( KeyCode ) & 0x0800 ) == 0;
			}
		}

		public class Bit {
			public static int HiWord ( int iValue ) {
				return ( ( iValue >> 16 ) & 0xFFFF );
			}
			public static int LoWord ( int iValue ) {
				return ( iValue & 0xFFFF );
			}
		}
	}
}
