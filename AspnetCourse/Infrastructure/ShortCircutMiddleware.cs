using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AspnetCourse.Infrastructure
{
    public class ShortCircutMiddleware
    {
        private readonly RequestDelegate _next;
        public ShortCircutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().ToLower() == "/home/index")
            {
                httpContext.Response.StatusCode = 404;
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }
    }
}
