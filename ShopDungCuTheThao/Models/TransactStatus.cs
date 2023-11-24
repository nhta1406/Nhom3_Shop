using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    [Table("TransactStatus")]
    public class TransactStatus
    {
        [Key]
        public int TransactStatusID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}