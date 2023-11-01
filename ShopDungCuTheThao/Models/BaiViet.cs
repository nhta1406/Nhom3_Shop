using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
        [Table("BaiViet")]
        public class BaiViet
        {
            [Key]
            public int ID { get; set; }
            [Required]
            public int TopicID { get; set; }
            [Required]
            public string Title { get; set; }
            [Required]
            public string Detail { get; set; }
            [Required]
            public string MetaKey { get; set; }
            [Required]
            public string MetaDesc { get; set; }
            [Required]
            public string IMG { get; set; }
            [Required]
            public int? CreateBy { get; set; }
            public string Slug { get; set; }
            public DateTime? CreateAt { get; set; }
            public int? UpdateBy { get; set; }
            public DateTime? UpdateAt { get; set; }
            public int Status { get; set; }
        }
}