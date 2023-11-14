using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    [Table("SanPham")]
    public class SanPham
    {
        [Key]
        public int ID { get; set; }
        public int CateID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public string MetaKey { get; set; }
        [Required]
        public string MetaDesc { get; set; }
        public string Slug { get; set; }
        public string IMG { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public double PriceSale { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
        [ForeignKey("CateID")]
        public LoaiSanPham LoaiSanPham { get; set; }
    }
}