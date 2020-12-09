using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wediary.Data.Models;
using Wediary.Models;

namespace Wediary.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Guest>()
                .HasOne(g => g.ApplicationUser)
                .WithMany(u => u.Guests)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Project>()
            .HasOne(p => p.ApplicationUser)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<TaskUser>()
            .HasOne(t => t.ApplicationUser)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
        */
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Guest> Guests{ get; set; }
        public DbSet<Project> Projects{ get; set; }
        public DbSet<TaskUser> TaskUsers{ get; set; }
    }
}
