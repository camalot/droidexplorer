using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;
using DroidExplorer.Configuration.UI;
using DroidExplorer.Core;

namespace DroidExplorer.Configuration {
  [XmlRoot("OptionNode")]
  public class OptionNode {

    public OptionNode ( ) {
      Children = new List<OptionNode> ( );
    }

    [XmlAttribute("Name")]
    public string Name { get; set; }
    [XmlAttribute ( "UIEditorType" )]
    public string UIEditorType { get; set; }
    [XmlAttribute ( "DataLoaderType" )]
    public string DataLoaderType { get; set; }

    [XmlElement("OptionNode")]
    public List<OptionNode> Children { get; set; }



    public OptionItemTreeNode CreateTreeNode ( ) {
      OptionItemTreeNode tn = new OptionItemTreeNode ( );
      tn.Text = this.Name;

      foreach ( var item in Children ) {
        tn.Nodes.Add ( item.CreateTreeNode() );
      }

      try {
        tn.DataLoader = CreateDataLoader ( );
        if ( tn.DataLoader != null ) {
          tn.DataLoader.Load ( tn );
        }
      } catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
        tn.DataLoader = null;
      }

      try {
        tn.UIEditor = CreateUIEditor ( );
      } catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
        tn.UIEditor = null;
      }

      return tn;
    }

    public IUIEditor CreateUIEditor ( ) {
      return CreateType<IUIEditor> ( this.UIEditorType );
    }

    public IOptionNodeDataLoader CreateDataLoader ( ) {
      return CreateType<IOptionNodeDataLoader> ( this.DataLoaderType );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    /// <typeparam name="I">base Interface type</typeparam>
    /// <param name="assemblyQualifiedTypeName"></param>
    /// <returns></returns>
    private T CreateType<T> ( string assemblyQualifiedTypeName ) {
      if ( string.IsNullOrEmpty ( assemblyQualifiedTypeName ) ) {
        return default(T);
      } else {
        string[] asmType = assemblyQualifiedTypeName.Split ( new char[ ] { ',' }, StringSplitOptions.RemoveEmptyEntries );
        string assemblyName = this.GetType ( ).Assembly.GetName ( ).Name;
        string typeName = string.Empty;

        if ( asmType.Length == 1 ) {
          typeName = asmType[ 0 ];
        } else if ( asmType.Length == 2 ) {
          typeName = asmType[ 0 ];
          assemblyName = asmType[ 1 ];
        } else {
          throw new ArgumentException ( "Invalid Type Name Defined" );
        }

        Type type = Type.GetType ( string.Format ( CultureInfo.InvariantCulture, "{0},{1}", typeName, assemblyName ) );
        if ( type.GetInterface ( typeof ( T ).FullName ) != null ) {
          T t = (T)Activator.CreateInstance ( type, new object[ ] { } );
          return t;
        } else {
          return default ( T );
        }
      }
    }
    
  }
}
