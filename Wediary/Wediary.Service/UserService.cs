using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Models;

namespace Wediary.Service
{
    public class UserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
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
            return _context.ApplicationUsers;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault( user=> user.Id == id);
        }

        public async Task SetImageProfile(string id, Uri uri)
        {

            var user = GetById(id);
            user.ImageProfileUrl = uri.AbsoluteUri;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
