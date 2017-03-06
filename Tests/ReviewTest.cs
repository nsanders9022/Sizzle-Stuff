using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace OnlineStore.Objects
{
    public class ReviewTest: IDisposable
    {
        public ReviewTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=online_store_test;Integrated Security=SSPI;";
        }

        //GetAll returns empty list if no reviews
        [Fact]
        public void GetAll_ForNoReviews_EmptyList()
        {
            //Arrange, Act, Assert
            List<Review> actualResult = Review.GetAll();
            List<Review> expectedResult = new List<Review>{};

            Assert.Equal(expectedResult, actualResult);
        }

        public void Dispose()
        {
            DB.DeleteAll("reviews");
        }

        //Checks that reviews table is empty at first
        [Fact]
        public void Test_ForNoRowsInReviewTable()
        {
            int actualResult = Review.GetAll().Count;
            int expectedResult = 0;

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks if EqualOverride is working
        [Fact]
        public void EqualOverride_ReviewsAreSame_true()
        {
            //Arrange, Act
            Review firstReview = new Review(1, 500, 5, "The toaster successfully imprinted a picture of my brother but the toast was extremely burnt");
            Review secondReview = new Review(1, 500, 5, "The toaster successfully imprinted a picture of my brother but the toast was extremely burnt");

            Assert.Equal(firstReview, secondReview);
        }

        //Checks if instances are saved to database
        [Fact]
        public void Save_ForReview_SavesToDatabase()
        {
            //Arrange
            Review newReview = new Review(1, 500, 5, "The toaster successfully imprinted a picture of my brother but the toast was extremely burnt");

            //Act
            newReview.Save();

            //Assert
            List<Review> actualResult = Review.GetAll();
            List<Review> expectedResult = new List<Review>{newReview};

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks that GetAll method works for multiple instances
        [Fact]
        public void GetAll_ForMultipleReviews_ReturnsListWithAllReviews()
        {
            //Arrange
            Review firstReview = new Review(1, 500, 5, "The toaster successfully imprinted a picture of my brother but the toast was extremely burnt");
            Review secondReview = new Review(2, 50, 8, "Awesome");
            firstReview.Save();
            secondReview.Save();

            //Act, Assert
            List<Review> actualResult = Review.GetAll();
            List<Review> expectedResult = new List<Review> {firstReview, secondReview};

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks that Find method finds correct user in database
        [Fact]
        public void Find_ForReview_FindsReviewInDatabase()
        {
            //Arrange
            Review testReview = new Review(1, 500, 5, "The toaster successfully imprinted a picture of my brother but the toast was extremely burnt");
            testReview.Save();

            //Act, AReviewt
            Review foundReview = Review.Find(testReview.GetId());
            Assert.Equal(testReview, foundReview);
        }

    }
}
