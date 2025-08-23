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
            //modelBuilder.Entity<City>().HasData(
            //    new City { CityId = 1, Name = "Douala" },
            //    new City { CityId = 2, Name = "Yaoundé" },
            //    new City { CityId = 3, Name = "Garoua" },
            //    new City { CityId = 4, Name = "Bamenda" },
            //    new City { CityId = 5, Name = "Bafoussam" },
            //    new City { CityId = 6, Name = "Maroua" },
            //    new City { CityId = 7, Name = "Nkongsamba" },
            //    new City { CityId = 8, Name = "Ngaoundéré" },
            //    new City { CityId = 9, Name = "Bertoua" },
            //    new City { CityId = 10, Name = "Loum" },
            //    new City { CityId = 11, Name = "Kumba" },
            //    new City { CityId = 12, Name = "Edéa" },
            //    new City { CityId = 13, Name = "Kumbo" },
            //    new City { CityId = 14, Name = "Foumban" },
            //    new City { CityId = 15, Name = "Mbouda" },
            //    new City { CityId = 16, Name = "Dschang" },
            //    new City { CityId = 17, Name = "Limbé" },
            //    new City { CityId = 18, Name = "Ebolowa" },
            //    new City { CityId = 19, Name = "Kousséri" },
            //    new City { CityId = 20, Name = "Guider" },
            //    new City { CityId = 21, Name = "Meiganga" },
            //    new City { CityId = 22, Name = "Yagoua" },
            //    new City { CityId = 23, Name = "Mbalmayo" },
            //    new City { CityId = 24, Name = "Bafang" },
            //    new City { CityId = 25, Name = "Tiko" },
            //    new City { CityId = 26, Name = "Bafia" },
            //    new City { CityId = 27, Name = "Wum" },
            //    new City { CityId = 28, Name = "Kribi" },
            //    new City { CityId = 29, Name = "Buea" },
            //    new City { CityId = 30, Name = "Sangmélima" },
            //    new City { CityId = 31, Name = "Foumbot" },
            //    new City { CityId = 32, Name = "Bangangté" },
            //    new City { CityId = 33, Name = "Batouri" },
            //    new City { CityId = 34, Name = "Banyo" },
            //    new City { CityId = 35, Name = "Nkambé" },
            //    new City { CityId = 36, Name = "Bali" },
            //    new City { CityId = 37, Name = "Mbanga" },
            //    new City { CityId = 38, Name = "Mokolo" },
            //    new City { CityId = 39, Name = "Melong" },
            //    new City { CityId = 40, Name = "Manjo" },
            //    new City { CityId = 41, Name = "Garoua-Boulaï" },
            //    new City { CityId = 42, Name = "Mora" },
            //    new City { CityId = 43, Name = "Kaélé" },
            //    new City { CityId = 44, Name = "Tibati" },
            //    new City { CityId = 45, Name = "Ndop" },
            //    new City { CityId = 46, Name = "Akonolinga" },
            //    new City { CityId = 47, Name = "Eséka" },
            //    new City { CityId = 48, Name = "Mamfé" },
            //    new City { CityId = 49, Name = "Obala" },
            //    new City { CityId = 50, Name = "Muyuka" },
            //    new City { CityId = 51, Name = "Nanga-Eboko" },
            //    new City { CityId = 52, Name = "Abong-Mbang" },
            //    new City { CityId = 53, Name = "Fundong" },
            //    new City { CityId = 54, Name = "Nkoteng" },
            //    new City { CityId = 55, Name = "Fontem" },
            //    new City { CityId = 56, Name = "Mbandjock" },
            //    new City { CityId = 57, Name = "Touboro" },
            //    new City { CityId = 58, Name = "Ngaoundal" },
            //    new City { CityId = 59, Name = "Yokadouma" },
            //    new City { CityId = 60, Name = "Pitoa" },
            //    new City { CityId = 61, Name = "Tombel" },
            //    new City { CityId = 62, Name = "Kékem" },
            //    new City { CityId = 63, Name = "Magba" },
            //    new City { CityId = 64, Name = "Bélabo" },
            //    new City { CityId = 65, Name = "Tonga" },
            //    new City { CityId = 66, Name = "Maga" },
            //    new City { CityId = 67, Name = "Koutaba" },
            //    new City { CityId = 68, Name = "Blangoua" },
            //    new City { CityId = 69, Name = "Guidiguis" },
            //    new City { CityId = 70, Name = "Bogo" },
            //    new City { CityId = 71, Name = "Batibo" },
            //    new City { CityId = 72, Name = "Yabassi" },
            //    new City { CityId = 73, Name = "Figuil" },
            //    new City { CityId = 74, Name = "Makénéné" },
            //    new City { CityId = 75, Name = "Gazawa" },
            //    new City { CityId = 76, Name = "Tcholliré" }
            //);


        }

    }
}
