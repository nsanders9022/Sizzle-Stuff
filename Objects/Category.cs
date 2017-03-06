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







        public static void DeleteAll()
        {
            DB.DeleteAll("categories");
        }

    }
}
