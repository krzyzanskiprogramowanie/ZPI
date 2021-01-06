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

        public async Task Create(Guest guest)
        {
            _context.Add(guest);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guest guest)
        {
            _context.Remove(guest);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Guest> GetAll(string id)
        {
            IEnumerable<Guest> listOut = _context.Guests.ToList().Where(user => user.UserId == id);
            return listOut;
        }

        public Guest GetById(int id)
        {
            return _context.Guests.FirstOrDefault(guest => guest.IdGuest == id);
        }

        public async Task Update(Guest guest)
        {
            _context.Guests.Update(guest);
            await _context.SaveChangesAsync();
        }
    }
}
