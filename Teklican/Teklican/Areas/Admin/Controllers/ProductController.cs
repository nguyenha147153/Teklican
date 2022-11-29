using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teklican.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        Teklican.Models.Entities.TeklicanEntities db = new Teklican.Models.Entities.TeklicanEntities();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var q = (from pro in db.Products
                     join ty in db.ProductTypes
                     on pro.id_ProductType equals ty.id_ProductType
                     select new Models.View.ProductView
                     {
                         id_Product = pro.id_Product,
                         name = pro.name,
                         id_ProductType = (int)pro.id_ProductType,
                         nameType = ty.name,
                         description = pro.description,
                         img = pro.img,
                         inventory = (int)pro.inventory,
                         price = (decimal)pro.price,
                         status = (bool)pro.status,
                         tax = (int)pro.tax
                     }).ToList();
            Session["listProduct"] = q;
            return View();
        }

        public ActionResult Create()
        {
            var q = db.ProductTypes.ToList();
            Session["comboboxType"] = q;
            return View();
        }
        public ActionResult Update(int id_product)
        {
            var q = db.ProductTypes.ToList();
            Session["comboboxType"] = q;

            var product = (from pro in db.Products
                           join ty in db.ProductTypes
                           on pro.id_ProductType equals ty.id_ProductType
                           where pro.id_Product == id_product
                           select new Models.View.ProductView
                           {
                               id_Product = pro.id_Product,
                               name = pro.name,
                               id_ProductType = (int)pro.id_ProductType,
                               nameType = ty.name,
                               description = pro.description,
                               img = pro.img,
                               inventory = (int)pro.inventory,
                               price = (decimal)pro.price,
                               status = (bool)pro.status,
                               tax = (int)pro.tax
                           }).FirstOrDefault();
            Session["ProductUpdate"] = product;
            return View();
        }

        public ActionResult UpdateProduct(HttpPostedFileBase file)
        {
            var id = Request.Form["id"];
            var name = Request.Form["name"];
            var price = Request.Form["price"];
            var tax = Request.Form["tax"];
            var description = Request.Form["description"];
            var type = Request.Form["type"];
            var st = Request.Form["status"];
            bool status = false;
            if (st == "on")
            {
                status = true;
            }
            var pro = db.Products.Find(int.Parse(id));
            string fileName = Request.Form["nameImage"];
            string fileNew = "";
            string _path = "";
            if (file != null)
            {
                fileNew = Path.GetFileName(file.FileName);
                _path = Path.Combine(Server.MapPath("~/images"), fileNew);
            }
            /*file.SaveAs(_path);*/

            var inventory = Request.Form["inventory"];
            if (name != "" && price != "" && tax != "" && description != "" && type != "nonne" && fileName != null)
            {

                pro.name = name;
                pro.id_ProductType = int.Parse(type);
                pro.img = fileName;
                pro.description = description;
                pro.price = decimal.Parse(price);
                pro.tax = int.Parse(tax);
                pro.status = status;
                pro.inventory = int.Parse(inventory);
                if (fileNew != pro.img)
                {
                    pro.img = fileNew;
                    db.SaveChanges();
                    file.SaveAs(_path);
                }
                else
                {
                    db.SaveChanges();
                }

                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        public ActionResult CreateProduct(HttpPostedFileBase file)
        {
            var name = Request.Form["name"];
            var price = Request.Form["price"];
            var tax = Request.Form["tax"];
            var description = Request.Form["description"];
            var type = Request.Form["type"];
            var st = Request.Form["status"];
            bool status = false;
            if (st == "on")
            {
                status = true;
            }
            string fileName = "";
            string _path = "";
            if (file != null)
            {
                fileName = Path.GetFileName(file.FileName);
                _path = Path.Combine(Server.MapPath("~/images"), fileName);
            }
            /*file.SaveAs(_path);*/

            var inventory = Request.Form["inventory"];
            if (name != "" && price != "" && tax != "" && description != "" && type != "nonne" && file != null)
            {
                Teklican.Models.Entities.Product pro = new Teklican.Models.Entities.Product();
                pro.name = name;
                pro.id_ProductType = int.Parse(type);
                pro.img = fileName;
                pro.description = description;
                pro.price = decimal.Parse(price);
                pro.tax = int.Parse(tax);
                pro.status = status;
                pro.inventory = int.Parse(inventory);
                db.Products.Add(pro);
                db.SaveChanges();
                file.SaveAs(_path);
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        public ActionResult delete(int id)
        {
            var q = db.Products.Find(id);
            db.Products.Remove(q);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult SerachProductName(string name)
        {
            var q = (from pro in db.Products
                     join ty in db.ProductTypes
                     on pro.id_ProductType equals ty.id_ProductType
                     where pro.name.Contains(name)
                     select new Models.View.ProductView
                     {
                         id_Product = pro.id_Product,
                         name = pro.name,
                         id_ProductType = (int)pro.id_ProductType,
                         nameType = ty.name,
                         description = pro.description,
                         img = pro.img,
                         inventory = (int)pro.inventory,
                         price = (decimal)pro.price,
                         status = (bool)pro.status,
                         tax = (int)pro.tax
                     }).ToList();
            Session["listSearch"] = q;
            return RedirectToAction("index");
        }
    }
}