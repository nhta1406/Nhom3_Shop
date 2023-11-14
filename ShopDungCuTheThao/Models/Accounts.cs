using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ShopDungCuTheThao.Models
{
    [Table("Accounts")]
    public class Accounts
    {
        [Key]
        public int AccountID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int RoleID { get; set; }
        public DateTime? Birthday { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Salt { get; set; }
        public DateTime? CreateAt { get; set; }
        public int Status { get; set; }
        [ForeignKey("RoleID")]
        public Roles Role { get; set; }
    }
}