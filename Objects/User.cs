using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace OnlineStore.Objects
{
    public class User
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _password;
        private string _username;
        private bool _adminPrivileges;

        public User (string firstName, string lastName, string userName, string password, bool adminPrivileges, it id = 0)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _password = password;
            _username = username;
            _adminPrivileges = adminPrivileges;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetFirstName()
        {
            return _firstName;
        }

        public string GetLastName()
        {
            return _lastName;
        }

        public string GetUsername()
        {
            return _username;
        }

        public string GetPassword()
        {
            return _password;
        }

        public bool GetAdminPrivileges()
        {
            return _adminPrivileges;
        }


    }
}
