using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wediary.Models.TaskUser
{
    public class TaskUserModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public decimal ExpectedPrice { get; set; }
        public decimal Payment { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string Contractor { get; set; }
        public string Category { get; set; }
    }
}
