using Microsoft.EntityFrameworkCore;
using LimsOutillageService.Models;
using System.Collections.Generic;

namespace LimsOutillageService.Data
{
    public class OutillageContext : DbContext
    {
        public OutillageContext(DbContextOptions<OutillageContext> options)
            : base(options)
        {
        }

        public DbSet<Outillage> Outillages { get; set; }
        public DbSet<Marque> Marques { get; set; }
    }
}