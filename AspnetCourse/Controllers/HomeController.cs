using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCourse.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCourse.Controllers
{
    //[MiddlewareFilter(typeof(ConfigMiddleware))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}