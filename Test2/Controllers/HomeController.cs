using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Compute(string x = "0")
        {
            int result = 0;
            return View(result);
        }
    }
}
