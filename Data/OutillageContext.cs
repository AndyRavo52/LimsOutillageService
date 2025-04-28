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
        public DbSet<EntreeOutillage> EntreesOutillage { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<ReformeOutillage> ReformesOutillage{ get; set; }
        public DbSet<ReportOutillage> ReportsOutillage{ get; set; }
    }
}