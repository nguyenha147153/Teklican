using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teklican.Models.ModelView
{
    public class Cart
    {
        public int id_product { get; set; }
        public int quantity { get; set; }
        public decimal sub_total { get; set; }
    }
}