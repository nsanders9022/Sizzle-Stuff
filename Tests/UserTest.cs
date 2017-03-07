using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace OnlineStore.Objects
{
    public class UserTest: IDisposable
    {
        public UserTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=online_store_test;Integrated Security=SSPI;";
        }

        //GetAll returns empty list if no users
        [Fact]
        public void GetAll_ForNoUsers_EmptyList()
        {
            //Arrange, Act, Assert
            List<User> actualResult = User.GetAll();
            List<User> expectedResult = new List<User>{};

            Assert.Equal(expectedResult, actualResult);
        }

        public void Dispose()
        {
            User.DeleteAll();
            Product.DeleteAll();
            Review.DeleteAll();
            Profile.DeleteAll();
            CartProduct.DeleteAll();
        }

        //Checks that user table is empty at first
        [Fact]
        public void Test_ForNoRowsInUserTable()
        {
            int actualResult = User.GetAll().Count;
            int expectedResult = 0;

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks if EqualOverride is working
        [Fact]
        public void EqualOverride_UsersAreSame_true()
        {
            //Arrange, Act
            User firstUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            User secondUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);

            Assert.Equal(firstUser, secondUser);
        }

        //Checks if instances are saved to database
        [Fact]
        public void Save_ForUser_SavesToDatabase()
        {
            //Arrange
            User newUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);

            //Act
            newUser.Save();

            //Assert
            List<User> actualResult = User.GetAll();
            List<User> expectedResult = new List<User>{newUser};

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks that GetAll method works for multiple instances
        [Fact]
        public void GetAll_ForMultipleUsers_ReturnsListWithAllUsers()
        {
            //Arrange
            User firstUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            User secondUser = new User("Nicole", "Sanders", "feet", "password", false);
            firstUser.Save();
            secondUser.Save();

            //Act, Assert
            List<User> actualResult = User.GetAll();
            List<User> expectedResult = new List<User> {firstUser, secondUser};

            Assert.Equal(expectedResult, actualResult);
        }

        //Checks that Find method finds correct user in database
        [Fact]
        public void Find_ForUser_FindsUserInDatabase()
        {
            //Arrange
            User testUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            testUser.Save();

            //Act, Assert
            User foundUser = User.Find(testUser.GetId());
            Assert.Equal(testUser, foundUser);
        }

        //Checks that update method changes firstName
        [Fact]
        public void UpdateName_ForUserFirstName_ChangesFirstName()
        {
            //Arrange
            User testUser = new User("Ally", "Holcombe", "eylookturkeys", "password", false);
            testUser.Save();
            string newName = "Allie";

            //Act
            testUser.UpdateName(newName, null);

            //Assert
            Assert.Equal(newName, testUser.GetFirstName());
        }

        //Checks that update method changes lastName
        [Fact]
        public void UpdateName_ForUserLastName_ChangesLastName()
        {
            //Arrange
            User testUser = new User("Allie", "Holcomb", "eylookturkeys", "password", false);
            testUser.Save();
            string newName = "Holcombe";

            //Act
            testUser.UpdateName(null, newName);

            //Assert
            Assert.Equal(newName, testUser.GetLastName());
        }

        //Checks that update method changes both first and last names
        [Fact]
        public void UpdateName_ForUserFirstAndLastName_ChangesFirstAndLastName()
        {
            //Arrange
            User testUser = new User("Ally", "Holcomb", "eylookturkeys", "password", false);
            testUser.Save();
            string newFirstName = "Allie";
            string newLastName = "Holcombe";

            //Act
            testUser.UpdateName(newFirstName, newLastName);

            string expectedResult = newFirstName + " " + newLastName;
            string actualResult = testUser.GetFirstName() + " " + testUser.GetLastName();

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        //CHecks that update method changes password
        [Fact]
        public void UpdatePassword_ForUserPassword_ChangesPassword()
        {
            //Arrange
            User testUser = new User("Ally", "Holcomb", "eylookturkeys", "password", false);
            testUser.Save();
            string newPassword = "banana phone";

            //Act
            testUser.UpdatePassword(newPassword);

            string expectedResult = newPassword;
            string actualResult = testUser.GetPassword();

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        //Deletes an individual user from the table
        [Fact]
        public void DeleteUser_DeletesUserFromDB_removeRow()
        {
            //Arrange
            User firstUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            User secondUser = new User("Nicole", "Sanders", "feet", "password", false);
            firstUser.Save();
            secondUser.Save();

            //Act
            firstUser.Delete();

            List<User> actualResult = User.GetAll();
            List<User> expectedResult = new List<User>{secondUser};

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        //Checks that all reviews associated with a user are returned by GetReviews
        [Fact]
        public void GetReviews_ForUser_ListOfReviews()
        {
            //Arrange
            User testUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            testUser.Save();

            Product firstProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
            Product secondProduct = new Product("Banana Corer", 13, 3, 30.99m, "Kind of weird");
            firstProduct.Save();
            secondProduct.Save();

            Review firstReview = new Review(testUser.GetId(), 1, 5, "I loved this!");
            Review secondReview = new Review(testUser.GetId(), 2, 4, "It was fun");
            firstReview.Save();
            secondReview.Save();

            //Act
            testUser.GetReviews();
            List<Review> actualResult = testUser.GetReviews();
            List<Review> expectedResult = new List<Review> {firstReview, secondReview};

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }

        // //Checks that profile is returned for user
        // [Fact]
        // public void AddProfile_ForUser_ChangeUserIdInProfile()
        // {
        //     //Arrange
        //     User testUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
        //     testUser.Save();
        //
        //     Profile newProfile = new Profile(testUser.GetId(), "422 Doggo St.", "Seattle", "WA", 98119, "2069543205");
        //     newProfile.Save();
        //
        //     //Act
        //     testUser.AddProfile(newProfile);
        //
        //     //Assert
        //     List<Profile> actualResult = testUser.GetProfiles();
        //     List<Profile> expectedResult = new List<Profile> {newProfile};
        //
        //     Assert.Equal(expectedResult, actualResult);
        // }

        //Checks that all profiles are returned for users
        public void GetProfiles_ForUser_ReturnsListofProfiles()
        {
            //Arrange
            User testUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            testUser.Save();

            Profile firstProfile = new Profile(testUser.GetId(), "422 Doggo St.", "Seattle", "WA", 98119, "2069543205");
            Profile secondProfile = new Profile(testUser.GetId(), "32416 SE 43rd PL", "Fall City", "WA", 98024, "2069543205");
            firstProfile.Save();
            secondProfile.Save();

            //Assert
            List<Profile> actualResult = testUser.GetProfiles();
            List<Profile> expectedResult = new List<Profile> {firstProfile, secondProfile};

            Assert.Equal(expectedResult, actualResult);
        }

        //Deletes all the items in a specific user's cart
        [Fact]
        public void EmptyCart_RemovesAllProductsFromUsersCart_void()
        {
            //Arrange
            User testUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            testUser.Save();
            CartProduct testCartProduct = new CartProduct(testUser.GetId(),2,5);
            testCartProduct.Save();
            CartProduct secondCartProduct = new CartProduct(testUser.GetId(),3,5);
            secondCartProduct.Save();
            CartProduct thirdCartProduct = new CartProduct(1,3,1);
            thirdCartProduct.Save();

            //Act
            testUser.EmptyCart();
            List<CartProduct> expected = new List<CartProduct> {thirdCartProduct};
            List<CartProduct> result = CartProduct.GetAll();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetCart_GetAllProductsFromUsersCart_ReturnTheListOfProductsFromTheUser()
        {
            //Arrange
            User testUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            testUser.Save();
            Product firstProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
            firstProduct.Save();
            Product secondProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
            secondProduct.Save();
            CartProduct testCartProduct = new CartProduct(testUser.GetId(),firstProduct.GetId(),5);
            testCartProduct.Save();
            CartProduct secondCartProduct = new CartProduct(testUser.GetId(),secondProduct.GetId(),5);
            secondCartProduct.Save();
            CartProduct thirdCartProduct = new CartProduct(1,firstProduct.GetId(),1);
            thirdCartProduct.Save();

            //Act
            List<Product> expected = new List<Product> {firstProduct, secondProduct};
            List<Product> result = testUser.GetCart();

            //Assert
            Assert.Equal(expected, result);
        }

        //Get the total price of all the items in the user's cart_products
        [Fact]
        public void GetTotal_ReturnsTotalPriceOfAllItems_decimal()
        {
            //Arrange
            User testUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            testUser.Save();
            Product firstProduct = new Product("Vegetti", 13, 5, 2.00m, "Great item for shredding zukes");
            firstProduct.Save();
            Product secondProduct = new Product("Vegetti", 13, 5, 20.00m, "Great item for shredding zukes");
            secondProduct.Save();
            CartProduct testCartProduct = new CartProduct(testUser.GetId(),firstProduct.GetId(),1);
            testCartProduct.Save();
            CartProduct secondCartProduct = new CartProduct(testUser.GetId(),secondProduct.GetId(),2);
            secondCartProduct.Save();

            //Act
            List<Product> expected = new List<Product> {firstProduct, secondProduct};
            List<Product> result = testUser.GetCart();

            decimal actualResult = testUser.GetTotal();
            decimal expectedResult = 42.00m;

            Assert.Equal(expectedResult, actualResult);
        }

        //Gets all of the rows in the cart_products table that belong to the user
        [Fact]
        public void GetCartProducts_GetsRowsFromCartProductsTable_List()
        {
            //Arrange
            User testUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            testUser.Save();
            Product firstProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
            firstProduct.Save();
            Product secondProduct = new Product("Vegetti", 13, 5, 20.99m, "Great item for shredding zukes");
            secondProduct.Save();
            CartProduct testCartProduct = new CartProduct(testUser.GetId(),firstProduct.GetId(),5);
            testCartProduct.Save();
            CartProduct secondCartProduct = new CartProduct(testUser.GetId(),secondProduct.GetId(),5);
            secondCartProduct.Save();

            //Act
            List<CartProduct> expected = new List<CartProduct> {testCartProduct, secondCartProduct};
            List<CartProduct> result = testUser.GetCartProducts();

            //Assert
            Assert.Equal(expected, result);
        }

        //Checks out a user's cart: count decreases and cart_products rows are DeleteProduct
        [Fact]
        public void Checkout_ChecksoutProductsFromUser_updatesTables()
        {
            //Arrange
            User testUser = new User("Allie", "Holcombe", "eylookturkeys", "password", false);
            testUser.Save();
            Product firstProduct = new Product("Vegetti", 13, 5, 2.00m, "Great item for shredding zukes");
            firstProduct.Save();
            Product secondProduct = new Product("Banana Corer", 13, 5, 20.99m, "Kind of weird");
            secondProduct.Save();
            CartProduct testCartProduct = new CartProduct(testUser.GetId(),firstProduct.GetId(),5);
            testCartProduct.Save();
            CartProduct secondCartProduct = new CartProduct(testUser.GetId(),secondProduct.GetId(),2);
            secondCartProduct.Save();

            //Act
            testUser.Checkout();

            //Checks that count was Updated
            int expectedCount = 8;
            // int actualCount = firstProduct.GetCount();
            int actualCount = Product.Find(firstProduct.GetId()).GetCount();
            Assert.Equal(expectedCount, actualCount);

            //Checks that cart is empty
            List<Product> expectedCart = new List<Product>{};
            List<Product> actualCart = testUser.GetCart();
            Assert.Equal(expectedCart, actualCart);

        }
    }
}
