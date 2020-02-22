using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CustomProvider.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("CustomUser");
            modelBuilder.Entity<Role>()
                .ToTable("CustomUserRole");

            modelBuilder.Entity<User>().Ignore(t => t.NormalizedUserName);
            modelBuilder.Entity<User>().Ignore(t => t.IsAuthenticated);
            modelBuilder.Entity<User>().Ignore(t => t.AuthenticationType);
            modelBuilder.Entity<User>().Ignore(t => t.Name);
        }


    }
}
