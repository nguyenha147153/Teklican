//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teklican.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrdersDetail
    {
        public int id { get; set; }
        public Nullable<int> id_order { get; set; }
        public Nullable<int> id_product { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> sub_total { get; set; }
        public Nullable<bool> status { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}