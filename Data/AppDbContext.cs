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
        public DbSet<UserRole> UserRoles { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("CustomUser");
            modelBuilder.Entity<Role>()
                .ToTable("CustomRole");
            modelBuilder.Entity<UserRole>()
                .ToTable("CustomUserRole").HasKey(x => new { x.UserId, x.RoleId });


            modelBuilder.Entity<UserRole>()
            .HasOne(pt => pt.User)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(pt => pt.RoleId);


            modelBuilder.Entity<User>().Ignore(t => t.IsAuthenticated);
            modelBuilder.Entity<User>().Ignore(t => t.AuthenticationType);
            modelBuilder.Entity<User>().Ignore(t => t.Name);
        }


    }
}
