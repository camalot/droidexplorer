using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core {
	public class ScreenCaptureCommandResult : CommandResult {

		public bool Completed { get; set; }

		public ScreenCaptureCommandResult ( string data ) : base ( ) {

			ProcessData ( data );
		}

		protected override void ProcessData ( string data ) {
			base.ProcessData ( data );
			var options = RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
			var limitPattern = @"^Time\slimit\reached$";
			var completedPattern = @"^Broadcast\scompleted:\sresult=(\d+)$";

			this.LogDebug ( data.REReplace("{","{{").REReplace("}","}}") );

			if ( data.IsMatch ( completedPattern, options ) || data.IsMatch ( limitPattern, options ) ) {
				Completed = true;
			}
		}
	}
}
