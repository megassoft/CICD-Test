using Microsoft.VisualStudio.TestTools.UnitTesting;
using CICD_Test.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using CICD_Test.Models;

namespace CICD_Unit_Test
{
    [TestClass]
    public class HomeTest
    {
        readonly HomeController testHome;
        const int X = 3, Y = 2;

        public HomeTest()
        {
            testHome = new HomeController(null);
        }

        [TestMethod]
        public void TestAdd()
        {
            const int EXPECTED = 5;

            int result = testHome.Add(X, Y);

            Assert.AreEqual(EXPECTED, result);
        }

        [TestMethod]
        public void TestMultiply()
        {
            //debug 
            const int EXPECTED = 6;

            int result = testHome.Multiply(X, Y);

            Assert.AreEqual(EXPECTED, result);
        }

        [TestMethod]
        public void TestIndex()
        {
            const string EXPECTED = "Index";

            ViewResult result = (ViewResult)testHome.Index();

            Assert.AreEqual(EXPECTED, result.ViewName);
        }

        [TestMethod]
        public void TestProducts()
        {
            const string EXPECTED = "PC";

            ViewResult result = (ViewResult)testHome.Products();

            Assert.AreEqual(EXPECTED, ((Product)result.ViewData.Model).name);
        }
    }
}
