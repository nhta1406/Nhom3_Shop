using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    [Table("DonHang")]
    public class DonHang
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public int AccountID { get; set; }
        public string Name { get; set; }
        public int Deleted { get; set; }
        public string Address { get; set; }
        public int TransactStatusID { get; set; }
        public double TotalMoney { get; set; }
        public int Paid { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? OrderDate { get; set; }
        [ForeignKey("AccountID")]
        public Accounts Accounts { get; set; }
        [ForeignKey("TransactStatusID")]
        public TransactStatus TransactStatus { get; set; }
    }
}