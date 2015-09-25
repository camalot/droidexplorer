using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DroidExplorer.Core;
using DroidExplorer.Core.Xml.Serialization;

namespace DroidExplorer.Plugins {
  [XmlRoot ( "BareBonesBackup" )]
  public class BareBonesBackupConfiguration {

    public BareBonesBackupConfiguration ( ) {
      Files = new List<string> ( );
    }

    [XmlElement ( "OutputFile" )]
    public string OutputFile { get; set; }

    [XmlArray ( "Files" ), XmlArrayItem ( "File" )]
    public List<string> Files { get; set; }

    public void Reload ( ) {
      _instance = null;
    }

    private static BareBonesBackupConfiguration _instance;
    public static BareBonesBackupConfiguration Instance {
      get {
        if ( _instance == null ) {
          System.IO.FileInfo file = new System.IO.FileInfo ( System.IO.Path.Combine ( CommandRunner.Instance.TempDataPath, "BareBonesBackup.config" ) );
          _instance = XmlSerializer<BareBonesBackupConfiguration>.Deserialize ( file );
        }
        return _instance;
      }
    }
  }
}
