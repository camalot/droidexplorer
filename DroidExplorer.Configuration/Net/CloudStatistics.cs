using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Web;
using DroidExplorer.Core;
using System.IO;
using DroidExplorer.Core.Configuration.Handlers;
using Camalot.Common.Extensions;

namespace DroidExplorer.Configuration.Net {
	public class CloudStatistics {

		private CloudStatistics() {
			var config = (CloudConfiguration)ConfigurationManager.GetSection("droidexplorer/cloud");
			HostUrl = new UriBuilder("http", config.Host, config.Port, config.Path).Uri.ToString();
			AppId = config.AppId;
			ApiKey = config.ApiKey;
			CloudRecordDelay = 7;
		}
		/// <summary>
		/// This is the number of days to wait between recording
		/// </summary>
		private int CloudRecordDelay { get; set; }
		private String HostUrl { get; set; }
		private String AppId { get; set; }
		private String ApiKey { get; set; }

		private static CloudStatistics _instance = null;
		public static CloudStatistics Instance {
			get { return _instance ?? (_instance = new CloudStatistics()); }
		}

		private static IEnumerable<string> FilteredProperties {
			get {
				return new String[] {
					"persist.radio.imei",
					"ril.IMEI",
					"ril.IMSI",
					"ril.iccid",
					"ril.serialnumber",
					"ro.boot.serialno",
					"ro.gsm.imei",
					"ro.ril.MEID",
					"ro.ril.MDN",
					"persist.radio.vzw.cdma.mdn"
				};
			}
		}

		public void RegisterModels(IEnumerable<DeviceListItem> dli) {
			try {
				var items = dli.ToList();
				var kvp = new List<KeyValuePair<String, String>>();

				var useragent = string.Format("{0}", this.GetType().Assembly.GetName().Version.ToString());
				var req = HttpWebRequest.Create(CreateUrl("model/add")) as HttpWebRequest;
				req.UserAgent = useragent;
				AddAuthenticationHeaders(req);

				for(int i = 0; i < items.Count; i++) {
					var d = items[i];
					// to post multiple items the array index needs to be in the prop name.
					kvp.Add(new KeyValuePair<string, string>("[{0}].Id".With(i), d.SerialNumber));
					kvp.Add(new KeyValuePair<string, string>("[{0}].ProductName".With(i), d.ProductName));
					kvp.Add(new KeyValuePair<string, string>("[{0}].ModelName".With(i), d.ModelName));
					kvp.Add(new KeyValuePair<string, string>("[{0}].DeviceName".With(i), d.DeviceName));
				}

				req.ContentType = "application/x-www-form-urlencoded";
				req.Method = "POST";

				var data = CreateFormattedPostRequest(kvp);
				this.LogDebug ( data );

				var bytes = data.GetBytes();
				req.ContentLength = bytes.Length;
				req.Timeout = 30 * 1000;
				using(var rs = req.GetRequestStream()) {
					rs.Write(bytes, 0, bytes.Length);
				}
				using(var resp = req.GetResponse() as HttpWebResponse) {
					if(resp.StatusCode != HttpStatusCode.OK) {
						this.LogError(String.Format("POST Statistics Failed (Error Code: {1}): {0}", resp.StatusDescription, resp.StatusCode));
					}
				}


			} catch(WebException wex) {
				this.LogError(wex.Message, wex);
			}
		}

