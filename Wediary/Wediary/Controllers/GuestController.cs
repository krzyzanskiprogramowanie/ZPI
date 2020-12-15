using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wediary.Data;
using Wediary.Data.Models;
using Wediary.Models;
using Wediary.Models.Guest;

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
        public IActionResult AddGuest()
        {


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SaveChanges(GuestModel m)
        {
            var userId = _userManager.GetUserId(User);
            string id = userId;
            var user = await _userManager.FindByIdAsync(userId);
            var changes = ReplyBuild(m, user, id);
            await _serviceGuest.Create(changes);
            return RedirectToAction("Index", "Guest", new { id = m.IdGuest });
        }

        private Guest ReplyBuild(GuestModel m, ApplicationUser user, string id)
        {
            return new Guest
            {
                UserId=id,
                Name=m.Name,
                Surname=m.Surname,
                Role=m.Role,
                IfAftermath=m.IfAftermath,
                IfSpecialDiet=m.IfSpecialDiet,
                IfAccommodation=m.IfAccommodation,
                DescriptionDiet=m.DescriptionDiet,
                InvitationStatus=m.InvitationStatus
            };
        }
    }
}
