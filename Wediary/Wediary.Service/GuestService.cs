using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Data.Models;

namespace Wediary.Service
{
    public class GuestService : IGuest
    {

        private readonly ApplicationDbContext _context;

        public GuestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(Guest guest)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

   

        public IEnumerable<Guest> GetAll(string id)
        {
            IEnumerable<Guest> listOut = _context.Guests.ToList().Where(user => user.UserId == id);
            return listOut;
        }

        public Guest GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
