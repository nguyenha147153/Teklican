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
    public class ProductsController : Controller
    {
        private CT25Team22Entities db = new CT25Team22Entities();

        // GET: Products
        public ActionResult Index()
        {
            var prod = (from Product in db.Products
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
            return View(prod);
        }

        // GET: Products/Details/5
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.id_ProductType = new SelectList(db.ProductTypes, "id_ProductType", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Product,name,id_ProductType,price,description,status,inventory,img,tax")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_ProductType = new SelectList(db.ProductTypes, "id_ProductType", "name", product.id_ProductType);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.id_ProductType = new SelectList(db.ProductTypes, "id_ProductType", "name", product.id_ProductType);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Product,name,id_ProductType,price,description,status,inventory,img,tax")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_ProductType = new SelectList(db.ProductTypes, "id_ProductType", "name", product.id_ProductType);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
