using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OnlineStore.Objects
{
    public class CartProductTest : IDisposable
    {
        public CartProductTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog= online_store_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void EqualOverride_CartProductsAreSame_true()
        {
            //Arrange, Act
            CartProduct firstCartProduct = new CartProduct(2,2,5);
            CartProduct secondCartProduct= new CartProduct(2,2,5);

            Assert.Equal(firstCartProduct, secondCartProduct);
        }

        //GetAll returns empty list if no cartproducts
        [Fact]
        public void GetAll_ForNoCartProducts_EmptyList()
        {
            //Arrange, Act, Assert
            List<CartProduct> actualResult = CartProduct.GetAll();
            List<CartProduct> expectedResult = new List<CartProduct>{};

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks if instances are saved to database
        [Fact]
        public void Save_ForCartProduct_SavesToDatabase()
        {
            //Arrange
            CartProduct newCartProduct = new CartProduct(2,2,5);

            //Act
            newCartProduct.Save();

            //Assert
            List<CartProduct> actualResult = CartProduct.GetAll();
            List<CartProduct> expectedResult = new List<CartProduct>{newCartProduct};

            Assert.Equal(expectedResult, actualResult);
        }

        //clears all rows in table
        public void Dispose()
        {
            CartProduct.DeleteAll();
        }
    }
}
