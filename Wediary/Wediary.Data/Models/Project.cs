using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Wediary.Models;

namespace Wediary.Data.Models
{
    public class Project
    {
        [Key]
        public int IdProject { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwa projektu")]
        [RegularExpression(@"^[A-Za-z0-9]+$")]
        public string Name { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreationDate { get; set; } //automatycznie

        public virtual string UserId{ get; set; }
        public virtual ApplicationUser User{ get; set; }
        public virtual List<Coordinate> Coordinates { get; set; }
    }
}
