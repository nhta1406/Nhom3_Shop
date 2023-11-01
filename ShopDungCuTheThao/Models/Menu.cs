using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    [Table("Menu")]
    public class Menu
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string Type { get; set; }
        public int Table { get; set; }
        public int ParentID { get; set; }
        public int Orders { get; set; }
        public int Status { get; set; }
    }
}