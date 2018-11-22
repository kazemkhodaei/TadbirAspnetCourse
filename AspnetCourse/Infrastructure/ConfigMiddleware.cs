using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace AspnetCourse.Infrastructure
{
    public class ConfigMiddleware
    {
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseResponseCompression();
        }
    }
}
