using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using IDP_Back_End.Repository;
using Microsoft.IdentityModel.Tokens;
using IDP_Back_End.Models;
using IDP_Back_End.Repository.Implementation;
using IDP_Back_End.ChatHubs;
using IDP_Back_End.Repository.Interface;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            // Adding JWT as authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtKey.Key,
                    ValidateLifetime = true
                };
            });

            // Fixing JSON reference loops so fetching data doesn't break the site
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // Adding SignalR
            services.AddSignalR();

            services.AddMvc();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICheckItemRepository, CheckItemRepository>();

            if (_env.IsDevelopment())
            {
                services.AddDbContext<DBContext>(
                     opt => opt.UseSqlite("Data Source=DevDB.db"));
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
                    DBInit.SeedDB(ctx);
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
                app.UseHttpsRedirection();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapHub<ChatHub>("/chathub");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Chat",
                    defaults: new { controller = "Chat", action = "Index" });

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "task/{id}",
                    defaults: new { controller = "Tasks", action = "TaskById" });

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "register",
                    defaults: new { controller = "Login", action = "Register" });

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "login",
                    defaults: new { controller = "Login", action = "Index" });

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Category",
                    defaults: new { controller = "Category", action = "Index" });
            });
        }
    }
}
