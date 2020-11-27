using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Wediary.Data.Models;

namespace Wediary.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }


        public string Surname { get; set; }


        public decimal Budget { get; set; }

        public DateTime WeddingDate { get; set; }

        public string ImageProfileUrl { get; set; }
        public virtual List<TaskUser> Tasks { get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual List<Guest> Guests { get; set; }
    }
}
