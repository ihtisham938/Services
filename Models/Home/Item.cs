using OnlineHomeServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineHomeServices.Models.Home
{
    public class Item
    {
        public Tbl_Service Service { get; set; }
        public int Quantity { get; set; }
    }
}