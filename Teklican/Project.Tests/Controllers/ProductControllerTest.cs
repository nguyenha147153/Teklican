using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Teklican.Controllers;
using Teklican.Areas.Admin.Controllers;
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
        [TestClass]
        public class OrdersControllerTest
        {
            [TestMethod]
            public void TestSearch()
            {
                var db = new CT25Team22Entities();
                var orders = db.Orders.ToList();
                var keyword = orders.First().sdtkh.Split().First();
                orders = orders.Where(o => o.sdtkh.ToLower().Contains(keyword.ToLower())).ToList();

                var controller = new OrdersController();
                var result = controller.Search(keyword) as ViewResult;
                Assert.IsNotNull(result);

                var model = result.Model as List<Order>;
                Assert.IsNotNull(model);

                Assert.AreEqual("Index", result.ViewName);
                Assert.AreEqual(orders.Count(), model.Count);
                Assert.AreEqual(keyword, result.ViewData["keyword"]);
            }
        }
    }
}
}
