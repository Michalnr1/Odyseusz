using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Odyseusz.domain;

namespace Odyseusz.dal
{
    public class AppDbContext : DbContext
    {
        public DbSet<Produkt> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data (optional)
            modelBuilder.Entity<Produkt>().HasData(
                new Produkt { Id = 1, Name = "Apple", Price = 1.00m },
                new Produkt { Id = 2, Name = "Banana", Price = 0.50m },
                new Produkt { Id = 3, Name = "Cherry", Price = 2.00m }
            );
        }
    }
}
