using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //First line of code to be executed
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}