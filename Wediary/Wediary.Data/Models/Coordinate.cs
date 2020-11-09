using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wediary.Data.Models
{
    public class Coordinate
    {

        [Key]
        public int IdCoordinates { get; set; }


        
        public string Name { get; set; }
        public string Type { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }

        public virtual int IdProject { get; set; }
        public virtual Project Project { get; set; }
    }
}
