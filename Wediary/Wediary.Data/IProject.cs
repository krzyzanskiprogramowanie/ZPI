using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data.Models;

namespace Wediary.Data
{
    public interface IProject
    {
        Project GetById(int id);
        IEnumerable<Project> GetAll(string id);

        Task Create(Project project);
        Task Delete(int id);
        Task Update(Project project);

    }
}
