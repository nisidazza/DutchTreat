using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        //save or read data from DutchContext
        public DutchSeeder(DutchContext ctx, IHostEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            //makes sure that the db exists
            _ctx.Database.EnsureCreated();

            //Seed the Main user - find by email
            StoreUser user = await _userManager.FindByNameAsync("ross@smith.com");

            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Ross",
                    LastName = "Smith",
                    Email = "ross@smith.com",
                    UserName = "ross@smith.com"
                };

                var result = await _userManager.CreateAsync(user, "P4s$w0rd");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder!");
                }
            }

            IEnumerable<Product> products;
            if (!_ctx.Products.Any())
            {
                //Need to create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);
            }
            else
            {
                products = _ctx.Products;
            }

            var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
            if (order != null)
            {
                order.User = user; //this update the order to be owned by the user
                order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
            }

            _ctx.SaveChanges();


        }

    }
}
