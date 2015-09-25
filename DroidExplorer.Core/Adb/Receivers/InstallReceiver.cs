using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DroidExplorer.Core.Adb {
	/**
		 * Output receiver for "pm install package.apk" command line.
		 */
	internal class InstallReceiver : MultiLineReceiver {

		private const String SUCCESS_OUTPUT = "Success"; //$NON-NLS-1$
		private const String FAILURE_PATTERN = "Failure\\s+\\[(.*)\\]"; //$NON-NLS-1$

		public InstallReceiver ( ) {
		}

		//@Override
		public override void ProcessNewLines ( String[] lines ) {
			foreach ( String line in lines ) {
				if ( line.Length > 0 ) {
					if ( line.StartsWith ( SUCCESS_OUTPUT ) ) {
						ErrorMessage = null;
					} else {
						Regex pattern = new Regex ( FAILURE_PATTERN, RegexOptions.Compiled );
						Match m = pattern.Match ( line );
						if ( m.Success ) {
							ErrorMessage = m.Groups[1].Value;
						}
					}
				}
			}
		}

		public bool IsCancelled { get { return false; } }

		public String ErrorMessage { get; private set; }
	}
}
