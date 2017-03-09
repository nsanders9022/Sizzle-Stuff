using System.Collections.Generic;
using Nancy;
using System;
using Nancy.ViewEngines.Razor;
using OnlineStore.Objects;

namespace OnlineStore
{
    public class HomeModule : NancyModule
    {
        public Dictionary<string, object> ModelMaker()
        {
            Dictionary<string, object> model = new Dictionary<string, object> {
                {"products", Product.GetAll()},
                {"categories", Category.GetAll()}
            };
            return model;
        }

        public HomeModule()
        {
            Get["/"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                List<Product> allProducts = Product.GetAll();
                // User newUser = User.FindUserByName(Request.Form["user-name"], Request.Form["password"]);
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
                CartProduct currentCartProduct = new CartProduct(addedProduct.GetId(), newUser.GetId(), Request.Form["quantity"]);
                currentCartProduct.Save();
                model.Add("categories", allCategories);
                model.Add("products", allProducts);
                model.Add("product", addedProduct);
                model.Add("user", newUser);
                model.Add("currentCartProduct", currentCartProduct);
                return View["confirmation.cshtml", model];
            };

            //DEFAULTING TO USING USER 1. NEED TO CHANGE TO SET TO THE LOGGED IN USER!!!!
            Post["/product_added"] = _ => {
                return View["index.cshtml", ModelMaker()];
            };

            // This view adds products to the database
            Post["/products"] = _ => {
                Product newProduct = new Product(Request.Form["product-name"], Request.Form["product-count"], Request.Form["product-rating"], Request.Form["product-price"], Request.Form["product-description"]);
                newProduct.Save();
                return View["index.cshtml", ModelMaker()];
            };

            //DEFAULTING TO USING USER 1. NEED TO CHANGE TO SET TO THE LOGGED IN USER!!!!
            Get["/checkout"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                User newUser = User.Find(1);
                List<Product> userProducts = newUser.GetCart();
                Console.WriteLine(userProducts.Count);
                List<CartProduct> userCartProducts = newUser.GetCartProducts();

                Dictionary<string,object> cart = new Dictionary<string, object>();
                cart.Add("userProducts", userProducts);
                cart.Add("userCartProducts", userCartProducts);

                model.Add("categories", allCategories);
                model.Add("userProducts", userProducts);
                model.Add("userCartProducts", userCartProducts);
                model.Add("user", newUser);
                model.Add("dictionary", cart);
                return View["checkout.cshtml", model];
            };

            //DEFAULTING TO USING USER 1. NEED TO CHANGE TO SET TO THE LOGGED IN USER!!!!
            Post["/success"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                User newUser = User.Find(1);
                newUser.Checkout();
                return View["success.cshtml", newUser];
            };

            Get["/product/delete/{id}"] = parameters => {
                Product SelectedProduct = Product.Find(parameters.id);
                return View["product_delete.cshtml", SelectedProduct];
            };

            Delete["product/deleted/{id}"] = parameters => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                User newUser = User.Find(1);
                Product deletedProduct = Product.Find(parameters.id);
                newUser.DeleteItem(deletedProduct.GetId());
                List<Product> userProducts = newUser.GetCart();
                Console.WriteLine(userProducts.Count);
                List<CartProduct> userCartProducts = newUser.GetCartProducts();
                model.Add("categories", allCategories);
                model.Add("userProducts", userProducts);
                model.Add("userCartProducts", userCartProducts);
                model.Add("user", newUser);

                return View["checkout.cshtml", model];
            };

            // Dummy Search page for table sorting testing
            Post["/search"] = _ => {
                return View["search.cshtml", ModelMaker()];
            };

            Post["/clear_cart"] = _ => {
                Dictionary<string,object> model = new Dictionary<string, object>();
                List<Category> allCategories = Category.GetAll();
                User newUser = User.Find(1);
                List<Product> userProducts = newUser.GetCart();
                Console.WriteLine(userProducts.Count);
                List<CartProduct> userCartProducts = newUser.GetCartProducts();
                model.Add("categories", allCategories);
                model.Add("userProducts", userProducts);
                model.Add("userCartProducts", userCartProducts);
                model.Add("user", newUser);
                newUser.EmptyCart();
                return View["checkout.cshtml", ModelMaker()];
            };
            // =====================BEGIN ADMIN VIEWS=========================================

            Get["/admin"] = _ => {
                return View["Admin/index.cshtml", ModelMaker()];
            };

            Get["/admin/products"] = _ => {
                return View["Admin/products.cshtml", ModelMaker()];
            };

            Post["/admin/products"] = _ => {
                Product newProduct = new Product(Request.Form["product-name"], Request.Form["product-count"], Request.Form["product-rating"], Request.Form["product-price"], Request.Form["product-description"]);
                newProduct.Save();
                return View["Admin/index.cshtml", ModelMaker()];
            };

            Get["/admin/products/{id}"] = parameters => {
                Product newProduct = Product.Find(parameters.id);
                Dictionary<string, object> model = ModelMaker();
                model.Add("product", newProduct);
                return View["Admin/product.cshtml", model];
            };

            Patch["/admin/products/{id}"] = parameters => {
                Product newProduct = Product.Find(parameters.id);
                newProduct.Update(Request.Form["update-product-name"], Request.Form["update-product-description"], Request.Form["update-product-count"], Request.Form["update-product-rating"], Request.Form["update-product-price"]);
                return View["Admin/products.cshtml", ModelMaker()];
            };

            Delete["/admin/products/{id}"] = parameters => {
                Product newProduct = Product.Find(parameters.id);
                newProduct.DeleteProduct();
                return View["Admin/products.cshtml", ModelMaker()];
            };

            Post["/admin/product/{id}/photos"] = parameters => {
                Picture newPicture =  Picture.UploadPicture(Request.Form["new-photo-url"], parameters.id, Request.Form["new-photo-name"], Request.Form["new-photo-alt-text"]);
                Dictionary<string, object> model = ModelMaker();
                model.Add("product", Product.Find(parameters.id));
                return View["Admin/product", model];
            };

            Post["/admin/search"] = _ => {
                Dictionary<string, object> model = ModelMaker();
                model["products"] = Product.SearchProductByName(Request.Form["search-bar"]);
                return View["Admin/products.cshtml", model];
            };

            Get["/admin/users"] = _ => {
                Dictionary<string, object> model = ModelMaker();
                model.Add("users", User.GetAll());
                return View["Admin/users.cshtml", model];
            };

            //Gets category page
            Get["/categories/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>{};
                List<Category> allCategories = Category.GetAll();
                Category thisCategory = Category.Find(parameters.id);
                List<Product> catProducts = thisCategory.GetProducts();
                model.Add("thisCategory", thisCategory);
                model.Add("products", catProducts);
                model.Add("categories", allCategories);
                return View["category.cshtml", model];
            };
        }
    }
}
