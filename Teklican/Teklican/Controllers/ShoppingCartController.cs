using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Teklican.Models.Entities;

namespace Teklican.Controllers
{
    public class ShoppingCartController : Controller
    {
        private CT25Team22Entities db = new CT25Team22Entities();
        private List<OrdersDetail> ShoppingCart = null;
        List<Models.ModelView.Cart> Cart = new List<Models.ModelView.Cart>();
        public ShoppingCartController() 
        {
            var session = System.Web.HttpContext.Current.Session;
            if (session["ShoppingCart"] != null)
            
                ShoppingCart = session["ShoppingCart"] as List<OrdersDetail>;
            else
            {
                ShoppingCart = new List<OrdersDetail>();
                session["ShoppingCart"] = ShoppingCart;
            }
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View(ShoppingCart);
        }

        // GET: ShoppingCart/Create
        [HttpPost]
        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Dangnhap", "home");
            }
            var productId = int.Parse(Request.Form["ProductId"]);
            var product = db.Products.Find(productId);
            var Quantity = int.Parse(Request.Form["Quantity"]);

            var price = product.price;
            var sub = (price * Quantity);
            
            if (Session["Cart"] != null)
            {
                Cart = (List<Models.ModelView.Cart>)Session["Cart"];
                var result = Cart.Where(p => p.id_product == productId).SingleOrDefault();
                if (result == null)
                {
                    Cart.Add(new Models.ModelView.Cart { id_product = product.id_Product, quantity = Quantity, sub_total = (decimal)sub });
                    Session["Cart"] = Cart;
                    return RedirectToAction("giohang", "Home");

                }
                Session["Cart"] = Cart;
                result.quantity = result.quantity + Quantity;
                result.sub_total = (decimal)(product.price * result.quantity);


            }
            else
            {
                Cart.Add(new Models.ModelView.Cart { id_product = product.id_Product, quantity = Quantity, sub_total = (decimal)sub });
                Session["Cart"] = Cart;
            }

            return RedirectToAction("giohang", "Home");
        }

        // POST: ShoppingCart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaSP,SoLuongSP,Gia,TrangThaiDH,Thue,ChietKhau,ThanhTien,Order_id,Product_id")] OrdersDetail ordersDetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.OrdersDetails.Add(ordersDetail);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Order_id = new SelectList(db.Orders, "id", "thongtinsp", ordersDetail.Order_id);
        //    ViewBag.Product_id = new SelectList(db.Products, "id_Product", "name", ordersDetail.Product_id);
        //    return View(ordersDetail);
        //}

        // GET: ShoppingCart/Edit/5
        [HttpPost]
        public ActionResult Edit(int productid, int quantity)
        {
           Cart = (List<Models.ModelView.Cart>)Session["Cart"];
            var productId = int.Parse(Request.Form["ProductId"]);
            var pro = db.Products.Where(d => d.id_Product == productid).FirstOrDefault();
            var Quantity = int.Parse(Request.Form["Quantity"]);
            var item = Cart.Find(s => s.id_product == productid);
            var newSub = pro.price * Quantity;
            if (item != null)
            {
                item.quantity = quantity;
                item.sub_total = (decimal)newSub;
            }
            return RedirectToAction("giohang", "home");
        }


        public ActionResult RemoveItem(int id)
        {
            Cart = (List<Models.ModelView.Cart>)Session["Cart"];
            Cart.RemoveAll(s => s.id_product == id);
            return RedirectToAction("giohang", "home");
        }


        // GET: ShoppingCart/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersDetail ordersDetail = db.OrdersDetails.Find(id);
            if (ordersDetail == null)
            {
                return HttpNotFound();
            }
            return View(ordersDetail);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
