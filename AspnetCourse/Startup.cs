using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspnetCourse.Controllers;
using AspnetCourse.Infrastructure;
using AspnetCourse.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AspnetCourse
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<UserRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpContextAccessor();

            
            services.AddSession();
            services.AddScoped<User>(sp =>
            {
                ISession session = sp.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
                User user = JsonConvert.DeserializeObject<User>(session.GetString("User")) ?? new User();

                return user;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles(new StaticFileOptions()
            {
                // use the following tar-gz conversion tool to convert the files to gzip
                // https://www.online-convert.com/result/f87c374d-1fcd-4440-88b6-a8f1452049ad
                OnPrepareResponse = context =>
                {
                    if (context.File.Name.EndsWith("js.gz"))
                    {
                        context.Context.Response.Headers.Add("Content-Encoding", "gzip");
                        context.Context.Response.Headers["Content-Type"] = "application/javascript";
                    }
                }
            });

            app.UseSession();
            app.UseMvcWithDefaultRoute();

        }
    }
}
