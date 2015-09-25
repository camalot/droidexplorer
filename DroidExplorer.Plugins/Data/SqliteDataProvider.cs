using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DroidExplorer.Core;
using System.Data.SQLite;
using System.IO;
using System.Globalization;

namespace DroidExplorer.Plugins.Data {
	/// <summary>
	/// 
	/// </summary>
	public abstract class SqliteDataProvider : IDisposable {

		/// <summary>
		/// Initializes a new instance of the <see cref="SqliteDataProvider"/> class.
		/// </summary>
		/// <param name="file">The file.</param>
		public SqliteDataProvider ( string file ) {
			File = file;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is connected.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is connected; otherwise, <c>false</c>.
		/// </value>
		public bool IsConnected { get; set; }
		/// <summary>
		/// Gets or sets the connection.
		/// </summary>
		/// <value>The connection.</value>
		protected SQLiteConnection Connection { get; set; }
		/// <summary>
		/// Gets or sets the database.
		/// </summary>
		/// <value>The database.</value>
		protected FileInfo Database { get; set; }
		/// <summary>
		/// Gets or sets the file.
		/// </summary>
		/// <value>The file.</value>
		protected string File { get; set; }

		/// <summary>
		/// Opens this instance.
		/// </summary>
		public void Open ( ) {
			FileInfo tempFile = new FileInfo ( File );
			if ( !tempFile.Exists ) {
				tempFile = CommandRunner.Instance.PullFile ( File );
			}

			if ( tempFile == null || !tempFile.Exists ) {
				throw new FileNotFoundException ( string.Format ( CultureInfo.InvariantCulture, "Unable to locate file '{0}'", File ), File );
			} else {
				this.Database = tempFile;
			}

			try {
				Connection = new SQLiteConnection ( string.Format ( CultureInfo.InvariantCulture, "Data Source={0};Version=3", Database.FullName ) );
				Connection.Open ( );
				IsConnected = true;
			} catch ( Exception ex ) {
				IsConnected = false;
				this.LogError ( ex.Message, ex );
				throw;
			}
		}

		/// <summary>
		/// Closes this instance.
		/// </summary>
		public void Close ( ) {
			try {
				if ( IsConnected || ( Connection != null && Connection.State == System.Data.ConnectionState.Open ) ) {
					if ( Connection != null ) {
						Connection.Close ( );
					}
				} else {
					this.LogWarn ( "Not Connected" );
				}
			} catch ( Exception ex ) {
				this.LogError ( ex.Message, ex );
			} finally {
				IsConnected = false;
			}
		}

		/// <summary>
		/// Executes the reader.
		/// </summary>
		/// <param name="sql">The SQL.</param>
		/// <returns></returns>
		protected SQLiteDataReader ExecuteReader ( string sql ) {
			if ( !IsConnected ) {
				Open ( );
			}

			SQLiteCommand cmd = new SQLiteCommand ( sql, this.Connection );
			return cmd.ExecuteReader ( );
		}

		/// <summary>
		/// Executes the non query.
		/// </summary>
		/// <param name="sql">The SQL.</param>
		/// <returns></returns>
		protected int ExecuteNonQuery ( string sql ) {
			if ( !IsConnected ) {
				Open ( );
			}

			SQLiteCommand cmd = new SQLiteCommand ( sql, this.Connection );
			return cmd.ExecuteNonQuery ( );
		}

		/// <summary>
		/// Executes the scalar.
		/// </summary>
		/// <param name="sql">The SQL.</param>
		/// <returns></returns>
		protected object ExecuteScalar ( string sql ) {
			if ( !IsConnected ) {
				Open ( );
			}

			SQLiteCommand cmd = new SQLiteCommand ( sql, this.Connection );
			return cmd.ExecuteScalar ( );
		}


		#region IDisposable Members

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose ( ) {
			if ( Connection != null ) {
				this.Connection.Dispose ( );
			}
			this.Close ( );
		}

		#endregion
	}
}
