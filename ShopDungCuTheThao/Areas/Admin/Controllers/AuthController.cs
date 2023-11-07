using ShopDungCuTheThao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ShopDungCuTheThao.Areas.Admin.Controllers
{
    public class AuthController : BaseController
    {
        protected ShopDungCuTheThaoDB db=new ShopDungCuTheThaoDB();
        public ActionResult Login()
        {
            return View();
        }
        private bool IsValidLogin(string username, string password)
        {
            ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();
            User account = db.NguoiDung.FirstOrDefault(a => a.UserName == username && a.Password == password);
            return (account != null);
        }
        [HttpPost]
        public ActionResult Login(string username,string password)
        {
            string error = null;
            User user = db.NguoiDung.FirstOrDefault(m => m.Status == 1 && m.Roles == "admin" && (m.UserName == username || m.Email == username) && m.Password == password);
            if (user == null) 
            {
                error = "Thông tin đăng nhập không chính xác";
            }
            else
            {
                Session["UserAdmin"] = username;
                ViewBag.UserName = password;
                Session["User_ID"] = user.ID;
               return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = error;
            ViewBag.UserName= password;
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["User_ID"] = "";
            return Redirect("~/Admin/Login");
        }
    }
}