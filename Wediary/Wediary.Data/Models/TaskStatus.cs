using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wediary.Data.Models
{
   public class TaskStatus
    {
        [Key]
        public int IdTasksStatus { get; set; }

        public string Name { get; set; }

        public virtual List<TaskUser> TaskUsers { get; set; }
    }
}
