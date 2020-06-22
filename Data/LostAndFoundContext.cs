using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LostAndFound.Models;

namespace LostAndFound.Data
{
    public class LostAndFoundContext : DbContext
    {
        public LostAndFoundContext (DbContextOptions<LostAndFoundContext> options)
            : base(options)
        {
        }

        public DbSet<LostAndFound.Models.Ad> Ad { get; set; }
    }
}
