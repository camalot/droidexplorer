using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DroidExplorer.Plugins.UI {
	/// <summary>
	/// 
	/// </summary>
	public partial class PackPasswordForm : Form {
		/// <summary>
		/// Initializes a new instance of the <see cref="PackPasswordForm" /> class.
		/// </summary>
		public PackPasswordForm ( ) {
			InitializeComponent ( );
		}

		/// <summary>
		/// Gets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		public String Password {
			get {
				return this.password.Text;
			}
		}

		private void ok_Click ( object sender, EventArgs e ) {

		}
	}
}
