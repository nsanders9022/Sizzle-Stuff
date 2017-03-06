using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OnlineStore.Objects
{
    public class CategoryTest : IDisposable
    {
        public CategoryTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog= online_store_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_EmptyDatabaseAtFirst()
        {
            //Arrange,Act
            int result = Category.GetAll().Count;
            //Assert
            Assert.Equal(0, result);
        }




        public void Dispose()
        {
            Category.DeleteAll();
        }
    }
}
