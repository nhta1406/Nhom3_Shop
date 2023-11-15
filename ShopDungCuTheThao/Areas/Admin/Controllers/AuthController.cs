using ShopDungCuTheThao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopDungCuTheThao.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        protected ShopDungCuTheThaoDB db=new ShopDungCuTheThaoDB();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Models.LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await db.taiKhoan.FirstOrDefaultAsync(a => a.UserName == model.UserName && a.Password == model.Password && a.RoleID == 1);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    Session["AdminID"] = user.AccountID.ToString();
                    Session["UserNameAdmin"] = user.UserName.ToString();
                    Session["Avatar"] = user.Avatar.ToString();
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Session.Clear();
            //Session.Abandon();
            return Redirect("~/Admin/Login");
        }
    }
}