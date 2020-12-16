using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data.Models;

namespace Wediary.Data
{
    public interface IProject
    {
        Project GetById(string id);
        IEnumerable<Project> GetAll(string id);

        Task Create(Project project);
        Task Delete(string id);
        Task UpdateUser(string id);
    }
}
