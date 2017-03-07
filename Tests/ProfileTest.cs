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

        //Checks that profile table is empty at first
        [Fact]
        public void Test_ForNoRowsInProfileTable()
        {
            int actualResult = Profile.GetAll().Count;
            int expectedResult = 0;

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks if EqualOverride is working
        [Fact]
        public void EqualOverride_ProfilesAreSame_true()
        {
            //Arrange, Act
            Profile firstProfile = new Profile(1, "123 First Street", "Seattle", "WA", 98006, "2062062062");
            Profile secondProfile = new Profile(1, "123 First Street", "Seattle", "WA", 98006, "2062062062");

            Assert.Equal(firstProfile, secondProfile);
        }

        //Checks if instances are saved to database
        [Fact]
        public void Save_ForProfile_SavesToDatabase()
        {
            //Arrange
            Profile newProfile = new Profile(1, "123 First Street", "Seattle", "WA", 98006, "2062062062");

            //Act
            newProfile.Save();

            //Assert
            List<Profile> actualResult = Profile.GetAll();
            List<Profile> expectedResult = new List<Profile>{newProfile};

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks that GetAll method works for multiple instances
        [Fact]
        public void GetAll_ForMultipleProfiles_ReturnsListWithAllProfiles()
        {
            //Arrange
            Profile firstProfile = new Profile(1, "123 First Street", "Seattle", "WA", 98006, "206-206-2062");
            Profile secondProfile = new Profile(2, "1001 Second Avenue", "Portland", "OR", 97006, "503-503-5035");
            firstProfile.Save();
            secondProfile.Save();

            //Act, Assert
            List<Profile> actualResult = Profile.GetAll();
            List<Profile> expectedResult = new List<Profile> {firstProfile, secondProfile};

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void DeleteProfile_DeletsProfileFromDB_removeRow()
        {
            //Arrange
            Profile firstProfile = new Profile(1, "123 First Street", "Seattle", "WA", 98006, "206-206-2062");
            Profile secondProfile = new Profile(2, "1001 Second Avenue", "Portland", "OR", 97006, "503-503-5035");
            firstProfile.Save();
            secondProfile.Save();

            //Act
            firstProfile.Delete();

            List<Profile> actualResult = Profile.GetAll();
            List<Profile> expectedResult = new List<Profile>{secondProfile};

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        //Checks that Find method finds correct profile in database
        [Fact]
        public void Find_ForProfile_FindsProfileInDatabase()
        {
            //Arrange
            Profile testProfile = new Profile(1, "123 First Street", "Seattle", "WA", 98006, "206-206-2062");
            testProfile.Save();

            //Act, Assert
            Profile foundProfile = Profile.Find(testProfile.GetId());
            Assert.Equal(testProfile, foundProfile);
        }

        //Checks that update method changes firstName
        [Fact]
        public void UpdateProfile_ForUserProfile_ChangesProfile()
        {
            //Arrange
            Profile testProfile = new Profile(1, "123 First Street", "Seattle", "WA", 98006, "206-206-2062");
            testProfile.Save();
            string newStreet = "111 Main Street";
            string newCity = "Vancouver";

            //Act
            testProfile.UpdateProfile(newStreet, newCity, null, 0, null);


            //Assert
            Assert.Equal(newStreet, testProfile.GetStreet());
            Assert.Equal(newCity, testProfile.GetCity());
        }
    }
}
