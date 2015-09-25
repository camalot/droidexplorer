using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.Configuration {
	public interface ISettings {
		/// <summary>
		/// Gets or sets the system settings.
		/// </summary>
		/// <value>The system settings.</value>
		ISystemStoredSettings SystemSettings { get; set; }
		/// <summary>
		/// Gets or sets the apk paths.
		/// </summary>
		/// <value>The apk paths.</value>
		List<string> ApkPaths { get; set; }
		/// <summary>
		/// Gets the user data directory.
		/// </summary>
		/// <value>The user data directory.</value>
		String UserDataDirectory { get; }
		/// <summary>
		/// Reloads this instance.
		/// </summary>
		void Reload ( );
		/// <summary>
		/// Saves this instance.
		/// </summary>
		void Save ( );
	}
}
