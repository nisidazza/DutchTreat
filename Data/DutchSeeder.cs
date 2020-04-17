using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
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

        //save or read data from DutchContext
        public DutchSeeder(DutchContext ctx, IHostEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            //makes sure that the db exists
            _ctx.Database.EnsureCreated();

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
