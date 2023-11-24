using Org.BouncyCastle.Asn1.X509;
using ShopDungCuTheThao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Web;
using System.Web.Mvc;

namespace ShopDungCuTheThao.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        ShopDungCuTheThaoDB db= new ShopDungCuTheThaoDB();
        public ActionResult Index()
        {
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("GioHang", "GioHang");
            }
            return View(gioHang);
        }

        // POST: /checkout/process
        [HttpPost]
        public ActionResult Checkout(MuaHangVM model)
        {
            List<GioHang> gioHang = Session["GioHang"] as List<GioHang>;
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("GioHang", "GioHang");
            }

            DonHang donHang = new DonHang();
            donHang.AccountID = int.Parse(Session["UserID"].ToString());
            donHang.Name = model.Name;
            donHang.Deleted = 0;
            donHang.Address = model.Address;
            donHang.TransactStatusID = 1;
            donHang.OrderDate = DateTime.Now;
            donHang.TotalMoney = Convert.ToInt32(gioHang.Sum(x => x.totalMoney));
            db.donHang.Add(donHang);
            db.SaveChanges();
            foreach (var item in gioHang)
            {
                ChiTietDonHang chiTietDonHang = new ChiTietDonHang();
                chiTietDonHang.OrderID = donHang.OrderID;
                chiTietDonHang.ID = item.iID;
                chiTietDonHang.Total = item.totalMoney;
                chiTietDonHang.Price = item.iPrice;
                chiTietDonHang.Quantity = item.iQuantity;
                chiTietDonHang.CreateDate= DateTime.Now;
                db.chiTietDonHang.Add(chiTietDonHang);
            }
            db.SaveChanges();
            Session["GioHang"] = null;

            return RedirectToAction("Dashboard", "TaiKhoan");
        }
        // GET: /checkout/confirmation
        public ActionResult Confirmation()
        {
            return View();
        }
    }
}