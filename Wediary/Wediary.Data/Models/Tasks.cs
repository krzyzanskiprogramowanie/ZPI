using System;
using System.Collections.Generic;
using System.Text;

namespace Wediary.Data.Models
{
  public class Tasks
    {
        public int IdTasks { get; set; }
        public string Name{ get; set; }

        public decimal Budget { get; set; }

        public int Quantity { get; set; }

        public string Unit { get; set; }

        public decimal ExpectedPrice { get; set; }
        public decimal UserPrice { get; set; }
        public decimal Payment { get; set; }
        public decimal TotalPrice { get; set; }

        public DateTime Date { get; set; }


        public int TaskStatus { get; set; }

        public string Contractor{ get; set; }


        //Obce

    }
}
