using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopDungCuTheThao
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               "User_Login",
               "User/Login",
               new { Controller = "TaiKhoan", action = "Login", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               "User_Logout",
               "User/Logout",
               new { Controller = "TaiKhoan", action = "Logout", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               "User_SignUp",
               "User/SignUp",
               new { Controller = "TaiKhoan", action = "SignUp", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
