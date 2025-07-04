using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vjezba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.DAL
{
    public class SecondHandManagerDbContext : DbContext
    {
        public SecondHandManagerDbContext(DbContextOptions<SecondHandManagerDbContext> options)
            : base(options)
        {

        }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ListingType> ListingTypes { get; set; }
        public DbSet<User> Users{ get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
                b.Property(t => t.LoginProvider).HasMaxLength(128);
                b.Property(t => t.Name).HasMaxLength(128);
            });

            modelBuilder.Entity<City>().HasData(new City { ID = 1, Name = "Zagreb" });
            modelBuilder.Entity<City>().HasData(new City { ID = 2, Name = "Velika Gorica" });
            modelBuilder.Entity<City>().HasData(new City { ID = 3, Name = "Vrbovsko" });

            modelBuilder.Entity<ListingType>().HasData(new ListingType { ID = 1, Name = "Odjeća", Description = "Ono što se odijeva, čime se pokriva tijelo" });
            modelBuilder.Entity<ListingType>().HasData(new ListingType { ID = 2, Name = "Tehnika", Description = "Elektronički uređaji i oprema" });
            modelBuilder.Entity<ListingType>().HasData(new ListingType { ID = 3, Name = "Namještaj", Description = "Predmeti za opremanje prostora" });
        }
    }
}
