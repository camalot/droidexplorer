using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DroidExplorer.Core.IO {
	internal class ScreenResolution {
		public const int QVGA = 320 * 240;
		public const int WQVGA = 320 * 480;
		public const int WQVGA2 = 400 * 240;
		public const int WQVGA3 = 432 * 240;
		public const int VGA = 640 * 480;
		public const int WGA = 800 * 480;
		public const int WVGA = 768 * 480;
		public const int WVGA2 = 854 * 480;
		public const int DVGA = 960 * 640;
		public const int PAL = 576 * 520;
		public const int NTSC = 486 * 440;
		public const int SVGA = 800 * 600;
		public const int WSVGA = 1024 * 576;
		public const int XGA = 1024 * 768;
		public const int XGAPLUS = 1152 * 864;
		public const int HD720 = 1280 * 720;
		public const int WXGA = 1280 * 768;
		public const int WXGA2 = 1280 * 800;
		public const int WXGA3 = 1280 * 854;
		public const int SXGA = 1280 * 1024;
		public const int WXGA4 = 1366 * 768;
		public const int SXGAMINUS = 1280 * 960;
		public const int SXGAPLUS = 1400 * 1050;
		public const int WXGAPLUS = 1440 * 900;
		public const int HD900 = 1600 * 900;
		public const int WSXGA = 1600 * 1024;
		public const int WSXGAPLUS = 1680 * 1050;
		public const int UXGA = 1600 * 1200;
		public const int HD1080 = 1920 * 1080;
		public const int QWXGA = 2048 * 1152;
		public const int WUXGA = 1920 * 1200;
		public const int TXGA = 1920 * 1400;
		public const int QXGA = 2048 * 1536;
		public const int WQHD = 2560 * 1440;
		public const int WQXGA = 2560 * 1600;
		public const int QSXGA = 2560 * 2048;
		public const int QSXGAPLUS = 2800 * 2100;
		public const int WQSXGA = 3200 * 2048;
		public const int QUXGA = 3200 * 2400;
		public const int QFHD = 3840 * 2160;
		public const int WQUXGA = 3840 * 2400;
		public const int HD4K = 4096 * 2304;
		public const int HXGA = 4096 * 3072;
		public const int WHXGA = 5120 * 3200;
		public const int HSXGA = 5120 * 4096;
		public const int WHSXGA = 6400 * 4096;
		public const int HUXGA = 6400 * 4800;
		public const int SHV = 7680 * 4320;
		public const int WHUXGA = 7680 * 4800;

		private ScreenResolution ( ) {
			Sizes = new Dictionary<int, Size> ( );
			Sizes.Add ( QVGA, new Size ( 320, 240 ) );
			Sizes.Add ( WQVGA, new Size ( 320, 480 ) );
			Sizes.Add ( WQVGA2, new Size ( 400, 240 ) );
			Sizes.Add ( WQVGA3, new Size ( 432, 240 ) );
			Sizes.Add ( NTSC, new Size ( 486, 440 ) );
			Sizes.Add ( VGA, new Size ( 640, 480 ) );
			Sizes.Add ( WGA, new Size ( 480, 800 ) );
			Sizes.Add ( WVGA2, new Size ( 480, 854 ) );
			Sizes.Add ( DVGA, new Size ( 960, 640 ) );
			Sizes.Add ( PAL, new Size ( 576, 520 ) );
			Sizes.Add ( SVGA, new Size ( 800, 600 ) );
			Sizes.Add ( WSVGA, new Size ( 1024, 576 ) );
			Sizes.Add ( XGA, new Size ( 1024, 768 ) );
			Sizes.Add ( XGAPLUS, new Size ( 1152, 864 ) );
			Sizes.Add ( HD720, new Size ( 1280, 720 ) );
			Sizes.Add ( WXGA, new Size ( 1280, 768 ) );
			Sizes.Add ( WXGA2, new Size ( 1280, 800 ) );
			Sizes.Add ( WXGA3, new Size ( 1280, 854 ) );
			Sizes.Add ( SXGA, new Size ( 1280, 1024 ) );
			Sizes.Add ( WXGA4, new Size ( 1366, 768 ) );
			Sizes.Add ( SXGAMINUS, new Size ( 1280, 960 ) );
			Sizes.Add ( SXGAPLUS, new Size ( 1400, 1050 ) );
			Sizes.Add ( WXGAPLUS, new Size ( 1440, 900 ) );
			Sizes.Add ( HD900, new Size ( 1600, 900 ) );
			Sizes.Add ( WSXGA, new Size ( 1600, 1024 ) );
			Sizes.Add ( WSXGAPLUS, new Size ( 1680, 1050 ) );
			Sizes.Add ( UXGA, new Size ( 1600, 1200 ) );
			Sizes.Add ( HD1080, new Size ( 1920, 1080 ) );
			Sizes.Add ( QWXGA, new Size ( 2048, 1152 ) );
			Sizes.Add ( WUXGA, new Size ( 1920, 1200 ) );
			Sizes.Add ( TXGA, new Size ( 1900, 1400 ) );
			Sizes.Add ( QXGA, new Size ( 2048, 1536 ) );
			Sizes.Add ( WQHD, new Size ( 2560, 1440 ) );
			Sizes.Add ( WQXGA, new Size ( 2560, 1600 ) );
			Sizes.Add ( QSXGA, new Size ( 2560, 2048 ) );
			Sizes.Add ( QSXGAPLUS, new Size ( 2800, 2100 ) );
			Sizes.Add ( WQSXGA, new Size ( 3200, 2048 ) );
			Sizes.Add ( QUXGA, new Size ( 3200, 2400 ) );
			Sizes.Add ( QFHD, new Size ( 3840, 2160 ) );
			Sizes.Add ( WQUXGA, new Size ( 3840, 2400 ) );
			Sizes.Add ( HD4K, new Size ( 4096, 2304 ) );
			Sizes.Add ( HXGA, new Size ( 4096, 3072 ) );
			Sizes.Add ( WHXGA, new Size ( 5120, 3200 ) );
			Sizes.Add ( HSXGA, new Size ( 5120, 4096 ) );
			Sizes.Add ( WHSXGA, new Size ( 6400, 4096 ) );
			Sizes.Add ( HUXGA, new Size ( 6400, 4800 ) );
			Sizes.Add ( SHV, new Size ( 7680, 4320 ) );
			Sizes.Add ( WHUXGA, new Size ( 7680, 4800 ) );

			// not a real resolution its here for comparison purposes only.
			Sizes.Add ( int.MaxValue / 2, new Size ( int.MaxValue, int.MaxValue ) );
		}

		private static ScreenResolution _instance = null;
		public static ScreenResolution Instance {
			get {
				if ( _instance == null ) {
					_instance = new ScreenResolution ( );
				}
				return _instance;
			}
		}

		public Dictionary<int, Size> Sizes { get; private set; }


		public Size CalculateSize ( long pixels ) {
			// this doesnt work very well, unless the number of pixels is exact!
			List<int> ordered = new List<int> ( Sizes.Keys );
			ordered.Sort ( );

			Console.WriteLine ( "total sizes: {0}", ordered.Count );
			for ( int i = 0; i < ordered.Count - 1; i++ ) {
				if ( pixels >= ordered[i] && pixels < ordered[i + 1] ) {
					Console.WriteLine ( "Resolution: {0}", ordered[i] );
					return Sizes[ordered[i]];
				}
			}

			// throw exception because we couldnt find it.
			throw new IndexOutOfRangeException ( );

		}

		public int PixelsFromSize ( Size size ) {

			foreach ( var item in Sizes.Keys ) {
				var itemSize = Sizes[item];
				if ( size.Equals ( itemSize ) ) {
					Console.WriteLine ( "Size: {0}", itemSize.ToString ( ) );
					return item;
				}
			}

			throw new IndexOutOfRangeException ( );

		}
	}
}
