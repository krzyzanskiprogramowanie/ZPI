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
        [Display(Name="Nazwa")]
        public string Name{ get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public decimal ExpectedPrice { get; set; }
        public decimal Payment { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string Contractor{ get; set; }
        public string Category { get; set; }
        public string State { get; set; }

        public virtual string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
