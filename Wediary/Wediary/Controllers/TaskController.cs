using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Data.Models;

namespace Wediary.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskUser _serviceTaskUser;
        private readonly IApplicationUser _serviceApplicationUser;
        public TaskController(ITaskUser taskUser, IApplicationUser applicationUser)
        {
            _serviceTaskUser = taskUser;
            _serviceApplicationUser = applicationUser;
        }

        public IActionResult SaveChanges(string id, string name)
        {
            name = "test";

            var userTaskName = _serviceApplicationUser.GetById(id);

            var task = _serviceTaskUser.Create(new TaskUser
            {
                UserId = userTaskName.Id,
                Budget = 123,
                Name = name
            }) ;


            return View(task);
        }



    }
}
