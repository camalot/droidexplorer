using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DroidExplorer.Core {
	public class IntegerListCommandResult : CommandResult {
		List<int> _items = null;
		public IntegerListCommandResult ( string data ) {
			this.Values = new List<int> ( );
			ProcessData ( data );
		}

		public List<int> Values {
			get { return this._items; }
			private set { this._items = value; }
		}

		protected override void ProcessData ( string data ) {
			base.ProcessData ( data );

			Regex regex = new Regex ( @"(\d{1,})", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase );
			Match m = regex.Match ( data );
			while ( m.Success ) {
				string sn = m.Groups[ 1 ].Value;
				if ( !string.IsNullOrEmpty ( sn ) ) {
					int i = 0;
					int.TryParse ( sn, out i );
					if ( i != 0 ) {
						Values.Add ( i );
					}
				}
				m = m.NextMatch ( );
			}
		}
	}
}
