using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using restaurantWebApp.Data.Configration;
using restaurantWebApp_DAL.Models;


namespace restaurantWebApp_DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Meal>? Meals { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<Order>? Orders { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cart>().HasMany(m => m.Meals).WithMany(c=>c.Carts);
            builder.Entity<Order>().HasMany(m => m.Meals).WithMany(o=>o.Orders);
            builder.Entity<Customer>().HasMany(o => o.OrderList);
            //builder.ApplyConfiguration(new RoleConfigration());
            base.OnModelCreating(builder);
        }
    }
}