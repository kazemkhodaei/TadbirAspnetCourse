using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspnetCourse.Models;

namespace AspnetCourse.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
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
