using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Models;

namespace Wediary.Service
{
    public class ApplicationUserServices : IApplicationUser
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(u => u.Id == id);
        }

        public Task SetImageProfile(string id, Uri uri)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
