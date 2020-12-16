using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wediary.Models.ProjectManager
{
    public class ProjectModel
    {
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string JsonTable { get; set; }
        public string JsonGuest{ get; set; }
        public DateTime CreationDate { get; set; }

    }
}
