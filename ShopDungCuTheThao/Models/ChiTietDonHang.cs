using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    [Table("ChiTietDonHang")]
    public class ChiTietDonHang
    {
        [Key]
        public int OrderDetailsID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int ID { get; set; }
        public double Price { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        [ForeignKey("OrderID")]
        public DonHang DonHang { get; set; }
        [ForeignKey("ID")]
        public SanPham SanPham { get; set; }
    }
}