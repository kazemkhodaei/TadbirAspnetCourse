using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        //[MyFilter(new UserRepository())]
        public IActionResult Index(IDistributedCache cache)
        {

            return Content("I ran!");
        }

        [ServiceFilter(typeof(MyFilter))]
        public IActionResult SetSession(string id, string name)
        {
            return Content("");
        }
    }

    public class MyFilter : ActionFilterAttribute
    {
        private readonly UserRepository _repository;

        public MyFilter(UserRepository repository)
        {
            _repository = repository;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}