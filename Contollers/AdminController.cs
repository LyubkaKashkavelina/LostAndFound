using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LostAndFound.Models;
using LostAndFound.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Contollers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAdRepository _adRepository;
        public AdminController(RoleManager<IdentityRole> roleManager, IAdRepository adRepository)
        {
            _roleManager = roleManager;
            _adRepository = adRepository;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole 
                { 
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        public ViewResult ListManageAds()
        {
            IEnumerable<Ad> ads;

                ads = _adRepository.AllAds.OrderByDescending(p => p.DateOfPublish);

            return View(new AdsListViewModel
            {
                Ads = ads
            });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
