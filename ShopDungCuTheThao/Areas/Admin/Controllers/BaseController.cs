using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDungCuTheThao.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public BaseController() 
        {
            if (System.Web.HttpContext.Current.Session["UserAdmin"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/Login");
            }
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["UserAdmin"] != null)
            {
                ViewBag.UserName = Session["UserAdmin"].ToString();
            }
            else
            {
                ViewBag.UserName = "";
            }

            base.OnActionExecuted(filterContext);
        }
    }
}