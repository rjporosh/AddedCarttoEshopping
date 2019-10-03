﻿using Ecommerce.DatabaseContext.FluentConfiguration;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.DatabaseContext
{
    public class EcommerceDbContext: IdentityDbContext<IdentityUser>
    {
        public long CurrentUserId { get; set; }
        public EcommerceDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductVariants> ProductVariants { get; set;  }
        public DbSet<Size> Size { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies(false)
                .UseSqlServer("Server=(local);Database=Porosh; Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductFluentConfiguration());
            modelBuilder.Entity<ProductOrder>().HasKey(c => new {c.ProductId, c.OrderId});
            modelBuilder.Entity<Product>().HasKey(c => new { c.ProductVariantsId, c.ParentId/*,c.StocksId*/ });
           // modelBuilder.Entity<Product>().HasOne(c => c.Stocks);
            modelBuilder.Entity<Product>().HasOne(c => c.ProductVariants).WithOne(c=>c.Product);
           // modelBuilder.Entity<Product>().HasOne(c => c.ProductVariantList);

            modelBuilder.Entity<ProductVariants>().HasKey(c => new { c.Id,c.ProductsId, c.SizeId});
            modelBuilder.Entity<ProductVariants>().HasOne(c => c.Product);
           // modelBuilder.Entity<ProductVariants>().HasOne(c => c.ProductList);



            modelBuilder.Entity<ProductOrder>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(pt => pt.ProductId)
                ;

            modelBuilder.Entity<ProductOrder>()
                .HasOne(pt => pt.Order)
                .WithMany(t => t.Products)
                .HasForeignKey(pt => pt.OrderId);
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.IsActive);
        }

    
    }
}
