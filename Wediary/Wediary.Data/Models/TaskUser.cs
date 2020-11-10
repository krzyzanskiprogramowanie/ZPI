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
        [StringLength(50, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwa zadania")]
        [RegularExpression(@"^[A-Za-z0-9]+$")]
        public string Name{ get; set; }


        [Range(1, 10000000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Budżet")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Budget { get; set; }


        [Range(0, 10000000)]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        [Display(Name = "Jednostka")]
        [RegularExpression(@"^[A-Za-z]+$")]
        [DataType(DataType.Text)]
        public string Unit { get; set; }



        [Range(1, 10000000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Oczekiwana cena")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ExpectedPrice { get; set; }



        [Range(1, 10000000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Cena rzeczywista")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UserPrice { get; set; }



        [Range(1, 10000000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Zaliczka")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Payment { get; set; }



        [Range(1, 10000000)]
        [DataType(DataType.Currency)]
        [Display(Name = "Cena całkowita")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }


        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }



        [Required]
        [StringLength(50, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Wykonawca")]
        [RegularExpression(@"^[A-Za-z0-9]+$")]
        public string Contractor{ get; set; }



        public virtual TaskStatus TaskStatus{ get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual string IdUser { get; set; }

    }
}
