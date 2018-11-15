using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AspnetCourse.Infrastructure
{
    public class ExistsInDbConstraint : IRouteConstraint
    {
        private static string[] db = new[] { "index", "save" };
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return db.Contains(values[routeKey]?.ToString().ToLowerInvariant());
        }
    }
}
