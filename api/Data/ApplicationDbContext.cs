using DemoShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace DemoShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Favourite> Favorites { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Favourite>(x => x.HasKey(p => new { p.AppUserId, p.ProductId }));

            builder.Entity<Favourite>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Favourites)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<Favourite>()
                .HasOne(u => u.Product)
                .WithMany(u => u.Favorites)
                .HasForeignKey(p => p.ProductId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                 new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
