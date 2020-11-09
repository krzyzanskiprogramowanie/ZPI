using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wediary.Data.Models
{
    public class InvitationStatus
    {
        [Key]
        public int IdInvitationStatus { get; set; }

        public string Name { get; set; }
    }

}
