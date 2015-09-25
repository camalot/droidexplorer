using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.Plugins {
  public interface IPluginExtendedInfo : IPlugin {
    string Author { get; }
    string Url { get; }
    string Contact { get; }
    string Copyright { get; }
  }
}
