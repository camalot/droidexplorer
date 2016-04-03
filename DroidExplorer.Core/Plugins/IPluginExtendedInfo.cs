using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.Plugins {
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="DroidExplorer.Core.Plugins.IPlugin" />
	public interface IPluginExtendedInfo : IPlugin {
		/// <summary>
		/// Gets the author.
		/// </summary>
		/// <value>
		/// The author.
		/// </value>
		string Author { get; }
		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		string Url { get; }
		/// <summary>
		/// Gets the contact.
		/// </summary>
		/// <value>
		/// The contact.
		/// </value>
		string Contact { get; }
		/// <summary>
		/// Gets the copyright.
		/// </summary>
		/// <value>
		/// The copyright.
		/// </value>
		string Copyright { get; }
  }
}
