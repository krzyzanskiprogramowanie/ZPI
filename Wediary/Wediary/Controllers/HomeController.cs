using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wediary.Data;
using Wediary.Data.Models;
using Wediary.Models;
using Wediary.Models.Guest;
using Wediary.Models.HelpModels;
using Wediary.Service;

namespace Wediary.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IApplicationUser _userService;
        private readonly IGuest _guestService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITaskUser _taskUserService;
        

        public HomeController(IApplicationUser userService, IGuest guestService, UserManager<ApplicationUser> userManager, ITaskUser taskUserService)
        {
            _userService = userService;
            _guestService = guestService;
            _userManager = userManager;
            _taskUserService = taskUserService;
        }

        [Authorize]
        public IActionResult Index()
        {

            var userId = _userManager.GetUserId(User);
            var user = _userService.GetById(userId);
            var guest = _guestService.GetAll(userId);
            int guestInvitationStatus=0;
            int guestCounter = 0;
            foreach (var item in guest)
            {
                if (!(item.InvitationStatus == "Niezakceptowany"))
                {
                    guestInvitationStatus ++;

                }
                guestCounter++;
            }
            var task = _taskUserService.GetAll(userId);
            int taskCompleted = 0;
            int taskCounter = 0;
            foreach (var item in task)
            {
                if (item.State== "Zakońcozny")
                {
                    taskCompleted++;
                }
                taskCounter++;
            }


            var model = new MainPageDataModel()
            {
                Budget = user.Budget,
                FullNameBride = user.BrideName,
                FullNameGroom=user.GroomName,
                CounterGuest=guestCounter,
                ConfirmGuests=guestInvitationStatus,
                CounterTask=taskCounter,
                EndTasks=taskCompleted,
                WeddingDate=user.WeddingDate.Day.ToString() +"."
                + user.WeddingDate.Month.ToString() +"." 
                + user.WeddingDate.Year.ToString()
            };
            return View(model);
        }


        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult MainInfo()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpGet]
        public IActionResult ConfigureProfile()
        {
            return View(new ProfileModel());
        }

        [HttpPost]
        public async Task<IActionResult> ConfigureProfile(ProfileModel profile)
        {
            var user = await _userManager.GetUserAsync(User);
            var IdUser = _userManager.GetUserId(User);
            user.BrideName = profile.BrideName +" "+  profile.BrideSurname;
            user.GroomName = profile.GroomName +" "+ profile.GroomSurname;
            user.Budget = profile.Budget;
            user.WeddingDate = profile.WeddingDate;
            user.Id = IdUser;

            if(!ModelState.IsValid)
            {
                return View(profile);
            }


            await _userService.UpdateUser(user);

            var brideGuest = new Guest()
            {
                Name = profile.BrideName,
                Surname = profile.BrideSurname,
                Role = "Pani Młoda",
                UserId = IdUser,
                InvitationStatus = "Niezakceptowany",
            };

            var groomGuest = new Guest()
            {
                Name = profile.GroomName,
                Surname = profile.GroomSurname,
                Role = "Pan Młody",
                UserId = IdUser,
                InvitationStatus = "Niezakceptowany",
            };

            await _guestService.Create(brideGuest);
            await _guestService.Create(groomGuest);

            return RedirectToAction("Index", "Guest", new { id = user.Id });
        }
    }
}
