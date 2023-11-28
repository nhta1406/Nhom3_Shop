using ShopDungCuTheThao.Models;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;
using System.Web.Security;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Net.PeerToPeer;

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
            if (Session["UserID"] == null && Session["Cart"] == null) 
            {
                return RedirectToAction("Login", "TaiKhoan");
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Models.LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await db.taiKhoan.FirstOrDefaultAsync(a => a.UserName == model.UserName && a.RoleID == 2);
                if (user != null && user.RoleID == 2 && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    Session["UserID"] = user.AccountID.ToString();
                    Session["UserName"] = user.UserName.ToString();
                    Session["FullName"] = user.Name.ToString();
                    if (user.Phone != null) 
                    {
                        Session["Phone"] = user.Phone.ToString();
                    }
                    if (user.Email != null)
                    {
                        Session["Email"] = user.Email.ToString();

                    }
                    if (user.Avatar != null)
                    {
                        Session["AvatarKH"] = user.Avatar.ToString();
                    }
                    if (user.Birthday != null)
                    {
                        Session["Birthday"] = user.Birthday.ToString();
                    }
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
                string password = model.Password;
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
                var account = new Accounts
                {
                        Name = model.FullName,
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = hashedPassword,
                        RoleID = 2,
                        Salt = salt,
                        Phone = null,
                        CreateAt = DateTime.Now,
                        Status = 1,
                };
                db.taiKhoan.Add(account);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "TrangChu");
            }
            return View( model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserName"] = null;
            Session["UserID"] = null;
            return RedirectToAction("Index", "TrangChu");
        }
        public ActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePassword(Models.ChangePassword model)
       {
                string userId = (string)Session["UserName"];
                var user = await db.taiKhoan.FirstOrDefaultAsync(a => a.UserName == userId && a.RoleID == 2);
                if (user != null && BCrypt.Net.BCrypt.Verify(model.OldPassword, user.Password))
                {
                    if (model.NewPassword == model.ConfirmPassword)
                    {
                        user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Login", "TaiKhoan");
                    }
                    else
                    {
                        ModelState.AddModelError("ConfirmPassword", "New password and confirm password do not match.");
                    }
                }
                else
                {
                    ModelState.AddModelError("CurrentPassword", "Invalid current password.");
                }
            return RedirectToAction("Login", "TaiKhoan");
        }
        public ActionResult UpdateInfo()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateInfo(Models.ChangeInfo model)
        {
            if (ModelState.IsValid)
            {
                string userId = (string)Session["UserName"];
                var user = await db.taiKhoan.FirstOrDefaultAsync(a => a.UserName == userId && a.RoleID == 2);
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Name = model.FullName;
                    if (user.Email == null) 
                    {
                        user.Email = model.Email;
                    }
                    else
                    {
                        user.Email = Session["Email"].ToString();
                    }
                    var checkPhone = await db.taiKhoan.FirstOrDefaultAsync(a => a.Phone == null && a.RoleID == 2);
                    if (user.Phone == null)
                    {
                        user.Phone = model.Phone;
                    }
                    else
                    {
                        user.Phone = Session["Phone"].ToString();
                    }
                    if (user.Birthday == null) 
                    {
                        user.Birthday = model.Birthday;
                    }
                    else
                    {
                        user.Birthday = Convert.ToDateTime(Session["Birthday"]);
                    }
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Dashboard", "TaiKhoan");
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                return View(model);
            }
        }
        //public ActionResult UpdateAvatar()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> UpdateAvatar(Models.UpdateAvatar model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string userId = (string)Session["UserName"];
        //        var user = await db.taiKhoan.FirstOrDefaultAsync(a => a.UserName == userId && a.RoleID == 2);
        //        if (user != null)
        //        {
        //            user.Avatar = model.Avatar;
        //            await db.SaveChangesAsync();
        //        }
        //        return RedirectToAction("Dashboard", "TaiKhoan");
        //    }
        //    return View(model);
        //}
        public ActionResult UploadAvatar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAvatar(HttpPostedFileBase avatarFile, int accountId)
        {
            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(avatarFile.FileName);
                var uniqueFileName = GetUniqueFileName(fileName);
                var filePath = Path.Combine(Server.MapPath("~/Public/avatar/"), uniqueFileName);
                avatarFile.SaveAs(filePath);
                var avatarPath =uniqueFileName;

                var account = db.taiKhoan.FirstOrDefault(x => x.AccountID == accountId);
                if (account != null)
                {
                    account.Avatar = avatarPath;
                    Session["AvatarKH"] = account.Avatar;
                    db.SaveChanges();
                    return Json(new { success = true, filePath = avatarPath });
                }
                else
                {
                    return Json(new { success = false, error = "Account not found." });
                }
            }
            else
            {
                return Json(new { success = false, error = "No file uploaded." });
            }
        }
        private string GetUniqueFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var uniqueFileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), extension);
            return uniqueFileName;
        }

    }
}