﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspnetCourse.Infrastructure;
using AspnetCourse.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            services.AddMvc();


            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:ConnectionString"], b => b.MigrationsAssembly("AspnetCourse"));
            });

            // add authorization-requirement handler
            services.AddTransient<IAuthorizationHandler, NotAdminHandler>();
            services.AddTransient<IAuthorizationHandler, NotAdminHandler>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jwt secret code as string")),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    // add custom token validator, which will provide a CliamsPrincipal object for down stream authorization and authentication layers
                    // x.SecurityTokenValidators.Clear();
                    // x.SecurityTokenValidators.Add(new validator());
                });

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("NotAdminPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new NotAdminRequirement());
                });
            });

            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseSession();
            app.UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SeedDatabase(app.ApplicationServices);
        }

        private void SeedDatabase(IServiceProvider provider)
        {
            UserManager<IdentityUser> userManager = (UserManager<IdentityUser>)provider.GetRequiredService(typeof(UserManager<IdentityUser>));
            RoleManager<IdentityRole> roleManager = (RoleManager<IdentityRole>)provider.GetRequiredService(typeof(RoleManager<IdentityRole>));

            var identityUser = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@tadbir.com"
            };

            if (userManager.FindByNameAsync(identityUser.UserName).Result == null)
            {
                IdentityResult result = userManager.CreateAsync(identityUser, "@123123aA").Result;
            }

            if (roleManager.RoleExistsAsync("admin").Result == false)
            {
                var result = roleManager.CreateAsync(new IdentityRole("admin")).Result;
            }



            // THERE IS ANOTHER OPTION:
            /*
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<yourDBContext>();
                    DbInitializer.Seed(context);//<---Do your seeding here
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }  
            }*/

        }
    }

    /*public class validator : ISecurityTokenValidator
    {
        public bool CanValidateToken => throw new NotImplementedException();

        public int MaximumTokenSizeInBytes { get; set; } = 1024;

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            
        }
    }*/
}
