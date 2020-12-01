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
        public DateTime CreationDate { get; set; }

        public virtual string UserId{ get; set; }
        public virtual ApplicationUser User{ get; set; }
        public virtual List<Coordinate> Coordinates { get; set; }
    }
}
