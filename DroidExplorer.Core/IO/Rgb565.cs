using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using DroidExplorer.Core.Exceptions;

namespace DroidExplorer.Core.IO {
	public static class Rgb565 {
		public static Image ToImage ( String file ) {
			using ( FileStream fs = new FileStream ( file, FileMode.Open, FileAccess.Read ) ) {
				return ToImage ( fs );
			}
		}



		public static Image ToImage ( Stream stream ) {
			byte[] buffer = new byte[32768];
			using ( MemoryStream ms = new MemoryStream ( ) ) {
				while ( true ) {
					int read = stream.Read ( buffer, 0, buffer.Length );
					if ( read <= 0 ) {
						return ToImage ( ms.ToArray ( ) );
					}
					ms.Write ( buffer, 0, read );
				}
			}

		}

		public static Image ToImage( PixelFormat format, byte[] buffer ) {
			int pixels = buffer.Length / 2;
			Size imageSize = ScreenResolution.Instance.CalculateSize ( pixels );
			pixels = ScreenResolution.Instance.PixelsFromSize ( imageSize );
			Bitmap bitmap = new Bitmap ( imageSize.Width, imageSize.Height, format );
			BitmapData bitmapdata = bitmap.LockBits ( new Rectangle ( 0, 0, imageSize.Width, imageSize.Height ), ImageLockMode.WriteOnly, format );
			Bitmap image = new Bitmap ( imageSize.Width, imageSize.Height, format );
			for ( int i = 1; i < pixels * 2; i++ ) {
				Marshal.WriteByte ( bitmapdata.Scan0, i, buffer[i] );
			}
			bitmap.UnlockBits ( bitmapdata );
			using ( Graphics g = Graphics.FromImage ( image ) ) {
				g.DrawImage ( bitmap, new Point ( 0, 0 ) );
				return image;
			}

		}

		public static Image ToImage ( byte[] buffer ) {
			return ToImage ( PixelFormat.Format16bppRgb565, buffer );
		}

		public static Image ToImage( PixelFormat format, byte[] data, int width, int height ) {
			int pixels = data.Length / 2;
			Bitmap bitmap = null;
			Bitmap image = null;
			BitmapData bitmapdata = null;
			try {
				bitmap = new Bitmap ( width, height, format );
				bitmapdata = bitmap.LockBits ( new Rectangle ( 0, 0, width, height ), ImageLockMode.WriteOnly, format );
				image = new Bitmap ( width, height, format );

				for ( int i = 0; i < data.Length; i++ ) {
					Marshal.WriteByte ( bitmapdata.Scan0, i, data[i] );
				}
				bitmap.UnlockBits ( bitmapdata );
				using ( Graphics g = Graphics.FromImage ( image ) ) {
					g.DrawImage ( bitmap, new Point ( 0, 0 ) );
					return image;
				}

			} catch ( Exception ) {
				throw;
			}
		}

		public static Image ToImage ( byte[] data, int width, int height ) {
			return ToImage ( PixelFormat.Format16bppRgb565, data, width, height );
		}

		public static bool ToRgb565 ( this Image image, string file ) {
			try {
				Bitmap bmp = image as Bitmap;
				BitmapData bmpData = bmp.LockBits ( new Rectangle ( 0, 0, bmp.Width, bmp.Height ), ImageLockMode.ReadOnly, PixelFormat.Format16bppRgb565 );
				using ( FileStream fs = new FileStream ( file, FileMode.Create, FileAccess.Write, FileShare.Read ) ) {
					byte[] buffer = new byte[307200];
					Marshal.Copy ( bmpData.Scan0, buffer, 0, buffer.Length );
					fs.Write ( buffer, 0, buffer.Length );
				}
				bmp.UnlockBits ( bmpData );
				return true;
			} catch {
				return false;
			}
		}
	}

}
