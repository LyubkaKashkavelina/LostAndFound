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
    public class UserController : Controller
    {
        
        private readonly IAdRepository _adRepository;

        public UserController(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public ViewResult ListMyAds(string userId)
        {
            IEnumerable<Ad> ads;

                ads = _adRepository.AllAds.Where(a => a.UserId == userId)
                    .OrderBy(a => a.DateOfPublish);

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
