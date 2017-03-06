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
    }
}
