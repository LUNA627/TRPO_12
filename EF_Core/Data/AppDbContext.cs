using EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EF_Core.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<UserInterestGroup> UserInterestGroups { get; set; }
        public DbSet<InterestGroup> InterestGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LUNA\\SQLEXPRESS;Database=UserDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>() // отношение один-к-одному
                .HasOne(s => s.Profile)
                .WithOne(ps => ps.User)
                .HasForeignKey<UserProfile>(ps => ps.UserId);

            modelBuilder.Entity<Role>() // отношение один-ко-многим
                .HasMany(g => g.Users)
                .WithOne(s => s.Role)
                .HasForeignKey(s => s.RoleId);


            modelBuilder.Entity<UserInterestGroup>()
                .HasKey(e => new { e.UserId, e.InterestGroupId });

            modelBuilder.Entity<UserInterestGroup>()
                .HasOne(e => e.User)
                .WithMany(u => u.UserInterestGroups)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<UserInterestGroup>()
                .HasOne(e => e.InterestGroup)
                .WithMany(s => s.UserInterestGroups)
                .HasForeignKey(q => q.InterestGroupId);
        }

    }
}
