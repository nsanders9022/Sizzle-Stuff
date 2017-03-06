using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace OnlineStore.Objects
{
    public class UserTest: IDisposable
    {
        public UserTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=online_store_test;Integrated Security=SSPI;";
        }

        //GetAll returns empty list if no users
        [Fact]
        public void GetAll_ForNoUsers_EmptyList()
        {
            //Arrange, Act, Assert
            List<User> actualResult = User.GetAll();
            List<User> expectedResult = new List<User>{};

            Assert.Equal(expectedResult, actualResult);
        }

        public void Dispose()
        {
            DB.DeleteAll("users");
        }

    }
}
