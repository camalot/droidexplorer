using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Camalot.Common.Extensions;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;
using DroidExplorer.Core.UI;

namespace DroidExplorer.Plugins.UI {
	public partial class ScreenRecordForm : PluginForm {

		private readonly Size SMALL_SIZE = new Size ( 529, 293 );
		private readonly Size LARGE_SIZE = new Size ( 529, 358 );
		private readonly IList<VideoSize> CommonResolutions;


		public ScreenRecordForm ( IPluginHost pluginHost ) : base ( pluginHost ) {
			this.StickyWindow = new DroidExplorer.UI.StickyWindow ( this );
			CommonResolutions = GetCommonResolutions ( );
			InitializeComponent ( );

			var defaultFile = "screenrecord_{0}_{1}.mp4".With ( this.PluginHost.Device, DateTime.Now.ToString ( "yyyy-MM-dd-hh" ) );
			this.location.Text = "/sdcard/{0}".With ( defaultFile );

			var resolution = new VideoSize ( PluginHost.CommandRunner.GetScreenResolution ( ) );
			var sizes = CommonResolutions.Concat ( new List<VideoSize> { resolution } ).OrderBy ( x => x.Size.Width ).Select ( x => x ).ToList ( );
			resolutionList.DataSource = sizes;
			resolutionList.DisplayMember = "Display";
			resolutionList.ValueMember = "Size";
			resolutionList.SelectedItem = resolution;

			rotateList.DataSource = GetRotateArgumentsList ( );
			rotateList.DisplayMember = "Display";
			rotateList.ValueMember = "Arguments";

			var bitrates = new List<BitRate> ( );

			for ( int i = 1; i < 25; i++ ) {
				bitrates.Add ( new BitRate ( i ) );
			}

			bitrateList.DataSource = bitrates;
			bitrateList.DisplayMember = "Display";
			bitrateList.ValueMember = "Value";
			bitrateList.SelectedItem = bitrates.Single ( x => x.Mbps == 4 );
			var ts = new TimeSpan ( 0, 0, 0, timeLimit.Value, 0 );
			displayTime.Text = ts.ToString ( );
		}

		private IList<VideoSize> GetCommonResolutions ( ) {
			return new List<VideoSize> {
				new VideoSize(new Size(176 ,144)),
				new VideoSize(new Size(352 ,288)),
				new VideoSize(new Size(720 ,480)),
				new VideoSize(new Size(864 ,486)),
				new VideoSize(new Size(960 ,720)),
				new VideoSize(new Size(1024 ,576)),
				new VideoSize(new Size(1280 ,720)),
				new VideoSize(new Size(1366 ,768)),
				new VideoSize(new Size(1280 ,1080)),
				new VideoSize(new Size(1440 ,1080)),
				new VideoSize(new Size(1920 ,1080)),
				new VideoSize(new Size(1880 ,2048)),
			};
		}

		private IList<RotateArguments> GetRotateArgumentsList ( ) {
			return new List<RotateArguments> {
				new RotateArguments("None", "", false),
				new RotateArguments("90 degrees", "hflip,vflip", true),
				new RotateArguments("180 degrees", "hflip,vflip", false),
				new RotateArguments("270 degrees","",true)
			};
		}


		private void timeLimit_ValueChanged ( object sender, EventArgs e ) {
			var ts = new TimeSpan ( 0, 0, 0, timeLimit.Value, 0 );
			displayTime.Text = ts.ToString ( );
			remaining.Text = ts.ToString ( );
			progress.Maximum = timeLimit.Value;

		}

		private void browse_Click ( object sender, EventArgs e ) {
			var sfd = new Core.UI.SaveFileDialog ( "/sdcard/" ) {
				Title = "Save video capture...",
				DefaultExt = "mp4",
				Filter = "Video Files (*.mp4)|*.mp4",
				AddExtension = true,
				FilterIndex = 0,
				InitialDirectory = "/sdcard/",
				FileName = Core.IO.Path.GetFileName(location.Text)
			};
			if ( sfd.ShowDialog ( this ) == DialogResult.OK ) {
				var fn = sfd.FileName;
				if ( !fn.EndsWith ( ".mp4" ) ) {
					fn += ".mp4";
				}
				location.Text = fn;
			}
		}

		private void start_Click ( object sender, EventArgs e ) {
			this.Size = LARGE_SIZE;
			this.progress.Value = this.progress.Minimum;

			var timer = new System.Windows.Forms.Timer ( ) {
				Interval = 1000
			};

			var resSize = ( (VideoSize)resolutionList.SelectedItem ).Size;
			var bitrate = ( (BitRate)bitrateList.SelectedItem ).Value;
			var tsLimit = new TimeSpan ( 0, 0, timeLimit.Value );
			var fileLocation = location.Text;

			var saveAfter = copyToPC.Checked;
			var rotateArguments = ( (RotateArguments)rotateList.SelectedItem ).Arguments;
			var shouldRotate = !string.IsNullOrWhiteSpace ( rotateArguments );
			var deleteOffDevice = deleteAfterCopy.Checked;

			var thread = new Thread ( ( ) => {
				this.InvokeIfRequired ( ( ) => timer.Enabled = true );

				var result = PluginHost.CommandRunner.ScreenCapture (
					PluginHost.Device,
					resSize,
					bitrate,
					tsLimit,
					shouldRotate,
					fileLocation
				);

				if ( saveAfter ) {
					SaveFile ( fileLocation, rotateArguments, deleteOffDevice );
				}

				ToggleControlsEnabled ( true );
				this.InvokeIfRequired ( ( ) => this.Size = SMALL_SIZE );
			} );

			ToggleControlsEnabled ( false );
			timer.Tick += ( s, te ) => {
				progress.Increment ( progress.Step );
				var remainingSeconds = new TimeSpan ( 0, 0, ( timeLimit.Value + 1 ) - progress.Value );
				remaining.Text = remainingSeconds.ToString ( );

				// stop timer
				if ( progress.Value == progress.Maximum ) {
					timer.Enabled = false;
					remaining.Text = new TimeSpan ( 0, 0, 0 ).ToString ( );
					ToggleControlsEnabled ( true );
				}
			};


			thread.Start ( );
		}

