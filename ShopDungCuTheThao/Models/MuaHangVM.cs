using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDungCuTheThao.Models
{
    public class MuaHangVM
    {
        public string Name { get; set; }
        public int AccountID {get; set;}
        public string Address { get; set; }
    }
}