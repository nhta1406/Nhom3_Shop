using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDungCuTheThao.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext != null && (Session["AccountID"] == null || Session["AccountID"].ToString() == "0"))
            {
                filterContext.Result = new RedirectResult("~/Admin/Login");
            }
            base.OnActionExecuting(filterContext);
        }
        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    if (Session["AccountID"] != null)
        //    {
        //        ViewBag.UserName = Session["UserName"].ToString();
        //    }
        //    else
        //    {
        //        ViewBag.UserName = string.Empty;
        //    }
        //    base.OnActionExecuted(filterContext);
        //}
    }
}