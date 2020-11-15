using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data.Models;

namespace Wediary.Data
{
    public interface ICoordinate
    {
        Coordinate GetById(string id);
        IEnumerable<Coordinate> GetAll();

        Task Create(Coordinate applicationUser);
        Task Delete(string id);
        Task UpdateUser(string id);
    }
}
