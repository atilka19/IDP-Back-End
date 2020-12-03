using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using IDP_Back_End.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using IDP_Back_End.Models;

namespace IDP_Back_End
{
    public class Startup
    {
        public IConfiguration _conf { get; }

        private IWebHostEnvironment _env { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            _conf = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Adding JWT as authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Adding our secret and validation parameters
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = JwtKey.Key,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });

            services.AddScoped<IUserRepository, UserRepository>();

            if (_env.IsDevelopment())
            {
                services.AddDbContext<DBContext>(
                     opt => opt.UseSqlite("Data Source=BiCDatabase.db"));
            }
            else if (_env.IsProduction())
            {
                services.AddDbContext<DBContext>(
                    opt => opt
                        .UseSqlServer(_conf.GetConnectionString("defaultConnection")));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (IServiceScope scope = app.ApplicationServices.CreateScope())
                {
                    DBContext ctx = scope.ServiceProvider.GetService<DBContext>();
                    ctx.Database.EnsureCreated();
                    /*
                     * TODO Create File to Seed DB with Data for testing
                    DBInit.SeedDB(ctx);
                    */
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<DBContext>();
                    ctx.Database.EnsureCreated();
                }
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
