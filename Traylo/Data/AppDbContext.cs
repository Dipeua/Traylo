using Microsoft.EntityFrameworkCore;
using Traylo.Models;

namespace Traylo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<DeliveryPerson> DeliveryPeople { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockHistory> StockHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "Paulin",
                    PasswordHash = "admin",
                    Role = UserRole.Admin,
                    CityId = null
                },
                new User
                {
                    UserId = 2,
                    Username = "Dipeua",
                    PasswordHash = "admin",
                    Role = UserRole.Admin,
                    CityId = null
                },
                new User
                {
                    UserId = 3,
                    Username = "Stephanie",
                    PasswordHash = "manager",
                    Role = UserRole.Manager,
                    CityId = null
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "CARDIHIT" },
                new Product { ProductId = 2, Name = "ST HEART" },
                new Product { ProductId = 3, Name = "DIABETIC" },
                new Product { ProductId = 4, Name = "XENOPROST ACTIVE" },
                new Product { ProductId = 5, Name = "SLIMUX" },
                new Product { ProductId = 6, Name = "PROSTUROX" },
                new Product { ProductId = 7, Name = "FAST ACTIVE" },
                new Product { ProductId = 8, Name = "GLUCOZEIN" }
            );

        }

    }
}
