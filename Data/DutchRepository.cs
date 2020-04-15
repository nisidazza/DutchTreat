using DutchTreat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;

        //inject DutchContext
        public DutchRepository(DutchContext ctx)
        {
            _ctx = ctx;
        }

        //get a list of all the products
        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
        }

        //get a list of products by category
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                    .Where(p => p.Category == category)
                    .ToList();
        }
    }
}
