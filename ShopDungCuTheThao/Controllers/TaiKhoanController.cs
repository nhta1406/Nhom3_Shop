using ShopDungCuTheThao.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Web.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using System.Web.Helpers;
using System.Data.Entity.Validation;

namespace ShopDungCuTheThao.Controllers
{
    public class TaiKhoanController : Controller
    {
        protected ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string error = null;
            User user = db.NguoiDung.FirstOrDefault(m => m.Status == 1 && m.Roles == "user" && (m.UserName == username || m.Email == username) && m.Password == password);
            if (user == null)
            {
                error = "Thông tin đăng nhập không chính xác";
            }
            else
            {
                Session["UserCustomer"] = username;
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.Error = error;
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.CreateAt = DateTime.Now;
                user.CreateBy = int.Parse(Session["UserCustomer"].ToString());
                user.Roles = "user";
                user.Status = 1;
                db.NguoiDung.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "TrangChu");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserCustomer"] = null;
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}