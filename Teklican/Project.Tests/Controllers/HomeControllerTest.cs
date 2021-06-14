using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Teklican;
using Teklican.Controllers;
using Teklican.Models.ModelView;
using Teklican.Models.Entities;

namespace Project.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);

        }
        //public void Login()
        //{
        //    var controller = new HomeController();
        //    var result = controller.Index() as ViewResult;
        //    // Assert
        //    var usename = result.Model as String;
        //    Assert.IsNotNull(usename);
        //    var password = result.Model as String;
        //    Assert.IsNotNull(password);
        //    var users = result.Model as List<AccountView>;
        //    var db = new CT25Team22Entities();


        //}
        [TestMethod]
        public void TestDetails()
        {
            var controller = new HomeController();
            var result0 = controller.Details(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team22Entities();
            var product = db.Products.First();
            var result1 = controller.Details(product.id_Product) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as Product;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.img, model.img);
            Assert.AreEqual(product.name, model.name);
            Assert.AreEqual(product.price, model.price);
            Assert.AreEqual(product.description, model.description);
        }


        [TestMethod]
        public void Details()
        {
            var controller = new HomeController();
            var result0 = controller.Details(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team22Entities();
            var product = db.Products.First();
            var result1 = controller.Details(product.id_Product) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as Product;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.img, model.img);
            Assert.AreEqual(product.name, model.name);
            Assert.AreEqual(product.price, model.price);
            Assert.AreEqual(product.description, model.description);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
