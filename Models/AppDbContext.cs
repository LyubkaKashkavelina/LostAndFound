using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<PetCategory> PetCategories { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Region> Regions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed categories
            modelBuilder.Entity<PetCategory>().HasData(new PetCategory { PetCategoryId = 1, CategoryName = "Изгубени" });
            modelBuilder.Entity<PetCategory>().HasData(new PetCategory { PetCategoryId = 2, CategoryName = "Намерени" });

            //seed types
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 1, TypeName = "Cats" });
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 2, TypeName = "Dogs" });
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 3, TypeName = "Birds" });
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 4, TypeName = "Large animals" });
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 5, TypeName = "Others" });

            //seed regions
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 1, RegionName = "Blagoevgrad" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 2, RegionName = "Burgas" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 3, RegionName = "Dobrich" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 4, RegionName = "Gabrovo" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 5, RegionName = "Haskovo" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 6, RegionName = "Kardzhali" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 7, RegionName = "Kyustendil" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 8, RegionName = "Lovech" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 9, RegionName = "Montana" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 10, RegionName = "Pazardzhik" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 11, RegionName = "Pernik" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 12, RegionName = "Pleven" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 13, RegionName = "Plovdiv" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 14, RegionName = "Razgrad" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 15, RegionName = "Ruse" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 16, RegionName = "Shumen" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 17, RegionName = "Silistra" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 18, RegionName = "Sliven" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 19, RegionName = "Smolyan" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 20, RegionName = "Sofia City" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 21, RegionName = "Sofia (province)" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 22, RegionName = "Stara Zagora" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 23, RegionName = "Targovishte" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 24, RegionName = "Varna" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 25, RegionName = "Veliko Tarnovo" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 26, RegionName = "Vidin" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 27, RegionName = "Vratsa" });
            modelBuilder.Entity<Region>().HasData(new Region { RegionId = 28, RegionName = "Yambol" });

            //seed pets
            //modelBuilder.Entity<Ad>()
            //    .HasData(new Ad()
            //    {
            //        AddId = 1,
            //        PetName = "Goshko",
            //        Location = "Sofia",
            //        AdDescription = "ff",
            //        IsPopular = true,
            //        PetCategoryId = 2,
            //    });

        }
    }
}
