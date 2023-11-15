using ShopDungCuTheThao.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;
using System.Web.Security;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ShopDungCuTheThao.Controllers
{
    public class TaiKhoanController : Controller
    {
        protected ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();
        private readonly Microsoft.AspNet.Identity.UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;

        public TaiKhoanController(Microsoft.AspNet.Identity.UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public TaiKhoanController()
            : this(new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public TaiKhoanController(Microsoft.AspNet.Identity.UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public ActionResult Dashboard()
        {
            return View();
        }
        [AllowAnonymous]
        [Route("dang-nhap.html",Name ="DangNhap")]
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
                var user = await db.taiKhoan.FirstOrDefaultAsync(a => a.UserName == model.UserName && a.Password == model.Password);
                if (user != null && user.RoleID == 2)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    Session["UserID"] =user.AccountID.ToString();
                    Session["UserName"] = user.UserName.ToString();
                    return RedirectToAction("Dashboard", "TaiKhoan");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Models.RegisterViewModel1 model)
        {
            if (ModelState.IsValid)
            {
                var role = await db.phanQuyen.FirstOrDefaultAsync(r => r.RoleID == 2);

                if (role == null)
                {
                    role = new Models.Roles { RoleID = 2, RoleName = "RoleName" };
                    db.phanQuyen.Add(role);
                    await db.SaveChangesAsync();
                }
                var account = new Accounts
                {
                    UserName = model.UserName,
                    Password = model.Password
                };
                db.taiKhoan.Add(account);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "TrangChu");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            //HttpContext.Session.Remove("AccountID");
            FormsAuthentication.SignOut();
            //Session.Remove("UserName");
            Session["UserName"] = null;
            Session["UserID"] = null;
            return RedirectToAction("Index", "TrangChu");
        }
    }
}