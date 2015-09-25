using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Core.Configuration {
	public interface ISystemStoredSettings {
		/// <summary>
		/// Reloads this instance.
		/// </summary>
		void Reload ( );
		/// <summary>
		/// Gets or sets the SDK path.
		/// </summary>
		/// <value>The SDK path.</value>
		String SdkPath { get; set; }
		/// <summary>
		/// Gets or sets the install path.
		/// </summary>
		/// <value>The install path.</value>
		String InstallPath { get; set; }
		/// <summary>
		/// Gets or sets the SDK platform version.
		/// </summary>
		/// <value>The SDK platform version.</value>
		int SdkPlatformVersion { get; set; }
		/// <summary>
		/// Gets the platform tools path.
		/// </summary>
		/// <value>The platform tools path.</value>
		String PlatformToolsPath { get; }
		/// <summary>
		/// Gets the SDK tools path.
		/// </summary>
		/// <value>The SDK tools path.</value>
		String SdkToolsPath { get;  }

		/// <summary>
		/// Gets the build tools path.
		/// </summary>
		String BuildToolsPath { get; }

		/// <summary>
		/// Gets or sets a value indicating whether we should use an existing SDK.
		/// </summary>
		/// <value><c>true</c> if we should use an existing SDK; otherwise, <c>false</c>.</value>
		bool UseExistingSdk { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether device information will be recorded to the cloud.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if device information will be recorded to the cloud; otherwise, <c>false</c>.
		/// </value>
		bool RecordDeviceInformationToCloud { get; set; }
		DateTime GetLastRecordCloud(string deviceId);
		void SetLastRecordCloud(string deviceId);
	}
}
