using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using DutchTreat.Data;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //First line of code to be executed
            var host = CreateHostBuilder(args).Build();

            //instantiate and buil the seeder
            SeedDb(host);

            host.Run();
        }

        private static void SeedDb(IHost host)
        {
            //DutchSeeder contains a scoped dependency injection so we need a scopeFactory
            //the scopeFactory creates a scope for the lifetime of the request
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                //change the seeder to get the service from inside the scope
                //var seeder = host.Services.GetService<DutchSeeder>();
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.Seed();
            }

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