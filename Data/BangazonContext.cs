using Microsoft.EntityFrameworkCore;
using Bangazon.Models;

namespace Bangazon.Data
{
    public class BangazonContext : DbContext
    {
      //Here's the interface between the controllers and the database
        public BangazonContext(DbContextOptions<BangazonContext> options)
            : base(options)
        { }
        //What tables do we want to interact with? Those are called DbSets (database sets)
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<LineItem> LineItem { get; set; }

        //OnModelCreating is the name of the method that will create default values 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //This is the directive to the database to execute this function itself and put the date created in the database. We have to write these when using default values. 
            modelBuilder.Entity<Customer>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
            
            modelBuilder.Entity<Customer>()
                .Property(b => b.LastUpdated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

            modelBuilder.Entity<Order>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

            modelBuilder.Entity<PaymentType>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
            
            modelBuilder.Entity<Product>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
        }

    }

}