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
    }
}
