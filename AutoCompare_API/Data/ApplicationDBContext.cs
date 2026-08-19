using AutoCompare_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AutoCompare_API.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            // this is basic configuration that is needed.  Passes the options to the base class.
        }

        //public DbSet<ProductItem> ProductItems { get; set; }  // to create ProductItems table in the database
        //public DbSet<OrderHeader> OrderHeaders { get; set; } // to create OrderHeaders table in the database
        //public DbSet<OrderDetail> OrderDetails { get; set; } // to create OrderDetails table in the database
        //public DbSet<ProductReview> ProductReviews { get; set; } // to create OrderDetails table in the database
        public DbSet<MasterService> MasterServices { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopService> ShopServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shop>()
                .HasOne(s => s.Owner)
                .WithMany()
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShopService>()
                .HasOne(ss => ss.Shop)
                .WithMany()
                .HasForeignKey(ss => ss.shopId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ShopService>()
            //    .HasOne(ss => ss.MasterService)
            //    .WithMany(ms => ms.shopServices)
            //    .HasForeignKey(ss => ss.masterServiceId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MasterService>().HasData(
                new MasterService { masterServiceId = 1, name = "Oil Change" },
                new MasterService { masterServiceId = 2, name = "Filter Change" },
                new MasterService { masterServiceId = 3, name = "Fluid Exchange" },
                new MasterService { masterServiceId = 4, name = "Tire Services" },
                new MasterService { masterServiceId = 5, name = "Filter Replacement" },
                new MasterService { masterServiceId = 6, name = "Battery Testing & Replacement" }
            );

            modelBuilder.Entity<Shop>().HasData(
                new Shop
                {
                    Id = 1,
                    Name = "Quick Lube & Tire Centre",
                    Category = "Oil Change & Tires",
                    Address = "1245 Pembina Highway",
                    City = "Winnipeg",
                    Province = "MB",
                    PostalCode = "R3T 2C1",
                    Lat = 49.8074,
                    Lng = -97.1532,
                    Phone = "204-555-0101",
                    Email = "info@quicklubetire.ca",
                    IsOpen = true,
                    WorkHours = "Mon-Fri 7:00-19:00, Sat 8:00-17:00",
                    Bio = "Fast oil changes, tire rotations, and seasonal tire swaps for all makes and models.",
                    CreatedAt = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc)
                },
                new Shop
                {
                    Id = 2,
                    Name = "Precision Auto Mechanics",
                    Category = "General Repair",
                    Address = "88 Portage Avenue",
                    City = "Winnipeg",
                    Province = "MB",
                    PostalCode = "R3T 2C1",
                    Lat = 49.8951,
                    Lng = -97.1384,
                    Phone = "204-555-0142",
                    Email = "service@precisionauto.ca",
                    IsOpen = true,
                    WorkHours = "Mon-Sat 8:00-18:00",
                    Bio = "Full-service auto repair including engine diagnostics, suspension work, and scheduled maintenance.",
                    CreatedAt = new DateTime(2025, 2, 3, 0, 0, 0, DateTimeKind.Utc)
                },
                new Shop
                {
                    Id = 3,
                    Name = "Express Brake & Alignment",
                    Category = "Brakes & Alignment",
                    Address = "502 St. Mary's Road",
                    City = "Winnipeg",
                    Province = "MB",
                    PostalCode = "R3T 2C1",
                    Lat = 49.8283,
                    Lng = -97.1105,
                    Phone = "204-555-0198",
                    Email = "bookings@expressbrake.ca",
                    IsOpen = true,
                    WorkHours = "Mon-Fri 8:00-17:30",
                    Bio = "Specialists in brake pads, rotors, calipers, and precision wheel alignment.",
                    CreatedAt = new DateTime(2025, 2, 20, 0, 0, 0, DateTimeKind.Utc)
                },
                new Shop
                {
                    Id = 4,
                    Name = "Metro Transmission Specialists",
                    Category = "Transmission",
                    Address = "3100 Regent Avenue West",
                    City = "Winnipeg",
                    Province = "MB",
                    PostalCode = "R3T 2C1",
                    Lat = 49.8958,
                    Lng = -97.0156,
                    Phone = "204-555-0167",
                    Email = "repairs@metrotrans.ca",
                    IsOpen = true,
                    WorkHours = "Mon-Fri 7:30-18:00",
                    Bio = "Transmission rebuilds, clutch repair, and drivetrain diagnostics for domestic and import vehicles.",
                    CreatedAt = new DateTime(2025, 3, 8, 0, 0, 0, DateTimeKind.Utc)
                },
                new Shop
                {
                    Id = 5,
                    Name = "Roadside Mobile Repair Co.",
                    Category = "Mobile Service",
                    Address = "Mobile — Greater Winnipeg Area",
                    City = "Winnipeg",
                    Province = "MB",
                    PostalCode = "R3T 2C1",
                    Lat = 49.8847,
                    Lng = -97.1876,
                    Phone = "204-555-0133",
                    Email = "dispatch@roadsidemobile.ca",
                    IsOpen = true,
                    WorkHours = "Daily 6:00-22:00",
                    Bio = "On-site battery boosts, flat tire changes, minor repairs, and emergency roadside assistance.",
                    CreatedAt = new DateTime(2025, 3, 22, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
