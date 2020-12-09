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
        public string Name { get; set; }
        public string JsonTable { get; set; }
        public string JsonGuest { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual string UserId{ get; set; }
        public virtual ApplicationUser ApplicationUser{ get; set; }
    }
}