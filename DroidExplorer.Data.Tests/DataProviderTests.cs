using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DroidExplorer.Data.Tests {
	public class DataProviderTests {
		public DataProviderTests ( ) {
			DataProvider = new DatabaseProvider ( );
		}

		public DatabaseProvider DataProvider { get; set; }
		[Fact]
		public void CreateTableTest ( ) {
			DataProvider.CreateTable(new DatabaseProvider.TableBuilder().AddTable("TestTable").AddColumn(new DatabaseProvider.TableColumn{
				Name = "Col1",
				Type = "NVARCHAR(50)",
				AllowNull = false
			}));
		}
		
		[Fact]
		public void DeleteTableTest ( ) {
			Assert.True ( DataProvider.TableExists ( "TestTable" ) );

			DataProvider.DropTable ( "TestTable" );
		}

	}
}
