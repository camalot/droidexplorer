using System;
using System.Collections.Generic;
using System.Text;
using DroidExplorer.Core.IO;
using System.Text.RegularExpressions;

namespace DroidExplorer.Core {
	public class ProcessInfoListCommandResult : CommandResult {
		private List<ProcessInfo> _processes = null;
		public ProcessInfoListCommandResult ( string data )
			: base ( ) {
			this.Processes = new List<ProcessInfo> ( );
			ProcessData ( data );
		}

		public List<ProcessInfo> Processes {
			get { return this._processes; }
			private set { this._processes = value; }
		}

		protected override void ProcessData ( string data ) {
			base.ProcessData ( data );

      Regex regex = new Regex ( Properties.Resources.ProcessStatsRegexPattern, RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase );
			Match m = regex.Match ( data );
			while ( m.Success ) {
				int pid = 0;
				int.TryParse ( m.Groups[ 2 ].Value, out pid );
				int ppid = 0;
				int.TryParse ( m.Groups[ 3 ].Value, out ppid );
				long size = 0;
				long.TryParse ( m.Groups[ 4 ].Value, out size );
				string user = m.Groups[ 1 ].Value;
				string name = m.Groups[ 5 ].Value;

				Processes.Add ( new ProcessInfo ( pid, name, ppid, size, user ) );

				m = m.NextMatch ( );
			}
			
		}
	}
}
