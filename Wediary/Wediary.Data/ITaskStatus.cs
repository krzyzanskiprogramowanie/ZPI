    using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data.Models;

namespace Wediary.Data
{
    public interface ITaskStatus
    {
        Models.TaskStatus GetById(string id);
        IEnumerable<Models.TaskStatus> GetAll();

        Task Create(Models.TaskStatus project);
        Task Delete(string id);
        Task UpdateUser(string id);
    }
}
