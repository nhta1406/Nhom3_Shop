using ShopDungCuTheThao.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    public class ShopDungCuTheThaoDB : DbContext
    {
        public ShopDungCuTheThaoDB() : base("name=Connection")
        {

        }
        public DbSet<BaiViet> baiViet { get; set; }
        public DbSet<ChiTietDonHang> chiTietDonHang { get; set; }
        public DbSet<ChuDe> chuDe { get; set; }
        public DbSet<LienHe> lienHe { get; set; }
        public DbSet<DonHang> donHang { get; set; }
        public DbSet<LoaiSanPham> loaiSanPham { get; set; }
        public DbSet<Customer> khachHang { get; set; }
        public DbSet<Accounts> taiKhoan { get; set; }
        public DbSet<Roles> phanQuyen { get; set; }
        public DbSet<SanPham> sanPham { get; set; }
    }
}