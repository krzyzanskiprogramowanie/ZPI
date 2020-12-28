using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data.Models;

namespace Wediary.Data
{
    public interface ITaskUser
    {
        TaskUser GetById(int id);
        IEnumerable<TaskUser> GetAll(string id);

        Task Create(TaskUser taskUser);
        Task Delete(int id);
        Task Update(TaskUser task);
    }
}
