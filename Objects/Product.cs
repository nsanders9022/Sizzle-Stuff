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

    }
}
