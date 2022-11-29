using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teklican.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        Teklican.Models.Entities.TeklicanEntities db = new Teklican.Models.Entities.TeklicanEntities();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var q = (from or in db.Orders
                     join cus in db.Customers
                     on or.id_cus equals cus.MAKH
                     select new Models.View.OrderView
                     {
                         id = or.id,
                         id_cus = (int)or.id_cus,
                         addr_cus = cus.Address,
                         name_cus = cus.Fullname,
                         total = (decimal)or.total,
                         status = (bool)or.status,
                         createDay = (DateTime)or.create_day
                     }).ToList();
            Session["listOrder"] = q;
            return View();
        }
        public ActionResult OrderDetail(int id_order)
        {
            var q = (from ord in db.OrdersDetails
                     join pro in db.Products
                     on ord.id_product equals pro.id_Product
                     join ty in db.ProductTypes
                     on pro.id_ProductType equals ty.id_ProductType
                     where ord.id_order == id_order
                     select new Models.View.OrderDetailView
                     {
                         id = ord.id,
                         id_product = (int)ord.id_product,
                         name_product = pro.name,
                         id_order= (int)ord.id_order,
                         quantity = (int)ord.quantity,
                         price = (decimal)pro.price,
                         sub_total = (decimal)ord.sub_total,
                         type = ty.name
                     }).ToList();
            Session["listDetail"] = q;
            return View();
        }
        public ActionResult deleteOrderDetail(int id, int id_order) 
        {
            var q = db.OrdersDetails.Find(id);
            db.OrdersDetails.Remove(q);
            db.SaveChanges();
            updateTotal(id_order);
            return RedirectToAction("OrderDetail", new { id_order = id_order });
        }
        public ActionResult updateOrderDetail(int id_orderdetail)
        {
            var q = (from ord in db.OrdersDetails
                     join pro in db.Products
                     on ord.id_product equals pro.id_Product
                     join ty in db.ProductTypes
                     on pro.id_ProductType equals ty.id_ProductType
                     where ord.id == id_orderdetail
                     select new Models.View.OrderDetailView
                     {
                         id = ord.id,
                         id_order = (int)ord.id_order,
                         name_product = pro.name,
                         price = (decimal)pro.price,
                         quantity = (int)ord.quantity,
                         sub_total = (decimal)ord.sub_total,
                         type = ty.name
                     }).FirstOrDefault();
            Session["orderDetail"] = q;
            return View();
        }
        public ActionResult updateOD()
        {
            var id_orderdetail = int.Parse(Request.Form["id_orderdetail"]);
            var id_order = int.Parse(Request.Form["id_order"]);
            var quantityString = Request.Form["quantity"];
            var quantity = 0;
            if(quantityString == "")
            {
                quantity = 0;
            }
            else
            {
                quantity = int.Parse(quantityString);
            }
            var subtotal = decimal.Parse(Request.Form["sub"]);
            
            if(quantity == 0)
            {
                var a = db.OrdersDetails.Find(id_orderdetail);
                db.OrdersDetails.Remove(a);
                db.SaveChanges();
                updateTotal(id_order);
                return RedirectToAction("OrderDetail", new { id_order = id_order });
            }
            var q = db.OrdersDetails.Find(id_orderdetail);
            q.quantity = quantity;
            q.sub_total = subtotal;
            db.SaveChanges();
            updateTotal(id_order);
            return RedirectToAction("OrderDetail", new { id_order = id_order });
        }
        public void updateTotal(int id_order)
        {
            var listOrdertail = db.OrdersDetails.Where(d => d.id_order == id_order).ToList();
            decimal total = 0;
            for (var i = 0; i < listOrdertail.Count(); i++)
            {
                total += (decimal)listOrdertail[i].sub_total;
            }
            var order = db.Orders.Find(id_order);
            order.total = total;
            db.SaveChanges();
        }

        public ActionResult CompleteOrder(int id_order)
        {
            var q = db.Orders.Find(id_order);
            q.status = false;
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult SerachCustomerName(string name)
        {
            var q = (from or in db.Orders
                     join cus in db.Customers
                     on or.id_cus equals cus.MAKH
                     where cus.Fullname.Contains(name)
                     select new Models.View.OrderView
                     {
                         id = or.id,
                         id_cus = (int)or.id_cus,
                         name_cus = cus.Fullname,
                         total = (decimal)or.total,
                         status = (bool)or.status,
                         createDay = (DateTime)or.create_day
                     }).ToList();
            Session["listSearchOrder"] = q;
            return RedirectToAction("index");
        }
    }
}