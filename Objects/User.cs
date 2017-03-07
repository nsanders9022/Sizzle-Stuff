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

        public void UpdatePassword(string newPassword)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE users SET password = @NewPassword OUTPUT INSERTED.password WHERE id=@UserId;", conn);
            cmd.Parameters.Add(new SqlParameter("@NewPassword", newPassword));
            cmd.Parameters.Add(new SqlParameter("@UserId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._password = rdr.GetString(0);
            }

            DB.CloseSqlConnection(conn, rdr);
        }

        public static void DeleteAll()
        {
            DB.DeleteAll("users");
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id = @UserId;", conn);
            cmd.Parameters.Add(new SqlParameter("@UserId", this.GetId()));
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        //get all reviews associated with this UserId
        public List<Review> GetReviews()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE user_id = @UserId;", conn);
            cmd.Parameters.Add(new SqlParameter("@UserId", this.GetId().ToString()));
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Review> allReviews = new List<Review>{};

            while(rdr.Read())
            {
                int reviewId = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);
                int productId = rdr.GetInt32(2);
                int rating = rdr.GetInt32(3);
                string reviewText = rdr.GetString(4);

                Review newReview = new Review(userId, productId, rating, reviewText, reviewId);
                allReviews.Add(newReview);
            }

            DB.CloseSqlConnection(conn, rdr);
            return allReviews;
        }

        //Gets all profiles with matching user id
        public List<Profile> GetProfiles()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM profiles WHERE user_id = @UserId;", conn);
            cmd.Parameters.Add(new SqlParameter("@UserId", this.GetId()));

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Profile> allProfiles = new List<Profile>{};

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);
                string street = rdr.GetString(2);
                string city = rdr.GetString(3);
                string state = rdr.GetString(4);
                int zipCode = rdr.GetInt32(5);
                string phoneNumber = rdr.GetString(6);

                Profile newProfile = new Profile(userId, street, city, state, zipCode, phoneNumber, id);

                allProfiles.Add(newProfile);
            }

            DB.CloseSqlConnection(conn, rdr);
            return allProfiles;
        }

        //Clears all the items in the cart_products table for this user
        public void EmptyCart()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM cart_products WHERE user_id = @UserId;", conn);
            cmd.Parameters.Add(new SqlParameter("@UserId", this.GetId()));

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        //Gets all products in a user's cart
        public List<Product> GetCart()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand ("SELECT products.* FROM users JOIN cart_products ON (users.id = cart_products.user_id) JOIN products ON (products.id = cart_products.product_id) WHERE users.id = @UserId;",conn);

            cmd.Parameters.Add(new SqlParameter ("@UserId", this.GetId().ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Product> allProducts = new List<Product>{};

            while(rdr.Read())
            {
                int productId = rdr.GetInt32(0);
                string productName = rdr.GetString(1);
                int productCount = rdr.GetInt32(2);
                int productRating = rdr.GetInt32(3);
                decimal productPrice = rdr.GetDecimal(4);
                string productDescription = rdr.GetString(5);
                Product newProduct = new Product(productName, productCount, productRating, productPrice, productDescription, productId);

                allProducts.Add(newProduct);
            }

            DB.CloseSqlConnection(conn, rdr);
            return allProducts;
        }

        //Gets the cart_products rows associated with the user
        public List<CartProduct> GetCartProducts()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cart_products WHERE user_id = @UserId;", conn);
            cmd.Parameters.Add(new SqlParameter("@UserId", this.GetId().ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();
            List<CartProduct> allCartProducts = new List<CartProduct>{};

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                int userId = rdr.GetInt32(1);
                int productId = rdr.GetInt32(2);
                int quantity = rdr.GetInt32(3);

                CartProduct newCartProduct = new CartProduct(userId, productId, quantity, id);
                allCartProducts.Add(newCartProduct);
            }

            DB.CloseSqlConnection(conn, rdr);
            return allCartProducts;
        }

        //Gets total price of all the items in the user's cart_products
        public decimal GetTotal()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand ("SELECT quantity FROM cart_products WHERE user_id = @UserId;",conn);

            cmd.Parameters.Add(new SqlParameter ("@UserId", this.GetId().ToString()));

            SqlDataReader rdr = cmd.ExecuteReader();

            List<int> allQuantities = new List<int>{};

            while(rdr.Read())
            {
                int quantity = rdr.GetInt32(0);
                allQuantities.Add(quantity);
            }


            List<Product> userProducts = this.GetCart();
            decimal total = 00.00m;

            for(var item = 0; item < userProducts.Count; item ++)
            {
                total += userProducts[item].GetPrice() * allQuantities[item];
            }
            return total;
        }

        public void Checkout()
        {
            //UPDATE COUNT

            //Get list of products
            List<Product> userProducts = this.GetCart();
            //Get list of current count for each product
            List<int> productCount = new List<int>{};
            foreach(Product product in userProducts)
            {
                productCount.Add(product.GetCount());
                Console.WriteLine("Product Stock: " + product.GetName() + ", " + product.GetCount());
            }

            //Get list of quanities
            List<CartProduct> cartProducts = this.GetCartProducts();
            List<int> quantityCount = new List<int>{};
            foreach(CartProduct product in cartProducts)
            {
                quantityCount.Add(product.GetQuantity());
                Console.WriteLine("Cart Quantity: " + product.GetQuantity());
            }
            //Updates count for each product based on quantity just purchased by a user
            for (int i = 0; i < userProducts.Count; i++)
            {
                userProducts[i].UpdateCount(productCount[i] - quantityCount[i]);
                Console.WriteLine("Final Count: " + userProducts[i].GetCount());
            }

            //EMPTY USER CART
            this.EmptyCart();
        }

        public static User FindUserByName(string userName, string password)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand ("SELECT * FROM users WHERE username = @UserName AND password = @Password;",conn);

            cmd.Parameters.Add(new SqlParameter ("@UserName", userName));
            cmd.Parameters.Add(new SqlParameter ("@Password", password));

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

            User foundUser = new User(foundfirstName, foundlastName, foundusername, foundpassword, foundadminPrivileges, foundid);

            DB.CloseSqlConnection(conn, rdr);

            if (foundUser.GetId() != 0)
            {
                return foundUser;
            }
            else
            {
                return null;
            }
        }
    }
}