		public void RegisterDevice( Managed.Adb.Device device, DeviceListItem dli) {

			try {
				var kvp = new List<KeyValuePair<String, String>>();
				var useragent = String.Format("{0}", this.GetType().Assembly.GetName().Version.ToString());


				// if the Id is an IP, we need to find the real SerialNumber.
				var realSerialNo = device.SerialNumber;
				var kvpid = new KeyValuePair<string, string>("Id", realSerialNo);
				if(realSerialNo.Contains(":")) {
					// it is probably an IP:PORT combo. Lets try to get the real serial number, or what we have if we can't find it.
					var tid = device.Properties.FirstOrValue(x => x.Key == "ro.serialno" || x.Key == "ro.boot.serialno", new KeyValuePair<string, string>("default.serialno", realSerialNo)).Value;
					if(!string.IsNullOrWhiteSpace(tid) && tid != realSerialNo) {
						kvpid = new KeyValuePair<string, string>("Id", tid);
						realSerialNo = tid;
					}
				}

				// should we record
				if(!ShouldRecordStatistics(realSerialNo)) {
					this.LogDebug("Skipping registration of device because delay not reached.");
					return;
				}

				kvp.Add(kvpid);
				kvp.Add(new KeyValuePair<string, string>("ProductName", dli.ProductName));
				kvp.Add(new KeyValuePair<string, string>("ModelName", dli.ModelName));
				kvp.Add(new KeyValuePair<string, string>("DeviceName", dli.DeviceName));

				device.Properties.Add("ro.droidexplorer.version", useragent);
				device.Properties.Add("ro.droidexplorer.root", true.ToString());
				device.Properties.Add("ro.droidexplorer.busybox", true.ToString());
				device.Properties.Add("ro.droidexplorer.architecture", Architecture.IsRunningX64 ? "x64" : "x86");
				device.Properties.Add("ro.droidexplorer.platform", Environment.OSVersion.Platform.ToString());
				device.Properties.Add("ro.droidexplorer.platformversion", Environment.OSVersion.VersionString);

				var propCount = 0;
				foreach(var item in device.Properties.Where(item => !FilteredProperties.Contains(item.Key))) {
					kvp.Add(new KeyValuePair<String, String>(String.Format("Properties[{0}].Name", propCount), item.Key));
					kvp.Add(new KeyValuePair<String, String>(String.Format("Properties[{0}].Value", propCount), item.Value));
					++propCount;
				}

				var req = HttpWebRequest.Create(CreateUrl("device/add")) as HttpWebRequest;
				req.UserAgent = useragent;
				AddAuthenticationHeaders(req);

				req.ContentType = "application/x-www-form-urlencoded";
				req.Method = "POST";

				var data = CreateFormattedPostRequest(kvp);
				this.LogDebug ( data );

				var bytes = data.GetBytes();
				req.ContentLength = bytes.Length;
				req.Timeout = 60 * 1000;
				using(var rs = req.GetRequestStream()) {
					rs.Write(bytes, 0, bytes.Length);
				}
				using(var resp = req.GetResponse() as HttpWebResponse) {
					if(resp.StatusCode != HttpStatusCode.OK) {
						this.LogError(string.Format("POST Statistics Failed (Error Code: {1}): {0}", resp.StatusDescription, resp.StatusCode));
					}
				}

				// track when we recorded this
				Settings.Instance.SystemSettings.SetLastRecordCloud(realSerialNo);
			} catch(WebException ex) {
				this.LogError(ex.Message, ex);
			}
		}

		private void AddAuthenticationHeaders(HttpWebRequest req) {
			req.Headers.Add("Authentication-Token", ApiKey);
			req.Headers.Add("Application-Identifier", AppId);
		}

		private String CreateUrl(String path) {
			var ub = new Uri(HostUrl);
			var u = new Uri(ub, path);

			return u.ToString();
		}

		private String CreateFormattedPostRequest(ICollection<KeyValuePair<String, String>> values) {
			var paramterBuilder = new StringBuilder();
			var counter = 0;
			foreach(var value in values) {
				paramterBuilder.AppendFormat("{0}={1}", value.Key, HttpUtility.UrlEncode(value.Value));
				if(counter != values.Count - 1) {
					paramterBuilder.Append("&");
				}
				counter++;
			}

			return paramterBuilder.ToString();
		}

		private bool ShouldRecordStatistics(string deviceId) {
			var date = Settings.Instance.SystemSettings.GetLastRecordCloud(deviceId);
			var plusOffset = date.AddDays(this.CloudRecordDelay);

			return plusOffset <= DateTime.UtcNow.Date;
		}
	}
}
