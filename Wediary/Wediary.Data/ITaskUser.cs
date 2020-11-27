using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data.Models;

namespace Wediary.Data
{
    public interface ITaskUser
    {
        TaskUser GetById(string id);
        IEnumerable<TaskUser> GetAll();

        Task Create(TaskUser taskUser);
        Task Delete(string id);
        Task UpdateTask(string id);
    }
}
