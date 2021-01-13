using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Wediary.Models;

namespace Wediary.Data.Models
{
    public class Guest
    {
        [Key]
        public int IdGuest { get; set; }

        [Required(ErrorMessage = "Imie gościa jest wymagane")]
        [Display(Name = "Imie gościa")]
        [StringLength(70, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko gościa jest wymagane")]
        [Display(Name = "Nazwisko gościa")]
        [StringLength(70, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string Surname { get; set; }

        [Display(Name = "Rola")]
        [StringLength(50, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string Role { get; set; }


        public bool IfAftermath { get; set; }
        public bool IfSpecialDiet { get; set; }
        public bool IfAccommodation { get; set; }

        [Display(Name = "Opis diety")]
        [StringLength(70, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string DescriptionDiet { get; set; }
        public string InvitationStatus { get; set; }

        public virtual string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
