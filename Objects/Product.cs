using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace OnlineStore.Objects
{
    public class Product
    {
        private int _id;
        private string _name;
        private int _count;
        private int _rating;
        private decimal _price;
        private string _description;

        public Product (string newName, int newCount, int newRating, decimal newPrice, string newDescription, int newId = 0)
        {
            _id = newId;
            _name = newName;
            _count = newCount;
            _rating = newRating;
            _price = newPrice;
            _description = newDescription;
        }

        public override bool Equals(System.Object otherProduct)
        {
            if (!(otherProduct is Product))
            {
                return false;
            }
            else
            {
                Product newProduct = (Product) otherProduct;
                bool idIdentity = this.GetId() == newProduct.GetId();
                bool nameIdentity = this.GetName() == newProduct.GetName();
                bool countIdentity = this.GetCount() == newProduct.GetCount();
                bool ratingIdentity = this.GetRating() == newProduct.GetRating();
                bool priceIdentity = this.GetPrice() == newProduct.GetPrice();
                bool descriptionIdentity = this.GetDescription() == newProduct.GetDescription();
                return (idIdentity && nameIdentity && countIdentity && ratingIdentity && priceIdentity && descriptionIdentity);
            }
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int newId)
        {
            _id = newId;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetCount()
        {
            return _count;
        }

        public int GetRating()
        {
            return _rating;
        }

        public decimal GetPrice()
        {
            return _price;
        }

        public void SetPrice(decimal newPrice)
        {
            _price = newPrice;
        }

        public string GetDescription()
        {
            return _description;
        }

        public void SetCount(int newCount)
        {
            _count = newCount;
        }


        public static List<Product> GetAll()
        {

            List<Product> allProducts = new List<Product>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM products;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int productId = rdr.GetInt32(0);
                string productName = rdr.GetString(1);
                int productCount = rdr.GetInt32(2);
                int productRating = rdr.GetInt32(3);
                decimal productPrice = rdr.GetDecimal(4);
                string productDescription = rdr.GetString(5);
                Product newProduct = new Product(productName, productCount, productRating, productPrice, productDescription, productId);
                allProducts.Add(newProduct);
            }

            DB.CloseSqlConnection(conn,rdr);
            return allProducts;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd  = new SqlCommand ("INSERT into products (name, count,rating, price,description) OUTPUT INSERTED.id VALUES(@ProductName,@ProductCount, @ProductRating, @ProductPrice, @ProductDescription);",conn);


            cmd.Parameters.Add(new SqlParameter("@ProductName", this.GetName()));
            cmd.Parameters.Add(new SqlParameter("@ProductCount", this.GetCount()));
            cmd.Parameters.Add(new SqlParameter("@ProductRating", this.GetRating()));
            cmd.Parameters.Add(new SqlParameter("@ProductPrice", this.GetPrice()));
            cmd.Parameters.Add(new SqlParameter("@ProductDescription", this.GetDescription()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this.SetId(rdr.GetInt32(0));
            }
            DB.CloseSqlConnection(conn,rdr);
        }

        public static Product Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM products WHERE id = @ProductId;", conn);
            cmd.Parameters.Add(new SqlParameter("@ProductId", id.ToString()));
            SqlDataReader rdr = cmd.ExecuteReader();

            int productId = 0;
            string productName = null;
            int productCount = 0;
            int productRating = 0;
            decimal productPrice = 0;
            string productDescription = null;

            while(rdr.Read())
            {
                productId = rdr.GetInt32(0);
                productName = rdr.GetString(1);
                productCount = rdr.GetInt32(2);
                productRating = rdr.GetInt32(3);
                productPrice = rdr.GetDecimal(4);
                productDescription = rdr.GetString(5);
            }
            Product foundProduct = new Product(productName, productCount, productRating, productPrice, productDescription, productId);
            DB.CloseSqlConnection(conn, rdr);
            return foundProduct;
        }

        public void DeleteProduct()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM products WHERE id = @ProductId;",conn);
            cmd.Parameters.Add(new SqlParameter ("@ProductId",this.GetId()));

            cmd.ExecuteNonQuery();
            DB.CloseSqlConnection(conn);
        }

        //Change product count
        public void UpdateCount(int newCount)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE products SET count = @NewCount OUTPUT INSERTED.count WHERE id = @ProductId;", conn);
            cmd.Parameters.Add(new SqlParameter("@NewCount", newCount));
            cmd.Parameters.Add(new SqlParameter("@ProductId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this.SetCount(rdr.GetInt32(0));
            }
            DB.CloseSqlConnection(conn, rdr);
        }

        public void UpdatePrice(decimal newPrice)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd  = new SqlCommand("UPDATE products Set price = @NewPrice OUTPUT INSERTED.price WHERE id = @ProductId;", conn);
            cmd.Parameters.Add(new SqlParameter ("@NewPrice",newPrice));
            cmd.Parameters.Add(new SqlParameter("@ProductId",this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                this.SetPrice(rdr.GetDecimal(0));
            }
            DB.CloseSqlConnection(conn, rdr);
        }

        //Search products by name
        public static List<Product> SearchProductByName(string productName)
        {
            List<Product> foundProducts = new List<Product>{};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand ("SELECT * FROM products WHERE name LIKE @ProductName;", conn);
            cmd.Parameters.Add(new SqlParameter("@ProductName", "%" + productName + "%"));
            SqlDataReader rdr = cmd.ExecuteReader();


            while(rdr.Read())
            {
                int searchedProductId = rdr.GetInt32(0);
                string searchedProductName =rdr.GetString(1);
                int productCount = rdr.GetInt32(2);
                int productRating = rdr.GetInt32(3);
                decimal productPrice = rdr.GetDecimal(4);
                string productDescription = rdr.GetString(5);

                Product foundProduct = new Product(searchedProductName, productCount, productRating, productPrice, productDescription, searchedProductId);
                foundProducts.Add(foundProduct);
            }

            DB.CloseSqlConnection(conn, rdr);
            return foundProducts;
        }

        public static List<Product> SearchByRating(int minimumRating)
        {
            List<Product> foundProducts = new List<Product>{};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand ("SELECT * FROM products WHERE rating >= @MinimumRating;", conn);
            cmd.Parameters.Add(new SqlParameter("@MinimumRating", minimumRating));
            SqlDataReader rdr = cmd.ExecuteReader();


            while(rdr.Read())
            {
                int searchedProductId = rdr.GetInt32(0);
                string searchedProductName =rdr.GetString(1);
                int productCount = rdr.GetInt32(2);
                int productRating = rdr.GetInt32(3);
                decimal productPrice = rdr.GetDecimal(4);
                string productDescription = rdr.GetString(5);

                Product foundProduct = new Product(searchedProductName, productCount, productRating, productPrice, productDescription, searchedProductId);
                foundProducts.Add(foundProduct);
            }

            DB.CloseSqlConnection(conn, rdr);
            return foundProducts;
        }

        public void AddCategory (Category newCategory)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO products_categories (product_id, category_id) VALUES (@ProductId, @CategoryId);", conn);

            cmd.Parameters.Add(new SqlParameter("@ProductId", this.GetId()));
            cmd.Parameters.Add(new SqlParameter("@CategoryId", newCategory.GetId()));

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public List<Category> GetCategories()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT categories.* FROM products JOIN products_categories ON (products.id = products_categories.product_id) JOIN categories ON (products_categories.category_id = categories.id) WHERE products.id = @ProductId;", conn);

            cmd.Parameters.Add(new SqlParameter("@ProductId", this.GetId().ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Category> categories = new List<Category>{};

            while(rdr.Read())
            {
                int categoryId = rdr.GetInt32(0);
                string categoryName = rdr.GetString(1);

                Category newCategory = new Category(categoryName, categoryId);
                categories.Add(newCategory);
            }

            if (conn != null)
            {
                conn.Close();
            }

            return categories;
        }

        public void RemoveCategory(Category newCategory)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM products_categories WHERE product_id = @ProductID AND category_id = @CategoryId;", conn);

            cmd.Parameters.Add(new SqlParameter("@ProductId", this.GetId().ToString()));
            cmd.Parameters.Add(new SqlParameter("@CategoryId", newCategory.GetId().ToString()));

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }
        }

        public List<Review> GetProductReview()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("SELECT * FROM reviews where product_id = @ProductId;", conn);

          cmd.Parameters.Add(new SqlParameter("@ProductId", this.GetId().ToString()));

          List<Review> reviews = new List<Review>{};

          SqlDataReader rdr = cmd.ExecuteReader();

          while(rdr.Read())
          {
            int reviewId = rdr.GetInt32(0);
            int userId = rdr.GetInt32(1);
            int productId = rdr.GetInt32(2);
            int rating = rdr.GetInt32(3);
            string reviewText = rdr.GetString(4);

            Review newReview = new Review(userId, productId, rating, reviewText, reviewId);
            reviews.Add(newReview);
          }

          DB.CloseSqlConnection(conn, rdr);

          return reviews;
        }

        public void AddPicture(Picture newPicture)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO pictures_products (picture_id, product_id) VALUES (@NewPictureId, @NewProductId);", conn);
            cmd.Parameters.Add(new SqlParameter("@NewPictureId", newPicture.GetId()));
            cmd.Parameters.Add(new SqlParameter("@NewProductId", this.GetId()));

            cmd.ExecuteNonQuery();

            DB.CloseSqlConnection(conn);
        }

        public List<Picture> GetPictures()
        {
            List<Picture> foundPictures = new List<Picture> {};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT pictures.* FROM products JOIN pictures_products ON (products.id = pictures_products.product_id) JOIN pictures ON (pictures.id = pictures_products.picture_id) WHERE products.id = @TargetId;", conn);
            cmd.Parameters.Add(new SqlParameter("@TargetId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int pictureId = rdr.GetInt32(0);
                string pictureKey = rdr.GetString(1);
                string pictureAltText = rdr.GetString(2);
                Picture newPicture = new Picture(pictureKey, pictureAltText, pictureId);
                foundPictures.Add(newPicture);
            }

            DB.CloseSqlConnection(conn, rdr);
            return foundPictures;
        }

        // public void AddReview(int userId, int rating, string reviewText)
        // {
        //   SqlConnection conn = DB.Connection();
        //   conn.Open();
        //
        //   SqlCommand cmd = new SqlCommand("INSERT INTO reviews (user_id, product_id, rating, review_text) OUTPUT INSERTED.id VALUES(@userId, @productId, @rating, @reviewText);", conn);
        //   cmd.Parameters.Add(new SqlParameter("@userId", userId.ToString());
        //   cmd.Parameters.Add(new SqlParameter("@productId", this.GetId().ToString());
        //   cmd.Parameters.Add(new SqlParameter("@rating", rating.ToString());
        //   cmd.Parameters.Add(new SqlParameter("@reviewText", reviewText);
        //
        //   SqlDataReader rdr = cmd.ExecuteReader();
        //
        //   while(rdr.Read())
        //   {
        //     this.SetId(rdr.GetInt32(0));
        //   }
        // }

        public static void DeleteAll()
        {
            DB.DeleteAll("products");
        }

    }
}
