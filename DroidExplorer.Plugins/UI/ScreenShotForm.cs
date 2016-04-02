using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading;
using System.Net;
using DroidExplorer.Core.IO;
using DroidExplorer.Core;
using DroidExplorer.Core.UI;
using DroidExplorer.UI;
using System.Drawing.Drawing2D;
using Camalot.Common.Extensions;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI.Renderers.ToolStrip;

namespace DroidExplorer.Plugins.UI {
	/// <summary>
	/// 
	/// </summary>
	public partial class ScreenShotForm : PluginForm {
		private delegate void SetPictureBoxImageDelegate ( PictureBox pb, Image img );
		private delegate void SetToolStripItemEnabledDelegate ( ToolStripItem tsi, bool enabled );
		private delegate Image GetPictureBoxImageDelegate ( PictureBox pb );
		private delegate void SetIntegerDelegate ( int value );
		private delegate int GetIntegerDelegate ( );
		private delegate void GenericDelegate ( );
		private delegate bool GetToolStripButtonCheckedDelegate ( ToolStripButton tsi );
		private delegate void SetControlBooleanDelegate ( bool value );
		/// <summary>
		/// Initializes a new instance of the <see cref="ScreenShotForm"/> class.
		/// </summary>
		public ScreenShotForm ( IPluginHost pluginHost ) : base ( pluginHost ) {
			this.PluginHost = pluginHost.Require ( );
			InitializeComponent ( );
			//this.StickyWindow = new StickyWindow ( this );

			// set up the toolbar icons from the Resources project
			saveToolStripButton.Image = Resources.Images.saveHS;
			copyToolStripButton.Image = Resources.Images.Copy_6524;
			openInDefault.Image = Resources.Images.resource_16xLG;
			refreshToolStripButton.Image = Resources.Images.Refresh;
			PLModeToolStripButton.Image = Resources.Images.PortraitLandscapeHS;

			ResizeFromImage ( null );
			RefreshImage ( );
    }

		private Image CurrentImage { get; set; }
		private Image ScaledImage {
			get {
				return ResizeImage ( CurrentImage, 480, 800 );
			}
		}

		/// <summary>
		/// Handles the Click event of the refreshToolStripButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void refreshToolStripButton_Click ( object sender, EventArgs e ) {
			RefreshImage ( );
		}

		/// <summary>
		/// Gets the image from file.
		/// </summary>
		/// <param name="fb0">The FB0.</param>
		/// <returns></returns>
		private Image GetImageFromFile ( string fb0 ) {
			try {
				Image img = Rgb565.ToImage ( fb0 );
				System.IO.File.Delete ( fb0 );

				return img;
			} catch ( Exception ex ) {
				//System.Console.WriteLine ( ex );
				return null;
			}
		}


		/// <summary>
		/// Gets the image from raw image.
		/// </summary>
		/// <param name="ri">The ri.</param>
		/// <returns></returns>
		private Image GetImageFromRawImage ( Managed.Adb.RawImage ri ) {
			try {
				this.LogDebug ( "Image Size: {0}", ri.Size.ToString ( ) );
				if ( ri.Bpp == 16 ) {
					return ri.ToImage ( PixelFormat.Format16bppRgb565 );
				} else if ( ri.Bpp == 32 ) {
					return ri.ToImage ( PixelFormat.Format32bppArgb );
				} else {
					return Rgb565.ToImage ( ri.Data );
				}
			} catch ( Exception ex ) {
				//Console.WriteLine ( ex );
				return null;
			}
		}

		/// <summary>
		/// Refreshes the image.
		/// </summary>
		private void RefreshImage ( ) {
			if ( !ThreadRunning ) {
				new Thread ( new ThreadStart ( delegate {
					ThreadRunning = true;

					this.InvokeIfRequired ( ( ) => {
						SetToolStripItemEnabled ( copyToolStripButton, false );
						SetToolStripItemEnabled ( saveToolStripButton, false );
						SetToolStripItemEnabled ( PLModeToolStripButton, false );
						SetToolStripItemEnabled ( refreshToolStripButton, false );
						SetToolStripItemEnabled ( openInDefault, false );
					} );
					CurrentImage = GetScreenShot ( );

					if ( GetIsLandscapeModeChecked ( ) ) {
						CurrentImage = FlipImage ( CurrentImage );
					}


					this.InvokeIfRequired ( ( ) => {
						SetPictureBoxImage ( this.screen, null );
						SetPictureBoxImage ( this.screen, ScaledImage );
					} );
					ResizeFromImage ( ScaledImage );

					this.InvokeIfRequired ( ( ) => {
						SetToolStripItemEnabled ( copyToolStripButton, true );
						SetToolStripItemEnabled ( saveToolStripButton, true );
						SetToolStripItemEnabled ( PLModeToolStripButton, true );
						SetToolStripItemEnabled ( refreshToolStripButton, true );
						SetToolStripItemEnabled ( openInDefault, true );
					} );

					ThreadRunning = false;
				} ) ).Start ( );
			}
		}


