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

        DbSet<ApplicationUser> applicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUserGuests>()
                .HasKey(ag => new { ag.IdUser, ag.IdGuest });
            modelBuilder.Entity<ApplicationUserGuests>()
                .HasOne(ag => ag.User)
                .WithMany(u => (IEnumerable<ApplicationUserGuests>)u.Guests)
                .HasForeignKey(ag => ag.IdGuest);
            modelBuilder.Entity<ApplicationUserGuests>()
                .HasOne(ag => ag.Guest)
                .WithMany(g => (IEnumerable<ApplicationUserGuests>)g.ApplicationUsers);
        }
    }
}
