using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspnetCourse.Infrastructure
{
    public class ContentMiddleware
    {
        private readonly RequestDelegate _next;
        public ContentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().ToLower() == "/middleware")
            {
                await httpContext.Response.WriteAsync("This is from the content middleware", Encoding.UTF8);
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }
    }
}
