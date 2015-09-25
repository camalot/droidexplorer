using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace DroidExplorer.Data {
	internal interface IDataProvider : IDisposable {
		/// <summary>
		/// Executes the specified sql returning a data reader.
		/// </summary>
		/// <param name="sql">The SQL.</param>
		/// <param name="type">The command type.</param>
		/// <param name="params">The command params.</param>
		/// <returns></returns>
		IDataReader ExecuteReader( String sql, CommandType type, params IDataParameter[] @params );
		/// <summary>
		/// Executes the specified sql returning a data reader.
		/// </summary>
		/// <param name="sql">The SQL command.</param>
		/// <returns></returns>
		IDataReader ExecuteReader( String sql );
		/// <summary>
		/// Executes the specified sql returning a data reader.
		/// </summary>
		/// <param name="sql">The SQL command.</param>
		/// <param name="params">The command params.</param>
		/// <returns></returns>
		IDataReader ExecuteReader( String sql, params IDataParameter[] @params );
		/// <summary>
		/// Executes the specified sql returning a scalar value.
		/// </summary>
		/// <typeparam name="T">The result</typeparam>
		/// <param name="sql">The SQL.</param>
		/// <returns></returns>
		T ExecuteScalar<T>( String sql );
		/// <summary>
		/// Executes the specified sql returning a scalar value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sql">The SQL command.</param>
		/// <param name="params">The command params.</param>
		/// <returns></returns>
		T ExecuteScalar<T>( String sql, params IDataParameter[] @params );
		/// <summary>
		/// Executes the specified sql returning a scalar value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sql">The SQL command.</param>
		/// <param name="type">The command type.</param>
		/// <param name="params">The command params.</param>
		/// <returns></returns>
		T ExecuteScalar<T>( String sql, CommandType type, params IDataParameter[] @params );
		/// <summary>
		/// Executes the specified sql returning the number of rows affected.
		/// </summary>
		/// <param name="sql">The SQL command.</param>
		/// <param name="type">The command type.</param>
		/// <param name="params">The command params.</param>
		/// <returns></returns>
		int ExecuteNonQuery( string sql, CommandType type, params System.Data.IDataParameter[] @params );
		/// <summary>
		/// Executes the specified sql returning the number of rows affected.
		/// </summary>
		/// <param name="sql">The SQL.</param>
		/// <param name="params">The @params.</param>
		/// <returns></returns>
		int ExecuteNonQuery( string sql, params System.Data.IDataParameter[] @params );
		/// <summary>
		/// Executes the specified sql returning the number of rows affected.
		/// </summary>
		/// <param name="sql">The SQL.</param>
		/// <returns></returns>
		int ExecuteNonQuery( string sql );

		/// <summary>
		/// Opens the connection to the data source.
		/// </summary>
		/// <remarks>This is handled by the base class</remarks>
		void Open( );
		/// <summary>
		/// Closes the connection to the data source.
		/// </summary>
		/// <remarks>This is handled by the base class</remarks>
		void Close( );

		/// <summary>
		/// Gets the connection string.
		/// </summary>
		ConnectionStringSettings ConnectionString { get; }
		/// <summary>
		/// Gets the connection object. This must be set in the constructor of your subclass.
		/// </summary>
		IDbConnection Connection { get; }
	}
}
