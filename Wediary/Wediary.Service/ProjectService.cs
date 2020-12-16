using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Data.Models;

namespace Wediary.Service
{
    public class ProjectService : IProject
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

  

        public IEnumerable<Project> GetAll(string id)
        {
            IEnumerable<Project> listOut = _context.Projects.ToList().Where(user => user.UserId == id);
            return listOut;
        }

        public Project GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}
