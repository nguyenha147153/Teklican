using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teklican.Areas.Admin.Models.View
{
    public class OrderView
    {
        public int id { get; set; }
        public int id_cus { get; set; }
        public string addr_cus { get; set; }

        public string name_cus { get; set; }
        public decimal total { get; set; }
        public DateTime? createDay { get; set; }
        public bool status { get; set; }
    }
}