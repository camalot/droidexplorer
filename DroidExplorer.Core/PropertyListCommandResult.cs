using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DroidExplorer.Core.Collections;

namespace DroidExplorer.Core {
  public class PropertyListCommandResult : CommandResult {

    public PropertyListCommandResult ( string data )
      : base ( ) {
      PropertyList = new PropertyList ( );
      ProcessData ( data );
    }

    public PropertyList PropertyList { get; set; }

    protected override void ProcessData ( string data ) {
      base.ProcessData ( data );

      Regex regex = new Regex ( Properties.Resources.GetPropRegexPattern, RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase );
      Match m = regex.Match ( data );
      while ( m.Success ) {
        PropertyList.Add ( m.Groups[ 1 ].Value.Trim ( ), m.Groups[ 2 ].Value.Trim ( ).Replace ( "\r", string.Empty ).Replace ( "\n", string.Empty ) );
        m = m.NextMatch ( );
      }
    }
  }

  public class PropertyCommandResult : CommandResult {
    public PropertyCommandResult ( string data )
      : base ( ) {
      ProcessData ( data );
    }

    public string Value { get; set; }

    protected override void ProcessData ( string data ) {
      base.ProcessData ( data );
      Value = this.Output.ToString ( ).Trim ( ).Replace ( "\r", string.Empty ).Replace ( "\n", string.Empty );
    }
  }
}
