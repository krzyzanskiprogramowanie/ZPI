using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data.Models;

namespace Wediary.Data
{
    public interface IGuest
    {
        Guest GetById(string id);
        IEnumerable<Guest> GetAll(string id);

        Task Create(Guest guest);
        Task Delete(string id);
        Task UpdateUser(string id);
    }
}
