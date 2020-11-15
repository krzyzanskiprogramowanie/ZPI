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


        [Display(Name = "Nazwa koordynatu")]
        public string Name { get; set; }



        [Display(Name = "Typ koordynatu")]
        public string Type { get; set; }



        [Display(Name = "Współrzędna A")]
        public int A { get; set; }


        [Display(Name = "Współrzędna B")]
        public int B { get; set; }



        [Display(Name = "Współrzędna C")]
        public int C { get; set; }


        [Display(Name = "Współrzędna D")]
        public int D { get; set; }

        public virtual int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
