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
    public class UnitTest1
    {
        [TestMethod]
        public void Index()
        {
            
                // Arrange
                HomeController controller = new HomeController();

                ViewResult result = controller.Index() as ViewResult;

                Assert.IsNotNull(result);
 
        }
    }
}
