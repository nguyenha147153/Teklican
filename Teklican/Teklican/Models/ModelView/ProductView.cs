using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teklican.Models.ModelView
{
    public class ProductView
    {
        public int id_Product { get; set; }
        public string name { get; set; }
        public int id_ProductType { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public int inventory { get; set; }
        public string img { get; set; }
        public int tax { get; set; }
        public string nameType { get; set; }
    }
}