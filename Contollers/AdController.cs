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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Contollers
{
    public class AdController : Controller
    {
        private readonly IAdRepository _adRepository;
        private readonly IPetCategoryRepository _petCategoryRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _appDbContext;

        public AdController(IAdRepository adRepository,
                            IPetCategoryRepository petCategoryRepository,
                            IHostingEnvironment hostingEnvironment,
                            UserManager<IdentityUser> userManager,
                            AppDbContext appDbContext)
        {
            _adRepository = adRepository;
            _petCategoryRepository = petCategoryRepository;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

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

        public IActionResult DetailsWithButtons(int id)
        {
            var ad = _adRepository.GetAdById(id);
            if (ad == null)
            {
                return NotFound();
            }
            return View(ad);
        }

        [HttpGet]
        public ViewResult SearchedAds(string SearchTerm)
        {
            IEnumerable<Ad> ads;
            ads = _adRepository.Search(SearchTerm);

            if (string.IsNullOrEmpty(SearchTerm))
            {
                ads = _adRepository.AllAds.OrderBy(p => p.AddId);
            }
            return View(new AdsListViewModel
            {
                Ads = ads,
            });
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
            //List<PetType> petTypeList = new List<PetType>();
            //petTypeList = (from p in _appDbContext.PetTypes
            //               select p).ToList();

            //petTypeList.Insert(0, new PetType { PetTypeId = 0, TypeName = "Select" });

            //ViewBag.ListOfPetTypes = petTypeList;

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                string currentUserId = _userManager.GetUserId(HttpContext.User);

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
                    GenderType = model.GenderType,
                    PhotoPath = uniqueFileName,
                    PublisherEmail = model.PublisherEmail,
                    PublisherPhone = model.PublisherPhone,
                    UserId = currentUserId
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

        //[Authorize]
        //[HttpPost]
        //public IActionResult Delete(int adId)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //        var addToDelete = _adRepository.GetAdById(adId);

        //        //if (addToDelete == null)
        //        //{
        //        //    return NotFound();
        //        //}

        //        //var addToDelete = _adRepository.AllAds.Where(a => a.AddId == adId).FirstOrDefault();
        //        //_adRepository.DeleteAd(addToDelete);
        //        return RedirectToAction("CheckoutComplete");

        //    //}
        //    //return View(addToDelete);
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _appDbContext.Ads
                .Include(a => a.PetCategory)
                .Include(a => a.PetType)
                .FirstOrDefaultAsync(m => m.AddId == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ads/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ad = await _appDbContext.Ads.FindAsync(id);
            _appDbContext.Ads.Remove(ad);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("CheckoutComplete");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _appDbContext.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            ViewData["PetCategoryId"] = new SelectList(_appDbContext.Set<PetCategory>(), "PetCategoryId", "PetCategoryId", ad.PetCategoryId);
            ViewData["PetTypeId"] = new SelectList(_appDbContext.Set<PetType>(), "PetTypeId", "PetTypeId", ad.PetTypeId);
            return View(ad);
        }

        // POST: Ads/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddId,PetName,Location,AdDescription,IsPopular,PetCategoryId,PetTypeId,GenderType,PhotoPath,DateOfPublish,PublisherEmail,PublisherPhone,UserId")] Ad ad)
        {
            if (id != ad.AddId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appDbContext.Update(ad);
                    await _appDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.AddId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CheckoutComplete");
            }
            ViewData["PetCategoryId"] = new SelectList(_appDbContext.Set<PetCategory>(), "PetCategoryId", "PetCategoryId", ad.PetCategoryId);
            ViewData["PetTypeId"] = new SelectList(_appDbContext.Set<PetType>(), "PetTypeId", "PetTypeId", ad.PetTypeId);
            return View(ad);
        }

        private bool AdExists(int id)
        {
            return _appDbContext.Ads.Any(e => e.AddId == id);
        }

    }
}
