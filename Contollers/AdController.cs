using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LostAndFound.Models;
using LostAndFound.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Contollers
{
    public class AdController : Controller
    {
        private readonly IAdRepository _adRepository;
        private readonly IPetCategoryRepository _petCategoryRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AdController(IAdRepository adRepository, 
                            IPetCategoryRepository petCategoryRepository,
                            IHostingEnvironment hostingEnvironment)
        {
            _adRepository = adRepository;
            _petCategoryRepository = petCategoryRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        //public ViewResult List()
        //{
        //    PiesListViewModel piesListViewModel = new PiesListViewModel();
        //    //ViewBag.CurrentCategory = "Chesse cakes";
        //    //return View(_pieRepository.AllPies);

        //    piesListViewModel.Pies = _pieRepository.AllPies;
        //    piesListViewModel.CurrentCategory = "Chesse cakes";

        //    return View(piesListViewModel);
        //}

        public ViewResult List(string category)
        {
            IEnumerable<Ad> ads;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                ads = _adRepository.AllAds.OrderBy(p => p.AddId);
                currentCategory = "All pets";
            }
            else
            {
                ads = _adRepository.AllAds.Where(p => p.PetCategory.CategoryName == category)
                    .OrderBy(p => p.PetCategoryId);
                currentCategory = _petCategoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new AdsListViewModel
            {
                Ads = ads,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Details(int id)
        {
            var ad = _adRepository.GetAdById(id);
            if (ad == null)
            {
                return NotFound();
            }
            return View(ad);   
        }
        
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(AdCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null) 
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Ad newAd = new Ad
                {
                    PetName = model.PetName,
                    Location = model.Location,
                    AdDescription = model.AdDescription,
                    IsPopular = model.IsPopular,
                    PetCategoryId = model.PetCategoryId,
                    PetTypeId = model.PetTypeId,
                    PhotoPath = uniqueFileName
                };
                _adRepository.CreateAd(newAd);
                return RedirectToAction("CheckoutComplete");
            }

            return View();
        }

        [Authorize]
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy your delicious pies!";

            return View();
        }

    }
}
