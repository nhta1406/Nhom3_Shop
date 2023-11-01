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
    public class LoaiSanPhamController : BaseController
    {
        private ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();

        // GET: Admin/LoaiSanPham
        public ActionResult Index()
        {
            return View(db.loaiSanPham.ToList());
        }

        // GET: Admin/LoaiSanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.loaiSanPham.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPham/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(db.loaiSanPham.ToList(), "ID", "Name", 0);
            ViewBag.ListOrder = new SelectList(db.loaiSanPham.ToList(), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/LoaiSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                string slug=XString.Str_Slug(loaiSanPham.Name);
                loaiSanPham.Slug= slug;
                loaiSanPham.CreateAt = DateTime.Now;
                loaiSanPham.CreateBy = int.Parse(Session["UserAdmin"].ToString());
                loaiSanPham.UpdateAt = DateTime.Now;
                loaiSanPham.UpdateBy = int.Parse(Session["UserAdmin"].ToString());
                ViewBag.ListCat = new SelectList(db.loaiSanPham.ToList(), "ID", "Name", 0);
                ViewBag.ListOrder = new SelectList(db.loaiSanPham.ToList(), "Orders", "Name", 0);
                db.loaiSanPham.Add(loaiSanPham);
                db.SaveChanges();
                return RedirectToAction("Index","LoaiSanPham");
            }
            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.loaiSanPham.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // POST: Admin/LoaiSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ParentID,Orders,MetaKey,MetaDesc,CreateBy,Slug,CreateAt,UpdateBy,UpdateAt,Status")] LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSanPham);
        }

        // GET: Admin/LoaiSanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSanPham loaiSanPham = db.loaiSanPham.Find(id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            return View(loaiSanPham);
        }

        // POST: Admin/LoaiSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSanPham loaiSanPham = db.loaiSanPham.Find(id);
            db.loaiSanPham.Remove(loaiSanPham);
            db.SaveChanges();
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
