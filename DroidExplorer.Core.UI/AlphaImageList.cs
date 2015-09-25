using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace DroidExplorer.Core.UI {
	/// <summary>
	/// 
	/// </summary>
	public class AlphaImageList {
		/// <summary>
		/// 
		/// </summary>
		public enum IconSize { Large, Small, Shell };

		/// <summary>
		/// API Constants
		/// </summary>
		private const uint SHGFI_ICON = 0x100;	// get icon
		private const uint SHGFI_LINKOVERLAY = 0x8000;	// put a link overlay on icon
		private const uint SHGFI_SELECTED = 0x10000;	// show icon in selected state
		private const uint SHGFI_LARGEICON = 0x0;		// get large icon
		private const uint SHGFI_SMALLICON = 0x1;		// get small icon
		private const uint SHGFI_OPENICON = 0x2;		// get open icon
		private const uint SHGFI_SHELLICONSIZE = 0x4;		// get shell size icon
		private const uint SHGFI_USEFILEATTRIBUTES = 0x10;		// use passed dwFileAttribute
		private const uint SHGFI_TYPENAME = 0x400;	// get file type name

		/// <summary>
		/// A SHFILEINFO structure used to extract the icon resource of a file or folder.
		/// </summary>
		[StructLayout ( LayoutKind.Sequential )]
		private struct SHFILEINFO {
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs ( UnmanagedType.ByValTStr, SizeConst = 260 )]
			public string szDisplayName;
			[MarshalAs ( UnmanagedType.ByValTStr, SizeConst = 80 )]
			public string szTypeName;
		};

		/// <summary>
		/// A BITMAPINFO stucture used to store an alphablended bitmap for adding to imagelist.
		/// </summary>
		[StructLayout ( LayoutKind.Sequential )]
		private class BITMAPINFO {
			public Int32 biSize;
			public Int32 biWidth;
			public Int32 biHeight;
			public Int16 biPlanes;
			public Int16 biBitCount;
			public Int32 biCompression;
			public Int32 biSizeImage;
			public Int32 biXPelsPerMeter;
			public Int32 biYPelsPerMeter;
			public Int32 biClrUsed;
			public Int32 biClrImportant;
			public Int32 colors;
		};

		[DllImport ( "comctl32.dll" )]
		private static extern bool ImageList_Add ( IntPtr hImageList, IntPtr hBitmap, IntPtr hMask );
		[DllImport ( "kernel32.dll" )]
		private static extern bool RtlMoveMemory ( IntPtr dest, IntPtr source, int dwcount );
		[DllImport ( "shell32.dll" )]
		private static extern IntPtr SHGetFileInfo ( string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags );
		[DllImport ( "user32.dll" )]
		private static extern IntPtr DestroyIcon ( IntPtr hIcon );
		[DllImport ( "gdi32.dll" )]
		private static extern IntPtr CreateDIBSection ( IntPtr hdc, [In, MarshalAs ( UnmanagedType.LPStruct )]BITMAPINFO pbmi, uint iUsage, out IntPtr ppvBits, IntPtr hSection, uint dwOffset );

		/// <summary>
		/// Initializes a new instance of the <see cref="AlphaImageList"/> class.
		/// </summary>
		private AlphaImageList ( ) { }

		/// <summary>
		/// Adds an alphablended image to an existing imagelist from an Image object.
		/// </summary>
		/// <param name="sourceImage">The Image object to add to the imagelist.</param>
		/// <param name="destinationImagelist">An existing imagelist to add the image to.</param>
		/// <example>.AddFromImage( myImage, toolbarImageList );</example>
		public static void AddFromImage ( Image sourceImage, ImageList destinationImagelist ) {
			Add ( (Bitmap)sourceImage, destinationImagelist );
		}

		/// <summary>
		/// Adds an alphablended icon to an existing imagelist from an Icon object.
		/// </summary>
		/// <param name="sourceIcon">The Icon object to add to the imagelist</param>
		/// <param name="destinationImagelist">An existing imagelist to add the icon to.</param>
		/// <example>.AddFromImage( myIcon, toolbarImageList );</example>
		public static void AddFromImage ( Icon sourceIcon, ImageList destinationImagelist ) {
			AddFromImage ( string.Format ( "Icon{0}", destinationImagelist.Images.Count + 1 ), Icon.FromHandle ( sourceIcon.Handle ), destinationImagelist );

		}

		/// <summary>
		/// Adds from image.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="sourceIcon">The source icon.</param>
		/// <param name="destinationImagelist">The destination imagelist.</param>
		public static void AddFromImage ( string key, Icon sourceIcon, ImageList destinationImagelist ) {
			destinationImagelist.Images.Add ( key, Icon.FromHandle ( sourceIcon.Handle ) );
		}

		/// <summary>
		/// Adds an alphablended icon or image to an existing imagelist from an external image or icon file.
		/// </summary>
		/// <param name="FileName">The full path to a 32bit alphablended file.</param>
		/// <param name="Imagelist">An existing imagelist to add the image or icon to.</param>
		/// <example>.FromFile( "C:\\file.ico", toolbarImagelist );</example>
		public static void AddFromFile ( string fileName, ImageList destinationImagelist ) {
			Bitmap bm = new Bitmap ( fileName );

			if ( bm.RawFormat.Guid == ImageFormat.Icon.Guid ) {
				Icon icn = new Icon ( fileName );
				destinationImagelist.Images.Add ( Path.GetFileName ( fileName ), Icon.FromHandle ( icn.Handle ) );
				icn.Dispose ( );
			} else {
				Add ( bm, destinationImagelist );
			}
			bm.Dispose ( );
		}

		/// <summary>
		/// Extracts the icon of an existing file or folder on the system and adds the icon to an imagelist.
		/// </summary>
		/// <param name="fileName">A file or folder path to extract the icon from.</param>
		/// <param name="iconSize">The size to extract.</param>
		/// <param name="selectedState">A flag to add the selected state of the icon.</param>
		/// <param name="openState">A flag to add the open state of the icon.</param>
		/// <param name="linkOverlay">A flag to add a shortcut overlay to the icon.</param>
		/// <param name="destinationImagelist">An existing imagelist to add the extracted icon to.</param>
		/// <example>.AddIconOfFile( "C:\\folder\file.exe", Narbware.IconSize.Small, false, false, false, toolbarImageList );</example>
		public static void AddIconOfFile ( string fileName, IconSize iconSize, bool selectedState, bool openState, bool linkOverlay, ImageList destinationImagelist ) {
			uint uFlags = ( ( iconSize == IconSize.Large ) ? SHGFI_LARGEICON : 0 ) |
							( ( iconSize == IconSize.Small ) ? SHGFI_SMALLICON : 0 ) |
							( ( iconSize == IconSize.Shell ) ? SHGFI_SHELLICONSIZE : 0 ) |
							( ( selectedState ) ? SHGFI_SELECTED : 0 ) |
							( ( openState ) ? SHGFI_OPENICON : 0 ) |
							( ( linkOverlay ) ? SHGFI_LINKOVERLAY : 0 );

			Add ( fileName, destinationImagelist, uFlags );
		}

		/// <summary>
		/// Extracts the icon of an existing file or folder on the system using default parameters and adds the icon to an imagelist.
		/// </summary>
		/// <param name="fileName">A file or folder path to extract the icon from.</param>
		/// <param name="destinationImagelist">An existing imagelist to add the extracted icon to.</param>
		/// <example>.AddIconOfFile( "C:\\folder\file.exe", toolbarImageList );</example>
		public static void AddIconOfFile ( string fileName, ImageList destinationImagelist ) {
			Add ( fileName, destinationImagelist, 0 );
		}

		/// <summary>
		/// Extracts a file extension icon from the system and adds the icon to an imagelist.
		/// </summary>
		/// <param name="fileExtension">A file extension to extract the icon from.</param>
		/// <param name="iconSize">The size to extract.</param>
		/// <param name="selectedState">A flag to add the selected state of the icon.</param>
		/// <param name="openState">A flag to add the open state of the icon.</param>
		/// <param name="linkOverlay">A flag to add a shortcut overlay to the icon.</param>
		/// <param name="destinationImagelist">An existing imagelist to add the extracted icon to.</param>
		/// <example>.AddIconOfFileExt( ".jpg", Narbware.IconSize.Large, false, false, false, toolbarImagelist );</example>
		public static void AddIconOfFileExt ( string fileExtension, IconSize iconSize, bool selectedState, bool openState, bool linkOverlay, ImageList destinationImagelist ) {
			uint uFlags = SHGFI_USEFILEATTRIBUTES |
							( ( iconSize == IconSize.Large ) ? SHGFI_LARGEICON : 0 ) |
							( ( iconSize == IconSize.Small ) ? SHGFI_SMALLICON : 0 ) |
							( ( iconSize == IconSize.Shell ) ? SHGFI_SHELLICONSIZE : 0 ) |
							( ( selectedState ) ? SHGFI_SELECTED : 0 ) |
							( ( openState ) ? SHGFI_OPENICON : 0 ) |
							( ( linkOverlay ) ? SHGFI_LINKOVERLAY : 0 );

			Add ( fileExtension.Insert ( 0, "." ), destinationImagelist, uFlags );
		}

		/// <summary>
		/// Extracts a file extension icon from the system using default parameters and adds the icon to an imagelist.
		/// </summary>
		/// <param name="fileExtension">A file extension to extract the icon from.</param>
		/// <param name="destinationImagelist">An existing imagelist to add the extracted icon to.</param>
		/// <example>.AddIconOfFileExt( ".jpg", toolbarImagelist );</example>
		public static void AddIconOfFileExt ( string fileExtension, ImageList destinationImagelist ) {
			Add ( fileExtension.Insert ( 0, "." ), destinationImagelist, SHGFI_USEFILEATTRIBUTES );
		}

		/// <summary>
		/// Adds a 32bit alphablended icon or image to an existing imagelist from an embedded image or icon.
		/// </summary>
		/// <param name="ResourceName">The name of an embedded resource.</param>
		/// <param name="Imagelist">An existing imagelist to add the resource to.</param>
		/// <example>.AddEmbeddedResource( "ProjectName.file.ico", toolbarImagelist );</example>
		public static void AddEmbeddedResource ( string resourceName, ImageList destinationImagelist ) {
			Bitmap bm = new Bitmap ( Assembly.GetExecutingAssembly ( ).GetManifestResourceStream ( resourceName ) );

			if ( bm.RawFormat.Guid == ImageFormat.Icon.Guid ) {
				Icon icn = new Icon ( Assembly.GetExecutingAssembly ( ).GetManifestResourceStream ( resourceName ) );
				destinationImagelist.Images.Add ( resourceName, Icon.FromHandle ( icn.Handle ) );
				icn.Dispose ( );
			} else {
				AddFromImage ( bm, destinationImagelist );
			}
			bm.Dispose ( );
		}

		/// <summary>
		/// This method is used to add alphablended system icons to an imagelist using win32 api. This an added feature
		/// which is not included in the framework.
		/// </summary>
		/// <param name="pszPath">Path of the file to extract icon from.</param>
		/// <param name="il">Imagelist to which the image will be added to.</param>
		/// <param name="uFlags">Options that return the specified icon.</param>
		private static void Add ( string pszPath, ImageList il, uint uFlags ) {
			Add ( Path.GetFileName ( pszPath ), pszPath, il, uFlags );
		}

		/// <summary>
		/// Adds the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="pszPath">The PSZ path.</param>
		/// <param name="il">The il.</param>
		/// <param name="uFlags">The u flags.</param>
		private static void Add ( string key, string pszPath, ImageList il, uint uFlags ) {
			SHFILEINFO SHInfo = new SHFILEINFO ( );

			IntPtr hImg = SHGetFileInfo ( pszPath, 0, ref SHInfo, (uint)Marshal.SizeOf ( SHInfo ), SHGFI_ICON | uFlags );
			il.Images.Add ( key, Icon.FromHandle ( SHInfo.hIcon ) );
			DestroyIcon ( SHInfo.hIcon );
		}

		/// <summary>
		/// This method is used to add alphablended images to an imagelist using win32 api functions to overcome the broken
		/// .Add() method of the imagelist control which loses the alphablend info of images.
		/// </summary>
		/// <param name="bm">Bitmap containing alphablended image.</param>
		/// <param name="il">Imagelist to which the image will be added to.</param>
		private static void Add ( Bitmap bm, ImageList il ) {
			IntPtr hBitmap, ppvBits;
			BITMAPINFO bmi = new BITMAPINFO ( );

			if ( bm.Size != il.ImageSize ) {	// resize the image to dimensions of imagelist before adding
				bm = new Bitmap ( bm, il.ImageSize.Width, il.ImageSize.Height );
			}
			bmi.biSize = 40;			// Needed for RtlMoveMemory()
			bmi.biBitCount = 32;		// Number of bits
			bmi.biPlanes = 1;			// Number of planes
			bmi.biWidth = bm.Width;		// Width of our new bitmap
			bmi.biHeight = bm.Height;	// Height of our new bitmap
			bm.RotateFlip ( RotateFlipType.RotateNoneFlipY );	// Required due to the way bitmap is copied
			hBitmap = CreateDIBSection ( new IntPtr ( 0 ), bmi, 0, out ppvBits, new IntPtr ( 0 ), 0 );
			BitmapData bitmapData = bm.LockBits ( new Rectangle ( 0, 0, bm.Width, bm.Height ), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb );
			RtlMoveMemory ( ppvBits, bitmapData.Scan0, bm.Height * bitmapData.Stride ); // copies the bitmap
			bm.UnlockBits ( bitmapData );
			ImageList_Add ( il.Handle, hBitmap, new IntPtr ( 0 ) ); // adds the new bitmap to the imagelist control
		}
	}
}
