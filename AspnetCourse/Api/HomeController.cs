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
        public IActionResult SaveUser()
        {
            return Content("Save");
        }

        [HttpGet]
        [Produces("application/json", "application/xml")]
        public User GetUser()
        {
            return new User{ Name="Michael", Address = "New York"};
        }
    }
}
