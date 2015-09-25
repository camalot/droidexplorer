using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace DroidExplorer.Core.Market {
  public interface IMarketSource {
    string Name { get;  }
    Uri Url { get; }
    List<MarketApplication> GetApplications ( );
    IWebProxy Proxy { get; set; }
  }
}
