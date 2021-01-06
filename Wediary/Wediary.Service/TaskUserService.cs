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

        public async Task Delete(TaskUser task)
        {
            _context.Remove(task);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TaskUser> GetAll(string id)
        {
            IEnumerable<TaskUser> listOut = _context.TaskUsers.ToList().Where(user => user.UserId == id);
            return listOut;
        }

        public TaskUser GetById(int id)
        {
            return _context.TaskUsers.FirstOrDefault(task => task.IdTask == id);
        }

        public async Task Update(TaskUser task)
        {
            _context.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
