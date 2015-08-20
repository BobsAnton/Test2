using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test2.Controllers;
using System.Web.Mvc;

namespace Test2.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestCompute()
        {
            // Тестирование метода Compute контроллера Home.
            HomeController controller = new HomeController();
            ViewResult result = controller.Compute("(2+3)*2+4/2");
            Assert.AreEqual((double)12, result.Model); 
        }

        [TestMethod]
        public void TestSpaces()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Compute("2 + 3");
            Assert.AreEqual((double)5, result.Model); 
        }

        [TestMethod]
        public void TestUnaryMinus()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Compute("-2+300");
            Assert.AreEqual((double)298, result.Model); 
        }

        [TestMethod]
        public void TestTrigonometricFunc()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Compute("cos pi + pi");
            Assert.AreEqual(2.14, result.Model); 
        }
    }
}
