using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teklican.Models.ModelView
{
    public class AccountView
    {
        public int id { get; set; }
        public string username { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string pwd { get; set; }
        public string repwd { get; set; }
        public string nametype { get; set; }
    }
}