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

        //Checks that user table is empty at first
        [Fact]
        public void Test_ForNoRowsInUserTable()
        {
            int actualResult = User.GetAll().Count;
            int expectedResult = 0;

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks if EqualOverride is working
        [Fact]
        public void EqualOverride_UsersAreSame_true()
        {
            //Arrange, Act
            User firstUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            User secondUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);

            Assert.Equal(firstUser, secondUser);
        }

    }
}
