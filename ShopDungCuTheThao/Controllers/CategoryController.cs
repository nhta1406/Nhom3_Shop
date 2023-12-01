using ShopDungCuTheThao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDungCuTheThao.Controllers
{
    public class CategoryController : Controller
    {
        ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();
        public ActionResult Details(int ID)
        {
            var productList = db.sanPham.Where(s => s.ID == ID).ToList();
            return View(productList);
        }
        public ActionResult ParentCategory()
        {
            var categories = db.parentCategories.ToList();
            return PartialView(categories);
        }
        public ActionResult ByParentId(int parentId)
        {
            var products = db.loaiSanPham.Where(p => p.ParentID == parentId).ToList();
            return View(products);
        }
        public ActionResult ProductCategory(int catid, string namecat)
        {
            var listproduct = db.sanPham.Take(10).Where(m => m.Status == 1 && m.CateID == catid)
                                        .OrderByDescending(m => m.CreateAt)
                                        .ToList();
            ViewBag.CatName = namecat;
            return View("ProductCategory", listproduct);
        }
        public ActionResult GetNameCate(int parentId)
        {
            var categories = db.loaiSanPham.Where(p => p.ParentID == parentId).ToList();
            return PartialView(categories);
        }
        public ActionResult ShowCateDetails(int cateID)
        {
            var categories = db.loaiSanPham.Where(p => p.CateID == cateID).ToList();
            return View(categories);
        }
        public ActionResult CateHome(int cateID)
        {
            var categories = db.sanPham.Where(p => p.CateID == cateID).OrderByDescending(m => m.CreateAt).ToList();
            return View(categories);
        }
    }
}