		/// <summary>
		/// Gets the screen shot.
		/// </summary>
		/// <returns></returns>
		private Image GetScreenShot ( ) {

			var d = Managed.Adb.AdbHelper.Instance.GetDevices ( Managed.Adb.AndroidDebugBridge.SocketAddress ).Single ( m => m.SerialNumber == CommandRunner.Instance.DefaultDevice );

			//Managed.Adb.Device d = new Managed.Adb.Device ( CommandRunner.Instance.DefaultDevice, Managed.Adb.DeviceState.Online );
			Managed.Adb.RawImage ri = Managed.Adb.AdbHelper.Instance.GetFrameBuffer ( Managed.Adb.AndroidDebugBridge.SocketAddress, d );
			return GetImageFromRawImage ( ri );
		}


		/// <summary>
		/// Resizes from image.
		/// </summary>
		/// <param name="img">The img.</param>
		private void ResizeFromImage ( Image img ) {
			if ( img == null ) {
				img = new Bitmap ( 480,800 );
			}

			this.InvokeIfRequired ( ( ) => {
				this.Width = img.Width + ( SystemInformation.BorderSize.Width * 2 ) + 20;
				this.Height = GetToolStripBottom ( ) + img.Height + SystemInformation.BorderSize.Height + 20 + SystemInformation.CaptionHeight;
			} );

		}

		private Image ResizeImage ( Image source,
										 /* note changed names */
										 int canvasWidth, int canvasHeight ) {
			//Image image = Image.FromFile(path + originalFilename);

			var originalWidth = source.Width;
			var originalHeight = source.Height;

			System.Drawing.Image thumbnail =
					new Bitmap ( canvasWidth, canvasHeight ); // changed parm names
			System.Drawing.Graphics graphic =
									 System.Drawing.Graphics.FromImage ( thumbnail );

			graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphic.SmoothingMode = SmoothingMode.HighQuality;
			graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphic.CompositingQuality = CompositingQuality.HighQuality;

			/* ------------------ new code --------------- */

			// Figure out the ratio
			double ratioX = (double)canvasWidth / (double)originalWidth;
			double ratioY = (double)canvasHeight / (double)originalHeight;
			// use whichever multiplier is smaller
			double ratio = ratioX < ratioY ? ratioX : ratioY;

			// now we can get the new height and width
			int newHeight = Convert.ToInt32 ( originalHeight * ratio );
			int newWidth = Convert.ToInt32 ( originalWidth * ratio );

			// Now calculate the X,Y position of the upper-left corner 
			// (one of these will always be zero)
			int posX = Convert.ToInt32 ( ( canvasWidth - ( originalWidth * ratio ) ) / 2 );
			int posY = Convert.ToInt32 ( ( canvasHeight - ( originalHeight * ratio ) ) / 2 );

			graphic.Clear ( Color.Black ); // white padding
			graphic.DrawImage ( source, posX, posY, newWidth, newHeight );

			/* ------------- end new code ---------------- */

			System.Drawing.Imaging.ImageCodecInfo[] info =
											 ImageCodecInfo.GetImageEncoders ( );
			EncoderParameters encoderParameters;
			encoderParameters = new EncoderParameters ( 1 );
			encoderParameters.Param[0] = new EncoderParameter ( System.Drawing.Imaging.Encoder.Quality,
											 100L );
			//thumbnail.Save(path + newWidth + "." + originalFilename, info[1],
			//								 encoderParameters);
			return thumbnail;
		}

		/// <summary>
		/// Flips the image.
		/// </summary>
		/// <param name="img">The img.</param>
		/// <returns></returns>
		private Image FlipImage ( Image img ) {
			if ( GetIsLandscapeModeChecked ( ) ) {
				img.RotateFlip ( RotateFlipType.Rotate270FlipNone );
			} else {
				img.RotateFlip ( RotateFlipType.Rotate90FlipNone );
			}
			return img;
		}


