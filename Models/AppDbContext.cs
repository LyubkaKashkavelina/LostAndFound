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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed categories
            modelBuilder.Entity<PetCategory>().HasData(new PetCategory { PetCategoryId = 1, CategoryName = "Lost" });
            modelBuilder.Entity<PetCategory>().HasData(new PetCategory { PetCategoryId = 2, CategoryName = "Found" });

            //seed types
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 1, TypeName = "Cats" });
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 2, TypeName = "Dogs" });
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 3, TypeName = "Birds" });
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 4, TypeName = "Large animals" });
            modelBuilder.Entity<PetType>().HasData(new PetType { PetTypeId = 5, TypeName = "Others" });

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
