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
        public User SaveUser([FromBody] User user)
        {
            return user;
        }

        [AcceptVerbs("PUT", "PATCH")]
        public JsonResult GetUser()
        {
            return Json(new User{ Name="Michael", Address = "New York"});
        }
    }
}
