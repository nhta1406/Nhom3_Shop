using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShopDungCuTheThao.Models;

namespace ShopDungCuTheThao.Areas.Admin.Controllers
{
    public class AccountsController : Controller
    {
        private ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();

        // GET: Admin/Accounts
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5; 
            var taiKhoan = db.taiKhoan.Include(a => a.Role).OrderBy(a => a.CreateAt); 
            IPagedList<Accounts> pagedListTaiKhoan = taiKhoan.ToPagedList(pageNumber, pageSize);
            return View(pagedListTaiKhoan);
        }

        // GET: Admin/Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.taiKhoan.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // GET: Admin/Accounts/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.phanQuyen, "RoleID", "RoleName");
            return View();
        }
        //[Bind(Include = "AccountID,Name,Email,Phone,UserName,Password,RoleID,Birthday,Avatar,Address,Salt,CreateAt,Status")]
        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                string password = accounts.Password;
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
                accounts.Password = hashedPassword;
                accounts.Salt = salt;
                accounts.CreateAt = DateTime.Now;
                accounts.Status = 1;
                db.taiKhoan.Add(accounts);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Tài Khoản đã được tạo thành công.";
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.phanQuyen, "RoleID", "RoleName", accounts.RoleID);
            return View(accounts);
        }

        // GET: Admin/Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.taiKhoan.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.phanQuyen, "RoleID", "RoleName", accounts.RoleID);
            return View(accounts);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,Name,Email,Phone,UserName,Password,RoleID,Birthday,Avatar,Address,Salt,CreateAt,Status")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Tài Khoản đã được sửa thành công.";
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.phanQuyen, "RoleID", "RoleName", accounts.RoleID);
            return View(accounts);
        }

        // GET: Admin/Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.taiKhoan.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: Admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accounts accounts = db.taiKhoan.Find(id);
            db.taiKhoan.Remove(accounts);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Tài Khoản đã được xóa thành công.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
