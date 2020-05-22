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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }

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
               .HasForeignKey(x => x.AssignmentID)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Project>()
               .HasMany(c => c.Users)
               .WithOne(e => e.Project);

            modelBuilder.Entity<Project>()
               .HasMany(c => c.Assignments)
               .WithOne(e => e.Project);

            modelBuilder.Entity<Assignment>()
               .HasMany(c => c.Comments)
               .WithOne(e => e.Assignment);
        }
    }
}
