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
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);

            var prod = result.Model as List<ProductView>;
            Assert.IsNotNull(prod);

            var db = new CT25Team22Entities();
            Assert.AreEqual(db.Products.Count(), prod.Count);
        }
        public void Login()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            // Assert
            var usename = result.Model as String;
            Assert.IsNotNull(usename);
            var password = result.Model as String;
            Assert.IsNotNull(password);
            var users = result.Model as List<CustomerView>;
            var db = new CT25Team22Entities();
            
            
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
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
