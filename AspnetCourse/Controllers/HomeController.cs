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
        
        [HttpsOnly]
        public IActionResult Index(IDistributedCache cache)
        {

            return Content("I ran!");
        }

        [HttpsOnly]
        public IActionResult SetSession()
        {

            return Content("Set done!");
        }
    }

    public class HttpsOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.IsHttps)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}