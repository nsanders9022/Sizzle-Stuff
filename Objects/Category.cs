using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace OnlineStore.Objects
{
    public class Category
    {
        private int _id;
        private string _name;

        public Category(string newName, int newId = 0)
        {
            _id = newId;
            _name = newName;
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
        public void SetName(string newName)
        {
            _name = newName;
        }


        public override bool Equals(System.Object otherCategory)
        {
            if (!(otherCategory is Category))
            {
                return false;
            }
            else
            {
                Category newCategory = (Category) otherCategory;
                bool idEquality = (this.GetId() == newCategory.GetId());
                bool nameEquality = (this.GetName() == newCategory.GetName());
                return (idEquality && nameEquality);
            }
        }

        public  static List<Category>  GetAll()
        {
            List<Category> allCategories = new List<Category>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand ("SELECT * FROM categories;",conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int categoryId = rdr.GetInt32(0);
                string categoryName = rdr.GetString(1);
                Category newCategory = new Category(categoryName, categoryId);
                allCategories.Add(newCategory);
            }
            DB.CloseSqlConnection(conn,rdr);
            return allCategories;
        }

        public void Save()
        {
            int potentialId = this.IsNewEntry();
            if (potentialId == -1)
            {
                SqlConnection conn = DB.Connection();
                conn.Open();

                SqlCommand cmd  = new SqlCommand("INSERT INTO categories (name) OUTPUT inserted.id VALUES (@CategoryName);", conn);
                cmd.Parameters.Add(new SqlParameter ("@CategoryName",this.GetName()));

                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    potentialId = rdr.GetInt32(0);
                }
                DB.CloseSqlConnection(conn,rdr);
            }
            this.SetId(potentialId);
        }

        public int IsNewEntry()
        {
            // This function checks to see if the object instance already exists in the database, returning the DB id if it already exists and -1 if it does not
            int potentialId = -1;

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT id FROM categories WHERE name = @TargetName", conn);
            cmd.Parameters.Add(new SqlParameter("@TargetName", this.GetName()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                potentialId = rdr.GetInt32(0);
            }
            DB.CloseSqlConnection(conn, rdr);

            return potentialId;
        }

        public static Category Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd  = new SqlCommand ("SELECT * FROM categories WHERE id = @CategoryId;",conn);
            cmd.Parameters.Add(new SqlParameter("@CategoryId", id.ToString()));
            SqlDataReader rdr = cmd.ExecuteReader();

            int categoryId = 0;
            string categoryName = null;
            while(rdr.Read())
            {
                categoryId = rdr.GetInt32(0);
                categoryName = rdr.GetString(1);
            }
            Category foundCategory = new Category (categoryName,categoryId);
            DB.CloseSqlConnection(conn,rdr);
            return foundCategory;
        }

        public void DeleteCategory()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id = @CategoryId;",conn);
            cmd.Parameters.Add(new SqlParameter ("@CategoryId",this.GetId()));

            cmd.ExecuteNonQuery();
            DB.CloseSqlConnection(conn);
        }

        public void Update(string newName)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE categories Set name = @NewName OUTPUT INSERTED.name WHERE id = @CategoryId;",conn);

            cmd.Parameters.Add(new SqlParameter("@NewName",newName));
            cmd.Parameters.Add(new SqlParameter("@CategoryId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._name = rdr.GetString(0);
            }
            DB.CloseSqlConnection(conn, rdr);
        }

        //Search products by name
        public static List<Category> SearchCategoryByName(string categoryName)
        {
            List<Category> foundCategories = new List<Category>{};
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand ("SELECT * FROM categories WHERE name LIKE @CategoryName;", conn);
            cmd.Parameters.Add(new SqlParameter("@CategoryName", "%" + categoryName + "%"));
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int searchedCategoryId = rdr.GetInt32(0);
                string searchedCategoryName =rdr.GetString(1);
                Category foundCategory = new Category(searchedCategoryName, searchedCategoryId);
                foundCategories.Add(foundCategory);
            }

            DB.CloseSqlConnection(conn, rdr);
            return foundCategories;
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand ("SELECT products.* FROM categories JOIN products_categories ON (categories.id = products_categories.category_id) JOIN products ON (products_categories.product_id = products.id) WHERE categories.id = @CategoryId;",conn);


            cmd.Parameters.Add(new SqlParameter ("@CategoryId",this.GetId().ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Product> allProducts = new List<Product>{};

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

        public static void DeleteAll()
        {
            DB.DeleteAll("categories");

        }

    }
}
