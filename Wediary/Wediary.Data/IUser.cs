using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wediary.Models;

namespace Wediary.Data
{
    public interface IUser //zmien nazwe 
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        Task Create(ApplicationUser applicationUser);
        Task Delete(string id);
        Task UpdateUser(string id);
        //xa
        //xa

    }
}
