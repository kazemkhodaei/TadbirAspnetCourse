﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCourse.Api
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public string Index(string myVal)
        {
            return RouteData.Values["legacyUrl"].ToString();
            //return myVal;
        }

        public string Update(string myVal)
        {
            return myVal;
        }
    }
}