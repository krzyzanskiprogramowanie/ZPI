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


        [Required]
        [StringLength(1000, MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Rola")]
        [RegularExpression(@"^[A-Za-z0-9]+$")]
        public string Role { get; set; }


        [Display(Name = "Czy zostaje na poprawinach ?")]
        public bool IfAftermath { get; set; }



        [Display(Name = "Czy specjalna dieta?")]
        public bool IfSpecialDiet { get; set; }



        [StringLength(1000, MinimumLength = 2)]
        [DataType(DataType.MultilineText)]
        [RegularExpression(@"^[A-Za-z0-9]+$")]
        [Display(Name = "Opis diety")]
        public string DescriptionDiet { get; set; }

        public virtual string IdApplicationUser { get; set; }

        
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual InvitationStatus InvitationStatus { get; set; }




    }
}
