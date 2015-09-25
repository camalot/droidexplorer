using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DroidExplorer.Data {
	public sealed class DatabaseProvider : SqlCeDataProvider {

		public DatabaseProvider ( )
			: base ( "DroidExplorer.Database" ) {

			if (!DatabaseExists) {
				CreateDatabase ( );
			}
		}




		public IEnumerable<String> GetAllTables ( ) {
			var tables = new HashSet<String> ( );
			using ( var reader = ExecuteReader ( "select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", System.Data.CommandType.Text, null ) ) {
				while ( reader.Read ( ) ) {
					tables.Add ( reader.GetString ( 0 ) );
				}
			}
			return tables;
		}

		public bool TableExists ( String name ) {
			if ( String.IsNullOrEmpty ( name ) ) {
				throw new ArgumentNullException ( "name", "table name cannot be null or empty" );
			}
			return GetAllTables ( ).Any ( m => String.Compare ( name, m, true ) == 0 );
		}

		public bool CreateTable ( TableBuilder builder ) {
			if ( builder == null ) {
				throw new ArgumentException ( "builder cannot be null", "builder" );
			}
			try {
				if ( !TableExists ( builder.Table.Name ) ) {
					Console.WriteLine ( "Create Table: {0}", builder.Create ( ) );
					this.ExecuteNonQuery ( builder.Create ( ) );
					return true;
				}
				return false;
			} catch ( Exception ) {
				return false;
			}
		}

		public bool CreateDatabase ( ) {
			try {
				SqlCeEngine engine = new SqlCeEngine ( ConnectionString.ConnectionString );
				DropDatabase ( );

				engine.CreateDatabase ( );
				InitConnection ( );
				return true;
			} catch ( Exception ex) {
				Console.WriteLine ( ex.ToString ( ) );
				return false;
			}
		}

		public bool DropDatabase ( ) {
			try {
				if ( DatabaseFile.Exists ) {
					DatabaseFile.Delete ( );
					return true;
				}
				return false;
			} catch ( Exception ) {
				return false;
			}
		}

		public bool DatabaseExists {
			get {
				return DatabaseFile != null && DatabaseFile.Exists;
			}
		}

		public bool DropTable ( String table ) {
			try {
				if ( TableExists ( table ) ) {
					ExecuteNonQuery ( String.Format("DROP TABLE [{0}];", table ) );
					return true;
				}
				Console.WriteLine ( "Table [{0}] does not exist", table );
				return false;
			} catch ( Exception ex ) {
				Console.WriteLine ( ex.ToString ( ) );
				return false;
			}
		}

		protected void InitializeDatabase ( ) {

		}

		protected FileInfo DatabaseFile {
			get {
				var regex = new Regex ( @"Data\sSource=\|DataDirectory\|\\(.*?)$", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace );
				var m = regex.Match ( ConnectionString.ConnectionString );
				if ( m.Success ) {
					var directoryName = System.IO.Path.GetDirectoryName ( this.GetType().Assembly.Location );
					var fileName = m.Groups[1].Value ;
					return new FileInfo ( fileName );
				}
				throw new ArgumentException ( "unable to get database from connection string", "data source" );
			}
		}

		public class TableBuilder {
			internal Table Table { get; set; }

			public TableBuilder AddTable ( String name ) {
				Table = new Table {
					Name = name,
					Columns = new List<TableColumn> ( )
				};
				return this;
			}

			public TableBuilder AddColumn ( TableColumn column ) {
				if ( Table == null ) {
					throw new ArgumentException ( "Must add a table before adding a column" );
				}
				if ( column.IsPrimaryKey && Table.Columns.Any ( m => m.IsPrimaryKey ) ) {
					throw new ArgumentException ( "cannot add another primary key to this table" );
				}
				Table.Columns.Add ( column );
				return this;
			}

			public String Create ( ) {
				var sb = new StringBuilder ( );
				if ( Table == null || Table.Columns == null || Table.Columns.Count == 0 ) {
					throw new ArgumentException ( "Unable to create table." );
				}

				sb.AppendFormat ( "CREATE TABLE [{0}] (" );

				// if it doesn't have a primary key column, create one
				if ( !Table.Columns.Any ( m => m.IsPrimaryKey ) ) {
					Table.Columns.Add ( TableColumn.PrimaryKey ( ) );
				}
				Table.Columns.ForEach ( m => {
					sb.AppendFormat ( "[{0}] {1} {2} {3} {4} {5},", m.Name, m.Type.ToUpper ( ), m.AllowNull ? "NULL" : "NOT NULL",
						String.IsNullOrEmpty ( m.Default ) || m.IsPrimaryKey || m.IsIdentity ? String.Empty : "DEFAULT " + m.Default,
						m.IsIdentity ? "IDENTITY(1,1)" : String.Empty, m.IsPrimaryKey ? "PRIMARY KEY" : String.Empty );
				} );
				sb.Remove ( sb.Length - 1, 1 );
				sb.Append ( ")" );
				return sb.ToString ( );
			}
		}

		public class Table {
			public String Name { get; set; }
			public List<TableColumn> Columns { get; set; }
		}

		public class TableColumn {
			public String Name { get; set; }
			public bool AllowNull { get; set; }
			public String Type { get; set; }
			public String Default { get; set; }
			public bool IsIdentity { get; set; }
			public bool IsPrimaryKey { get; set; }

			public static TableColumn PrimaryKey ( ) {
				return new TableColumn {
					Name = "_ID",
					IsIdentity = true,
					IsPrimaryKey = true,
					AllowNull = false,
					Type = "INT"
				};
			}
		}
	}
}
