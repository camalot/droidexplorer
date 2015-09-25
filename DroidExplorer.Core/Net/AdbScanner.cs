using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;
namespace DroidExplorer.Core.Net {
	public sealed class AdbScanner {
		public async Task<IEnumerable<string>> ScanAsync() {
			var result = new List<string>();
			var vcmd = "host:version";
			var okay = "OKAY0004001d";

			foreach(var item in NetworkInterface.GetAllNetworkInterfaces()) {
				if(item.NetworkInterfaceType == NetworkInterfaceType.Loopback || item.OperationalStatus != OperationalStatus.Up) continue;
				this.LogDebug("Scanning network interface: {0}", item.Description);
				UnicastIPAddressInformationCollection UnicastIPInfoCol = item.GetIPProperties().UnicastAddresses;
				foreach(UnicastIPAddressInformation UnicatIPInfo in UnicastIPInfoCol) {
					try {
						var gateway = item.GetIPProperties().GatewayAddresses.First().Address;
						var range = new IPAddressRange("{0}/{1}".With(gateway, UnicatIPInfo.IPv4Mask));

						foreach(var ip in range.Addresses.Skip(110)) {
							for(var port = 5550; port < 5560; port++) {
								this.LogDebug("Connecting to {0}:{1}", ip, port);
								using(var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) {
									try {
										socket.Blocking = true;

										var aresult = socket.BeginConnect(ip, port, null, null);
										bool success = aresult.AsyncWaitHandle.WaitOne(200, true);
										if(!success) {
											this.LogWarn("Connection Timeout");
											socket.Close();
											continue;
										}
										this.LogDebug("Connection successful: {0}:{1}", ip, port);
										var b = new byte[12];
										var data = "{0}{1}\n".With(vcmd.Length.ToString("X4"), vcmd).GetBytes();
										var count = socket.Send(data, 0, data.Length, SocketFlags.None);
										if(count == 0) {
											throw new InvalidOperationException("Unable to send version command to adb");
										}
										socket.ReceiveBufferSize = 4;
										count = socket.Receive(b,0,4, SocketFlags.None);
										var msg = b.GetString();
										if(string.Compare(msg, okay, false) == 0) {
											result.Add("{0}:{1}".With(ip.ToString(), port));
											break;
										}
										socket.Close();
									} catch(InvalidOperationException ioe) {
										this.LogError(ioe.Message, ioe);
									}
								}

							}
						}

					} catch(Exception ex) {
						this.LogError(ex.Message, ex);
					}
				}
			}

			return result;
		}

	}
}
