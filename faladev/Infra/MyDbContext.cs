using Microsoft.EntityFrameworkCore;
using System;

namespace Infra
{
    public class MyDbContext : DbContext
    {
        public DbSet<CustomMessage> CustomMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=faladev;user=root;password=qwerty",
                new MySqlServerVersion(new Version(8, 0, 27)),
                a => a.MigrationsAssembly("Infra"));
        }
    }
}
