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

        [Required(ErrorMessage = "{0} jest wymagane.")]
        [Display(Name = "Nazwa Projektu")]
        [StringLength(50, ErrorMessage = "Długość znaków {0} {1} {2}", MinimumLength = 1)]
        public string Name { get; set; }
        public string JsonTable { get; set; }
        public string JsonGuest { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual string UserId{ get; set; }
        public virtual ApplicationUser ApplicationUser{ get; set; }
    }
}