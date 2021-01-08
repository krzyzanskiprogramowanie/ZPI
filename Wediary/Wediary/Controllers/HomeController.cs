using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public HomeController(IApplicationUser userService, IGuest guestService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _guestService = guestService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

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
