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
        // [Fact]
        // public void EqualOverride_ReviewsAreSame_true()
        // {
        //     //Arrange, Act
        //     Review firstReview = new Review("Allie", "Holcombe", "eylookturkeys", "password", false);
        //     Review secondReview = new Review("Allie", "Holcombe", "eylookturkeys", "password", false);
        //
        //     Assert.Equal(firstReview, secondReview);
        // }

    }
}
