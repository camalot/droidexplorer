using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidExplorer.Core.Plugins {
	public abstract class BasePlugin : IPluginExtendedInfo{

		public BasePlugin ( IPluginHost host ) {
			this.PluginHost = host;
		}

		#region IPluginExtendedInfo Members

		public abstract string Author { get; }

		public abstract string Url { get; }

		public abstract string Contact { get; }

		public virtual string Copyright {
			get { return String.Format ( "Copyright © {1} {0}", DateTime.Now.Year, Author ); }
		}

		#endregion

		#region IPlugin Members

		public abstract string Name { get; }
		public abstract string Group { get; }

		public abstract string Description { get; }

		public virtual bool HasConfiguration {
			get { return false; }
		}

		public virtual System.Drawing.Image Image {
			get { return null; }
		}

		public virtual string Text {
			get { return Name; }
		}

		public abstract bool CreateToolButton { get; }

		public System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle {
			get { return System.Windows.Forms.ToolStripItemDisplayStyle.Image;  }
		}

		public virtual void Execute ( IPluginHost pluginHost, IO.LinuxDirectoryInfo currentDirectory ) {
			Execute ( pluginHost, currentDirectory, null );
		}
		public virtual void Execute ( IPluginHost pluginHost ) {
			Execute ( pluginHost, new	DroidExplorer.Core.IO.LinuxDirectoryInfo("/"), null );
		}

		public abstract void Execute ( IPluginHost pluginHost, IO.LinuxDirectoryInfo currentDirectory, string[] args );

		public virtual void Execute ( IPluginHost pluginHost, string[] args ) {
			Execute ( pluginHost, new DroidExplorer.Core.IO.LinuxDirectoryInfo ( "/" ), args );
		}


		public virtual async Task ExecuteAsync(IPluginHost pluginHost, IO.LinuxDirectoryInfo currentDirectory) {
			await ExecuteAsync(pluginHost,currentDirectory, null);
		}

		public virtual async Task ExecuteAsync(IPluginHost pluginHost) {
			await ExecuteAsync(pluginHost, new IO.LinuxDirectoryInfo("/"), null);
		}

		public virtual async Task ExecuteAsync(IPluginHost pluginHost, IO.LinuxDirectoryInfo currentDirectory, string[] args) {
			await Task.Run(() => { Execute(pluginHost, currentDirectory, args); });
		}

		public virtual async Task ExecuteAsync(IPluginHost pluginHost, string[] args) {
			await ExecuteAsync(pluginHost, new IO.LinuxDirectoryInfo("/"), args);
		}

		public virtual void Initialize ( IPluginHost pluginHost ) {
			this.LogDebug ( "Initializing Plugin: {0}", this.GetType ( ).Name );
			return;
		}

		//public virtual async Task InitializeAsync(IPluginHost pluginHost) {
		//	return;
		//}

		[Browsable ( false ), ReadOnly ( true )]
		public IPluginHost PluginHost { get; set; }

		public virtual bool Runnable {
			get { return true; }
		}

		public virtual System.Windows.Forms.ToolStripItem CreateToolStripMenuItem ( ){
			return PluginHelper.CreateToolStripMenuItemForPlugin ( this );
		}

		public virtual System.Windows.Forms.ToolStripItem CreateToolStripButton ( ) {
			return PluginHelper.CreateToolStripButtonForPlugin ( this );
		}

		public virtual int MinimumSDKToolsVersion {
			get { return 0; }
		}

		public virtual int MinimumSDKPlatformToolsVersion {
			get { return 0; }
		}

		public virtual string Id {
			get { 
				var t = this.GetType();
				return String.Format ( "{0},{1}", t.FullName, t.Assembly.GetName ( ).Name );
			}
		}

		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Browsable(false)]
		public virtual IPluginSettings Configuration { get; protected set; }

		#endregion

		public override string ToString() {
			return this.Name;
		}
	}
}
