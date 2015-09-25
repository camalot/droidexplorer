using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;
using Camalot.Common.Extensions;

namespace DroidExplorer.Plugins {
	/*
	 * .abex File Spec
	 *   - An abex is just a modified .ab backup. There is additional information added to the begining of the file
	 *   - Before it can be unpacked, it needs to be converted to an "basic"
	 * ENHANCED ANDROID BACKUP\n
	 * [DEVICE ID]\n
	 * [DROID EXPLORER VERSION]\n
	 * [ABEX VERSION]\n
	 * $$$ ENHANCED ANDROID BACKUP $$$\n
	 * {START OF BASIC ANDROID BACKUP FILE}
	 */

	/*
	 * Usage:
	 *	/b=<backupfile>						:		The backup file to convert
	 *	/convert=<basic|extended>	:		The type to convert to (basic|extended)
	 */

	/// <summary>
	/// 
	/// </summary>
	public class BackupConverter : BasePlugin {

		/// <summary>
		/// Defines the ABEX header
		/// </summary>
		public class ExtendedBackupHeader {
			/// <summary>
			/// Gets or sets the magic string.
			/// </summary>
			/// <value>
			/// The magic.
			/// </value>
			public String Magic { get; set; }
			/// <summary>
			/// Gets or sets the version.
			/// </summary>
			/// <value>
			/// The version.
			/// </value>
			public String Version { get; set; }
			/// <summary>
			/// Gets or sets the abex version.
			/// </summary>
			/// <value>
			/// The abex version.
			/// </value>
			public int AbexVersion { get; set; }
			/// <summary>
			/// Gets or sets the device.
			/// </summary>
			/// <value>
			/// The device.
			/// </value>
			public String Device { get; set; }
		}

		/// <summary>
		/// 
		/// </summary>
		public const String EXTENDED_HEADER_MAGIC = "ENHANCED ANDROID BACKUP";
		/// <summary>
		/// 
		/// </summary>
		private const String EXTENDED_HEADER_FOOTER = "$$$ ENHANCED ANDROID BACKUP $$$";
		private const char NEWLINE = '\n';
		/// <summary>
		/// Basic Header
		/// </summary>
		public const String BASIC_HEADER_MAGIC = "ANDROID BACKUP";
		/// <summary>
		/// ABEX Version
		/// </summary>
		public const int EXTENDED_ABEX_VERSION = 1;

		/// <summary>
		/// The conversion type
		/// </summary>
		public enum ConvertType {
			/// <summary>
			/// Basic Type
			/// </summary>
			Basic,
			/// <summary>
			/// Extended Type
			/// </summary>
			Extended
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BackupConverter" /> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public BackupConverter ( IPluginHost host )
			: base ( host ) {

		}

		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		public override string Author {
			get { return "Ryan Conrad"; }
		}

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		public override string Url {
			get { return DroidExplorer.Resources.Strings.ApplicationWebsiteUrl; }
		}

		/// <summary>
		/// Gets the contact.
		/// </summary>
		/// <value>
		/// The contact.
		/// </value>
		public override string Contact {
			get { return String.Empty; }
		}
		public override string Group { get { return "Backup"; } }


		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public override string Name {
			get { return "Backup Converter"; }
		}

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
			get { return "Converts to/from Android Backups and Enhanced Android Backups"; }
		}

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public override bool CreateToolButton {
			get { return false; }
		}

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The args.</param>
		public override void Execute ( IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args ) {
			try {
				var arguments = new Arguments ( args );

				if ( !arguments.Contains ( "convert" ) ) {
					// invalid
					this.LogError ( "Missing required 'convert' argument" );
					return;
				}

				var convertType = GetConvertType ( arguments["convert"] );
				var file = System.IO.Path.GetFullPath ( arguments["b", "backup"] );
				ConvertTo ( convertType, file );

			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
			}
		}

		/// <summary>
		/// Converts a backup to the specified type.
		/// </summary>
		/// <param name="convertType">Type of the convert.</param>
		/// <param name="sourceFile">The source file.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException"></exception>
		public String ConvertTo ( ConvertType convertType, String sourceFile ) {
			try {
				switch ( convertType ) {
					case ConvertType.Basic:
						this.LogDebug ( "Checking if Extended so can be converted to Basic" );
						if ( IsExtendedBackup ( sourceFile ) ) {
							this.LogDebug ( "Converting Extended to Basic" );
							return RemoveExtendedInfo ( sourceFile );
						} else {
							throw new ArgumentException ( "Did not pass Extended Backup test" );
						}
					case ConvertType.Extended:
						this.LogDebug ( "Checking if Basic so can be converted to Extended" );
						if ( IsBasicBackup ( sourceFile ) ) {
							this.LogDebug ( "Converting Basic to Extended" );
							return WriteExtendedInfo ( sourceFile );
						} else {
							throw new ArgumentException ( "Did not pass Basic Bacup test" );
						}
					default:
						throw new ArgumentException ( "Unsupported conversion type" );
				}
			} catch ( Exception ex ) {
				throw;
			}
		}

		/// <summary>
		/// Gets the extended header.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public ExtendedBackupHeader GetExtendedHeader ( String file ) {
			if( IsExtendedBackup(file) ) {
				using ( var fr = File.OpenRead(file) ) {
					var magic = ReadHeaderLine ( fr );
					var device = ReadHeaderLine ( fr );
					var ver = ReadHeaderLine ( fr );
					var abex = 0;
					int.TryParse ( ReadHeaderLine ( fr ), out abex );
					return new ExtendedBackupHeader {
						Magic = magic,
						Device = device,
						Version = ver,
						AbexVersion = abex
					};
				}
			} else {
				return null;
			}
		}

