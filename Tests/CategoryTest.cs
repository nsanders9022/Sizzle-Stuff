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

        [Fact]
        public void Delete_DeleteCategory_RemoveCategoryFromDatabase()
        {
            //Arrange
            Category testCategory1 = new Category("Dinnerware");
            testCategory1.Save();
            Category testCategory2 = new Category("Cookware");
            testCategory2.Save();
            //Act
            testCategory1.DeleteCategory();
            List<Category> expected = new List<Category>{testCategory2};
            List<Category> result = Category.GetAll();
            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_UpdateCategory_UpdateCategoryInDatabase()
        {
             //Arrange
            Category testCategory = new Category("Serveware");
            testCategory.Save();
            //Act
            testCategory.Update("Cookware");
            Category expected = new Category("Cookware",testCategory.GetId());
            //Assert
            Assert.Equal(expected, testCategory);
        }

        [Fact]
        public void Search_ByName_ListOfSimilarCategories()
        {
             //Arrange
            Category testCategory = new Category("Serveware");
            testCategory.Save();
            Category secondCategory = new Category("Serverfare");
            secondCategory.Save();
            Category thirdCategory = new Category("Blue things");
            thirdCategory.Save();
            //Act
            List<Category> expected = new List<Category>{testCategory, secondCategory};
            List<Category> result = Category.SearchCategoryByName("erve");
            //Assert
            Assert.Equal(expected, result);
        }

        public void Dispose()
        {
            Category.DeleteAll();
        }
    }
}
