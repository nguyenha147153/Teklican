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
    
    public partial class Product
    {
        public int id_Product { get; set; }
        public string name { get; set; }
        public Nullable<int> id_ProductType { get; set; }
        public Nullable<decimal> price { get; set; }
        public string description { get; set; }
        public Nullable<bool> status { get; set; }
        public Nullable<int> inventory { get; set; }
        public string img { get; set; }
        public Nullable<int> tax { get; set; }
    
        public virtual ProductType ProductType { get; set; }
    }
}
