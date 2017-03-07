using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using OnlineStore.Objects;

namespace OnlineStore
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml"];
            };
            Get["/sign_up"] = _ => {
                return View["sign_up.cshtml"];
            };
            Get["/log_in"] = _ => {
                return View["log_in.cshtml"];
            };

            //NEED TO ADD MODEL HERE FOR WHAT SHOWS UP ON THE MAIN PAGE
            //SET ADMIN PRIVILEGES TO 0, NEED TO CHANGE
            Post["/user_created"] = _ => {
                User newUser = new User(Request.Form["first-name"], Request.Form["last-name"], Request.Form["user-name"], Request.Form["password"], false);
                newUser.Save();
                return View["main.cshtml"];
            };

            //NEED TO ADD MODEL HERE FOR WHAT SHOWS UP ON THE MAIN PAGE
            Post["/signed_in"] = _ => {
                User findUser = User.FindUserByName(Request.Form["user-name"], Request.Form["password"]);
                return View["main.cshtml"];
            };
        }
    }
}
