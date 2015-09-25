using System;
using System.Collections.Generic;
using System.Text;
using Camalot.Common.Extensions;

namespace DroidExplorer.Core.Components {
  public class AaptBrandingCommandResultComparer : Comparer<AaptBrandingCommandResult>{
    public override int Compare ( AaptBrandingCommandResult x, AaptBrandingCommandResult y ) {
      return x.Label.Or("").CompareTo ( y.Label );
    }
  }
}
