using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LostAndFound.Models;
using LostAndFound.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Contollers
{
    public class HomeController : Controller
    {
        private readonly IAdRepository _adRepository;

        public HomeController(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PopularAds = _adRepository.PopularPies
            };

            return View(homeViewModel);
        }
    }
}
