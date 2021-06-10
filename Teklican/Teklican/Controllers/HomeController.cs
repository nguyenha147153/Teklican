using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using Teklican.Models.Entities;
using System.Data.Entity.Validation;

namespace Teklican.Controllers
{
    
    public class HomeController : Controller
    {
        /*private CT25Team22Entities db = new CT25Team22Entities();*/
        
        public ActionResult Index()
        {
            CT25Team22Entities db = new CT25Team22Entities();
            var q = (from Product in db.Products
                     join ProductType in db.ProductTypes
                     on Product.id_ProductType equals ProductType.id_ProductType
                     select new Models.ModelView.ProductView
                     {
                         id_Product = Product.id_Product,
                         name = Product.name,
                         id_ProductType = (int)Product.id_ProductType,
                         price = (decimal)Product.price,
                         description = Product.description,
                         status = (bool)Product.status,
                         inventory = (int)Product.inventory,
                         img = Product.img,
                         tax = (int)Product.tax,
                         nameType = ProductType.name
                     }
                ).ToList();
            Session["listProduct"] = q;
            return View(q);
        }
        public ActionResult Details(int? id)
        {
            CT25Team22Entities db = new CT25Team22Entities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Apple()
        {
            CT25Team22Entities db = new CT25Team22Entities();
            var q = (from Product in db.Products
                     select new Models.ModelView.ProductView
                     {
                         id_Product = Product.id_Product,
                         name = Product.name,
                         id_ProductType = (int)Product.id_ProductType,
                         price = (int)Product.price,
                         description = Product.description,
                         status = (bool)Product.status,
                         inventory = (int)Product.inventory,
                         img = Product.img,
                         tax = (int)Product.tax
                     }).ToList();
            if (Session["listProduct"] == null)
            {
                Session["listProduct"] = q;
            }
            return View();
        }
        public ActionResult Samsung()
        {
            return View();
        }
        public ActionResult lienhe()
        {
            return View();
        }
        public ActionResult tintuc()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Product()
        {
            CT25Team22Entities db = new CT25Team22Entities();
            var q = (from Product in db.Products
                     select new Models.ModelView.ProductView
                     {
                         id_Product = Product.id_Product,
                         name = Product.name,
                         id_ProductType = (int)Product.id_ProductType,
                         price = (int)Product.price,
                         description = Product.description,
                         status = (bool)Product.status,
                         inventory = (int)Product.inventory,
                         img = Product.img,
                         tax = (int)Product.tax
                     }).ToList();
            if(Session["listProduct"] == null)
            {
                Session["listProduct"] = q;
            }
            
            return View();
        }
        public ActionResult Search()
        {
            CT25Team22Entities db = new CT25Team22Entities();
            var name = Request.Form["search"];
            var p = (from Product in db.Products
                     where Product.name.Contains(name)
                     select new Models.ModelView.ProductView
                     {
                         id_Product = Product.id_Product,
                         name = Product.name,
                         id_ProductType = (int)Product.id_ProductType,
                         price = (int)Product.price,
                         description = Product.description,
                         status = (bool)Product.status,
                         inventory = (int)Product.inventory,
                         img = Product.img,
                         tax = (int)Product.tax
                     }).ToList();
            Session["listProduct"] = p;
            ViewBag.listProduct = p;
            TempData["listProduct"] = p;
            return RedirectToAction("Product");
        }
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult Login()
        {
            var username = Request.Form["uname"];
            var pass = Request.Form["password"];
            Models.Entities.CT25Team22Entities log = new Models.Entities.CT25Team22Entities();
            var tien = log.Customers.Where(d => d.username == username && d.Password == pass).Count();
            var users = (from pq in log.Customers
                         join pqs in log.AccountsTypes on pq.phanquyen equals pqs.id
                         where pq.username == username && pq.Password == pass
                         select new Models.ModelView.CustomerView
                         {
                             MAKH = pq.MAKH,
                             Fullname = pq.Fullname,
                             username = pq.username,
                             Email = pq.Email,
                             Address = pq.Address,
                             Phone = pq.Phone,
                             nametype = pqs.name
                         }).FirstOrDefault();
            if (users != null)
            {
                if (users.nametype != "" && users.nametype == "admin")
                {
                    Session["username"] = users;
                    return Json("Admin");
                }
                if (users.nametype != "" && users.nametype == "user")
                {
                    Session["username"] = users;
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }
            else
            {
                return Json("Fail");
            }
        }
        public ActionResult giohang()
        {
            if(Session["username"] == null)
            {
                return RedirectToAction("Dangnhap");
            }
            return View();
        }

        public ActionResult Payment()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Dangnhap");
            }
            Models.Entities.CT25Team22Entities db = new CT25Team22Entities();

            var listCart = (List<Models.ModelView.Cart>)Session["Cart"];
            decimal total = 0;
            for (var i = 0; i < listCart.Count; i++)
            {
                total += listCart[i].sub_total;
            }
            var customer = (Models.ModelView.CustomerView)Session["username"];
            Order order = new Order();
            order.id_cus = customer.MAKH;
            order.total = total;
            order.status = true;
            db.SaveChanges();

            var or = db.Orders.Add(order);
            OrdersDetail orderDetail = new OrdersDetail();
            for (var i = 0; i < listCart.Count; i++)
            {
                orderDetail.id_order = or.id;
                orderDetail.id_product = listCart[i].id_product;
                orderDetail.sub_total = listCart[i].sub_total;
                orderDetail.quantity = listCart[i].quantity;
                db.OrdersDetails.Add(orderDetail);
                db.SaveChanges();
            }

            return View();
        }

        public ActionResult checkout()
        {

            return View();
        }
        public ActionResult Register()
        {
            var fullname = Request.Form["fullname"];
            var username = Request.Form["username"];
            if (checkUsername(username) == false)
            {
                return Json("username already exists");
            }
            var address = Request.Form["address"];
            var phone = Request.Form["phone"];
            if (checkPhone(phone) == false)
            {
                return Json("phone already exists");
            }
            var email = Request.Form["email"];
            if (checkEmail(email) == false)
            {
                return Json("email already exists");
            }
            var pass = Request.Form["pass"];
            try
            {
                Models.Entities.CT25Team22Entities db = new CT25Team22Entities();
                Customer ac = new Customer();
                ac.Fullname = fullname;
                ac.username = username;
                ac.Address = address;
                ac.Phone = phone;
                ac.Email = email;
                ac.Password = pass;
                ac.phanquyen = 1;
                db.Customers.Add(ac);
                db.SaveChanges();
                return Json("success");
            }
            catch (DbEntityValidationException e)
            {
                return Json("fail");
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public bool checkUsername(string username)
        {
            Models.Entities.CT25Team22Entities database = new Models.Entities.CT25Team22Entities();
            var q = database.Customers.Where(d => d.username == username).Count();
            if(q == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkPhone(string phone)
        {
            Models.Entities.CT25Team22Entities database = new Models.Entities.CT25Team22Entities();
            var q = database.Customers.Where(d => d.Phone == phone).Count();
            if (q == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkEmail(string email)
        {
            Models.Entities.CT25Team22Entities database = new Models.Entities.CT25Team22Entities();
            var q = database.Customers.Where(d => d.Email == email).Count();
            if (q == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ActionResult UserProfile()
        {
            return View();
        }
        public ActionResult Changepwd()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index");
        }
    }
}