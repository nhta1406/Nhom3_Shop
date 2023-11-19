using Microsoft.AspNetCore.Http;
using ShopDungCuTheThao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ShopDungCuTheThao.Controllers
{
    public class GioHangController : Controller
    {
        ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();
        // GET: GioHang
        public static List<GioHang> LayGioHang(HttpContextBase httpContext)
        {
            List<GioHang> lstGioHang = httpContext.Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                httpContext.Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int ID, string strURL)
        {
            List<GioHang> lstGioHang = LayGioHang(HttpContext);
            GioHang sanpham = lstGioHang.Find(sp => sp.iID == ID);
            if (sanpham == null)
            {
                sanpham = new GioHang(ID);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iQuantity++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = LayGioHang(HttpContext);
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(sp => sp.iQuantity);
            }
            return tsl;
        }

        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHang> lstGioHang = LayGioHang(HttpContext);
            if (lstGioHang != null)
            {
                ttt += lstGioHang.Sum(sp => sp.totalMoney);
            }
            return ttt;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang(HttpContext);
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lstGioHang);
        }
    }
}