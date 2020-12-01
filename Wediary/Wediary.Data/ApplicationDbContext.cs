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

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Coordinate> Coordinates{ get; set; }
        public DbSet<Guest> Guests{ get; set; }
        public DbSet<Project> Projects{ get; set; }
        public DbSet<TaskUser> TaskUsers{ get; set; }
    }
}
