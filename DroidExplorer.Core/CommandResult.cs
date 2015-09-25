using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DroidExplorer.Core.Exceptions;
using System.Globalization;
using System.Xml.Serialization;

namespace DroidExplorer.Core {
	public abstract class CommandResult {
		private StringBuilder _output;
		private StringBuilder _special;

		public CommandResult ( ) {
			this.Output = new StringBuilder ( );
			this.SpecialOutput = new StringBuilder ( );
		}

		[XmlIgnore]
		public StringBuilder Output {
			get { return _output; }
			protected set { _output = value; }
		}

		[XmlIgnore]
		public StringBuilder SpecialOutput {
			get { return this._special; }
			protected set { _special = value; }
		}

		[XmlIgnore]
		public bool HasSpecialOutput {
			get { return string.Compare ( this.SpecialOutput.ToString(), string.Empty, true, CultureInfo.InvariantCulture ) != 0; }
		}

		protected virtual void ProcessData ( string data ) {
			Regex regex = new Regex ( Properties.Resources.IsErrorResultRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline );
			Match m = regex.Match ( data );
			if ( m.Success ) {
				throw new AdbException ( m.Groups[ 1 ].Value );
			}

			string[] split = data.Split ( new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries );
			regex = new Regex ( @"^\*\s?(.+)\s?\*", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
			foreach ( string var in split ) {
				if ( regex.IsMatch ( var ) ) {
					this.LogWarn ( var );
					this.SpecialOutput.AppendLine ( var );
				} else {
					this.Output.AppendLine ( var );
				}
			}
		}
	}
}
