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

        [Fact]
        public void Category_EqualityTest_ReturnTrueOnIdenticalObjects()
        {
            //Arrange,Act
            Category firstCategory = new Category("Dinnerware");
            Category secondCategory = new Category("Dinnerware");
            //Assert
            Assert.Equal(firstCategory, secondCategory);
        }

        [Fact]
        public void Save_SaveCategory_CategorySavesToDatabase()
        {
            //Arrange
            Category testCategory = new Category("Dinnerware");

            //Act
            testCategory.Save();
            List<Category> testCategoryList = Category.GetAll();
            List<Category> result = new List<Category> {testCategory};

            //Assert
            Assert.Equal(testCategoryList, result);
        }




        public void Dispose()
        {
            Category.DeleteAll();
        }
    }
}
