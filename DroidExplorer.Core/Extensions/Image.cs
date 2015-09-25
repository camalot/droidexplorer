using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidExplorer.Core {
	public static partial class DroidExplorerCoreExtensions {
		public static Image Resize(this Image source,
										 int canvasWidth, int canvasHeight) {
			var originalWidth = source.Width;
			var originalHeight = source.Height;

			if(canvasHeight == originalHeight && canvasWidth == originalWidth) {
				return source;
			}

			System.Drawing.Image thumbnail =
					new Bitmap(canvasWidth, canvasHeight);
			System.Drawing.Graphics graphic =
									 System.Drawing.Graphics.FromImage(thumbnail);

			graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphic.SmoothingMode = SmoothingMode.HighQuality;
			graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphic.CompositingQuality = CompositingQuality.HighQuality;

			// Figure out the ratio
			double ratioX = (double)canvasWidth / (double)originalWidth;
			double ratioY = (double)canvasHeight / (double)originalHeight;
			// use whichever multiplier is smaller
			double ratio = ratioX < ratioY ? ratioX : ratioY;

			// now we can get the new height and width
			int newHeight = Convert.ToInt32(originalHeight * ratio);
			int newWidth = Convert.ToInt32(originalWidth * ratio);

			// Now calculate the X,Y position of the upper-left corner 
			// (one of these will always be zero)
			int posX = Convert.ToInt32((canvasWidth - (originalWidth * ratio)) / 2);
			int posY = Convert.ToInt32((canvasHeight - (originalHeight * ratio)) / 2);

			graphic.Clear(Color.Black); // white padding
			graphic.DrawImage(source, posX, posY, newWidth, newHeight);

			System.Drawing.Imaging.ImageCodecInfo[] info =
											 System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
			System.Drawing.Imaging.EncoderParameters encoderParameters;
			encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
			encoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
											 100L);
			return thumbnail;
		}
	}
}
