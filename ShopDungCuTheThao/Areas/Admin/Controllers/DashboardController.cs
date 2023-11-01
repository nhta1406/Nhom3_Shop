﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDungCuTheThao.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Session["UserAdmin"].ToString()))
            {
                return Redirect("~/Admin/Login");
            }
            return View();
        }
    }
}