		/// <summary>
		/// Handles the Click event of the saveToolStripButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void saveToolStripButton_Click ( object sender, EventArgs e ) {
			System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog ( );
			sfd.Filter = "Portable Network Graphic (PNG)|*.png";
			sfd.FilterIndex = 0;
			sfd.FileName = "screenshot-{0}.png".With ( DateTime.Now.ToString ( "yyyy-MM-dd-hhmmss" ) );
			if ( sfd.ShowDialog ( this ) == DialogResult.OK ) {
				this.CurrentImage.Save ( sfd.FileName, ImageFormat.Png );
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is portrait mode.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is portrait mode; otherwise, <c>false</c>.
		/// </value>
		public bool IsPortraitMode {
			get {
				if ( this.InvokeRequired ) {
					return ( (int)this.Invoke ( new GetIntegerDelegate ( this.GetWidth ) ) ) < (int)this.Invoke ( new GetIntegerDelegate ( this.GetHeight ) );
				} else {
					return this.Width < this.Height;
				}
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether [thread running].
		/// </summary>
		/// <value><c>true</c> if [thread running]; otherwise, <c>false</c>.</value>
		private bool ThreadRunning { get; set; }

		/// <summary>
		/// Gets the is landscape mode checked.
		/// </summary>
		/// <returns></returns>
		private bool GetIsLandscapeModeChecked ( ) {
			if ( this.InvokeRequired ) {
				return (bool)this.Invoke ( new GetToolStripButtonCheckedDelegate ( this.GetToolStripButtonChecked ), PLModeToolStripButton );
			} else {
				return this.PLModeToolStripButton.Checked;
			}
		}

		/// <summary>
		/// Gets the tool strip button checked.
		/// </summary>
		/// <param name="tsi">The tsi.</param>
		/// <returns></returns>
		private bool GetToolStripButtonChecked ( ToolStripButton tsi ) {
			return tsi.Checked;
		}

		/// <summary>
		/// Handles the Click event of the copyToolStripButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void copyToolStripButton_Click ( object sender, EventArgs e ) {
			Clipboard.SetImage ( CurrentImage );
		}

		/// <summary>
		/// Handles the CheckedChanged event of the PLModeToolStripButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void PLModeToolStripButton_CheckedChanged ( object sender, EventArgs e ) {
			Image baseImage = this.screen.Image.Clone ( ) as Image;
			this.SetPictureBoxImage ( screen, null );
			FlipImage ( baseImage );
			ResizeFromImage ( baseImage );
			this.SetPictureBoxImage ( screen, baseImage );
		}

		private void openInDefault_Click ( object sender, EventArgs e ) {
			var tempFolder = FolderManagement.TempFolder;
			var tempFile = System.IO.Path.Combine ( tempFolder, "screenshot-{0}.png".With ( DateTime.Now.ToString ( "yyyy-MM-dd-hhmmss" ) ) );
			this.CurrentImage.Save ( tempFile, ImageFormat.Png );

			var psi = new System.Diagnostics.ProcessStartInfo ( tempFile ) {
				UseShellExecute = true
			};
			psi.Verb = psi.Verbs.Contains ( "edit" ) ? "edit" : "";

			new System.Diagnostics.Process {
				StartInfo = psi,
			}.Start ( );
		}


		/// <summary>
		/// Handles the MouseUp event of the pictureBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		private void pictureBox_MouseUp ( object sender, MouseEventArgs e ) {
			if ( e.Button == MouseButtons.Right ) {
				PLModeToolStripButton.Checked = !PLModeToolStripButton.Checked;
			}
		}

		/// <summary>
		/// Handles the Click event of the PLModeToolStripButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void PLModeToolStripButton_Click ( object sender, EventArgs e ) {
			PLModeToolStripButton.Checked = !PLModeToolStripButton.Checked;
		}

		/// <summary>
		/// Sets the picture box image.
		/// </summary>
		/// <param name="pb">The pb.</param>
		/// <param name="img">The img.</param>
		private void SetPictureBoxImage ( PictureBox pb, Image img ) {
			pb.Image = img;
		}

		/// <summary>
		/// Gets the picture box image.
		/// </summary>
		/// <param name="pb">The pb.</param>
		/// <returns></returns>
		private Image GetPictureBoxImage ( PictureBox pb ) {
			return pb.Image;
		}

		/// <summary>
		/// Sets the tool strip item enabled.
		/// </summary>
		/// <param name="tsi">The tsi.</param>
		/// <param name="enabled">if set to <c>true</c> [enabled].</param>
		private void SetToolStripItemEnabled ( ToolStripItem tsi, bool enabled ) {
			tsi.Enabled = enabled;
		}

		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <returns></returns>
		private int GetWidth ( ) {
			return this.Width;
		}

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <returns></returns>
		private int GetHeight ( ) {
			return this.Height;
		}

		/// <summary>
		/// Sets the width.
		/// </summary>
		/// <param name="value">The value.</param>
		private void SetWidth ( int value ) {
			this.Width = value;
		}

		/// <summary>
		/// Sets the height.
		/// </summary>
		/// <param name="value">The value.</param>
		private void SetHeight ( int value ) {
			this.Height = value;
		}

		/// <summary>
		/// Gets the tool strip bottom.
		/// </summary>
		/// <returns></returns>
		private int GetToolStripBottom ( ) {
			return this.toolStrip1.Bottom;
		}

	}

}
