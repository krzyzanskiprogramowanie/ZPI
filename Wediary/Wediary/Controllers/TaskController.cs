using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Data.Models;
using Wediary.Models;
using Wediary.Models.HelpModels;
using Wediary.Models.ManagerTables;
using Wediary.Models.TaskUser;

namespace Wediary.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskUser _serviceTaskUser;
        private readonly IApplicationUser _serviceApplicationUser;
        private static UserManager<ApplicationUser> _userManager;
        private readonly IProject _serviceProject;
        private readonly IGuest _serviceGuest;


        public TaskController(ITaskUser serviceTaskUser, IApplicationUser applicationUser, 
            UserManager<ApplicationUser> userManager,
            IProject serviceProject,IGuest serviceGuest
            )
        {
            _serviceTaskUser = serviceTaskUser;
            _serviceApplicationUser = applicationUser;
            _userManager = userManager;
            _serviceProject = serviceProject;
            _serviceGuest = serviceGuest;
        }
        


        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            IEnumerable<TaskUser> listOfTask = _serviceTaskUser.GetAll(userId);
            return View(listOfTask);
        }

        [HttpPost]
        public async Task<IActionResult> SaveChanges(TaskUserModel m)
        {
            var userId = _userManager.GetUserId(User);
            string id = userId;
            var user = await _userManager.FindByIdAsync(userId);
            var changes = ReplyBuild(m, user, id);
            await _serviceTaskUser.Create(changes);
            return RedirectToAction("Index", "Home", new { id = m.UserId });
        }

        private TaskUser ReplyBuild(TaskUserModel m, ApplicationUser user, string id)
        {
            return new TaskUser
            {
                UserId = id,
                Category = m.Category,
                Contractor = m.Contractor,
                Date = m.Date,
                ExpectedPrice = m.ExpectedPrice,
                Name = m.Name,
                Payment = m.Payment,
                Quantity = m.Quantity,
                Status = m.Status,
                TotalPrice = m.TotalPrice,
                Unit = m.Unit,
            };
        }

        [HttpPost]
        public async Task<IActionResult> See(CoordinatesModel coordinates)
        {
            string CoordinatesTableJson = coordinates.CoordinatesJson;
            var userId = _userManager.GetUserId(User);
            string id = userId;
            var user = await _userManager.FindByIdAsync(userId);
            var changes = ReplyBuildTableJson(CoordinatesTableJson, user, id);
            await _serviceProject.Create(changes);
            return RedirectToAction("Manager", "Home", new { id = coordinates.UserId });
        }

        private Project ReplyBuildTableJson(string coordinates, ApplicationUser user, string id)
        {
            return new Project
            {
                UserId = id,
                JsonTable = coordinates,
                CreationDate=DateTime.Now
            };
        }

        public IActionResult ParseToJson()
        {
            var userID = _userManager.GetUserId(User);
            var guest = _serviceGuest.GetAll(userID);
            JsonFile testJson = new JsonFile();
            testJson.valueJson = JsonConvert.SerializeObject(guest);
            return View(testJson); //widok bez zwracania
        }
    }
}