		private void SaveFile ( string fileLocation, string rotateArguments, bool deleteOffDevice ) {

			var fileName = Core.IO.Path.GetFileName ( fileLocation );

			var sfd = new System.Windows.Forms.SaveFileDialog {
				Title = "Copy Capture to PC",
				DefaultExt = "mp4",
				Filter = "Video (*.mp4)|*.mp4",
				FilterIndex = 0,
				ValidateNames = true,
				FileName = fileName,
				AddExtension = true,
				OverwritePrompt = true,
			};

			this.InvokeIfRequired ( ( ) => {
				if ( sfd.ShowDialog ( this ) == DialogResult.OK ) {
					var finfo = DroidExplorer.Core.IO.FileInfo.Create ( System.IO.Path.GetFileName ( fileLocation ), 0, null,
					null, null, DateTime.Now, false, fileLocation );
					var tempFile = new System.IO.FileInfo ( System.IO.Path.Combine ( FolderManagement.TempFolder, "{0}.mp4".With ( System.IO.Path.GetFileNameWithoutExtension ( System.IO.Path.GetRandomFileName ( ) ) ) ) );
					var dest = new System.IO.FileInfo ( sfd.FileName );
					PluginHost.Pull ( finfo, tempFile );

					if ( !string.IsNullOrWhiteSpace ( rotateArguments ) ) {
						RotateVideo ( tempFile, dest, rotateArguments );
					} else {
						if ( dest.Exists ) {
							dest.Delete ( );
						}
						tempFile.MoveTo ( dest.FullName );
					}

					if ( deleteOffDevice ) {
						this.LogDebug ( "Deleting {0}", fileLocation );
						PluginHost.CommandRunner.DeleteFile ( fileLocation );
					}

				}
			} );


		}

		private void RotateVideo ( System.IO.FileInfo input, System.IO.FileInfo output, string rotateArguments ) {
			this.LogDebug ( "Getting FFMpeg tool path." );
			var ffmpeg = FolderManagement.GetBundledTool ( "ffmpeg.exe" );
			var args = Resources.Strings.FFMpegRotateArguments.With ( input.FullName, output.FullName, rotateArguments );
			var process = new System.Diagnostics.Process {
				StartInfo = new System.Diagnostics.ProcessStartInfo ( ffmpeg ) {
					Arguments = args
				}
			};
			this.LogDebug ( "Running ffmpeg: {0}".With ( args ) );
			process.Start ( );
		}

		private void ToggleControlsEnabled ( bool enabled ) {
			start.InvokeIfRequired ( ( ) => start.Enabled = enabled );
			resolutionList.InvokeIfRequired ( ( ) => resolutionList.Enabled = enabled );
			bitrateList.InvokeIfRequired ( ( ) => bitrateList.Enabled = enabled );
			timeLimit.InvokeIfRequired ( ( ) => timeLimit.Enabled = enabled );
			copyToPC.InvokeIfRequired ( ( ) => copyToPC.Enabled = enabled );
			browse.InvokeIfRequired ( ( ) => browse.Enabled = enabled );
			rotateList.InvokeIfRequired ( ( ) => rotateList.Enabled = enabled && copyToPC.Checked );
			deleteAfterCopy.InvokeIfRequired ( ( ) => deleteAfterCopy.Enabled = enabled && copyToPC.Checked );
		}

		private void location_TextChanged ( object sender, EventArgs e ) {
			start.Enabled = !string.IsNullOrWhiteSpace ( location.Text );
		}


		private void copyToPC_CheckedChanged ( object sender, EventArgs e ) {
			rotateList.Enabled = copyToPC.Checked;
			deleteAfterCopy.Enabled = copyToPC.Checked;
		}

		/// <summary>
		/// 
		/// </summary>
		public class BitRate {
			public BitRate ( int mbps ) {
				Mbps = mbps.RequirePositive ( );
			}

			public int Mbps { get; private set; }

			public string Display {
				get {
					return "{0} Mbps".With ( Mbps );
				}
			}

			public int Value {
				get {
					return Mbps * 1000000;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public class VideoSize {
			public VideoSize ( Size size ) {
				Size = size;
			}

			public Size Size { get; private set; }

			public string Display {
				get {
					return "{0}x{1}".With ( Size.Width, Size.Height );
				}
			}
		}

		public class RotateArguments {
			public RotateArguments ( string display, string arguments, bool shouldRotate ) {
				Display = display;
				Arguments = arguments;
				ShouldRotate = shouldRotate;
			}
			public bool ShouldRotate { get; set; }
			public string Display { get; set; }
			public string Arguments { get; set; }
		}

	}
}
