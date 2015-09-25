using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DroidExplorer.Core.IO;
using System.Globalization;

namespace DroidExplorer.Core {
	public class DirectoryListCommandResult : CommandResult {
		private List<DroidExplorer.Core.IO.FileSystemInfo> _items;
		private string path = string.Empty;

		public DirectoryListCommandResult ( string data, string path )
			: base (  ) {
			this.path = path.Replace ( "\"", string.Empty ); ;
			FileSystemItems = new List<FileSystemInfo> ( );
			ProcessData ( data );
		}

		public List<DroidExplorer.Core.IO.FileSystemInfo> FileSystemItems {
			get { return _items; }
			set { _items = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <workitem id="11975" user="camalot">fixed the check if the FileSystemItem is null before adding it to the collection.</workitem>
		protected override void ProcessData ( string data ) {
			base.ProcessData ( data );

      Regex regex = new Regex ( Properties.Resources.LsResultRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline );
			Match m = regex.Match ( data.Replace ( "/r/n", "/n" ) );
			while ( m.Success ) {
				FileSystemInfo fsi = null;

				Permission up = new Permission ( m.Groups[2].Captures[0].Value );
				Permission gp = new Permission ( m.Groups[2].Captures[1].Value );
				Permission op = new Permission ( m.Groups[2].Captures[2].Value );
				long size = 0;
				long.TryParse ( m.Groups[6].Value, out size );

				bool isDirectory = string.Compare ( m.Groups[10].Value, "/", true ) == 0;
				bool isExec = string.Compare ( m.Groups[10].Value, "*", true ) == 0;

				string dtString = m.Groups[7].Value;
				if ( string.Compare ( dtString.Substring ( dtString.Length - 1, 1 ), " ", true ) == 0 ) {
					// need to add the year
					dtString += DateTime.Now.Year.ToString ( );
				}

				if ( string.Compare ( m.Groups[8].Value, string.Empty, true ) == 0 ) {
					// append midnight because no time exists
					dtString += "  00:00";
				} else {
					dtString += string.Format ( "  {0}", m.Groups[8].Value );
				}

				dtString = dtString.Replace ( "  ", " " );
				DateTime lastMod = DateTime.ParseExact ( dtString, "MMM d yyyy HH:mm", CultureInfo.InvariantCulture );

				string name = m.Groups[9].Value;

				if ( string.Compare ( m.Groups[1].Value, "-", true ) == 0 ) { // file 
					fsi = new FileInfo ( name, size, up, gp, op, lastMod, isExec, System.IO.Path.Combine ( this.path, name ) );
				} else if ( string.Compare ( m.Groups[1].Value, "d", true ) == 0 ) { // directory
					if ( String.Compare ( name, "..", false ) != 0 && String.Compare ( name, ".", false ) != 0 ) {
						fsi = new DirectoryInfo ( name, size, up, gp, op, lastMod, System.IO.Path.Combine ( this.path, name ) );
					}
				} else if ( string.Compare ( m.Groups[1].Value, "l", true ) == 0 ) { // link
          Regex rName = new Regex ( Properties.Resources.SymbolicLinkRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline );
					Match mName = rName.Match ( name );
					string link = string.Empty;
					string nname = name;
					if ( mName.Success ) {
						nname = mName.Groups[1].Value;
						link = mName.Groups[2].Value;
					}

					fsi = new SymbolicLinkInfo ( nname, link, size, up, gp, op, lastMod, isDirectory, isExec, System.IO.Path.Combine ( this.path, nname ) );
				}

				// workitem: 11975
				if ( fsi != null ) {
					FileSystemItems.Add ( fsi );
				}
				m = m.NextMatch ( );
			}
		}
	}
}
