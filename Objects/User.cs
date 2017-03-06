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

        public User (string firstName, string lastName, string username, string password, bool adminPrivileges, int id = 0)
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

        public void SetFirstName(string newFirstName)
        {
            _firstName = newFirstName;
        }

        public void SetLastName(string newLastName)
        {
            _lastName = newLastName;
        }

        public static List<User> GetAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM users;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<User> allUsers = new List<User>{};

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string firstName = rdr.GetString(1);
                string lastName = rdr.GetString(2);
                string username = rdr.GetString(3);
                string password = rdr.GetString(4);
                bool adminPrivileges = rdr.GetBoolean(5);

                User newUser = new User(firstName, lastName, username, password, adminPrivileges, id);
                allUsers.Add(newUser);
            }

            DB.CloseSqlConnection(conn, rdr);
            return allUsers;
        }

        public override bool Equals(System.Object otherUser)
        {
            if(!(otherUser is User))
            {
                return false;
            }
            else
            {
                User newUser = (User) otherUser;
                bool idEquality = (this.GetId() == newUser.GetId());
                bool firstNameEquality = (this.GetFirstName() == newUser.GetFirstName());
                bool lastNameEquality = (this.GetLastName() == newUser.GetLastName());
                bool usernameEquality = (this.GetUsername() == newUser.GetUsername());
                bool passwordEquality = (this.GetPassword() == newUser.GetPassword());
                bool adminPrivilegesEquality = (this.GetAdminPrivileges() == newUser.GetAdminPrivileges());
                return (idEquality && firstNameEquality && lastNameEquality && usernameEquality && passwordEquality && adminPrivilegesEquality);
            }
        }

        //Saves instances to database
        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO users (first_name, last_name, username, password, admin_privileges) OUTPUT INSERTED.id VALUES (@FirstName, @LastName, @Username, @Password, @AdminPrivileges);", conn);
            cmd.Parameters.Add(new SqlParameter("@FirstName", this.GetFirstName()));
            cmd.Parameters.Add(new SqlParameter("@LastName", this.GetLastName()));
            cmd.Parameters.Add(new SqlParameter("@Username", this.GetUsername()));
            cmd.Parameters.Add(new SqlParameter("@Password", this.GetPassword()));
            cmd.Parameters.Add(new SqlParameter("@AdminPrivileges", this.GetAdminPrivileges()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        //Finds instance in database
        public static User Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE id = @UserId;", conn);
            cmd.Parameters.Add(new SqlParameter("@UserId", id.ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundid = 0;
            string foundfirstName = null;
            string foundlastName = null;
            string foundusername = null;
            string foundpassword = null;
            bool foundadminPrivileges = false;

            while(rdr.Read())
            {
                foundid = rdr.GetInt32(0);
                foundfirstName = rdr.GetString(1);
                foundlastName = rdr.GetString(2);
                foundusername = rdr.GetString(3);
                foundpassword = rdr.GetString(4);
                foundadminPrivileges = rdr.GetBoolean(5);
            }
            User newUser = new User(foundfirstName, foundlastName, foundusername, foundpassword, foundadminPrivileges, foundid);

            DB.CloseSqlConnection(conn, rdr);
            return newUser;
        }

        //Change name of user
        public void UpdateName(string newFirstName = null, string newLastName = null)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            //new command to change any changed fields
            SqlCommand cmd = new SqlCommand("UPDATE users SET first_name = @newFirstName, last_Name = @newLastName OUTPUT INSERTED.first_name, INSERTED.last_name WHERE id = @UserId;", conn);

            //Get id of user to use in command

            cmd.Parameters.Add(new SqlParameter("@UserId", this.GetId()));

            //CHANGE FIRST NAME
            //If there is a new first name, change it
            if (!String.IsNullOrEmpty(newFirstName))
            {
                cmd.Parameters.Add(new SqlParameter("@newFirstName", newFirstName));
            }
            //if there isn't a new restaurant name, don't change the name
            else
            {
                cmd.Parameters.Add(new SqlParameter("@newFirstName", this.GetFirstName()));
            }

            //CHANGE LAST NAME
            //If there is a new last name, change it
            if (!String.IsNullOrEmpty(newLastName))
            {
                cmd.Parameters.Add(new SqlParameter("@newLastName", newLastName));
            }
            //if there isn't a new restaurant name, don't change the name
            else
            {
                cmd.Parameters.Add(new SqlParameter("@newLastName", this.GetLastName()));
            }


            //execute reader
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                this.SetFirstName(rdr.GetString(0));
                this.SetLastName(rdr.GetString(1));
            }

            DB.CloseSqlConnection(conn, rdr);
        }
    }
}
