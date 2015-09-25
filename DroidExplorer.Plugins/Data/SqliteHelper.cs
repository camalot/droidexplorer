using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroidExplorer.Plugins.Data {
	/// <summary>
	/// 
	/// </summary>
	public static class SqliteHelper {
		/// <summary>
		/// 0 = table
		/// </summary>
		public const string SelectStarFromTable = "SELECT * FROM {0}";
		/// <summary>
		/// 0 = table
		/// 1 = columns
		/// </summary>
		public const string SelectColumnsFromTable = "SELECT {1} FROM {0}";
		/// <summary>
		/// 0 = table
		/// 1 = where expression
		/// </summary>
		public const string SelectStarFromTableWhere = "SELECT * FROM {0} WHERE {1}";
		/// <summary>
		/// 0 = table
		/// 1 = columns
		/// 2 = where expression
		/// </summary>
		public const string SelectColumnsFromTableWhere = "SELECT {1} FROM {0} WHERE {2}";

		/// <summary>
		/// 0 = table
		/// </summary>
		public const string DeleteFromTable = "DELETE FROM {0}";
		/// <summary>
		/// 0 = table
		/// 1 = where expression
		/// </summary>
		public const string DeleteFromTableWhere = "DELETE FROM {0} WHERE {1}";

		/// <summary>
		/// 0 = table
		/// </summary>
		public const string DropTable = "DROP TABLE {0}";
		/// <summary>
		/// 0 = index
		/// </summary>
		public const string DropIndex = "DROP INDEX {0}";
		/// <summary>
		/// 0 = view
		/// </summary>
		public const string DropView = "DROP VIEW {0}";
		/// <summary>
		/// 0 = trigger
		/// </summary>
		public const string DropTrigger = "DROP TRIGGER {0}";


		/// <summary>
		/// 0 = table
		/// 1 = columns-list
		/// 2 = values-list
		/// </summary>
		public const string InsertInToTable = "INSERT INTO {0} ({1}) VALUES({2})";

		/// <summary>
		/// 0 = table
		/// 1 = columns-list
		/// 2 = select statement
		/// </summary>
		public const string InsertInToTableFromSelect = "INSERT INTO {0} ({1}) {2}";

		/// <summary>
		/// 0 = table
		/// 1 = assignment-list
		/// 2 = where expression
		/// </summary>
		public const string UpdateTable = "UPDATE {0} SET {1} WHERE {2}";

		/// <summary>
		/// 0 = table
		/// </summary>
		public const string PragmaTableInfo = "PRAGMA table_info ({0})";
		/// <summary>
		/// 0 = table
		/// </summary>
		public const string PragmaTableIndexList = "PRAGMA index_list ({0})";

		/// <summary>
		/// 0 = table
		/// </summary>
		public const string PragmaTableForeignKeyList = "PRAGMA foreign_key_list ({0})";

		/// <summary>
		/// 0 = index name
		/// </summary>
		public const string PragmaIndexInfo = "PRAGMA index_info ({0})";

		/// <summary>
		/// 
		/// </summary>
		public const string PragmaDatabaseList = "PRAGMA database_list";

		/// <summary>
		/// 
		/// </summary>
		public const string PragmaCollationList = "PRAGMA collation_list";
	}
}
