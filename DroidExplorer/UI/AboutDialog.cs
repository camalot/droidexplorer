using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DroidExplorer.Core.Plugins;
using System.Diagnostics;
using DroidExplorer.Core;
using DroidExplorer.Core.UI;

namespace DroidExplorer.UI {
  partial class AboutDialog : Form {
    public AboutDialog ( ) {
      InitializeComponent ( );
      this.Text = String.Format ( "About {0}", AssemblyTitle );
      this.productName.Text = AssemblyProduct;
      this.version.Text = String.Format ( "Version {0}", AssemblyVersion );
      this.copyright.Text = AssemblyCopyright;

      LoadPlugins ( );
    }

    private void LoadPlugins ( ) {
      foreach ( IPlugin plugin in Program.LoadedPlugins ) {
        string url = string.Empty;
        string copy = string.Empty;
        string author = string.Empty;
        object[] attributes = plugin.GetType ( ).GetCustomAttributes ( typeof ( AssemblyCompanyAttribute ), false );
        if ( attributes.Length != 0 ) {
          author = ( ( AssemblyCompanyAttribute )attributes[ 0 ] ).Company;
        }
        attributes = plugin.GetType ( ).GetCustomAttributes ( typeof ( AssemblyCopyrightAttribute ), false );
        if ( attributes.Length != 0 ) {
          copy = ( ( AssemblyCopyrightAttribute )attributes[ 0 ] ).Copyright;
        }

        if ( plugin is IPluginExtendedInfo ) {
          url = ( plugin as IPluginExtendedInfo ).Url;
          author = ( plugin as IPluginExtendedInfo ).Author;
          copy = ( plugin as IPluginExtendedInfo ).Copyright;
        }

        string name = plugin.Text;
        int imageIndex = -1;
        if ( plugin.Image != null ) {
          imageIndex = imageList1.Images.Count;
          AlphaImageList.AddFromImage ( plugin.Image, imageList1 );
        }

        ListViewItem lvi = new ListViewItem ( new string[ ] { name, author, url, copy }, imageIndex );
        lvi.Tag = url;
        this.plugins.Items.Add ( lvi );
      }

      this.plugins.AutoResizeColumns ( ColumnHeaderAutoResizeStyle.ColumnContent );
    }

    #region Assembly Attribute Accessors

    public string AssemblyTitle {
      get {
        object[] attributes = Assembly.GetExecutingAssembly ( ).GetCustomAttributes ( typeof ( AssemblyTitleAttribute ), false );
        if ( attributes.Length > 0 ) {
          AssemblyTitleAttribute titleAttribute = ( AssemblyTitleAttribute )attributes[ 0 ];
          if ( titleAttribute.Title != "" ) {
            return titleAttribute.Title;
          }
        }
        return System.IO.Path.GetFileNameWithoutExtension ( Assembly.GetExecutingAssembly ( ).CodeBase );
      }
    }

    public string AssemblyVersion {
      get {
        return Assembly.GetExecutingAssembly ( ).GetName ( ).Version.ToString ( );
      }
    }

    public string AssemblyDescription {
      get {
        object[] attributes = Assembly.GetExecutingAssembly ( ).GetCustomAttributes ( typeof ( AssemblyDescriptionAttribute ), false );
        if ( attributes.Length == 0 ) {
          return "";
        }
        return ( ( AssemblyDescriptionAttribute )attributes[ 0 ] ).Description;
      }
    }

    public string AssemblyProduct {
      get {
        object[] attributes = Assembly.GetExecutingAssembly ( ).GetCustomAttributes ( typeof ( AssemblyProductAttribute ), false );
        if ( attributes.Length == 0 ) {
          return "";
        }
        return ( ( AssemblyProductAttribute )attributes[ 0 ] ).Product;
      }
    }

    public string AssemblyCopyright {
      get {
        object[] attributes = Assembly.GetExecutingAssembly ( ).GetCustomAttributes ( typeof ( AssemblyCopyrightAttribute ), false );
        if ( attributes.Length == 0 ) {
          return "";
        }
        return ( ( AssemblyCopyrightAttribute )attributes[ 0 ] ).Copyright;
      }
    }

    public string AssemblyCompany {
      get {
        object[] attributes = Assembly.GetExecutingAssembly ( ).GetCustomAttributes ( typeof ( AssemblyCompanyAttribute ), false );
        if ( attributes.Length == 0 ) {
          return "";
        }
        return ( ( AssemblyCompanyAttribute )attributes[ 0 ] ).Company;
      }
    }
    #endregion

    private void dontate_Click ( object sender, EventArgs e ) {
      CommandRunner.Instance.LaunchProcessWindow ( Resources.Strings.DontateLink, string.Empty, true );
    }

    private void linkLabel1_LinkClicked ( object sender, LinkLabelLinkClickedEventArgs e ) {
      CommandRunner.Instance.LaunchProcessWindow ( Resources.Strings.ProjectLink, string.Empty, true );
    }

    private void linkLabel2_LinkClicked ( object sender, LinkLabelLinkClickedEventArgs e ) {
      CommandRunner.Instance.LaunchProcessWindow ( Resources.Strings.XdaThreadLink, string.Empty, true );
    }

    private void plugins_DoubleClick ( object sender, EventArgs e ) {
      if ( plugins.SelectedItems.Count > 0 ) {
        ListViewItem lvi = plugins.SelectedItems[ 0 ];
        string url = lvi.Tag.ToString ( );
        if ( !string.IsNullOrEmpty ( url ) ) {
          CommandRunner.Instance.LaunchProcessWindow ( url, string.Empty, true );
        }
      }
    }
  }
}
