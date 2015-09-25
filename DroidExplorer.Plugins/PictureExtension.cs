using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DroidExplorer.Core;
using DroidExplorer.Core.Plugins;
using Camalot.Common.Extensions;
using DroidExplorer.Core.IO;
using DroidExplorer.Core.UI.Components;

namespace DroidExplorer.Plugins {
	/// <summary>
	/// 
	/// </summary>
	public class PictureExtension : BasePlugin, IFileTypeHandler, IFileTypeIconHandler {

		/// <summary>
		/// Initializes a new instance of the <see cref="Shell"/> class.
		/// </summary>
		/// <param name="host">The host.</param>
		public PictureExtension(IPluginHost host)
			: base(host) {

			if(host != null) {
				this.PluginHost.RegisterFileTypeIconHandler(".jpg", this);
				this.PluginHost.RegisterFileTypeIconHandler(".png", this);
			}
		}

		/// <summary>
		/// Opens the specified file.
		/// </summary>
		/// <param name="file">The file.</param>
		public void Open(Core.IO.FileInfo file) {

		}

		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		public override string Author {
			get { return "Ryan Conrad"; }
		}

		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		public override string Url {
			get { return DroidExplorer.Resources.Strings.ApplicationWebsiteUrl; }
		}

		/// <summary>
		/// Gets the contact.
		/// </summary>
		/// <value>
		/// The contact.
		/// </value>
		public override string Contact {
			get { return string.Empty; }
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public override string Name {
			get { return "Picture Extension"; }
		}

		/// <summary>
		/// Gets the group.
		/// </summary>
		/// <value>
		/// The group.
		/// </value>
		public override string Group {
			get { return "Images"; }
		}

		/// <summary>
		/// Gets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public override string Description {
			get { return "Handles image icons"; }
		}

		/// <summary>
		/// Gets a value indicating whether [create tool button].
		/// </summary>
		/// <value>
		///   <c>true</c> if [create tool button]; otherwise, <c>false</c>.
		/// </value>
		public override bool CreateToolButton {
			get { return false; }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="PictureExtension"/> is runnable.
		/// </summary>
		/// <value>
		///   <c>true</c> if runnable; otherwise, <c>false</c>.
		/// </value>
		public override bool Runnable { get { return false; } }

		/// <summary>
		/// Executes the specified plugin host.
		/// </summary>
		/// <param name="pluginHost">The plugin host.</param>
		/// <param name="currentDirectory">The current directory.</param>
		/// <param name="args">The arguments.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		public override void Execute(IPluginHost pluginHost, Core.IO.LinuxDirectoryInfo currentDirectory, string[] args) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the large image.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public System.Drawing.Image GetLargeImage(Core.IO.FileSystemInfo file) {
			var key = GetKeyName(file);
			
			if(!Cache.Exists(Cache.ICON_IMAGE_CACHE, key)) {
				var temp = CommandRunner.Instance.PullFile(CommandRunner.Instance.DefaultDevice, file.FullPath);
				var cache = Cache.GetPath(Cache.ICON_IMAGE_CACHE);
				using(var img = System.Drawing.Image.FromFile(temp.FullName).Resize(32, 32)) {
					img.Save(System.IO.Path.Combine(cache, temp.Name));
				}
				return System.Drawing.Image.FromFile(System.IO.Path.Combine(cache, temp.Name));
			} else {
				return System.Drawing.Image.FromFile(Cache.Get(Cache.ICON_IMAGE_CACHE, key).FullName);
			}
		}

		/// <summary>
		/// Gets the small image.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public System.Drawing.Image GetSmallImage(Core.IO.FileSystemInfo file) {
			return GetLargeImage(file).Resize(16, 16);
		}

		/// <summary>
		/// Gets the name of the key.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public string GetKeyName(Core.IO.FileSystemInfo file) {
			return Cache.GetCacheKey(file);
		}


		/// <summary>
		/// Gets the ListView item.
		/// </summary>
		/// <param name="fsi">The fsi.</param>
		/// <returns></returns>
		public System.Windows.Forms.ListViewItem GetListViewItem(FileSystemInfo fsi) {
			return new FileSystemInfoListViewItem(fsi);
		}
	}
}
