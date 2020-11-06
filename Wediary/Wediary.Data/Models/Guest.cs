using System;
using System.Collections.Generic;
using System.Text;
using Wediary.Models;

namespace Wediary.Data.Models
{
    public class Guest
    {

        public int IdGuest { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string IfAftermath { get; set; }
        public string IfSpecialDiet { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }




    }
}
