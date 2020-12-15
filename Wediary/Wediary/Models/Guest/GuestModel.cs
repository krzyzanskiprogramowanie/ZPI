using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wediary.Models.Guest
{
    public class GuestModel
    {
        public int IdGuest { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public bool IfAftermath { get; set; }
        public bool IfSpecialDiet { get; set; }
        public bool IfAccommodation { get; set; }

        public string DescriptionDiet { get; set; }
        public string InvitationStatus { get; set; }
    }
}
