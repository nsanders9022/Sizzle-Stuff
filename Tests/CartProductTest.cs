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

        //GetAll returns empty list if no cartproducts
        [Fact]
        public void GetAll_ForNoCartProducts_EmptyList()
        {
            //Arrange, Act, Assert
            List<CartProduct> actualResult = CartProduct.GetAll();
            List<CartProduct> expectedResult = new List<CartProduct>{};

            Assert.Equal(expectedResult, actualResult);
        }

        //clears all rows in table
        public void Dispose()
        {
            CartProduct.DeleteAll();
        }
    }
}
