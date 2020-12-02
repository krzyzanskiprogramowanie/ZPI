using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wediary.Models;

namespace Wediary.Data
{
    public interface IApplicationUser 
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        Task SetImageProfile (string id, Uri uri);
        Task Create(ApplicationUser applicationUser);
        Task Delete(string id);
        Task UpdateUser(string id);
    }
}
