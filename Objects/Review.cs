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
        private int _productId;
        private int _rating;
        private string _reviewText;

        public Review(int newUserId, int newProductId, int newRating, string newReviewText, int newId = 0)
        {
            _id = newId;
            _userId = newUserId;
            _productId = newProductId;
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

        public int GetProductId()
        {
            return _productId;
        }

        public int GetRating()
        {
            return _rating;
        }

        public string GetReviewText()
        {
            return _reviewText;
        }

        public static List<Review> GetAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM reviews;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Review> allReviews = new List<Review>{};

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);
                int productId = rdr.GetInt32(2);
                int rating = rdr.GetInt32(3);
                string reviewText = rdr.GetString(4);

                Review newReview = new Review(userId, productId, rating, reviewText, id);
                allReviews.Add(newReview);
            }

            DB.CloseSqlConnection(conn, rdr);
            return allReviews;
        }

        public override bool Equals(System.Object otherReview)
        {
            if(!(otherReview is Review))
            {
                return false;
            }
            else
            {
                Review newReview = (Review) otherReview;
                bool idEquality = (this.GetId() == newReview.GetId());
                bool userIdEquality = (this.GetUserId() == newReview.GetUserId());
                bool productIdEquality = (this.GetProductId() == newReview.GetProductId());
                bool ratingEquality = (this.GetRating() == newReview.GetRating());
                bool reviewTextEquality = (this.GetReviewText() == newReview.GetReviewText());
                return (idEquality && userIdEquality && productIdEquality && ratingEquality && reviewTextEquality);
            }
        }

        //Saves instances to database
        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO reviews (user_id, product_id, rating, review_text) OUTPUT INSERTED.id VALUES (@UserId, @ProductId, @Rating, @ReviewText);", conn);
            cmd.Parameters.Add(new SqlParameter("@UserId", this.GetUserId()));
            cmd.Parameters.Add(new SqlParameter("@ProductId", this.GetProductId()));
            cmd.Parameters.Add(new SqlParameter("@Rating", this.GetRating()));
            cmd.Parameters.Add(new SqlParameter("@ReviewText", this.GetReviewText()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        //Finds instance in database
        public static Review Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE id = @ReviewId;", conn);
            cmd.Parameters.Add(new SqlParameter("@ReviewId", id.ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundid = 0;
            int foundUserId = 0;
            int foundProductId = 0;
            int foundRating = 0;
            string foundReviewText = null;

            while(rdr.Read())
            {
                foundid = rdr.GetInt32(0);
                foundUserId = rdr.GetInt32(1);
                foundProductId = rdr.GetInt32(2);
                foundRating = rdr.GetInt32(3);
                foundReviewText = rdr.GetString(4);
                       }
            Review newReview = new Review(foundUserId, foundProductId, foundRating, foundReviewText, foundid);

            DB.CloseSqlConnection(conn, rdr);
            return newReview;
        }
    }
}
