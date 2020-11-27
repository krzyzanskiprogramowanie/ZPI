using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Data.Models;

namespace Wediary.Service
{
    public class TaskUserService : ITaskUser
    {
        private readonly ApplicationDbContext _context;

        public TaskUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(TaskUser taskUser)
        {
            _context.Add(taskUser);
            await _context.SaveChangesAsync();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public TaskUser GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTask(string id)
        {
            throw new NotImplementedException();
        }
    }
}
