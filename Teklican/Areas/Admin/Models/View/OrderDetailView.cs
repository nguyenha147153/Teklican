using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teklican.Areas.Admin.Models.View
{
    public class OrderDetailView
    {
        public int id { get; set; }
        public int id_product { get; set; }
        public int id_order { get; set; }
        public decimal sub_total { get; set; }
        public string name_product { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string type { get; set; }
    }
}