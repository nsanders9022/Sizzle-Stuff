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

    [Fact]
    public void Find_GetProductById_ReturnTargetProduct()
    {
        //Arrange
        Product testProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
        testProduct.Save();

        //Act
        Product expected = testProduct;
        Product result = Product.Find(testProduct.GetId());

        //Assert
        Assert.Equal(expected, result);
    }
    //
    [Fact]
    public void Delete_RemovesAProductFromDatabase_DecrementDatabase()
    {
        //Arrange
        Product testProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
        testProduct.Save();
        Product secondProduct = new Product("Banana Corer", 13, 5, 20.99m, "Kind of weird");
        secondProduct.Save();

        //Act
        testProduct.DeleteProduct();
        List<Product> expected = new List<Product> {secondProduct};
        List<Product> result = Product.GetAll();

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void UpdateCount_AlterCount_UpdateProduct()
    {
        //Arrange
        Product testProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
        testProduct.Save();

        //Act
        testProduct.UpdateCount(15);
        int expected = 15;
        int result = Product.Find(testProduct.GetId()).GetCount();

        //Assert
        Assert.Equal(expected, result);
    }
    //
    [Fact]
    public void UpdatePrice_AlterPrice_UpdateProduct()
    {
        //Arrange
        Product testProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
        testProduct.Save();

        //Act
        testProduct.UpdatePrice(10.99m);
        decimal expected = 10.99m;
        decimal result = Product.Find(testProduct.GetId()).GetPrice();

        //Assert
        Assert.Equal(expected, result);
    }
    //
    [Fact]
    public void SearchByName_SearchAndReturn_TargetProduct()
    {
        //Arrange
        Product testProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
        testProduct.Save();

        //Act
        List<Product> expected = new List<Product> {testProduct};
        List<Product> result = Product.SearchProductByName("Veg");

        //Assert
        Assert.Equal(expected, result);
    }
  }
}
