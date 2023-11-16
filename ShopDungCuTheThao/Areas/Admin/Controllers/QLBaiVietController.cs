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
    public class QLBaiVietController : Controller
    {
        private ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();
        public ActionResult Index()
        {
            return View(db.baiViet);
        }
    }
}