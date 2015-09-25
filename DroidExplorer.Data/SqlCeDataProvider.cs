using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;

namespace DroidExplorer.Data {
	public  class SqlCeDataProvider : IDataProvider {

		public SqlCeDataProvider ( String connectionString ) {
			if ( ConfigurationManager.ConnectionStrings[connectionString] == null ) {
				throw new ArgumentException ( String.Format ( "Unable to locate referenced connection string: {0}", connectionString ) );
			}

			ConnectionString = ConfigurationManager.ConnectionStrings[connectionString];
			InitConnection ( );
		}

		protected	 void InitConnection ( ) {
			Connection = new SqlCeConnection ( ConnectionString.ConnectionString );
		}

		public SqlCeDataProvider ( ConnectionStringSettings connectionString ) {
			if ( connectionString == null || String.IsNullOrEmpty ( connectionString.ConnectionString ) ) {
				throw new ArgumentNullException ( "connectionString cannot be null" );
			}

			ConnectionString = connectionString;
			InitConnection ( );
		}


		#region IDataProvider Members

		public System.Data.IDataReader ExecuteReader ( string sql, System.Data.CommandType type, params System.Data.IDataParameter[] @params ) {
			Open ( );
			IDbCommand cmd = Connection.CreateCommand ( );
			cmd.CommandText = sql;
			cmd.CommandType = type;
			if ( @params != null ) {
				foreach ( var item in @params ) {
					cmd.Parameters.Add ( item );
				}
			}
			return cmd.ExecuteReader ( );
		}

		public System.Data.IDataReader ExecuteReader ( string sql ) {
			return ExecuteReader ( sql, CommandType.Text, null );
		}

		public System.Data.IDataReader ExecuteReader ( string sql, params System.Data.IDataParameter[] @params ) {
			return ExecuteReader ( sql, CommandType.StoredProcedure, @params );
		}

		public T ExecuteScalar<T> ( string sql ) {
			return ExecuteScalar<T> ( sql, CommandType.Text, null );
		}

		public T ExecuteScalar<T> ( string sql, params System.Data.IDataParameter[] @params ) {
			return ExecuteScalar<T> ( sql, CommandType.StoredProcedure, @params );
		}

		public T ExecuteScalar<T> ( string sql, System.Data.CommandType type, params System.Data.IDataParameter[] @params ) {
			Open ( );
			IDbCommand cmd = Connection.CreateCommand ( );
			cmd.CommandText = sql;
			cmd.CommandType = type;
			if ( @params != null ) {
				foreach ( var item in @params ) {
					cmd.Parameters.Add ( item );
				}
			}
			return (T)cmd.ExecuteScalar ( );
		}

		public int ExecuteNonQuery ( string sql, System.Data.CommandType type, params System.Data.IDataParameter[] @params ) {
			Open ( );
			IDbCommand cmd = Connection.CreateCommand ( );
			cmd.CommandText = sql;
			cmd.CommandType = type;
			if ( @params != null ) {
				foreach ( var item in @params ) {
					cmd.Parameters.Add ( item );
				}
			}
			return cmd.ExecuteNonQuery ( );
		}

		public int ExecuteNonQuery ( string sql, params System.Data.IDataParameter[] @params ) {
			return ExecuteNonQuery ( sql, CommandType.StoredProcedure, @params );
		}

		public int ExecuteNonQuery ( string sql ) {
			return ExecuteNonQuery ( sql, CommandType.Text, null );
		}

		public void Open ( ) {
			if ( Connection == null ) {
				throw new ArgumentException ( "Connection object is not initialized. Make sure you initialize the connection object before attempting to open a connection." );
			}

			if ( Connection != null && Connection.State == System.Data.ConnectionState.Closed || Connection.State == System.Data.ConnectionState.Broken ) {
				Connection.Open ( );
			}
		}

		public void Close ( ) {
			if ( Connection != null && Connection.State != System.Data.ConnectionState.Closed ) {
				Connection.Close ( );
			}
		}

		public System.Configuration.ConnectionStringSettings ConnectionString { get; private set; }

		public System.Data.IDbConnection Connection { get; private set; }

		#endregion

		#region IDisposable Members

		public void Dispose ( ) {
			Close ( );

			if ( Connection != null ) {
				Connection.Dispose ( );
			}
		}

		#endregion
	}
}
