using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Org.BouncyCastle.Asn1.X509;
using ShopDungCuTheThao.Models;

namespace ShopDungCuTheThao.Areas.Admin.Controllers
{
    [RedirectToLogin]
    public class DonHangController : Controller
    {
        private ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();

        // GET: Admin/DonHang
        public ActionResult Index()
        {
            var donHang1 = db.donHang.Include(d => d.Accounts).Include(c => c.TransactStatus);
            return View(donHang1.ToList());
        }
        public ActionResult ChangeStatus(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var order = db.donHang.Include(x => x.Accounts).FirstOrDefault(x => x.OrderID == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return PartialView("ChangeStatus");
        }

        [HttpPost]
        public ActionResult ChangeStatus(int id,DonHang order)
        {
            var donHang = db.donHang.FirstOrDefault(x => x.OrderID == id);
            ViewBag.ListTrangThai = new SelectList(db.donHang.ToList(), "OrderID", "TrangThai", donHang.OrderID);
            donHang.TransactStatusID = order.TransactStatusID;
            if (order.TransactStatusID == 5) 
            {
                donHang.Paid = 1;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Admin/DonHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.donHang.Include(d => d.Accounts).Include(c => c.TransactStatus).FirstOrDefault(d => d.OrderID == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            var chitietdonhang = db.chiTietDonHang.Include(x => x.SanPham).Where(x => x.OrderID == order.OrderID).ToList();
            ViewBag.ChiTiet = chitietdonhang;
            return View(order);
        }
        

        // GET: Admin/DonHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.donHang.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: Admin/DonHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.donHang.Find(id);
            db.donHang.Remove(donHang);
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
