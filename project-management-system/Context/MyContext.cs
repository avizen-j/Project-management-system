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
        public DbSet<UserTask> UserTasks { get; set; }

        public MyContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.UserID).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<UserTask>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
