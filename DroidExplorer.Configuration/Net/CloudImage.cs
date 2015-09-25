using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using DroidExplorer.Core;
using System.IO;
using System.Drawing.Imaging;
using DroidExplorer.Core.Configuration.Handlers;

namespace DroidExplorer.Configuration.Net {
	public class CloudImage {

		private CloudImage ( ) {
			var config = (CloudConfiguration)ConfigurationManager.GetSection ( "droidexplorer/cloud" );
			HostUrl = new UriBuilder ( "http", config.Host, config.Port, "" ).Uri.ToString ( );
		}

		private String HostUrl { get; set; }

		private static CloudImage _instance = null;
		public static CloudImage Instance {
			get { return _instance ?? (_instance = new CloudImage ( )); }
		}

		public FileInfo GetImage ( String deviceType ) {
			var file = Path.Combine ( Settings.Instance.ProgramDataDirectory, String.Format ( @"assets\{0}.png", deviceType ) );

			return InternalGetImage ( deviceType,
				CreateUrl ( String.Format ( "image/{0}/png", deviceType ) ),
				file, ImageFormat.Png );

		}

		public FileInfo GetIcon ( String deviceType ) {
			var file = Path.Combine ( Settings.Instance.ProgramDataDirectory, String.Format ( @"assets\{0}.ico", deviceType ) );
			var curl = CreateUrl(String.Format("image/{0}/ico", deviceType));
			return InternalGetImage ( deviceType,
				CreateUrl(String.Format("image/{0}/ico", deviceType)),
				file, ImageFormat.Icon );
		}


		internal FileInfo InternalGetImage ( String deviceType, String url, String output, ImageFormat imageType ) {
			if ( String.IsNullOrEmpty ( deviceType ) ) {
				throw new ArgumentNullException ( "deviceType" );
			}
			var file = new FileInfo ( output );

			if ( !IsImageExpired(file) ) {
				return file;
			}

			this.LogDebug(url);

			try {
				var req = HttpWebRequest.Create ( url ) as HttpWebRequest;
				req.Method = "GET";
				req.Timeout = 8000;
				using ( var resp = req.GetResponse ( ) as HttpWebResponse ) {
					if ( resp.StatusCode != HttpStatusCode.OK ) {
						this.LogError ( String.Format ( "Error getting image: (code: {1}): {0}", resp.StatusDescription, resp.StatusCode ) );
					} else {
						using ( var rs = resp.GetResponseStream ( ) ) {
							if ( !file.Directory.Exists ) {
								file.Directory.Create ( );
							}
							using ( var fs = new FileStream ( file.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read ) ) {
								var buffer = new byte[8 * 1024];
								var bread = 0;
								while ( ( bread = rs.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
									fs.Write ( buffer, 0, bread );
								}
							}
						}
					}
				}
				if ( !file.Exists ) {
					// do we need to reset file so it exists?
					file = new FileInfo ( file.FullName );
				}
			} catch ( Exception e ) {
				this.LogError ( e.Message, e );

				var tfile = new FileInfo ( Path.Combine ( Settings.Instance.SystemSettings.InstallPath, String.Format(@"Assets\[DEFAULT]{0}",file.Extension) ) );
				if ( tfile.Exists) {
					tfile.CopyTo ( file.FullName );
					// do we need to reset file so it exists?
					file = new FileInfo ( file.FullName );
				}

			}
			return file;
		}

		/// <summary>
		/// This checks if the image is less than [EXPIRE_TIME]. if it is older, then it is expired.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		private bool IsImageExpired ( FileInfo file ) {
			var expiresOn = DateTime.Now.Date.AddDays ( 7 );
			return !file.Exists || expiresOn.CompareTo ( file.LastWriteTime.Date ) <= 0;
		}

		private String CreateUrl ( String path ) {
			var ub = new Uri ( HostUrl );
			var u = new Uri ( ub, path );
			return u.ToString ( );
		}
	}
}
