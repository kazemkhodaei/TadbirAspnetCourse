using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspnetCourse.Models;
using Microsoft.AspNetCore.Mvc.Razor;

namespace AspnetCourse.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public string Index()
        {
            Task.Delay(2000).Wait();
            return "Finished";
        }

        [HttpPost]
        [Consumes("application/json")]
        public string SaveUser([FromBody] User user)
        {
            return Url.Action(nameof(GetUser1));
        }

        [AcceptVerbs("PUT", "PATCH")]
        public JsonResult GetUser1()
        {
            return Json(new User{ Name="Michael", Address = "New York"});
        }
    }
}
