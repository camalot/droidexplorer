using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.UI {
  public delegate void CloseComboBoxExtenderHandler ( );

  public interface IComboBoxExtender {
    ///if you want to modify look and feel of your control inside the combobox then
    ///use this method to set look and feel properties
    void SetUserInterface ( );

    /// This delegate would empower the child control to close the combobox drop down
    /// e.g you want to close drop down of combo when you double click child control
    CloseComboBoxExtenderHandler CloseComboBoxExtenderDelegate { get; set; }

    /// Text returned by this property would be used by combobox to display text while
    /// drop down is closed
    string DisplayText { get; }
  }
}
