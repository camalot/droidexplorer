using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Net;

namespace DroidExplorer.Core.Net {
	[XmlRoot ( "ProxyInfo" )]
	public class ProxyInfo {
		private bool _overrideDefaultProxy = false;
		private bool _requiresLogin = false;
		private string _serverName = string.Empty;
		private bool _bypassLocal = false;
		private int _proxyPort = 8080;
		private string _user = string.Empty;
		private string _pass = string.Empty;
		private string _domain = string.Empty;
		private List<string> _bypassList = null;
		private string _name = string.Empty;
		private bool _enabled = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProxyInfo"/> class.
		/// </summary>
		public ProxyInfo ( ) {
			this._bypassList = new List<string> ( );
		}

		/// <summary>
		/// Creates the WebProxy.
		/// </summary>
		/// <returns></returns>
		public WebProxy CreateProxy ( ) {
			WebProxy proxy = new WebProxy ( this.ProxyServer, this.ProxyPort );
			proxy.BypassProxyOnLocal = this.BypassLocal;
			proxy.BypassList = this.BypassList.ToArray ( );
			if ( !string.IsNullOrEmpty ( this.Username ) )
				proxy.Credentials = new NetworkCredential ( this.Username, this.Password, this.Domain );
			return proxy;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[XmlAttribute ( "Name" )]
		public string Name { get { return this._name; } set { this._name = value; } }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ProxyInfo"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		[XmlAttribute ( "Enabled" )]
		public bool Enabled { get { return this._enabled; } set { this._enabled = value; } }

		/// <summary>
		/// Gets or sets a value indicating whether [override default proxy].
		/// </summary>
		/// <value>
		/// 	<c>true</c> if [override default proxy]; otherwise, <c>false</c>.
		/// </value>
		[XmlElement ( "OverrideDefaultProxy" )]
		public bool OverrideDefaultProxy { get { return this._overrideDefaultProxy; } set { this._overrideDefaultProxy = value; } }
		/// <summary>
		/// Gets or sets a value indicating whether [requires login].
		/// </summary>
		/// <value><c>true</c> if [requires login]; otherwise, <c>false</c>.</value>
		[XmlElement ( "RequiresLogin" )]
		public bool RequiresLogin { get { return this._requiresLogin; } set { this._requiresLogin = value; } }
		/// <summary>
		/// Gets or sets a value indicating whether [bypass locas].
		/// </summary>
		/// <value><c>true</c> if [bypass locas]; otherwise, <c>false</c>.</value>
		[XmlElement ( "BypassLocal" )]
		public bool BypassLocal { get { return this._bypassLocal; } set { this._bypassLocal = value; } }
		/// <summary>
		/// Gets or sets the bypass list.
		/// </summary>
		/// <value>The bypass list.</value>
		[XmlArray ( "BypassList" ), XmlArrayItem ( "BypassItem" )]
		public List<string> BypassList { get { return this._bypassList; } set { this._bypassList = value; } }
		/// <summary>
		/// Gets or sets the name of the server.
		/// </summary>
		/// <value>The name of the server.</value>
		[XmlElement ( "Server" )]
		public string ProxyServer { get { return this._serverName; } set { this._serverName = value; } }
		/// <summary>
		/// Gets or sets the proxy port.
		/// </summary>
		/// <value>The proxy port.</value>
		[XmlElement ( "Port" )]
		public int ProxyPort { get { return this._proxyPort; } set { this._proxyPort = value; } }
		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>The username.</value>
		[XmlElement ( "Username" )]
		public string Username { get { return this._user; } set { this._user = value; } }
		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		[XmlElement ( "Password" )]
		public string Password { get { return this._pass; } set { this._pass = value; } }
		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		[XmlElement ( "Domain" )]
		public string Domain { get { return this._domain; } set { this._domain = value; } }
	}
}
