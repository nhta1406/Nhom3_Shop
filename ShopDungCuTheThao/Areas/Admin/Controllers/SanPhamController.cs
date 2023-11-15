using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopDungCuTheThao.Models;
using System.Drawing;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace ShopDungCuTheThao.Areas.Admin.Controllers
{
    [RedirectToLogin]
    public class SanPhamController : Controller
    {
        private ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();

        // GET: Admin/SanPham
        public ActionResult Index()
        {
            var list = db.sanPham.Join(
                db.loaiSanPham,
                p => p.CateID,
                c => c.CateID,
                (p, c) => new ProductCatogory
                {
                    ID = p.ID,
                    CateID = p.CateID,
                    Name = p.Name,
                    Detail = p.Detail,
                    MetaKey = p.MetaKey,
                    MetaDesc = p.MetaDesc,
                    Slug = p.Slug,
                    IMG = p.IMG,
                    Number = p.Number,
                    Price = p.Price,
                    PriceSale = p.PriceSale,
                    CreateBy = p.CreateBy,
                    CreateAt = p.CreateAt,
                    UpdateBy = p.UpdateBy,
                    UpdateAt = p.UpdateAt,
                    Status = p.Status,
                    CatName=c.Name
                }
                )
                .Where(m => m.Status != 0).ToList()
                .OrderByDescending(m=>m.CreateAt);
            // Sử dụng CultureInfo để định dạng giá trị tiền tệ
            var cultureInfo = new CultureInfo("vi-VN");
            return View(list);
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(db.loaiSanPham.ToList(), "CateID", "Name", 0);
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham model)
        {
            if (ModelState.IsValid)
            {
                string slug = XString.Str_Slug(model.Name);
                model.Slug = slug;
                model.CreateAt = DateTime.Now;
                if (Session["UserNameAdmin"] != null && int.TryParse(Session["UserNameAdmin"].ToString(), out int userId))
                {
                    model.CreateBy = userId;
                    model.UpdateBy = userId;
                }
                var Img = Request.Files["fileimg"];
                string[] FileExtension = { ".jpg", ".png", ".gif" };
                if (Img.ContentLength != 0)
                {
                    if (FileExtension.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {
                        string imgName = slug + Img.FileName.Substring(Img.FileName.LastIndexOf("."));
                        model.IMG = imgName;
                        string PathImg = Path.Combine(Server.MapPath("~/Public/img/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }
                ViewBag.ListCat = new SelectList(db.loaiSanPham.ToList(), "CateID", "Name", 0);
                db.sanPham.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SanPham model, HttpPostedFileBase fileimg)
        {
            if (ModelState.IsValid)
            {
                string slug = XString.Str_Slug(model.Name);
                model.Slug = slug;
                model.UpdateAt = DateTime.Now;
                model.UpdateBy = int.Parse(Session["UserAdmin"].ToString());
                if (fileimg != null && fileimg.ContentLength > 0)
                {
                    string[] FileExtension = { ".jpg", ".png", ".gif" };
                    string fileExtension = Path.GetExtension(fileimg.FileName);
                    if (FileExtension.Contains(fileExtension))
                    {
                        string imgName = slug + fileExtension;
                        model.IMG = imgName;
                        string pathImg = Path.Combine(Server.MapPath("~/Public/img/"), imgName);
                        fileimg.SaveAs(pathImg);
                    }
                }
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.sanPham.Find(id);
            db.sanPham.Remove(sanPham);
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
