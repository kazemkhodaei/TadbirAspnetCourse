using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspnetCourse.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
