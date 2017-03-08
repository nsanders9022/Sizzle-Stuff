// using Xunit;
// using System.Collections.Generic;
// using System;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace OnlineStore.Objects
// {
//     public class PictureTest : IDisposable
//     {
//         public PictureTest()
//         {
//             DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog= online_store_test;Integrated Security=SSPI;";
//         }
//
//         public void Dispose()
//         {
//             Picture.DeleteAll();
//         }
//
//         [Fact]
//         public void Test_EmptyDatabaseAtFirst()
//         {
//             //Arrange,Act
//             int result = Picture.GetAll().Count;
//             //Assert
//             Assert.Equal(0, result);
//         }
//
//         [Fact]
//         public void Picture_IdentityTest_ReturnTrueOnIdenticalObjects()
//         {
//             //Arrange,Act
//             Picture firstPicture = new Picture("location of picture", "picture of a dog");
//             Picture secondPicture = new Picture("location of picture", "picture of a dog");
//             //Assert
//             Assert.Equal(firstPicture, secondPicture);
//         }
//
//         [Fact]
//         public void Save_SinglePicture_PictureSaveToDatabase()
//         {
//             //Arrange
//             Picture firstPicture = new Picture("location of picture", "picture of a dog");
//
//             //Act
//             firstPicture.Save();
//             List<Picture> expected = new List<Picture> {firstPicture};
//             List<Picture> result = Picture.GetAll();
//
//             //Assert
//             Assert.Equal(expected, result);
//         }
//
//         [Fact]
//         public void Find_GetPictureById_ReturnTargetPicture()
//         {
//             //Arrange
//             Picture firstPicture = new Picture("location of picture", "picture of a dog");
//             firstPicture.Save();
//
//             //Act
//             Picture expected = firstPicture;
//             Picture result = Picture.Find(firstPicture.GetId());
//
//             //Assert
//             Assert.Equal(expected, result);
//         }
//
//         [Fact]
//         public void Delete_RemovesAPictureFromDatabase_DecrementDatabase()
//         {
//             //Arrange
//             Picture firstPicture = new Picture("location of picture", "picture of a dog");
//             firstPicture.Save();
//             Picture secondPicture = new Picture("location of picture", "picture of a cat");
//             secondPicture.Save();
//
//             //Act
//             firstPicture.Delete();
//             List<Picture> expected = new List<Picture> {secondPicture};
//             List<Picture> result = Picture.GetAll();
//
//             //Assert
//             Assert.Equal(expected, result);
//         }
//
//         [Fact]
//         public void Update_AlterPictureKeyOrAltText_ChangeDatabaseEntry()
//         {
//             //Arrange
//             Picture firstPicture = new Picture("location of picture", "picture of a dog");
//             firstPicture.Save();
//             Picture secondPicture = new Picture("new location", "picture of a cat");
//             secondPicture.Save();
//
//             //Act
//             firstPicture.Update("new location", "picture of a cat");
//             Picture expected = secondPicture;
//             Picture result = Picture.Find(firstPicture.GetId());
//             result.SetId(secondPicture.GetId());
//
//             //Assert
//             Assert.Equal(expected, result);
//         }
//
//         [Fact]
//         public void ParseFileType_test_test()
//         {
//             //Arrange
//             string newString = "www.things.com";
//             string expectedResult = ".com";
//             string actualResult = Picture.ParseFileType(newString);
//
//             Assert.Equal(expectedResult, actualResult);
//         }
//
//         [Fact]
//         public void UploadPicture_SavePictureToFile_AchieveFile()
//         {
//             //Arrange
//             Picture firstPicture = Picture.UploadPicture("http://i.imgur.com/e0YQGow.jpg", 1, "Izzy", "Part wolf, part teenage girl");
//
//             firstPicture.Delete();
//         }
//     }
// }
