using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace OnlineStore.Objects
{
    public class Review
    {
        private int _id;
        private int _userId;
        private int _reviewId;
        private int _rating;
        private string _reviewText;

        public Review(int newUserId, int newReviewId, int newRating, string newReviewText, int newId = 0)
        {
            _id = newId;
            _userId = newUserId;
            _reviewId = newReviewId;
            _rating = newRating;
            _reviewText = newReviewText;
        }

        public int GetId()
        {
            return _id;
        }

        public int GetUserId()
        {
            return _userId;
        }

        public int GetReviewId()
        {
            return _reviewId;
        }

        public int GetRating()
        {
            return _rating;
        }

        public string GetReviewText()
        {
            return _reviewText;
        }
    }
}
