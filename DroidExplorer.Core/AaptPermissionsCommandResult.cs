using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DroidExplorer.Core {
	/// <summary>
	/// 
	/// </summary>
	public class AaptPermissionsCommandResult : CommandResult {
		/// <summary>
		/// Initializes a new instance of the <see cref="AaptPermissionsCommandResult"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public AaptPermissionsCommandResult ( string data ) :base(){
			Permissions = new List<string> ( );
			ProcessData ( data );
		}

		/// <summary>
		/// Gets or sets the permissions.
		/// </summary>
		/// <value>The permissions.</value>
		public List<string> Permissions { get; private set; }

		/// <summary>
		/// Gets or sets the local apk.
		/// </summary>
		/// <value>The local apk.</value>
		public string LocalApk {get;set;}

		/// <summary>
		/// Processes the data.
		/// </summary>
		/// <param name="data">The data.</param>
		protected override void ProcessData ( string data ) {
			base.ProcessData ( data );
			Regex regex = new Regex ( Properties.Resources.ApkPermissionsRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline );
			Match m = regex.Match ( data );
			while ( m.Success ) {
				Permissions.Add ( m.Groups[1].Value );
				m = m.NextMatch ( );
			}
		}
	}
}
