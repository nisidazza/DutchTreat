using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchContext: DbContext
    {
        public DutchContext(DbContextOptions<DutchContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        //this method specify how the mapping is going to happen between the entities and the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Example:
            //modelBuilder.Entity<Product>()
            //    .Property(p => p.Title)
            //    .HasMaxLength(50);

            modelBuilder.Entity<Order>()
                //add default or seeded data to our model - 
                .HasData(new Order() // EF Core 2.1 introduce HasData - good for a limit number of data
                {
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    OrderNumber = "12345"
                });
        }
    }
}
