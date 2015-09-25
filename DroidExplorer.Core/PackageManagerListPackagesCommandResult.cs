using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DroidExplorer.Core {
  public class PackageManagerListPackagesCommandResult : CommandResult {

    public PackageManagerListPackagesCommandResult ( string data )
      : base ( ) {
      Packages = new Dictionary<string, string> ( );
      ProcessData ( data );
    }

    public Dictionary<string,string> Packages { get; private set; }

    protected override void ProcessData ( string data ) {
      base.ProcessData ( data );

      Regex regex = new Regex ( Properties.Resources.PackageManagerListPackagesRegexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline );
      Match m = regex.Match ( data );
      while ( m.Success ) {
				this.Packages.Add ( m.Groups[ 2 ].Value.Trim (), m.Groups[ 1 ].Value.Trim () );
        m = m.NextMatch ( );
      }
    }
  }
}
