using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

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
                //setup configuration
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        {
            //Removing the default configuration options 
            builder.Sources.Clear();
            //configuration may exist in different format - the order matters!!!
            builder.AddJsonFile("config.json", false, true)
                   .AddEnvironmentVariables(); 
        }
    }
}