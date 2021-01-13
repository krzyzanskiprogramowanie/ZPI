using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Wediary.Models;

namespace Wediary.Data.Models
{
    public class TaskUser
    {
        [Key]
        public int IdTask { get; set; }

        [Required]
        [Display(Name = "Nazwa zadania")]
        [StringLength(50, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name = "Wykonawca")]
        [StringLength(50, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string Contractor { get; set; }

        [Display(Name = "Kategoria")]
        [StringLength(50, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string Category { get; set; }

        [Required(ErrorMessage = "{0} jest wymagana")]
        [Display(Name = "Ilość")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} musi być w przedziale")]
        public int Quantity { get; set; }

        [Display(Name = "Jednostka")]
        [StringLength(50, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string Unit { get; set; }

        [Required(ErrorMessage = "{0} jest wymagany")]
        [Display(Name = "Cena oczekiwana")]
        [Range(0, 1000000000, ErrorMessage = "{0} musi być w przedziale {1} {2}")]
        public decimal ExpectedPrice { get; set; }

        [Required(ErrorMessage = "{0} jest wymagana")]
        [Display(Name = "Zaliczka")]
        [Range(0, 1000000000, ErrorMessage = "{0} musi być w przedziale {1} {2}")]
        public decimal Payment { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }


        public virtual string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
