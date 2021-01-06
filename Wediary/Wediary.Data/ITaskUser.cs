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
        Task Delete(TaskUser task);
        Task Update(TaskUser task);
    }
}
