using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NovaScotiaWoodworks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaScotiaWoodworks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddAuthentication(Configuration["Cookie"]).AddCookie(Configuration["Cookie"], options =>
            {
                //Shows which cookie contains the authentication security context
                options.Cookie.Name = Configuration["Cookie"];
                //Specifies the login page location to be redireted to
                options.LoginPath = "/Account/Login";
                //Specifies the access denied page location to be redireted to
                options.AccessDeniedPath = "/Account/AccessDenied";
                //Determine how long the cookie is valid for
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            });

            //Adds in a security policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly",
                    policy => policy.RequireClaim("Admin", "true"));
                options.AddPolicy("MustBeBusiness",
                    policy => policy.RequireClaim("AccountType", "Business"));
            });

            //Configure db with default connection string for local sql server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddNotyf(config => 
            { 
                config.DurationInSeconds = 3; 
                config.IsDismissable = true; 
                config.Position = NotyfPosition.BottomRight; 
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Add authentication middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseNotyf();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
