using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<TaskUser> GetAll(string id)
        {
            IEnumerable<TaskUser> listOut = _context.TaskUsers.ToList().Where(user => user.UserId == id);
            return listOut;
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
