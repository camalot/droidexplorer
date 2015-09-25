using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DroidExplorer.Core.Plugins {
  public interface IFileTypeHandler {
    void Open ( DroidExplorer.Core.IO.FileInfo file );
		string Name { get; }
		string Text { get; }
		Image Image { get; }
  }
}
