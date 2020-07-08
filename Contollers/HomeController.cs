using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LostAndFound.Models;
using LostAndFound.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LostAndFound.Contollers
{
    public class HomeController : Controller
    {
        private readonly IAdRepository _adRepository;
        private readonly AppDbContext _appDbContext;

        public HomeController(IAdRepository adRepository, AppDbContext appDbContext)
        {
            _adRepository = adRepository;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            
            var homeViewModel = new HomeViewModel
            {
                PopularAds = _adRepository.PopularAds
            };
            ViewData["PetTypeId"] = new SelectList(_appDbContext.Set<PetType>(), "PetTypeId", "TypeName");
            ViewData["RegionId"] = new SelectList(_appDbContext.Set<Region>(), "RegionId", "RegionName");

            return View(homeViewModel);
        }
    }
}
