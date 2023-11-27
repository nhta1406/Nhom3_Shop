using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopDungCuTheThao.Models;

namespace ShopDungCuTheThao.Areas.Admin.Controllers
{
    [RedirectToLogin]
    public class AdminRolesController : Controller
    {
        private ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();

        // GET: Admin/AdminRoles
        public ActionResult Index()
        {
            return View(db.phanQuyen.ToList());
        }

        // GET: Admin/AdminRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = db.phanQuyen.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // GET: Admin/AdminRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleID,RoleName,Decripstion")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                db.phanQuyen.Add(roles);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Phân Quyền đã được tạo thành công.";
                return RedirectToAction("Index");
            }

            return View(roles);
        }

        // GET: Admin/AdminRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = db.phanQuyen.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // POST: Admin/AdminRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleID,RoleName,Decripstion")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roles).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Phân Quyền đã được sửa thành công.";
                return RedirectToAction("Index");
            }
            return View(roles);
        }

        // GET: Admin/AdminRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles roles = db.phanQuyen.Find(id);
            if (roles == null)
            {
                return HttpNotFound();
            }
            return View(roles);
        }

        // POST: Admin/AdminRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roles roles = db.phanQuyen.Find(id);
            db.phanQuyen.Remove(roles);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Phân Quyền đã được xóa thành công.";
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
