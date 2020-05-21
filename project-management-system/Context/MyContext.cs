using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Context
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public MyContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignmentUser>().HasKey(k => new { k.AssignmentID, k.UserID });

            modelBuilder.Entity<AssignmentUser>()
                .HasOne(x => x.User)
                .WithMany(x => x.AssignmentUsers)
                .HasForeignKey(x => x.UserID);

            modelBuilder.Entity<AssignmentUser>()
               .HasOne(x => x.Assignment)
               .WithMany(x => x.AssignmentUsers)
               .HasForeignKey(x => x.AssignmentID);

            modelBuilder.Entity<Project>()
               .HasMany(c => c.Users)
               .WithOne(e => e.Project);

            modelBuilder.Entity<Project>()
               .HasMany(c => c.Assignments)
               .WithOne(e => e.Project)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
