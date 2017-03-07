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

        public void SetStreet(string newStreet)
        {
            _street = newStreet;
        }

        public void SetCity(string newCity)
        {
            _city = newCity;
        }

        public void SetState(string newState)
        {
            _state = newState;
        }

        public void SetZipCode(int newZipCode)
        {
            _zipCode = newZipCode;
        }

        public void SetPhoneNumber(string newPhoneNumber)
        {
            _phoneNumber = newPhoneNumber;
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

        //Saves instances to database
        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO profiles (customer_id, street, city, state, zip_code, phone_number) OUTPUT INSERTED.id VALUES (@CustomerId, @Street, @City, @State, @ZipCode, @PhoneNumber);", conn);
            cmd.Parameters.Add(new SqlParameter("@CustomerId", this.GetCustomerId()));
            cmd.Parameters.Add(new SqlParameter("@Street", this.GetStreet()));
            cmd.Parameters.Add(new SqlParameter("@City", this.GetCity()));
            cmd.Parameters.Add(new SqlParameter("@State", this.GetState()));
            cmd.Parameters.Add(new SqlParameter("@ZipCode", this.GetZipCode()));
            cmd.Parameters.Add(new SqlParameter("@PhoneNumber", this.GetPhoneNumber()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static void DeleteAll()
        {
            DB.DeleteAll("profiles");
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM profiles WHERE id = @ProfileId;", conn);
            cmd.Parameters.Add(new SqlParameter("@ProfileId", this.GetId()));
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        //Finds instance in database
        public static Profile Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM profiles WHERE id = @ProfileId;", conn);
            cmd.Parameters.Add(new SqlParameter("@ProfileId", id.ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundId = 0;
            int foundCustomerId = 0;
            string foundStreet = null;
            string foundCity = null;
            string foundState = null;
            int foundZipCode = 0;
            string foundPhoneNumber = null;

            while(rdr.Read())
            {
                foundId = rdr.GetInt32(0);
                foundCustomerId = rdr.GetInt32(1);
                foundStreet = rdr.GetString(2);
                foundCity = rdr.GetString(3);
                foundState = rdr.GetString(4);
                foundZipCode = rdr.GetInt32(5);
                foundPhoneNumber = rdr.GetString(6);
            }
            Profile newProfile = new Profile(foundCustomerId, foundStreet, foundCity, foundState, foundZipCode, foundPhoneNumber, foundId);

            DB.CloseSqlConnection(conn, rdr);
            return newProfile;
        }

        //Change name of user
        public void UpdateProfile(string newStreet = null, string newCity = null, string newState = null, int newZipCode = 0, string newPhoneNumber = null)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            //new command to change any changed fields
            SqlCommand cmd = new SqlCommand("UPDATE profiles SET street = @newStreet, city = @newCity, state = @newState, zip_code = @newZipCode, phone_number = @newPhoneNumber OUTPUT INSERTED.street, INSERTED.city, INSERTED.state, INSERTED.zip_code, INSERTED.phone_number WHERE id = @ProfileId;", conn);

            //Get id of user to use in command

            cmd.Parameters.Add(new SqlParameter("@ProfileId", this.GetId()));

            //CHANGE STREET
            //If there is a new street, change it
            if (!String.IsNullOrEmpty(newStreet))
            {
                cmd.Parameters.Add(new SqlParameter("@newStreet", newStreet));
            }
            //if there isn't a new street, don't change the name
            else
            {
                cmd.Parameters.Add(new SqlParameter("@newStreet", this.GetStreet()));
            }

            //CHANGE CITY
            //If there is a new city, change it
            if (!String.IsNullOrEmpty(newCity))
            {
                cmd.Parameters.Add(new SqlParameter("@newCity", newCity));
            }
            //if there isn't a new city, don't change the name
            else
            {
                cmd.Parameters.Add(new SqlParameter("@newCity", this.GetCity()));
            }

            //CHANGE STATE
            //If there is a new state, change it
            if (!String.IsNullOrEmpty(newState))
            {
                cmd.Parameters.Add(new SqlParameter("@newState", newState));
            }
            //if there isn't a new state, don't change the name
            else
            {
                cmd.Parameters.Add(new SqlParameter("@newState", this.GetState()));
            }

            //CHANGE ZIP CODE
            //If there is a new zip code, change it
            if (!String.IsNullOrEmpty(newZipCode.ToString()))
            {
                cmd.Parameters.Add(new SqlParameter("@newZipCode", newZipCode.ToString()));
            }
            //if there isn't a new state, don't change the name
            else
            {
                cmd.Parameters.Add(new SqlParameter("@newZipCode", this.GetZipCode()));
            }

            //CHANGE PHONE NUMBER
            //If there is a new phone number, change it
            if (!String.IsNullOrEmpty(newPhoneNumber))
            {
                cmd.Parameters.Add(new SqlParameter("@newPhoneNumber", newPhoneNumber));
            }
            //if there isn't a new state, don't change the name
            else
            {
                cmd.Parameters.Add(new SqlParameter("@newPhoneNumber", this.GetPhoneNumber()));
            }

            //execute reader
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                this.SetStreet(rdr.GetString(0));
                this.SetCity(rdr.GetString(1));
                this.SetState(rdr.GetString(2));
                this.SetZipCode(rdr.GetInt32(3));
                this.SetPhoneNumber(rdr.GetString(4));
            }

            DB.CloseSqlConnection(conn, rdr);
        }

    }
}
