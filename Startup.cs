using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DutchTreat
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //ENABLE RAZOR PAGES - 1/2
            services.AddRazorPages();

            services.AddControllersWithViews();

            services.AddTransient<IMailService, NullMailService>();
            // Support for real mail service
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

            //this turns-on generic routing inside ASP.NET Core 3.1
            app.UseRouting();

            app.UseEndpoints(cfg => {
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