using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test2.Compute;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Compute(string x = "0")
        {
            double result;
            Exception e = Expression.Check(x);
            if (e == null)
                result = Expression.Compute(x);
            else
            {
                if (e is BadBracketsException)
                    ModelState.AddModelError("x", e.Message);
                if (e is BadOperationsException)
                    ModelState.AddModelError("x", e.Message);
                if (e is BadExpressionException)
                    ModelState.AddModelError("x", e.Message);
                result = 0;
            }
                
            return View(result);
        }
    }
}
