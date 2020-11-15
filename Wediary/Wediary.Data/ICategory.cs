using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data.Models;

namespace Wediary.Data
{
   public interface ICategory
    {

        Category GetById(string id);
        IEnumerable<Category> GetAll();

        Task Create(Category category);
        Task Delete(int id);
        Task UpdateCategory(int id);
    }
}
