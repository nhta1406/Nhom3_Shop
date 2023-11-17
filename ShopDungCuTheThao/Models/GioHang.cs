using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    public class GioHang
    {
        ShopDungCuTheThaoDB db = new ShopDungCuTheThaoDB();
        public int iID { get; set; }
        public string iName { get; set; }
        public string iDetail { get; set; }
        public string iIMG { get; set; }
        public int iQuantity { get; set; }
        public double iPrice { get; set; }
        public double iPriceSale { get; set; }
        public double totalMoney
        {
            get
            {
                if (iPriceSale > 0)
                {
                    return iPriceSale * iQuantity;
                }
                else
                {
                    return iPrice * iQuantity;
                }
            }
        }
        public GioHang(int ID)
        {
            iID = ID;
            SanPham sanPham = db.sanPham.Single(s => s.ID == iID);
            iName= sanPham.Name;
            iDetail = sanPham.Detail;
            iPrice = sanPham.Price;
            iPriceSale = sanPham.PriceSale;
            iIMG = sanPham.IMG;
            iQuantity = 1;
        }
    }
}