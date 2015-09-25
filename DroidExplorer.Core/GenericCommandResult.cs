using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DroidExplorer.Core {
	public class GenericCommandResult : CommandResult {
		public GenericCommandResult (string data ) : base () {
			ProcessData ( data );
		}

		protected override void ProcessData ( string data ) {
			base.ProcessData ( data );

			
		}
	}
}
