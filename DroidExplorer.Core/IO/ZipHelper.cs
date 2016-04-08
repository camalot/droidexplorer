using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace DroidExplorer.Core.IO {
	/// <summary>
	/// 
	/// </summary>
	public static class ZipHelper {
		/// <summary>
		/// Unzips the specified zip file.
		/// </summary>
		/// <param name="zipFile">The zip file.</param>
		/// <param name="outPath">The out path.</param>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
		/// <param name="flat">if set to <c>true</c> [flat].</param>
		/// <returns></returns>
		public static bool Unzip ( string zipFile, string outPath, string fileName, bool overwrite, bool flat ) {
			bool ret = true;
			try {
				if ( File.Exists ( zipFile ) ) {
					string baseDirectory = outPath;

					using ( ZipInputStream ZipStream = new ZipInputStream ( System.IO.File.OpenRead ( zipFile ) ) ) {
						ZipEntry theEntry;
						while ( ( theEntry = ZipStream.GetNextEntry ( ) ) != null && theEntry.CanDecompress ) {
							if ( theEntry.IsFile ) {
								if ( !string.IsNullOrEmpty ( theEntry.Name ) && ( string.Compare ( theEntry.Name, fileName, false ) == 0 || string.IsNullOrEmpty ( fileName ) ) ) {
									string fileWithPath = flat ? Path.GetFileName ( theEntry.Name ) : theEntry.Name;
									string strNewFile = @"" + baseDirectory + @"\" + fileWithPath;
									System.IO.FileInfo fileInfo = new System.IO.FileInfo ( strNewFile );
									if ( fileInfo.Exists && !overwrite ) {
										continue;
									}

									if ( !fileInfo.Directory.Exists ) {
										fileInfo.Directory.Create ( );
									}

									using ( FileStream streamWriter = System.IO.File.Create ( strNewFile ) ) {
										byte[ ] data = new byte[2048];
										int readSize = 0;
										while ( ( readSize = ZipStream.Read ( data, 0, data.Length ) ) > 0 ) {
											streamWriter.Write ( data, 0, readSize );
										}
									}
								}
							} else if ( theEntry.IsDirectory && !flat ) {
								string strNewDirectory = @"" + baseDirectory + @"\" + theEntry.Name;
								if ( !Directory.Exists ( strNewDirectory ) ) {
									Directory.CreateDirectory ( strNewDirectory );
								}
							}
						}
					}
				}
			} catch ( Exception ex ) {
        Logger.LogError ( typeof ( ZipHelper ), ex.Message, ex );
				ret = false;
			}
			return ret;
		}
	}
}
