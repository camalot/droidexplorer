using System;
using System.Collections.Generic;
using System.Text;
using DroidExplorer.Core.Market;

namespace DroidExplorer.Core.Components {
  public class MarketApplicationNameComparer : Comparer<MarketApplication>{
    public override int Compare ( MarketApplication x, MarketApplication y ) {
      return x.Name.CompareTo ( y.Name );
    }
  }
}
