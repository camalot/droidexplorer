using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using DroidExplorer.Core.Configuration.Handlers;
using Newtonsoft.Json;
using Camalot.Common.Extensions;
using Camalot.Common.Serialization;

namespace DroidExplorer.Core.Net {
	public class VersionCheckService {

		public delegate void AsyncVersionCheck ( SoftwareRelease release );

		private VersionCheckService ( ) {
			var config = (CloudConfiguration)ConfigurationManager.GetSection ( "droidexplorer/cloud" );
			HostUrl = new UriBuilder ( "http", config.Host, config.Port, config.Path ).Uri.ToString ( );
		}
		private String HostUrl { get; set; }

		private static VersionCheckService _instance = null;
		public static VersionCheckService Instance {
			get { return _instance ?? (_instance = new VersionCheckService ( )); }
		}

		public SoftwareRelease GetLatestVersion ( ) {
			try {
				var req = HttpWebRequest.Create ( HostUrl + "update/latest/" ) as HttpWebRequest;
				var resp = req.GetResponse ( ) as HttpWebResponse;

				using ( var sr = new StreamReader ( resp.GetResponseStream ( ) ) ) {
					using ( var jr = new JsonTextReader ( sr ) ) {
						var serializer = JsonSerializationBuilder.Build().Create();
						return serializer.Deserialize<SoftwareRelease> ( jr );
					}
				}
			} catch ( Exception ex ) {
				this.LogWarn ( ex.Message, ex );
				return new SoftwareRelease {
					ID = 0,
					Description = "N/A",
					Name = "N/A",
					TimeStamp = DateTime.UtcNow,
					Version = new Version ( 0, 0, 0, 0 )
				};
			}
		}

		public void BeginGetLatestVersion ( AsyncVersionCheck callback ) {
			try {
				var req = HttpWebRequest.Create ( HostUrl + "update/latest/" ) as HttpWebRequest;
				req.Timeout = 15 * 1000;

				req.BeginGetResponse(delegate ( IAsyncResult result ) {
					try {
						var resp = ( result.AsyncState as HttpWebRequest ).EndGetResponse ( result ) as HttpWebResponse;
						using ( var sr = new StreamReader ( resp.GetResponseStream ( ) ) ) {
							using ( var jr = new JsonTextReader ( sr ) ) {
								var serializer = JsonSerializationBuilder.Build().Create();
								var version = serializer.Deserialize<SoftwareRelease>(jr);
								if ( callback != null ) {
									callback ( version );
								}
							}
						}
					} catch ( Exception e ) {
						this.LogWarn ( e.Message, e );
					}
				}, req );
			} catch ( Exception ex ) {
				this.LogWarn ( ex.Message, ex );
			}
		}

		public Version GetVersion ( Type type ) {
			return type.Assembly.GetName ( ).Version;
		}

		public Version GetVersion ( ) {
			return this.GetVersion ( this.GetType ( ) );
		}
	}
}
