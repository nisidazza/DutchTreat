using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DutchTreat.Data
{
    public class DutchContext : IdentityDbContext<StoreUser>
    {
        public DutchContext(DbContextOptions<DutchContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        //this method specifies how the mapping is going to happen between the entities and the database
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

            modelBuilder.Entity<Product>()
                    .Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                    .Property(p => p.UnitPrice)
                    .HasColumnType("decimal(18,2)");
        }
    }
}