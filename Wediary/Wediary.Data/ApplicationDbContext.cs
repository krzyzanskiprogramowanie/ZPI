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
        DbSet<Category> Categories{ get; set; }
        DbSet<Coordinate> Coordinates{ get; set; }
        DbSet<Guest> Guests{ get; set; }
        DbSet<InvitationStatus> InvitationStatuses{ get; set; }
        DbSet<Project> Projects{ get; set; }
        DbSet<Models.TaskStatus> TaskStatuses{ get; set; }
        DbSet<TaskUser> TaskUsers{ get; set; }

     
    }
}
