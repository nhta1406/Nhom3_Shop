 using ShopDungCuTheThao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDungCuTheThao.Controllers
{
    public class TrangChuController : Controller
    {
        private ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;
                ViewBag.Username = username;
                var listproduct = db.loaiSanPham.Where(m => m.Status == 1)
                         .OrderByDescending(m => m.CreateAt)
                         .ToList();
                return View(listproduct);
            }
            else
            {
                var listproduct = db.loaiSanPham.Where(m => m.Status == 1)
                         .OrderByDescending(m => m.CreateAt)
                         .ToList();
                ViewBag.test = db.loaiSanPham.Count();
                return View(listproduct);
            }
        }
        public ActionResult ProductHome(int catid,string namecat)
        {
            var listproduct = db.sanPham.Where(m => m.Status == 1 && m.CateID == catid)
                                        .OrderByDescending(m => m.CreateAt)
                                        .ToList();
            ViewBag.CatName = namecat;
            return View("ProductHome",listproduct);
        }
        public ActionResult ProductName()
        {
            var listNameProduct = db.sanPham.ToList();
            return View(listNameProduct);
        }
        public ActionResult CategoryName()
        {
            var listCategoryName = db.loaiSanPham.ToList();
            return View(listCategoryName);
        }
    }
}