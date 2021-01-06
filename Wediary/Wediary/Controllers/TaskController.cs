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
            return RedirectToAction("Index", "Task", new { id = m.UserId });
        }

        public async Task<IActionResult> ChangeToActive(int id, string state)
        {
            var taskUpdate = _serviceTaskUser.GetById(id);
            taskUpdate.State = state;
            await _serviceTaskUser.Update(taskUpdate);
            return RedirectToAction("Index", "Task", new { id = taskUpdate.UserId });
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
                Unit = m.Unit,
                State = StateTaskModels.Active,
                TotalPrice = m.Quantity * m.ExpectedPrice - m.Payment,
            };
        }

        [HttpPost]
        public async Task<IActionResult> See(CoordinatesModel coordinates, string name, int id)
        {
            string CoordinatesTableJson = coordinates.CoordinatesJson;
            string CoordiatesGuest = coordinates.GuestJson;
            int x = coordinates.IdProject;
            string Name = name;
            int ID = id;
            var userId = _userManager.GetUserId(User);
            string IdUser = userId;
            var user = await _userManager.FindByIdAsync(userId);
            //var Project = _serviceProject.GetById(id);
            var changes = ReplyBuildTableJson(CoordinatesTableJson,IdUser, Name, ID,CoordiatesGuest);
            await _serviceProject.Update(changes);
            return RedirectToAction("Index", "TableManager", new { id = coordinates.UserId });
        }

        public IActionResult EditTask(int id)
        {
            var modelView = _serviceTaskUser.GetById(id);
            var outPutModel = new TaskUserModel()
            {
                Id = modelView.IdTask,
                Name = modelView.Name,
                Contractor = modelView.Contractor,
                Category = modelView.Category,
                Quantity = modelView.Quantity,
                Unit = modelView.Unit,
                ExpectedPrice = modelView.ExpectedPrice,
                Payment = modelView.Payment,
                TotalPrice = modelView.TotalPrice,
                Date = modelView.Date,
            };
            return View(outPutModel);
        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = _serviceTaskUser.GetById(id);
            await _serviceTaskUser.Delete(task);
            return RedirectToAction("Index", "Task", new { id = task.UserId });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateTask(TaskUserModel model, int id)
        {
            var actualTask = _serviceTaskUser.GetById(id);
            actualTask.Name = model.Name;
            actualTask.Quantity = model.Quantity;
            actualTask.Unit = model.Unit;
            actualTask.ExpectedPrice = model.ExpectedPrice;
            actualTask.Payment = model.Payment;
            actualTask.TotalPrice = model.Quantity * model.ExpectedPrice - model.Payment;
            actualTask.Date = model.Date;
            actualTask.Contractor = model.Contractor;
            await _serviceTaskUser.Update(actualTask);
            return RedirectToAction("Index", "Task", new { id = actualTask.UserId });
        }

        private Project ReplyBuildTableJson(string coordinates, string id, string name, int ID,string coordinatesGuest)
        {
            return new Project
            {
                UserId = id,
                JsonTable = coordinates,
                JsonGuest=coordinatesGuest,
                CreationDate=DateTime.Now,
                IdProject=ID,
                Name=name
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
