using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Plugins.Contacts {
  public class Phone {
    public enum PhoneType {
      CUSTOM = 0,
      HOME = 1,
      MOBILE = 2,
      WORK = 3,
      WORKFAX = 4,
      HOMEFAX = 5,
      PAGER = 6,
      OTHER = 7
    }

    public long ID { get; set; }
    public long PersonID { get; set; }
    public PhoneType Type { get; set; }
    public string Number { get; set; }
    public string Key { get; set; }
    public string Label { get; set; }
    public bool IsPrimary { get; set; }

  }
}