		/// <summary>
		/// Writes the extended info.
		/// </summary>
		/// <param name="file">The file.</param>
		private String WriteExtendedInfo ( string file ) {
			try {
				var path = Path.GetDirectoryName ( file );
				var temp = Path.Combine ( path, Path.GetFileNameWithoutExtension ( file ) + ".tabex" );
				var outFile = Path.Combine ( path, Path.GetFileNameWithoutExtension ( temp ) + ".abex" );
				using ( var fw = File.OpenWrite ( temp ) ) {
					var ver = this.GetType ( ).Assembly.GetName ( ).Version.ToString ( );
					var did = this.PluginHost == null ? CommandRunner.Instance.DefaultDevice : this.PluginHost.Device;

					WriteHeaderLine ( fw, EXTENDED_HEADER_MAGIC );
					WriteHeaderLine ( fw, did );
					WriteHeaderLine ( fw, ver );
					WriteHeaderLine ( fw, EXTENDED_ABEX_VERSION.ToString ( ) );
					WriteHeaderLine ( fw, EXTENDED_HEADER_FOOTER );

					using ( var fr = File.OpenRead ( file ) ) {
						// now we start writing to the temp file. This isnt the most efficient way, but it is the only way
						var buffer = new byte[1024 * 15];
						var bread = 0;
						while ( ( bread = fr.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
							fw.Write ( buffer, 0, bread );
						}

					}
				}
				File.Move ( temp, outFile );
				return outFile;
			} catch ( Exception ex ) {
				throw;
			}
		}

		/// <summary>
		/// Removes the extended info.
		/// </summary>
		/// <param name="file">The file.</param>
		private String RemoveExtendedInfo ( string file ) {
			try {
				var path = Path.GetDirectoryName ( file );
				var temp = Path.Combine ( path, Path.GetFileNameWithoutExtension ( file ) + ".tab" );
				var outFile = Path.Combine ( path, Path.GetFileNameWithoutExtension ( temp ) + ".ab" );
				using ( var fw = File.OpenWrite ( temp ) ) {
					using ( var fr = File.OpenRead ( file ) ) {
						var found = false;
						var lcount = 0;
						while ( ( ++lcount ) < 10 ) {
							var line = ReadHeaderLine ( fr );
							if ( line.CompareTo ( EXTENDED_HEADER_FOOTER ) == 0 ) {
								found = true;
								break;
							}
						}
						// now we start writing to the temp file. This isnt the most efficient way, but it is the only way
						if ( found ) {
							var buffer = new byte[1024 * 15];
							var bread = 0;
							while ( ( bread = fr.Read ( buffer, 0, buffer.Length ) ) > 0 ) {
								fw.Write ( buffer, 0, bread );
							}
						}
					}
				}
				File.Move ( temp, outFile );
				return outFile;
			} catch ( Exception ex ) {
				throw;
			}
		}

		/// <summary>
		/// Gets the type of the convert.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		private ConvertType GetConvertType ( string type ) {
			try {
				return (ConvertType)Enum.Parse ( typeof ( ConvertType ), type, true );
			} catch ( Exception ex ) {
				throw;
			}
		}

		/// <summary>
		/// Determines whether [is extended backup] [the specified file].
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>
		///   <c>true</c> if [is extended backup] [the specified file]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsExtendedBackup ( String file ) {
			using ( var fr = File.OpenRead ( file ) ) {
				var hl = ReadHeaderLine ( fr );
				return File.Exists ( file ) &&
						Path.GetExtension ( file ).ToLower ( ).CompareTo ( ".abex" ) == 0 &&
						hl.CompareTo ( EXTENDED_HEADER_MAGIC ) == 0;
			}
		}

		/// <summary>
		/// Determines whether [is basic backup] [the specified file].
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>
		///   <c>true</c> if [is basic backup] [the specified file]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsBasicBackup ( String file ) {
			using ( var fr = File.OpenRead ( file ) ) {
				var hl = ReadHeaderLine ( fr );
				return File.Exists ( file ) &&
					Path.GetExtension ( file ).ToLower ( ).CompareTo ( ".ab" ) == 0 &&
					hl.CompareTo ( BASIC_HEADER_MAGIC ) == 0;
			}
		}


		/// <summary>
		/// Reads the header line.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		private String ReadHeaderLine ( Stream input ) {
			int c;
			StringBuilder buffer = new StringBuilder ( 80 );
			while ( ( c = input.ReadByte ( ) ) >= 0 ) {
				if ( c == NEWLINE )
					break; // consume and discard the newlines
				buffer.Append ( (char)c );
			}
			return buffer.ToString ( );
		}

		/// <summary>
		/// Writes the header line.
		/// </summary>
		/// <param name="output">The output.</param>
		/// <param name="line">The line.</param>
		private void WriteHeaderLine ( Stream output, String line ) {
			var buffer = line.Trim ( ).GetBytes ( Encoding.UTF8 );
			output.Write ( buffer, 0, buffer.Length );
			output.WriteByte ( (byte)NEWLINE );
		}

		/// <summary>
		/// Gets the minimum SDK platform tools version.
		/// </summary>
		/// <value>
		/// The minimum SDK platform tools version.
		/// </value>
		public override int MinimumSDKPlatformToolsVersion {
			get {
				return 13;
			}
		}

		/// <summary>
		/// Gets the minimum SDK tools version.
		/// </summary>
		/// <value>
		/// The minimum SDK tools version.
		/// </value>
		public override int MinimumSDKToolsVersion {
			get {
				return 13;
			}
		}
	}
}
