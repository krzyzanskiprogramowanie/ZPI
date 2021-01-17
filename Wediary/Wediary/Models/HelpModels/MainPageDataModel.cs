using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wediary.Models.HelpModels
{
    public class MainPageDataModel
    {
        public string FullNameGroom { get; set; }
        public string  FullNameBride{ get; set; }
        public decimal Budget { get; set; }
        public int EndTasks { get; set; }
        public int ConfirmGuests { get; set; }
        public int CounterGuest { get; set; }
        public int CounterTask { get; set; }
        public DateTime WeddingDate { get; set; }
    }
}
