using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using DroidExplorer.Core.Exceptions;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.Adb {
	public class AdbHelper {
		public static String DEFAULT_ENCODING = "ISO-8859-1";

		private AdbHelper ( ) {

		}

		private static AdbHelper _instance = null;
		public static AdbHelper Instance {
			get {
				if ( _instance == null ) {
					_instance = new AdbHelper ( );
				}
				return _instance;
			}
		}

		private const int WAIT_TIME = 5;

		public Socket Open ( IPAddress address, IDevice device, int port ) {
			Socket s = new Socket ( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			try {
				s.Connect ( address, port );
				s.Blocking = true;
				s.NoDelay = false;

				SetDevice ( s, device );

				byte[] req = CreateAdbForwardRequest ( null, port );
				if ( !Write ( s, req ) ) {
					throw new IOException ( "failed submitting request to ADB" );
				}
				AdbResponse resp = ReadAdbResponse ( s, false );
				if ( !resp.Okay ) {
					throw new IOException ( "connection request rejected" );
				}
				s.Blocking = true;
			} catch ( IOException ) {
				s.Close ( );
				throw;
			}
			return s;
		}

		public int GetAdbVersion ( IPEndPoint address ) {
			byte[] request = FormAdbRequest ( "host:version" );
			byte[] reply;
			Socket adbChan = new Socket ( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			try {
				adbChan.Connect ( address );
				adbChan.Blocking = true;
				if ( !Write ( adbChan, request ) )
					throw new IOException ( "failed asking for adb version" );

				AdbResponse resp = ReadAdbResponse ( adbChan, false /* readDiagString */);
				if ( !resp.IOSuccess || !resp.Okay ) {
					this.LogError ( "Got timeout or unhappy response from ADB req: " + resp.Message );
					adbChan.Close ( );
					return -1;
				}

				reply = new byte[4];
				if ( !Read ( adbChan, reply ) ) {
					this.LogError ( "error in getting data length" );

					adbChan.Close ( );
					return -1;
				}

				String lenHex = Encoding.Default.GetString ( reply );
				int len = int.Parse ( lenHex, System.Globalization.NumberStyles.HexNumber );

				// the protocol version.
				reply = new byte[len];
				if ( !Read ( adbChan, reply ) ) {
					this.LogError ( "did not get the version info" );

					adbChan.Close ( );
					return -1;
				}

				String sReply = Encoding.Default.GetString ( reply );
				return int.Parse ( sReply, System.Globalization.NumberStyles.HexNumber );

			} catch ( Exception ex ) {
				Console.WriteLine ( ex );
				throw;
			}
		}

		public byte[] CreateAdbForwardRequest ( String address, int port ) {
			String request;

			if ( address == null )
				request = "tcp:" + port;
			else
				request = "tcp:" + port + ":" + address;
			return FormAdbRequest ( request );
		}

		private byte[] FormAdbRequest ( String req ) {
			String resultStr = String.Format ( "{0}{1}\n", req.Length.ToString ( "X4" ), req ); //$NON-NLS-1$
			byte[] result;
			try {
				result = Encoding.Default.GetBytes ( resultStr );
			} catch ( EncoderFallbackException efe ) {
				this.LogError ( efe.Message, efe );
				return null;
			}

			System.Diagnostics.Debug.Assert ( result.Length == req.Length + 5, String.Format ( "result: {1}{0}\nreq: {3}{2}", result.Length, Encoding.Default.GetString ( result ), req.Length, req ) );
			return result;
		}

		private bool Write ( Socket socket, byte[] data ) {
			try {
				Write ( socket, data, -1, 5 * 1000 );
			} catch ( IOException e ) {
				this.LogError ( e.Message, e );
				return false;
			}

			return true;
		}

		private void Write ( Socket socket, byte[] data, int length, int timeout ) {
			//using ( var buf = new MemoryStream ( data, 0, length != -1 ? length : data.Length ) ) {
			int numWaits = 0;
			int count = -1;

			//while ( buf.Position != buf.Length ) {
			try {
				count = socket.Send ( data, 0, length != -1 ? length : data.Length, SocketFlags.None );
				if ( count < 0 ) {
					throw new IOException ( "channel EOF" );
				} else if ( count == 0 ) {
					// TODO: need more accurate timeout?
					if ( timeout != 0 && numWaits * WAIT_TIME > timeout ) {
						throw new IOException ( "timeout" );
					}
					// non-blocking spin
					Thread.Sleep ( WAIT_TIME );
					numWaits++;
				} else {
					numWaits = 0;
				}
			} catch ( SocketException sex ) {
				Console.WriteLine ( sex );
				throw;
			}
			//}
			//}
		}

		AdbResponse ReadAdbResponse ( Socket socket, bool readDiagString ) {

			AdbResponse resp = new AdbResponse ( );

			byte[] reply = new byte[4];
			if ( !Read ( socket, reply ) ) {
				return resp;
			}
			resp.IOSuccess = true;

			if ( IsOkay ( reply ) ) {
				resp.Okay = true;
			} else {
				readDiagString = true; // look for a reason after the FAIL
				resp.Okay = false;
			}

			// not a loop -- use "while" so we can use "break"
			while ( readDiagString ) {
				// length string is in next 4 bytes
				byte[] lenBuf = new byte[4];
				if ( !Read ( socket, lenBuf ) ) {
					Console.WriteLine ( "Expected diagnostic string not found" );
					break;
				}

				String lenStr = ReplyToString ( lenBuf );

				int len;
				try {
					len = int.Parse ( lenStr, System.Globalization.NumberStyles.HexNumber );

				} catch ( FormatException nfe ) {
					this.LogError ( "Expected digits, got '" + lenStr + "': "
										+ lenBuf[0] + " " + lenBuf[1] + " " + lenBuf[2] + " "
										+ lenBuf[3] );
					this.LogError ( "reply was " + ReplyToString ( reply ) );
					break;
				}

				byte[] msg = new byte[len];
				if ( !Read ( socket, msg ) ) {
					this.LogError ( "Failed reading diagnostic string, len=" + len );
					break;
				}

				resp.Message = ReplyToString ( msg );
				this.LogError ( "Got reply '" + ReplyToString ( reply ) + "', diag='"
								+ resp.Message + "'" );

				break;
			}

			return resp;
		}

		private bool Read ( Socket socket, byte[] data ) {
			try {
				Read ( socket, data, -1, 15 * 1000 );
			} catch ( IOException e ) {
				this.LogError ( "readAll: IOException: " + e.Message, e );
				return false;
			}

			return true;
		}

		private void Read ( Socket socket, byte[] data, int length, int timeout ) {
			int expLen = length != -1 ? length : data.Length;
			using ( var buf = new MemoryStream ( expLen ) ) {
				buf.Position = 0;
				int numWaits = 0;
				int count = -1;
				while ( count != 0 ) {
					try {
						socket.ReceiveBufferSize = expLen;
						byte[] buffer = new byte[socket.ReceiveBufferSize];
						count = socket.Receive ( buffer );
						if ( count < 0 ) {
							this.LogError ( "read: channel EOF" );
							throw new IOException ( "EOF" );
						} else if ( count == 0 ) {
							// TODO: need more accurate timeout?
							if ( timeout != 0 && numWaits * WAIT_TIME > timeout ) {
								this.LogError ( "read: timeout" );
								throw new IOException ( "timeout" );
							}
							// non-blocking spin
							Thread.Sleep ( WAIT_TIME );
							numWaits++;
						} else {
							//Console.WriteLine ( Encoding.Default.GetString ( buffer ) );
							buf.Write ( buffer, 0, count );
							numWaits = 0;

							if ( buf.Position == buf.Length ) {
								if ( buf.Length >= expLen ) {
									byte[] outBuffer = buf.ToArray ( );
									Array.Copy ( outBuffer, data, data.Length );
									count = -1;
									break;
								}
							}
						}
					} catch ( SocketException sex ) {
						throw new IOException ( String.Format ( "No Data to read: {0}", sex.Message ) );
					}
				}


			}
		}

		private byte[] CreateJdwpForwardRequest ( int pid ) {
			String req = String.Format ( "jdwp:{0}", pid );
			return FormAdbRequest ( req );
		}

		public bool CreateForward ( IPEndPoint adbSockAddr, Device device, int localPort, int remotePort ) {

			Socket adbChan = new Socket ( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			try {
				adbChan.Connect ( adbSockAddr );
				adbChan.Blocking = true;

				byte[] request = FormAdbRequest ( String.Format ( "host-serial:{0}:forward:tcp:{1};tcp:{2}", //$NON-NLS-1$
								device.SerialNumber, localPort, remotePort ) );

				if ( !Write ( adbChan, request ) ) {
					throw new IOException ( "failed to submit the forward command." );
				}

				AdbResponse resp = ReadAdbResponse ( adbChan, false /* readDiagString */);
				if ( !resp.IOSuccess || !resp.Okay ) {
					throw new IOException ( "Device rejected command: " + resp.Message );
				}
			} finally {
				if ( adbChan != null ) {
					adbChan.Close ( );
				}
			}

			return true;
		}

		private bool IsOkay ( byte[] reply ) {
			return reply[0] == (byte)'O' && reply[1] == (byte)'K'
								&& reply[2] == (byte)'A' && reply[3] == (byte)'Y';
		}

		private String ReplyToString ( byte[] reply ) {
			String result;
			try {
				result = Encoding.Default.GetString ( reply );
			} catch ( DecoderFallbackException uee ) {
				this.LogError ( uee.Message, uee );
				result = "";
			}
			return result;
		}

		public List<Device> GetDevices ( IPEndPoint address ) {
			byte[] request = FormAdbRequest ( "host:devices" ); //$NON-NLS-1$
			byte[] reply;
			Socket socket = new Socket ( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

			try {
				socket.Connect ( address );
				socket.Blocking = true;
				if ( !Write ( socket, request ) )
					throw new IOException ( "failed asking for devices" );

				AdbResponse resp = ReadAdbResponse ( socket, false /* readDiagString */);
				if ( !resp.IOSuccess || !resp.Okay ) {
					this.LogError ( "Got timeout or unhappy response from ADB fb req: " + resp.Message );
					socket.Close ( );
					return null;
				}

				reply = new byte[4];
				if ( !Read ( socket, reply ) ) {
					this.LogError ( "error in getting data length" );
					socket.Close ( );
					return null;
				}
				String lenHex = Encoding.Default.GetString ( reply );
				int len = int.Parse ( lenHex, System.Globalization.NumberStyles.HexNumber );

				reply = new byte[len];
				if ( !Read ( socket, reply ) ) {
					this.LogError ( "error in getting data" );
					socket.Close ( );
					return null;
				}

				List<Device> s = new List<Device> ( );
				String[] data = Encoding.Default.GetString ( reply ).Split ( new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries );
				foreach ( var item in data ) {
					s.Add ( Device.CreateFromAdbData ( item ) );
				}
				return s;
			} finally {
				socket.Close ( );
			}
		}

		public RawImage GetFrameBuffer ( IPEndPoint adbSockAddr, IDevice device ) {

			RawImage imageParams = new RawImage ( );
			byte[] request = FormAdbRequest ( "framebuffer:" ); //$NON-NLS-1$
			byte[] nudge = {
						0
				};
			byte[] reply;

			Socket adbChan = new Socket ( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			try {
				adbChan.Connect ( adbSockAddr );
				adbChan.Blocking = true;

				// if the device is not -1, then we first tell adb we're looking to talk
				// to a specific device
				SetDevice ( adbChan, device );
				if ( !Write ( adbChan, request ) )
					throw new IOException ( "failed asking for frame buffer" );

				AdbResponse resp = ReadAdbResponse ( adbChan, false /* readDiagString */);
				if ( !resp.IOSuccess || !resp.Okay ) {
					this.LogError ( "Got timeout or unhappy response from ADB fb req: " + resp.Message );
					adbChan.Close ( );
					return null;
				}

				// first the protocol version.
				reply = new byte[4];
				if ( !Read ( adbChan, reply ) ) {
					this.LogError ( "got partial reply from ADB fb:" );

					adbChan.Close ( );
					return null;
				}
				BinaryReader buf;
				int version = 0;
				using ( MemoryStream ms = new MemoryStream ( reply ) ) {
					buf = new BinaryReader ( ms );

					version = buf.ReadInt32 ( );
				}

				// get the header size (this is a count of int)
				int headerSize = RawImage.GetHeaderSize ( version );
				// read the header
				reply = new byte[headerSize * 4];
				if ( !Read ( adbChan, reply ) ) {
					this.LogWarn ( "got partial reply from ADB fb:" );

					adbChan.Close ( );
					return null;
				}
				using ( MemoryStream ms = new MemoryStream ( reply ) ) {
					buf = new BinaryReader ( ms );

					// fill the RawImage with the header
					if ( imageParams.ReadHeader ( version, buf ) == false ) {
						this.LogWarn ( "Unsupported protocol: " + version );
						return null;
					}
				}

				this.LogDebug ( "image params: bpp=" + imageParams.Bpp + ", size="
								+ imageParams.Size + ", width=" + imageParams.Width
								+ ", height=" + imageParams.Height );

				if ( !Write ( adbChan, nudge ) )
					throw new IOException ( "failed nudging" );

				reply = new byte[imageParams.Size];
				if ( !Read ( adbChan, reply ) ) {
					this.LogWarn ( "got truncated reply from ADB fb data" );
					adbChan.Close ( );
					return null;
				}

				imageParams.Data = reply;
			} finally {
				if ( adbChan != null ) {
					adbChan.Close ( );
				}
			}

			return imageParams;
		}

		/// <summary>
		/// Executes a shell command on the remote device
		/// </summary>
		/// <param name="endPoint">The end point.</param>
		/// <param name="command">The command.</param>
		/// <param name="device">The device.</param>
		/// <param name="rcvr">The RCVR.</param>
		/// <remarks>Should check if you CanSU before calling this.</remarks>
		public void ExecuteRemoteRootCommand ( IPEndPoint endPoint, String command, Device device, IShellOutputReceiver rcvr ) {
			ExecuteRemoteRootCommand ( endPoint, String.Format ( "su -c \"{0}\"", command ), device, rcvr, int.MaxValue );
		}

		/// <summary>
		/// Executes a shell command on the remote device
		/// </summary>
		/// <param name="endPoint">The end point.</param>
		/// <param name="command">The command.</param>
		/// <param name="device">The device.</param>
		/// <param name="rcvr">The RCVR.</param>
		/// <param name="maxTimeToOutputResponse">The max time to output response.</param>
		public void ExecuteRemoteRootCommand ( IPEndPoint endPoint, String command, Device device, IShellOutputReceiver rcvr, int maxTimeToOutputResponse ) {
			ExecuteRemoteCommand ( endPoint, String.Format ( "su -c \"{0}\"", command ), device, rcvr );
		}

		/// <summary>
		/// Executes the remote command.
		/// </summary>
		/// <param name="endPoint">The end point.</param>
		/// <param name="command">The command.</param>
		/// <param name="device">The device.</param>
		/// <param name="rcvr">The RCVR.</param>
		/// <param name="maxTimeToOutputResponse">The max time to output response.</param>
		/// <exception cref="AdbException">failed submitting shell command</exception>
		/// <exception cref="System.OperationCanceledException"></exception>
		/// <exception cref="Managed.Adb.Exceptions.ShellCommandUnresponsiveException"></exception>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
		/// <exception cref="UnknownOptionException"></exception>
		/// <exception cref="CommandAbortingException"></exception>
		/// <exception cref="PermissionDeniedException"></exception>
		public void ExecuteRemoteCommand ( IPEndPoint endPoint, String command, Device device, IShellOutputReceiver rcvr, int maxTimeToOutputResponse ) {
			Socket socket = new Socket ( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

			if ( !device.IsOnline ) {
				return;
			}

			try {
				socket.Connect ( endPoint );
				socket.Blocking = true;

				SetDevice ( socket, device );

				byte[] request = FormAdbRequest ( "shell:" + command );
				if ( !Write ( socket, request ) ) {
					throw new AdbException ( "failed submitting shell command" );
				}

				AdbResponse resp = ReadAdbResponse ( socket, false /* readDiagString */);
				if ( !resp.IOSuccess || !resp.Okay ) {
					throw new AdbException ( "sad result from adb: " + resp.Message );
				}

				byte[] data = new byte[16384];
				int timeToResponseCount = 0;

				while ( true ) {
					int count;

					if ( rcvr != null && rcvr.IsCancelled ) {
						this.LogWarn("execute: cancelled" );
						throw new OperationCanceledException ( );
					}

					count = socket.Receive ( data );
					if ( count < 0 ) {
						// we're at the end, we flush the output
						rcvr.Flush ( );
						this.LogInfo("execute '" + command + "' on '" + device + "' : EOF hit. Read: " + count );
						break;
					} else if ( count == 0 ) {
						try {
							int wait = WAIT_TIME * 5;
							timeToResponseCount += wait;
							if ( maxTimeToOutputResponse > 0 && timeToResponseCount > maxTimeToOutputResponse ) {
								throw new AdbException ( );
							}
							Thread.Sleep ( wait );
						} catch ( ThreadInterruptedException ) { }
					} else {
						timeToResponseCount = 0;

						string[] cmd = command.Trim ( ).Split ( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
						string sdata = data.GetString ( 0, count, AdbHelper.DEFAULT_ENCODING );

						var sdataTrimmed = sdata.Trim ( );
						if ( sdataTrimmed.EndsWith ( String.Format ( "{0}: not found", cmd[0] ) ) ) {
							this.LogWarn( "The remote execution returned: '{0}: not found'", cmd[0] );
							throw new FileNotFoundException ( string.Format ( "The remote execution returned: '{0}: not found'", cmd[0] ) );
						}

						if ( sdataTrimmed.EndsWith ( "No such file or directory" ) ) {
							this.LogWarn ( "The remote execution returned: {0}", sdataTrimmed );
							throw new FileNotFoundException ( String.Format ( "The remote execution returned: {0}", sdataTrimmed ) );
						}

						// for "unknown options"
						if ( sdataTrimmed.Contains ( "Unknown option" ) ) {
							this.LogWarn ( "The remote execution returned: {0}", sdataTrimmed );
							throw new UnknownOptionException ( sdataTrimmed );
						}

						// for "aborting" commands
						if ( sdataTrimmed.IsMatch ( "Aborting.$" ) ) {
							this.LogWarn ( "The remote execution returned: {0}", sdataTrimmed );
							throw new CommandAbortingException ( sdataTrimmed );
						}

						// for busybox applets 
						// cmd: applet not found
						if ( sdataTrimmed.IsMatch ( "applet not found$" ) && cmd.Length > 1 ) {
							this.LogWarn ( "The remote execution returned: '{0}'", sdataTrimmed );
							throw new FileNotFoundException ( string.Format ( "The remote execution returned: '{0}'", sdataTrimmed ) );
						}

						// checks if the permission to execute the command was denied.
						// workitem: 16822
						if ( sdataTrimmed.IsMatch ( "(permission|access) denied$" ) ) {
							this.LogWarn ( "The remote execution returned: '{0}'", sdataTrimmed );
							throw new PermissionDeniedException ( String.Format ( "The remote execution returned: '{0}'", sdataTrimmed ) );
						}

						// Add the data to the receiver
						if ( rcvr != null ) {
							rcvr.AddOutput ( data, 0, count );
						}
					}
				}
			} /*catch ( Exception e ) {
				Log.e ( TAG, e );
				Console.Error.WriteLine ( e.ToString ( ) );
				throw;
			}*/ finally {
				if ( socket != null ) {
					socket.Close ( );
				}
				rcvr.Flush ( );
			}
		}

		/// <summary>
		/// Executes a shell command on the remote device
		/// </summary>
		/// <param name="endPoint">The socket end point</param>
		/// <param name="command">The command to execute</param>
		/// <param name="device">The device to execute on</param>
		/// <param name="rcvr">The shell output receiver</param>
		/// <exception cref="FileNotFoundException">Throws if the result is 'command': not found</exception>
		/// <exception cref="IOException">Throws if there is a problem reading / writing to the socket</exception>
		/// <exception cref="OperationCanceledException">Throws if the execution was canceled</exception>
		/// <exception cref="EndOfStreamException">Throws if the Socket.Receice ever returns -1</exception>
		public void ExecuteRemoteCommand ( IPEndPoint endPoint, String command, Device device, IShellOutputReceiver rcvr ) {
			ExecuteRemoteCommand ( endPoint, command, device, rcvr, int.MaxValue );
		}


		//public void ExecuteRemoteCommand ( IPEndPoint adbSockAddr, String command, Device device, IShellOutputReceiver rcvr ) {
		//	Socket adbChan = new Socket ( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
		//	try {
		//		adbChan.Connect ( adbSockAddr );
		//		adbChan.Blocking = true;

		//		// if the device is not -1, then we first tell adb we're looking to
		//		// talk to a specific device
		//		SetDevice ( adbChan, device );

		//		byte[] request = FormAdbRequest ( "shell:" + command ); //$NON-NLS-1$
		//		if ( !Write ( adbChan, request ) )
		//			throw new IOException ( "failed submitting shell command" );

		//		AdbResponse resp = ReadAdbResponse ( adbChan, false /* readDiagString */);
		//		if ( !resp.IOSuccess || !resp.Okay ) {
		//			this.LogWarn ( "ADB rejected shell command (" + command + "): " + resp.Message );
		//			throw new IOException ( "sad result from adb: " + resp.Message );
		//		}

		//		byte[] data = new byte[16384];
		//		int count = -1;
		//		while ( count != 0 ) {

		//			if ( rcvr != null && rcvr.IsCancelled ) {
		//				this.LogWarn ( "execute: cancelled" );
		//				break;
		//			}

		//			count = adbChan.Receive ( data );
		//			if ( count < 0 ) {
		//				// we're at the end, we flush the output
		//				rcvr.Flush ( );
		//				this.LogWarn ( "execute '" + command + "' on '" + device + "' : EOF hit. Read: "
		//								+ count );
		//			} else {
		//				if ( rcvr != null ) {
		//					rcvr.AddOutput ( data, 0, (int)count );
		//				}
		//			}
		//		}
		//	} finally {
		//		if ( adbChan != null ) {
		//			adbChan.Close ( );
		//		}
		//	}
		//}

		private void SetDevice ( Socket adbChan, IDevice device ) {
			// if the device is not -1, then we first tell adb we're looking to talk
			// to a specific device
			if ( device != null ) {
				String msg = "host:transport:" + device.SerialNumber; //$NON-NLS-1$
				byte[] device_query = FormAdbRequest ( msg );

				if ( !Write ( adbChan, device_query ) ) {
					throw new IOException ( "failed submitting device (" + device + ") request to ADB" );
				}

				AdbResponse resp = ReadAdbResponse ( adbChan, false /* readDiagString */);
				if ( !resp.Okay ) {
					throw new IOException ( "device (" + device + ") request rejected: " + resp.Message );
				}
			}

		}

		public void Reboot ( String into, IPEndPoint adbSockAddr, Device device ) {
			byte[] request;
			if ( into == null ) {
				request = FormAdbRequest ( "reboot:" ); //$NON-NLS-1$
			} else {
				request = FormAdbRequest ( "reboot:" + into ); //$NON-NLS-1$
			}

			Socket adbChan = new Socket ( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			try {
				adbChan.Connect ( adbSockAddr );
				adbChan.Blocking = true;

				// if the device is not -1, then we first tell adb we're looking to talk
				// to a specific device
				SetDevice ( adbChan, device );

				if ( !Write ( adbChan, request ) ) {
					throw new IOException ( "failed asking for reboot" );
				}
			} finally {
				if ( adbChan != null ) {
					adbChan.Close ( );
				}
			}
		}

	}

	internal class AdbResponse {
		public AdbResponse ( ) {
			// ioSuccess = okay = timeout = false;
			Message = String.Empty;
		}

		public bool IOSuccess { get; set; } // read all expected data, no timeoutes

		public bool Okay { get; set; } // first 4 bytes in response were "OKAY"?

		public bool Timeout { get; set; }// TODO: implement

		public String Message { get; set; } // diagnostic string
	}
}
