using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OnlineStore.Objects
{
  public class ProductTest : IDisposable
  {
    public ProductTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog= online_store_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Product.DeleteAll();
    }

    [Fact]
    public void Test_EmptyDatabseAtFirst()
    {
        //Arrange,Act
        int result = Product.GetAll().Count;
        //Assert
        Assert.Equal(0, result);
    }
  }
}
