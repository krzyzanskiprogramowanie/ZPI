using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Wediary.Data.Models;

namespace Wediary.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Imie i nazwisko panny młodej jest wymagane")]
        [Display(Name = "Imie i nazwisko panny młodej")]
        [StringLength(70, ErrorMessage = "Długość znaków {0} {1} {2}",MinimumLength = 1)]
        public string BrideName  { get; set; }

        [Required(ErrorMessage = "Imie i nazwisko pana młodego jest wymagane")]
        [Display(Name = "Imie i nazwisko pana młodego")]
        [StringLength(70, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string GroomName  { get; set; }

        [Display(Name ="Budżet")]
        [Range(0, 1000000000, ErrorMessage = "{0} musi być w przedziale {1} {2}")]
        public decimal Budget { get; set; }

        public DateTime WeddingDate { get; set; }

        public string ImageProfileUrl { get; set; }
        public virtual List<TaskUser> Tasks { get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual List<Guest> Guests { get; set; }
    }
}
