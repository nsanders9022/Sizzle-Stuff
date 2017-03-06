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
        public string GetName()
        {
            return _name;
        }
    }
}
