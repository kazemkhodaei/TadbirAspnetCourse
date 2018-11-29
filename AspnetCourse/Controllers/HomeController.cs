using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCourse.Infrastructure;
using AspnetCourse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace AspnetCourse.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        [MyFilter]
        public IActionResult Index(IDistributedCache cache)
        {

            return Content("I ran!");
        }

        [MyFilter]
        public IActionResult SetSession(string id, string name)
        {

            return Content("Set done!");
        }
    }

    public class MyFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if ((string)context.ActionArguments["id"] == "1")
            {
                context.Result = new ContentResult() { Content = "1" };
            }
        }
    }
}