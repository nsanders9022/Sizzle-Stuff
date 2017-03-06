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

        public int GetId()
        {
            return _id;
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
                int productPrice = rdr.GetInt32(4);
                string productDescription = rdr.GetString(5);
                Product newProduct = new Product(productName, productCount, productRating, productPrice, productDescription, productId);
                allProducts.Add(newProduct);
            }

            DB.CloseSqlConnection(conn,rdr);
            return allProducts;
        }

        public static void DeleteAll()
        {
            DB.DeleteAll("products");
        }

    }
}
