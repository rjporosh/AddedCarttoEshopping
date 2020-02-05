using Ecommerce.DatabaseContext.FluentConfiguration;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.DatabaseContext
{
    public class EcommerceDbContext : IdentityDbContext<ApplicationUser>
    {
        public long CurrentUserId { get; set; }
        public EcommerceDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductVariants> ProductVariants { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies(false)
                .UseSqlServer("Server=(local)\\sqlexpress;Database=Ecommerce; Integrated Security=true")
                .EnableSensitiveDataLogging() ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>(b =>
            {
                // Each User can have many UserClaims
                //b.HasMany(e => e.Claims)
                //    .WithOne()
                //    .HasForeignKey(uc => uc.UserId)
                //    .IsRequired();

                //// Each User can have many UserLogins
                //b.HasMany(e => e.Logins)
                //    .WithOne()
                //    .HasForeignKey(ul => ul.UserId)
                //    .IsRequired();

                //// Each User can have many UserTokens
                //b.HasMany(e => e.Tokens)
                //    .WithOne()
                //    .HasForeignKey(ut => ut.UserId)
                //    .IsRequired();

                //// Each User can have many entries in the UserRole join table
                //b.HasMany(e => e.UserRoles)
                //    .WithOne()
                //    .HasForeignKey(ur => ur.UserId)
                //    .IsRequired();
                modelBuilder.ApplyConfiguration(new ProductFluentConfiguration());
                modelBuilder.Entity<ProductOrder>().HasKey(c => new { c.ProductId, c.OrderId });

                modelBuilder.Entity<Product>()

                    .HasOne(c => c.Parent)
                    .WithMany(c => c.Childs)
                    .HasForeignKey(c => c.ParentId)
                     .IsRequired(false);

                modelBuilder.Entity<Product>()
                    .HasOne(c => c.Stocks)
                    .WithOne(c => c.Product)
                    .IsRequired(false);
                modelBuilder.Entity<Stock>()
                    .HasOne(c => c.Product)
                    .WithOne(c => c.Stocks)
                    .IsRequired(false);

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
            });
        }
    }
}

            
         
    
    
 
