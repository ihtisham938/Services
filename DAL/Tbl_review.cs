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
    
    public partial class Tbl_review
    {
        public int id { get; set; }
        public Nullable<int> Orderid { get; set; }
        public Nullable<int> rating { get; set; }
        public string Review { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string reviwername { get; set; }
        public string reviewname { get; set; }
    }
}