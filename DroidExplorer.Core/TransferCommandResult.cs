using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DroidExplorer.Core.Exceptions;

namespace DroidExplorer.Core {
	public class TransferCommandResult : CommandResult {

		public TransferCommandResult (string data) : base () {
			ProcessData ( data );
		}

		public int KillobytesPerSecond {
			get;
			private set;
		}

		protected override void ProcessData ( string data ) {
			base.ProcessData ( data );

			Regex regex = new Regex ( @"^(\d{1,})\s", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline );
			Match m = regex.Match ( data );
			if ( !m.Success ) {
        this.KillobytesPerSecond = 0;
			} else {
				int l = 0;
				int.TryParse ( m.Groups[ 1 ].Value, out l );
				this.KillobytesPerSecond = l;
			}

		}
	}
}
