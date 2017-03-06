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

    [Fact]
    public void Product_IdentityTest_ReturnTrueOnIdenticalObjects()
    {
        //Arrange,Act
        Product firstProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
        Product secondProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
        //Assert
        Assert.Equal(firstProduct, secondProduct);
    }
    [Fact]
    public void Save_SingleProduct_ProductSaveToDatabase()
    {
        //Arrange
        Product testProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");

        //Act
        testProduct.Save();
        List<Product> testList = Product.GetAll();
        List<Product> result = new List<Product> {testProduct};

        //Assert
        Assert.Equal(testList, result);
    }










  }
}
