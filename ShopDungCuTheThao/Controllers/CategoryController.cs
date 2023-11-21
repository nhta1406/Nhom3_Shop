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
    }
}