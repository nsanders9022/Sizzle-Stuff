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
        private int _zipCode;
        private string _phoneNumber;

        public Profile(int newCustomerId, string newStreet, string newCity, string newState, int newZipCode, string newPhoneNumber, int newId = 0)
        {
            _id = newId;
            _customerId = newCustomerId;
            _street = newStreet;
            _city = newCity;
            _state = newState;
            _zipCode = newZipCode;
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

        public int GetZipCode()
        {
            return _zipCode;
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
                int zipCode = rdr.GetInt32(5);
                string phoneNumber = rdr.GetString(6);

                Profile newProfile = new Profile(customerId, street, city, state, zipCode, phoneNumber, id);
                allProfiles.Add(newProfile);
            }

            DB.CloseSqlConnection(conn, rdr);
            return allProfiles;
        }

        public override bool Equals(System.Object otherProfile)
        {
            if(!(otherProfile is Profile))
            {
                return false;
            }
            else
            {
                Profile newProfile = (Profile) otherProfile;
                bool idEquality = (this.GetId() == newProfile.GetId());
                bool customerIdEqulity = (this.GetCustomerId() == newProfile.GetCustomerId());
                bool streetEquality = (this.GetStreet() == newProfile.GetStreet());
                bool cityEquality = (this.GetCity() == newProfile.GetCity());
                bool stateEquality = (this.GetState() == newProfile.GetState());
                bool zipCodeEquality = (this.GetZipCode() == newProfile.GetZipCode());
                bool phoneNumberEquality = (this.GetPhoneNumber() == newProfile.GetPhoneNumber());
                return (idEquality && customerIdEqulity && streetEquality && cityEquality && stateEquality && zipCodeEquality && phoneNumberEquality);
            }
        }
    }
}
