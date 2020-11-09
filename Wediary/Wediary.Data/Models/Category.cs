using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wediary.Data.Models
{
   public class Category
    {
        [Key]
        public int IdCategory { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwa Kategorii")]
        [RegularExpression(@"^[A-Za-z0-9]+$")]
        public string Name { get; set; }

        public virtual TaskUser Task{ get; set; }
        public virtual int IdTask { get; set; }
    }
}
