using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Data.Models;
using Wediary.Models;
using Wediary.Models.ProjectManager;

namespace Wediary.Controllers
{
    public class TableManagerController : Controller
    {
        private static UserManager<ApplicationUser> _userManager;
        private readonly IProject _serviceProject;
        private readonly IGuest _serviceGuest;
        private readonly IApplicationUser _serviceApplicationUser;

        public TableManagerController(IProject serviceProject, 
            IGuest serviceGuest, 
            IApplicationUser serviceApplicationUser,
            UserManager<ApplicationUser> userManager
            )
        {
            _serviceProject = serviceProject;
            _serviceGuest = serviceGuest;
            _serviceApplicationUser = serviceApplicationUser;
            _userManager = userManager;
        }


        public IActionResult Manager()
        {
            ViewData["Message"] = "Your Manager page.";

            return View();
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            IEnumerable<Project> projects = _serviceProject.GetAll(userId);

            return View(projects);
        }
        public IActionResult AddProject()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SaveChanges(ProjectModel m)
        {
            var userId = _userManager.GetUserId(User);
            string id = userId;
            var user = await _userManager.FindByIdAsync(userId);
            var changes = ReplyBuild(m, user, id);
            await _serviceProject.Create(changes);
            return RedirectToAction("Manager", "TableManager", new { id = m.IdProject });
        }

        private Project ReplyBuild(ProjectModel m, ApplicationUser user, string id)
        {
            return new Project
            {
                UserId=id,
                Name=m.Name,
                CreationDate=DateTime.Now,
                JsonTable=null,
                JsonGuest=null,
                IdProject=m.IdProject
             
            };
        }

    }
}
