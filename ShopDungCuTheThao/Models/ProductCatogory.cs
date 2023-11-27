using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    public class ProductCatogory
    {
        [Key]
        public int ID { get; set; }
        public int CateID { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string MetaKey { get; set; }
        public string MetaDesc { get; set; }
        public string Slug { get; set; }
        public string IMG { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public double PriceSale { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Status { get; set; }
        public string CatName { get; set; }
    }
}