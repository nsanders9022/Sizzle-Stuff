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
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd  = new SqlCommand("INSERT INTO categories (name) OUTPUT inserted.id VALUES (@CategoryName);", conn);
            cmd.Parameters.Add(new SqlParameter ("@CategoryName",this.GetName()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this.SetId(rdr.GetInt32(0));
            }
            DB.CloseSqlConnection(conn,rdr);
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

        public static void DeleteAll()
        {
            DB.DeleteAll("categories");

        }

    }
}
