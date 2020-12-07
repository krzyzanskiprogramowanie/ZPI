using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Data.Models;
using Wediary.Models;

namespace Wediary.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuest _serviceGuest;
        private readonly IApplicationUser _serviceApplicationUser;
        private static UserManager<ApplicationUser> _userManager;

        public GuestController(IGuest serviceGuest, IApplicationUser serviceApplicationUser, UserManager<ApplicationUser> userManager)
        {
            _serviceGuest = serviceGuest;
            _serviceApplicationUser = serviceApplicationUser;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            IEnumerable<Guest> listOfTask = _serviceGuest.GetAll(userId);
            return View(listOfTask);
        }
        public IActionResult GuestList()
        {
            ViewData["Message"] = "Your Manager page.";

            return View();
        }

    }
}
