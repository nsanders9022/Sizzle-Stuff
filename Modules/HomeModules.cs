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
                {"categories", Category.GetAll()},
                {"user", User.currentUser}
            };
            return model;
        }

        public HomeModule()
        {
            Get["/"] = _ => {
                Dictionary<string, object> model = ModelMaker();
                return View["index.cshtml", model];
            };

            Post["/"] = _ => {
                Dictionary<string, object> model = ModelMaker();
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
                User.SetCurrentUser(newUser);
                Dictionary<string, object> model = ModelMaker();
                return View["login_status.cshtml", model];
            };

            Post["/signed_in"] = _ => {
                User findUser = User.FindUserByName(Request.Form["user-name"], Request.Form["password"]);
                Dictionary<string, object> model = ModelMaker();
                if(findUser == null)
                {
                    model["user"] = null;
                }
                else
                {
                    User.SetCurrentUser(findUser);
                    model["user"] = findUser;
                }
                return View["login_status.cshtml", model];
            };

            Get["/product/{id}"] = parameters => {
                Dictionary<string,object> model = ModelMaker();
                Product newProduct = Product.Find(parameters.id);
                model.Add("product", newProduct);
                return View["product.cshtml", model];
            };

            Post["/confirmation"] = _ => {
                Dictionary<string,object> model = ModelMaker();
                Product addedProduct = Product.Find(Request.Form["product-id"]);
                CartProduct currentCartProduct = new CartProduct(((User)model["user"]).GetId(), addedProduct.GetId(), Request.Form["quantity"]);
                currentCartProduct.Save();
                model.Add("product", addedProduct);
                model.Add("currentCartProduct", currentCartProduct);
                return View["confirmation.cshtml", model];
            };

            Post["/product_added"] = _ => {
                Dictionary<string, object> model = ModelMaker();
                return View["index.cshtml", model];
            };

            // This view adds products to the database
            Post["/products"] = _ => {
                Product newProduct = new Product(Request.Form["product-name"], Request.Form["product-count"], Request.Form["product-rating"], Request.Form["product-price"], Request.Form["product-description"]);
                newProduct.Save();
                Dictionary<string, object> model = ModelMaker();
                return View["index.cshtml", model];
            };

            Get["/checkout"] = _ => {
                Dictionary<string,object> model = ModelMaker();
                User currentUser = (User)model["user"];
                List<Category> allCategories = Category.GetAll();
                List<Product> userProducts = currentUser.GetCart();
                List<CartProduct> userCartProducts = currentUser.GetCartProducts();
                model.Add("userProducts", userProducts);
                model.Add("userCartProducts", userCartProducts);
                return View["checkout.cshtml", model];
            };

            Post["/success"] = _ => {
                Dictionary<string,object> model = ModelMaker();
                User newUser = (User)model["user"];
                newUser.Checkout();
                Profile mainProfile = newUser.GetProfiles()[0];
                model.Add("mainProfile", newUser.GetProfiles()[0]);
                return View["success.cshtml", model];
            };

            Get["/product/delete/{id}"] = parameters => {
                Product SelectedProduct = Product.Find(parameters.id);
                return View["product_delete.cshtml", SelectedProduct];
            };

            Delete["product/deleted/{id}"] = parameters => {
                Dictionary<string,object> model = ModelMaker();
                User newUser = (User)model["user"];
                newUser.DeleteItem(parameters.id);
                List<Product> userProducts = newUser.GetCart();
                List<CartProduct> userCartProducts = newUser.GetCartProducts();
                model.Add("userProducts", userProducts);
                model.Add("userCartProducts", userCartProducts);
                return View["checkout.cshtml", model];
            };

// This looks like it will actually create an entirely new cartProduct, instead of updating an existing one?
            Patch["product/update_quantity"] = _ => {
                Dictionary<string,object> model = ModelMaker();
                User newUser = (User)model["user"];
                Product updateProduct = Product.Find(Request.Form["product-id"]);
                CartProduct newCartProduct = new CartProduct(newUser.GetId(), updateProduct.GetId(), 0);
                newCartProduct.UpdateQuantity(Request.Form["new-quantity"]);
                List<Product> userProducts = newUser.GetCart();
                List<CartProduct> userCartProducts = newUser.GetCartProducts();
                model.Add("userProducts", userProducts);
                model.Add("userCartProducts", userCartProducts);
                return View["checkout.cshtml", model];
            };

            // Dummy Search page for table sorting testing
            Post["/search"] = _ => {
                return View["search.cshtml", ModelMaker()];
            };

            Post["/clear_cart"] = _ => {
                Dictionary<string,object> model = ModelMaker();
                User newUser = (User)model["user"];
                newUser.EmptyCart();
                List<Product> userProducts = newUser.GetCart();
                List<CartProduct> userCartProducts = newUser.GetCartProducts();
                model.Add("userProducts", userProducts);
                model.Add("userCartProducts", userCartProducts);
                return View["checkout.cshtml", model];
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
                newPicture.Save();
                Product currentProduct = Product.Find(parameters.id);
                currentProduct.AddPicture(newPicture);
                Dictionary<string, object> model = ModelMaker();
                model.Add("product", currentProduct);
                return View["Admin/product", model];
            };

            Delete["/admin/product/{productId}/photos/{photoId}"] = parameters => {
              Picture.Find(parameters.photoId).Delete();
              Dictionary<string, object> model = ModelMaker();
              model.Add("product", Product.Find(parameters.productId));
              return View["Admin/product.cshtml", model];
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
