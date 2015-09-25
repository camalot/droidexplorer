using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core;
using DroidExplorer.Core.UI;
using DroidExplorer.Configuration;
using DroidExplorer.Core.Plugins;

namespace DroidExplorer.Runner {
	public partial class SelectPluginDialog : Form {
		public SelectPluginDialog ( ) {
			InitializeComponent ( );
			this.plugins.SetVistaExplorerStyle ( );
			LoadPlugins ( );
		}

		public string SelectedPlugin { get; set; }
		public string[] CommandlineArguments {
			get {
				return this.arguments.Text.Split ( ' ' );
			}
		}

		private void LoadPlugins ( ) {
			Settings.Instance.PluginSettings.GetPlugins ( null );
			foreach ( var item in Settings.Instance.PluginSettings.Plugins ) {
				IPlugin iplug = Settings.Instance.PluginSettings.GetPlugin ( item.ID.Replace ( " ", string.Empty ) );
				if ( iplug != null && iplug.Runnable && item.Enabled ) {
					ListViewItem lvi = new ListViewItem ( iplug.Text );
					lvi.Tag = item;
					imageList.Images.Add ( item.ID, iplug.Image ?? DroidExplorer.Resources.Images.android16 );
					lvi.ImageKey = item.ID;
					this.plugins.Items.Add ( lvi );
				}
			}
		}

		private void ok_Click ( object sender, EventArgs e ) {
			if ( this.plugins.SelectedItems.Count == 0 ) {
				return;
			}

			try {
				ListViewItem lvi = this.plugins.SelectedItems[0];
				PluginInfo pi = lvi.Tag as PluginInfo;
				this.SelectedPlugin = pi.ID;
				this.DialogResult = DialogResult.OK;
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
			}
		}

		private void plugins_DoubleClick ( object sender, EventArgs e ) {
			this.ok.PerformClick ( );
		}
	}
}
