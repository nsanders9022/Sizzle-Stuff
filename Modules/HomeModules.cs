using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using OnlineStore.Objects;

namespace OnlineStore
{
    public class HomeModule : NancyModule
    {
        public Dictionary<string, object> ModelMaker()
        {
            Dictionary<string, object> model = new Dictionary<string, object> {
                {"products", Product.GetAll()}
            };
            return model;
        }
        public HomeModule()
        {
            Get["/"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                List<Product> allProducts = Product.GetAll();
                model.Add("categories", allCategories);
                model.Add("products", allProducts);
                return View["index.cshtml", model];
            };

            Post["/"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                List<Product> allProducts = Product.GetAll();
                model.Add("categories", allCategories);
                model.Add("products", allProducts);
                return View["index.cshtml", model];
            };

            Get["/sign_up"] = _ => {
                return View["sign_up.cshtml"];
            };

            Get["/log_in"] = _ => {
                return View["log_in.cshtml"];
            };

            //SET ADMIN PRIVILEGES TO FALSE, NEED TO CHANGE
            Post["/user_created"] = _ => {
                User newUser = new User(Request.Form["first-name"], Request.Form["last-name"], Request.Form["user-name"], Request.Form["password"], false);
                newUser.Save();
                Profile newProfile = new Profile(newUser.GetId(), Request.Form["street"], Request.Form["city"], Request.Form["state"], Request.Form["zip-code"], Request.Form["phone-number"]);
                newProfile.Save();
                return View["login_status.cshtml", newUser];
            };

            Post["/signed_in"] = _ => {
                User findUser = User.FindUserByName(Request.Form["user-name"], Request.Form["password"]);
                return View["login_status.cshtml", findUser];
            };
            //DEFAULTING TO USING USER 1. NEED TO CHANGE TO SET TO THE LOGGED IN USER!!!!
            Get["/product/{id}"] = parameters=> {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                List<Product> allProducts = Product.GetAll();
                User newUser = User.Find(1);
                Product newProduct = Product.Find(parameters.id);
                model.Add("categories", allCategories);
                model.Add("products", allProducts);
                model.Add("product", newProduct);
                model.Add("user", newUser);
                return View["product.cshtml", model];
            };

            //DEFAULTING TO USING USER 1. NEED TO CHANGE TO SET TO THE LOGGED IN USER!!!!
            Post["/confirmation"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                List<Product> allProducts = Product.GetAll();
                User newUser = User.Find(1);
                Product addedProduct = Product.Find(Request.Form["product-id"]);
                CartProduct currentCartProduct = new CartProduct(addedProduct.GetId(), 1, Request.Form["quantity"]);
                currentCartProduct.Save();
                model.Add("categories", allCategories);
                model.Add("products", allProducts);
                model.Add("product", addedProduct);
                model.Add("user", newUser);
                return View["confirmation.cshtml", model];
            };

            //DEFAULTING TO USING USER 1. NEED TO CHANGE TO SET TO THE LOGGED IN USER!!!!
            Post["/product_added"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                List<Product> allProducts = Product.GetAll();
                User newUser = User.Find(1);
                Product addedProduct = Product.Find(Request.Form["product-id"]);
                CartProduct currentCartProduct = new CartProduct(addedProduct.GetId(), newUser.GetId(), Request.Form["quantity"]);
                currentCartProduct.Save();
                model.Add("categories", allCategories);
                model.Add("products", allProducts);
                model.Add("product", addedProduct);
                model.Add("user", newUser);
                return View["index.cshtml", model];
            };

            // This view adds products to the database
            Post["/products"] = _ => {
                Product newProduct = new Product(Request.Form["product-name"], Request.Form["product-count"], Request.Form["product-rating"], Request.Form["product-price"], Request.Form["product-description"]);
                newProduct.Save();
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                List<Product> allProducts = Product.GetAll();
                model.Add("categories", allCategories);
                model.Add("products", allProducts);
                model.Add("product", newProduct);
                return View["index.cshtml", model];
            };

            //DEFAULTING TO USING USER 1. NEED TO CHANGE TO SET TO THE LOGGED IN USER!!!!
            Post["/checkout"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                User newUser = User.Find(1);
                List<Product> userProducts = newUser.GetCart();
                List<CartProduct> userCartProducts = newUser.GetCartProducts();
                model.Add("categories", allCategories);
                model.Add("userProducts", userProducts);
                model.Add("userCartProducts", userCartProducts);
                model.Add("user", newUser);
                return View["checkout.cshtml", model];
            };

            //DEFAULTING TO USING USER 1. NEED TO CHANGE TO SET TO THE LOGGED IN USER!!!!
            Post["/success"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                User newUser = User.Find(1);
                newUser.Checkout();
                return View["success.cshtml", newUser];
            };

            // Dummy Search page for table sorting testing
            Post["/search"] = _ => {
                return View["search.cshtml", ModelMaker()];
            };
        }
    }
}
