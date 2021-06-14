using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Teklican.Controllers;
using Teklican.Models.ModelView;
using Teklican.Models.Entities;

namespace Project.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);

            var prod = result.Model as List<ProductView>;
            Assert.IsNotNull(prod);

            var db = new CT25Team22Entities();
            Assert.AreEqual(db.Products.Count(), prod.Count);
        }
    }
}
