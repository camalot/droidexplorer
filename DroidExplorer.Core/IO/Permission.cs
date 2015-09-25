using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.IO {
	/// <summary>
	/// 
	/// </summary>
	public class Permission {

		/// <summary>
		/// 
		/// </summary>
		[Flags]
		public enum Modes {
			/// <summary>
			/// 
			/// </summary>
			NoAccess = 0,
			/// <summary>
			/// 
			/// </summary>
			Execute = 1,
			/// <summary>
			/// 
			/// </summary>
			Write = 2,
			/// <summary>
			/// 
			/// </summary>
			Read = 4
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Permission"/> class.
		/// </summary>
		public Permission ( ) : this ("---") {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Permission"/> class.
		/// </summary>
		/// <param name="linuxPermissions">The linux permissions.</param>
		public Permission ( string linuxPermissions ) {
			this.CanRead = string.Compare ( linuxPermissions.Substring ( 0, 1 ), "r", false ) == 0;
			this.CanWrite = string.Compare ( linuxPermissions.Substring ( 1, 1 ), "w", false ) == 0;
			this.CanExecute = string.Compare ( linuxPermissions.Substring ( 2, 1 ), "x", false ) == 0 || string.Compare ( linuxPermissions.Substring ( 2, 1 ), "t", false ) == 0;
			this.CanDelete = this.CanWrite && string.Compare ( linuxPermissions.Substring ( 2, 1 ), "t", false ) != 0;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance can execute.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can execute; otherwise, <c>false</c>.
		/// </value>
		public bool CanExecute { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can write.
		/// </summary>
		/// <value><c>true</c> if this instance can write; otherwise, <c>false</c>.</value>
		public bool CanWrite { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can read.
		/// </summary>
		/// <value><c>true</c> if this instance can read; otherwise, <c>false</c>.</value>
		public bool CanRead { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can delete.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance can delete; otherwise, <c>false</c>.
		/// </value>
		public bool CanDelete { get; private set; }

		/// <summary>
		/// Converts the permissions to bit value that can be casted to an integer and used for calling chmod
		/// </summary>
		/// <returns></returns>
		public Modes ToChmod ( ) {
			Modes val = Modes.NoAccess;
			if ( CanRead ) {
				val |= Modes.Read;
			}

			if ( CanWrite ) {
				val |= Modes.Write;
			}

			if ( CanExecute ) {
				val |= Modes.Execute;
			}
			int ival = (int)val;
			return val;
		}

	}
}
