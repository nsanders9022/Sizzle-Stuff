using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace OnlineStore.Objects
{
    public class ProfileTest: IDisposable
    {
        public ProfileTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=online_store_test;Integrated Security=SSPI;";
        }

        //GetAll returns empty list if no profiles
        [Fact]
        public void GetAll_ForNoProfiles_EmptyList()
        {
            //Arrange, Act, Assert
            List<Profile> actualResult = Profile.GetAll();
            List<Profile> expectedResult = new List<Profile>{};

            Assert.Equal(expectedResult, actualResult);
        }

        public void Dispose()
        {
            DB.DeleteAll("profiles");
        }

        //Checks that user table is empty at first
        [Fact]
        public void Test_ForNoRowsInProfileTable()
        {
            int actualResult = Profile.GetAll().Count;
            int expectedResult = 0;

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
