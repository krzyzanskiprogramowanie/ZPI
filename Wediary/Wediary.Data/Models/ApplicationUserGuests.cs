using System;
using System.Collections.Generic;
using System.Text;
using Wediary.Models;

namespace Wediary.Data.Models
{
    public class ApplicationUserGuests
    {
        public virtual string IdUser { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual int IdGuest { get; set; }
        public virtual Guest Guest{ get; set; }
    }
}
