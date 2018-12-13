using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
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

        [HttpGet]
        public ViewResult Index()
        {
            return View(new User { Name = "a", Address = "b" });
        }


        public IActionResult SetSession(int id)
        {
            return Content((10 / id).ToString());
        }
    }

    public class MyFilter : ExceptionFilterAttribute
    {
        public MyFilter()
        {

        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DivideByZeroException)
            {
                var x = context.RouteData.Values["action"];
                context.Result = new ContentResult() { Content = "error" };
                context.HttpContext.Response.StatusCode = 403;

                context.ExceptionHandled = true;
            }
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            return base.OnExceptionAsync(context);
        }
    }
}