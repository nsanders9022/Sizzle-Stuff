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

        public string GetDescription()
        {
            return _description;
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

        public static void DeleteAll()
        {
            DB.DeleteAll("products");
        }

    }
}
