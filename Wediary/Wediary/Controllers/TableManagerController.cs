﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Data.Models;
using Wediary.Models;
using Wediary.Models.ManagerTables;
using Wediary.Models.ProjectManager;

namespace Wediary.Controllers
{
    [Authorize]
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


        public IActionResult Manager(string name, int id)
        {
           var model =new CoordinatesModel
           { 
               IdProject=id,
               Name=name
            };
           
            ViewData["FieldsList"] = GetAllGuest();
            ViewData["JsonGuest"] = GetGuestFromDb(id);
            ViewData["JsonTables"] = GetTablesFromDb(id);
            return View(model);
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
            return RedirectToAction("Index", "TableManager", new { id = m.IdProject });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var project = _serviceProject.GetById(id);
            await _serviceProject.Delete(project);
            return RedirectToAction("Index", "TableManager", new { id = project.IdProject });
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

        private string GetAllGuest()
        {

            string userId = _userManager.GetUserId(User);
            IEnumerable<Guest> listOfTask = _serviceGuest.GetAll(userId);
            string tabName="";
            foreach (var item in listOfTask)
            {
                    tabName += item.Name+" "+item.Surname+" "+item.IdGuest + ", ";
            }

            return tabName;
        }

        private string GetGuestFromDb(int id)
        {
            string userId = _userManager.GetUserId(User);
            string jsonGuest = _serviceProject.getJsonGuest(userId, id);
            return jsonGuest;
        }

        private string GetTablesFromDb(int id)
        {
            string userId = _userManager.GetUserId(User);
            string jsonGuest = _serviceProject.getJsonTables(userId, id);
            return jsonGuest;
        }

    }
}
