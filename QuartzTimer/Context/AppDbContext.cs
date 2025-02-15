using Microsoft.EntityFrameworkCore;
using QuartzTimer.Entities;
using System.Collections.Generic;

namespace QuartzTimer.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-141UAGI;Database=QuartzTestDb;Integrated Security=True;TrustServerCertificate=True;");
        }
        public DbSet<Student> Students { get; set; }
    }
}
