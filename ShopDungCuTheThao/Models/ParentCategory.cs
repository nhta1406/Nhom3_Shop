using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    [Table("ParentCategory")]
    public class ParentCategory
    {
        [Key]
        public int ParentID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; } 
    }
}