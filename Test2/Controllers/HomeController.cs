using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test2.Models;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Compute(string x = "0")
        {
            double result = Expression.Compute(x);
            return View(result);
        }
    }
}
