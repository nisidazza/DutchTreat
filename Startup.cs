using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace DutchTreat
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //configure Identity
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
                        {
                            cfg.User.RequireUniqueEmail = true;
                        })
                    .AddEntityFrameworkStores<DutchContext>();

            //manage authentication
            services.AddAuthentication()
                .AddCookie() // via Cookies
                .AddJwtBearer(cfg =>
                { //via Jwt
                    //add token validation parameters
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _config["Tokens:Issuer"],
                        ValidAudience = _config["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                    };
                });

            services.AddDbContext<DutchContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            });

            services.AddTransient<DutchSeeder>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //register DutchRepository with the appropriate service layer
            // add IDutchRepository as a service and DutchRepository as implementation
            services.AddScoped<IDutchRepository, DutchRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient(typeof(IMyLogger<>), typeof(MyLogger<>));

            //ENABLE RAZOR PAGES - 1/2
            services.AddRazorPages();

            services.AddControllersWithViews();

            services.AddTransient<IMailService, NullMailService>();

            // Support for real mail service

            services.AddMvc()
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) // env.IsEnvironment("Development") see project's properties  - Debug
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Add Error Page
                app.UseExceptionHandler("/error");
            }

            //middlewares - the order is important!
            //app.UseDefaultFiles(); with MVC we don't need html file anymore
            app.UseStaticFiles();

            app.UseNodeModules();

            //the authentication has to be call before Routing and through before the endpoints
            app.UseAuthentication();

            //this turns-on generic routing inside ASP.NET Core 3.1
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(cfg =>
            {
                //ENABLE RAZOR PAGES - 2/2
                cfg.MapRazorPages();

                cfg.MapControllerRoute("Fallback",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", action = "Index" }
                    );
            });
        }
    }
}