using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace OnlineStore.Objects
{
    public class Profile
    {
        private int _id;
        private int _customerId;
        private string _street;
        private string _city;
        private string _state;
        private string _phoneNumber;

        public Profile(int newCustomerId, string newStreet, string newCity, string newState, string newPhoneNumber, int newId = 0)
        {
            _id = newId;
            _customerId = newCustomerId;
            _street = newStreet;
            _city = newCity;
            _state = newState;
            _phoneNumber = newPhoneNumber;
        }

        public int GetId()
        {
            return _id;
        }

        public int GetCustomerId()
        {
            return _customerId;
        }

        public string GetStreet()
        {
            return _street;
        }

        public string GetCity()
        {
            return _city;
        }

        public string GetState()
        {
            return _state;
        }

        public string GetPhoneNumber()
        {
            return _phoneNumber;
        }

        public static List<Profile> GetAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM profiles;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Profile> allProfiles = new List<Profile>{};

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                int customerId = rdr.GetInt32(1);
                string street = rdr.GetString(2);
                string city = rdr.GetString(3);
                string state = rdr.GetString(4);
                string phoneNumber = rdr.GetString(5);

                Profile newProfile = new Profile(customerId, street, city, state, phoneNumber, id);
                allProfiles.Add(newProfile);
            }

            DB.CloseSqlConnection(conn, rdr);
            return allProfiles;
        }
    }
}
