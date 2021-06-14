using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teklican.Models.ModelView
{
    public class CartView
    {
        public string MaSP { get; set; }

        public int SoLuongSP { get; set; }

        public decimal Gia { get; set; }

        public string TrangThaiDH { get; set; }

        public decimal Thue { get; set; }

        public int ChietKhau { get; set; }

        public decimal ThanhTien { get; set; }

        public int Order_id { get; set; }

        public int Product_id { get; set; }

        public int id { get; set; }

        public string thongtinsp { get; set; }

        public string tenkh { get; set; }

        public string diachigiaohang { get; set; }

        public string sdtkh { get; set; }

        public Nullable<System.DateTime> ngaytaodon { get; set; }

        public Nullable<System.DateTime> ngaycapnhat { get; set; }

        public string trangthaidon { get; set; }
    }
}