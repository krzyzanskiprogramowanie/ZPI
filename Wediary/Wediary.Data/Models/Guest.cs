using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Wediary.Models;

namespace Wediary.Data.Models
{
    public class Guest
    {
        [Key]
        public int IdGuest { get; set; }


        public string Name { get; set; }

        public string Surname { get; set; }


        public string Role { get; set; }


        public bool IfAftermath { get; set; }



        public bool IfSpecialDiet { get; set; }

        public string DescriptionDiet { get; set; }

        public virtual string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual int InvitationId { get; set; }
        public virtual InvitationStatus InvitationStatus { get; set; }




    }
}
