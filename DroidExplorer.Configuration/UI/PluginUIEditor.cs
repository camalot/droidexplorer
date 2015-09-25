using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DroidExplorer.Core;

namespace DroidExplorer.Configuration.UI {
	public class PluginUIEditor : Control, IUIEditor {

		public PluginUIEditor() {
			InitializeComponent();
			LoadList();
		}
		private Button remove;
		private Button add;
		private ListView paths;
		private ColumnHeader columnHeader1;

		private void InitializeComponent() {

			this.remove = new System.Windows.Forms.Button();
			this.remove.Click += new EventHandler(remove_Click);

			this.add = new System.Windows.Forms.Button();
			this.add.Click += new EventHandler(add_Click);

			this.paths = new System.Windows.Forms.ListView();

			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// remove
			// 
			this.remove.Location = new System.Drawing.Point(140, 216);
			this.remove.Name = "remove";
			this.remove.Size = new System.Drawing.Size(106, 35);
			this.remove.TabIndex = 2;
			this.remove.Text = DroidExplorer.Resources.Strings.Button_Remove;
			this.remove.UseVisualStyleBackColor = true;
			this.remove.Enabled = false;
			// 
			// add
			// 
			this.add.Location = new System.Drawing.Point(13, 216);
			this.add.Name = "add";
			this.add.Size = new System.Drawing.Size(106, 35);
			this.add.TabIndex = 1;
			this.add.Text = DroidExplorer.Resources.Strings.Button_Add;
			this.add.UseVisualStyleBackColor = true;
			// 
			// paths
			// 
			this.paths.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.paths.Dock = System.Windows.Forms.DockStyle.Top;
			this.paths.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.paths.Location = new System.Drawing.Point(10, 5);
			this.paths.MultiSelect = false;
			this.paths.Name = "paths";
			this.paths.Size = new System.Drawing.Size(383, 202);
			this.paths.TabIndex = 0;
			this.paths.UseCompatibleStateImageBehavior = false;
			this.paths.View = System.Windows.Forms.View.Details;
			this.paths.ItemSelectionChanged += paths_ItemSelectionChanged;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = DroidExplorer.Resources.Strings.Configuration_PluginDirectoriesHeading;
			this.columnHeader1.Width = 372;

			this.Dock = DockStyle.Fill;
			this.Controls.AddRange(new Control[] { this.paths, this.add, this.remove });

			this.ResumeLayout(false);

		}

		void paths_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			this.remove.Enabled = !(e.Item == null || Settings.Instance.PluginSettings.DefaultPlaths.Any(x => string.Compare(x, e.Item.Text) == 0));
		}

		void remove_Click(object sender, EventArgs e) {
			if(this.paths.SelectedItems.Count == 1) {
				ListViewItem lvi = this.paths.SelectedItems[0];
				Settings.Instance.PluginSettings.Paths.Remove(lvi.Text);

				if(Settings.Instance.PluginSettings.DefaultPlaths.Any(x => string.Compare(x, lvi.Text) == 0)) {
					return;
				}

				this.paths.Items.Remove(lvi);
			}
		}

		void add_Click(object sender, EventArgs e) {
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.Description = DroidExplorer.Resources.Strings.Configuration_SelectPluginDirectory;
			if(fbd.ShowDialog(this.FindForm()) == DialogResult.OK) {
				string path = fbd.SelectedPath;
				foreach(ListViewItem item in paths.Items) {
					if(string.Compare(item.Text, path, true) != 0) {
						paths.Items.Add(path);
						Settings.Instance.PluginSettings.Paths.Add(path);
					}
				}
			}
		}
		#region IUIEditor Members

		public void SetSourceObject(object obj) {

		}

		#endregion

		private void LoadList() {
			paths.Items.Clear ( );
			var merged = Settings.Instance.PluginSettings.Paths.Union(Settings.Instance.PluginSettings.DefaultPlaths);
			foreach(var item in merged) {
				paths.Items.Add (item, item,0 );
			}
		}

	}
}
