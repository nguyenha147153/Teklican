using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using Teklican.Models.Entities;

namespace Teklican.Controllers
{
    
    public class HomeController : Controller
    {
        private CT25Team22Entities db = new CT25Team22Entities();
        
        public ActionResult Index()
        {
            var q = (from Product in db.Products
                     join ProductType in db.ProductTypes
                     on Product.id_ProductType equals ProductType.id_ProductType
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
                         tax = (int)Product.tax,
                         nameType = ProductType.name
                     }
                ).ToList();
            Session["listProduct"] = q;
            return View(q);
        }
        public ActionResult Details(int? id)
        {
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
            var tien = log.Accounts.Where(d => d.usernname == username && d.pwd == pass).Count();
            var users = (from pq in log.Accounts
                         join pqs in log.AccountsTypes on pq.phanquyen equals pqs.id
                         where pq.usernname == username && pq.pwd == pass
                         select new Models.ModelView.AccountView
                         {
                             id = pq.id,
                             username = pq.usernname,
                             email = pq.email,
                             address = pq.address,
                             phone = pq.phone,
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
        public ActionResult Register()
        {
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
                Models.Entities.CT25Team22Entities acc = new Models.Entities.CT25Team22Entities();
                Models.Entities.Account ac = new Models.Entities.Account();
                ac.usernname = username;
                ac.address = address;
                ac.phone = phone;
                ac.email = email;
                ac.pwd = pass;
                ac.phanquyen = 1;
                acc.Accounts.Add(ac);
                acc.SaveChanges();
                return Json("success");
            }
            catch
            {
                return Json("fail");
            }
        }
        public bool checkUsername(string username)
        {
            Models.Entities.CT25Team22Entities database = new Models.Entities.CT25Team22Entities();
            var q = database.Accounts.Where(d => d.usernname == username).Count();
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
            var q = database.Accounts.Where(d => d.phone == phone).Count();
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
            var q = database.Accounts.Where(d => d.email == email).Count();
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