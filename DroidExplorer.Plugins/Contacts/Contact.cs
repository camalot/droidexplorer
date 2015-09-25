using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DroidExplorer.Plugins.Contacts {
  public class Contact {

    public Contact ( ) {
      Phones = new List<Phone> ( );

    }

    public long ID { get; set; }
    public string SyncAccount { get; set; }
    public string SyncID { get; set; }
    public long? SyncTime { get; set; }
    public long? SyncVersion { get; set; }
    public string SyncLocalID { get; set; }
    public bool SyncDirty { get; set; }
    public bool SyncMark { get; set; }

    public string Name { get; set; }
    public string Notes { get; set; }
    public long TimesContacted { get; set; }
    public long? LastTimeContacted { get; set; }
    public bool IsStarred { get; set; }
    public long? PrimaryPhone { get; set; }
    public long? PrimaryOrganization { get; set; }
    public long? PrimaryEmail { get; set; }
    public long? PhotoVersion { get; set; }
    public bool? CustomRingtone { get; set; }
    public bool? SendToVoiceMail { get; set; }
    public string PhoneticName { get; set; }

    public Image Photo { get; set; }

    public List<Phone> Phones { get; set; }

  }
}
