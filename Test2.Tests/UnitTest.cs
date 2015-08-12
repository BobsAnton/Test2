﻿using System;
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
            Assert.AreEqual(12, result.Model); 
        }
    }
}