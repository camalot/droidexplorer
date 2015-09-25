using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DroidExplorer.Bootstrapper.Authentication {
	public partial class CredentialConfirmForm : Form {

		private string _target = "Secured Application";
		bool expectConfirmation;

		public CredentialConfirmForm ( string target )
			: this ( target, null ) { }
		/// <summary>Initializes a new instance of the <see cref="T:SecureCredentialsLibrary.CredentialsDialog"/> class
		/// with the specified target and caption.</summary>
		/// <param name="target">The name of the target for the credentials, typically a server name.</param>
		/// <param name="caption">The caption of the dialog (null will cause a system default title to be used).</param>
		public CredentialConfirmForm ( string target, string caption )
			: this ( target, caption, null ) { }
		/// <summary>Initializes a new instance of the <see cref="T:SecureCredentialsLibrary.CredentialsDialog"/> class
		/// with the specified target, caption and message.</summary>
		/// <param name="target">The name of the target for the credentials, typically a server name.</param>
		/// <param name="caption">The caption of the dialog (null will cause a system default title to be used).</param>
		/// <param name="message">The message of the dialog (null will cause a system default message to be used).</param>
		public CredentialConfirmForm ( string target, string caption, string message )
			: this ( target, caption, message, null ) { }
		/// <summary>Initializes a new instance of the <see cref="T:SecureCredentialsLibrary.CredentialsDialog"/> class
		/// with the specified target, caption, message and banner.</summary>
		/// <param name="target">The name of the target for the credentials, typically a server name.</param>
		/// <param name="caption">The caption of the dialog (null will cause a system default title to be used).</param>
		/// <param name="message">The message of the dialog (null will cause a system default message to be used).</param>
		/// <param name="banner">The image to display on the dialog (null will cause a system default image to be used).</param>
		public CredentialConfirmForm ( string target, string caption, string message, Image banner ) {
			InitializeComponent ( );
			this.Target = target;
			this.Caption = caption;
			this.Message = message;
			this.Banner = banner;
		}

		public bool SaveChecked {
			get { return this.save.Checked; }
			set { this.save.Checked = value; }
		}

		public bool SaveDisplayed {
			get { return this.save.Visible; }
			set { this.save.Visible = value; }
		}

		public string Username {
			get { return this.username.Text; }
			set { this.username.Text = value; }
		}

		public string Password {
			get { return this.password.Text; }
			set { this.password.Text = value; }
		}

		public bool ExpectConfirmation {
			get { return expectConfirmation; }
			set { expectConfirmation = value; }
		}

		public Image Banner {
			get { return this.banner.Image; }
			set {
				if ( value == null ) {
					this.banner.Image = Properties.Resources.security_background;
				} else {
					this.banner.Image = value;
				}
			}
		}

		public string Target {
			get { return this._target; }
			set {
				this._target = value;
			}
		}

		public string Message {
			get { return this.welcome.Text; }
			set {
				if ( string.IsNullOrEmpty ( value ) ) {
					this.welcome.Text = string.Format ( "Welcome to {0}", this.Target );
				} else {
					this.welcome.Text = value;
				}
			}
		}

		public string Caption {
			get {
				return this.Text;
			}
			set {
				if ( string.IsNullOrEmpty ( value ) ) {
					this.Text = string.Format ( "Connect to {0}", this.Target );
				} else {
					this.Text = value;
				}
			}
		}

		public void SaveCredentials ( ) {
			if ( !DPAPI.CanUseDPAPI )
				return;

			byte[] data;
			using ( MemoryStream stream = new MemoryStream ( ) )
			using ( BinaryWriter writer = new BinaryWriter ( stream ) ) {
				writer.Write ( Username );
				writer.Write ( Password );
				data = stream.GetBuffer ( );
			}

			DPAPI dpapi = new DPAPI ( );
			dpapi.Entropy = GetEntropy ( );
			byte[] cipher = dpapi.Encrypt ( data );
			using ( FileStream stream = new FileStream ( PersistFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None ) )
			using ( BinaryWriter writer = new BinaryWriter ( stream ) ) {
				writer.Write ( cipher.Length );
				writer.Write ( cipher );
			}
		}

		public bool LoadCredentials ( ) {
			if ( !DPAPI.CanUseDPAPI )
				return false;
			if ( File.Exists ( PersistFile ) ) {
				byte[] cipher;
				using ( FileStream stream = new FileStream ( PersistFile, FileMode.Open, FileAccess.Read, FileShare.None ) )
				using ( BinaryReader reader = new BinaryReader ( stream ) ) {
					int length = reader.ReadInt32 ( );
					cipher = reader.ReadBytes ( length );
				}

				DPAPI dpapi = new DPAPI ( );
				dpapi.Entropy = GetEntropy ( );

				try {
					byte[] data = dpapi.Decrypt ( cipher );

					using ( MemoryStream stream = new MemoryStream ( data ) )
					using ( BinaryReader reader = new BinaryReader ( stream ) ) {
						Username = reader.ReadString ( );
						Password = reader.ReadString ( );
					}
				} catch ( Exception ) {
					return false;
				}

				return true;
			} else
				return false;
		}

		byte[] GetEntropy ( ) {
			System.Security.Cryptography.SHA1Managed sha1 = new System.Security.Cryptography.SHA1Managed ( );
			return sha1.ComputeHash ( System.Text.Encoding.UTF8.GetBytes ( Target ) );
		}

		string GetHashString ( string s ) {
			System.Security.Cryptography.MD5CryptoServiceProvider hashProvider = new System.Security.Cryptography.MD5CryptoServiceProvider ( );
			byte[] hash = hashProvider.ComputeHash ( System.Text.Encoding.UTF8.GetBytes ( s ) );
			return new Guid ( hash ).ToString ( );
		}

		string PersistFile {
			get { return Environment.GetFolderPath ( Environment.SpecialFolder.LocalApplicationData ) + string.Format ( "/Credentials_{0}.bin", GetHashString ( Target ) ); }
		}

		private void ok_Click ( object sender, EventArgs e ) {
			this.DialogResult = DialogResult.OK;
		}
	}
}