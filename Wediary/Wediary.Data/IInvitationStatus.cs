using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wediary.Data.Models;

namespace Wediary.Data
{
    interface IInvitationStatus
    {
        InvitationStatus GetById(string id);
        IEnumerable<InvitationStatus> GetAll();

        Task Create(InvitationStatus invitationStatus);
        Task Delete(string id);
        Task UpdateUser(string id);
    }
}
