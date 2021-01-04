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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> GetAll(string id)
        {
            IEnumerable<Project> listOut = _context.Projects.ToList().Where(user => user.UserId == id);
            return listOut;
        }

        public Project GetById(int id)
        {

            return _context.Projects.FirstOrDefault(project => project.IdProject == id);
        }

        public string getJsonGuest(string id, int projectId)
        {
            var findProject =  _context.Projects.FirstOrDefault(project => project.UserId == id && project.IdProject == projectId);
            var findJson =  findProject.JsonGuest;
            return findJson;
        }

        public string getJsonTables(string id, int projectId)
        {
            var findProject = _context.Projects.FirstOrDefault(project => project.UserId == id && project.IdProject == projectId);
            var findTables = findProject.JsonTable;
            return findTables;
        }

        public  async Task Update(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }

    }
}
