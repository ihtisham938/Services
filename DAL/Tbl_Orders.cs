//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineHomeServices.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Orders
    {
        public int id { get; set; }
        public string description { get; set; }
        public string SellerName { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public string Phone_number { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> orderprice { get; set; }
    }
}
