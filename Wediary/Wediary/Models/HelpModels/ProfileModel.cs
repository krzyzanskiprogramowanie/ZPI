using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wediary.Models.HelpModels
{
    public class ProfileModel
    {
        public string BrideName { get; set; }
        public string BrideSurname { get; set; }
        public string GroomName { get; set; }
        public string GroomSurname { get; set; }
        //[RegularExpression(@"^[0-9]{9}[,][0-9]{2}$", ErrorMessage = "Podaj kwotę.")]
        public decimal Budget { get; set; }
        public DateTime WeddingDate { get; set; }
    }
}
