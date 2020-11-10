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
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Imię")]
        [RegularExpression(@"^[A-Za-z]+$")]
        public string Name { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwisko")]
        [RegularExpression(@"^[A-Za-z]+$")]
        public string Surname { get; set; }


        [Range(1, 10000000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Budżet")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Budget { get; set; }
       
        
        [Display(Name = "Data ślubu")]
        [DataType(DataType.Date)]
        public DateTime WeddingDate { get; set; }

        public virtual List<TaskUser> Tasks { get; set; }
        public virtual List<Project> Projects { get; set; }
        public virtual List<Guest> Guests { get; set; }
    }
}
