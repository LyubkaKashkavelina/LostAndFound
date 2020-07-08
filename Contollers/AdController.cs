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
                ads = _adRepository.ActiveAds.OrderByDescending(a => a.DateOfPublish);
                currentCategory = "Всички";
            }
            else
            {
                ads = _adRepository.ActiveAds.Where(a => a.PetCategory.CategoryName == category)
                    .OrderByDescending(a => a.DateOfPublish);
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
        public ViewResult SearchedAds(string SearchTerm, int PetTypeId, int RegionId)
        {
            IEnumerable<Ad> ads;
            string currentCategory;

            ads = _adRepository.ActiveAds;
            currentCategory = "All ads";


            if (string.IsNullOrEmpty(SearchTerm) && RegionId == 0 && PetTypeId == 0)
            {
                ads = _adRepository.ActiveAds.OrderByDescending(p => p.DateOfPublish);
            }

            if (string.IsNullOrEmpty(SearchTerm) && RegionId == 0 && PetTypeId != 0)
            {
                ads = _adRepository.ActiveAds.Where(a => a.PetTypeId == PetTypeId).OrderByDescending(p => p.DateOfPublish);
            }

            if (string.IsNullOrEmpty(SearchTerm) && RegionId != 0 && PetTypeId == 0)
            {
                ads = _adRepository.ActiveAds.Where(a => a.RegionId == RegionId).OrderByDescending(p => p.DateOfPublish);
            }

            if (string.IsNullOrEmpty(SearchTerm) && RegionId != 0 && PetTypeId != 0)
            {
                ads = _adRepository.ActiveAds.Where(a => a.RegionId == RegionId)
                                             .Where(a => a.PetTypeId == PetTypeId)
                                             .OrderByDescending(p => p.DateOfPublish);
            }

            if (!(string.IsNullOrEmpty(SearchTerm)) && RegionId == 0 && PetTypeId == 0)
            {
                ads = _adRepository.Search(SearchTerm)
                     .OrderByDescending(p => p.DateOfPublish);
            }

            if (!(string.IsNullOrEmpty(SearchTerm)) && RegionId == 0 && PetTypeId != 0)
            {
                ads = _adRepository.Search(SearchTerm)
                                   .Where(a => a.PetTypeId == PetTypeId)
                                   .OrderByDescending(p => p.DateOfPublish);
            }

            if (!(string.IsNullOrEmpty(SearchTerm)) && RegionId != 0 && PetTypeId == 0)
            {
                ads = _adRepository.Search(SearchTerm)
                                   .Where(a => a.RegionId == RegionId)
                                   .OrderByDescending(p => p.DateOfPublish);
            }

            if (!(string.IsNullOrEmpty(SearchTerm)) && RegionId != 0 && PetTypeId != 0)
            {
                ads = _adRepository.Search(SearchTerm)
                                   .Where(a => a.RegionId == RegionId)
                                   .Where(a => a.PetTypeId == PetTypeId)
                                   .OrderByDescending(p => p.DateOfPublish);
            }


            return View(new AdsListViewModel
            {
                Ads = ads,
                CurrentCategory = currentCategory
            });
        }

        [Authorize]
        public IActionResult Checkout()
        {
            ViewData["PetCategoryId"] = new SelectList(_appDbContext.Set<PetCategory>(), "PetCategoryId", "CategoryName");
            ViewData["PetTypeId"] = new SelectList(_appDbContext.Set<PetType>(), "PetTypeId", "TypeName");
            ViewData["RegionId"] = new SelectList(_appDbContext.Set<Region>(), "RegionId", "RegionName");
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(AdCreateViewModel model)
        {           

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
                    UserId = currentUserId,
                    IsArchived = model.IsArchived,
                    RegionId = model.RegionId
                };
                _adRepository.CreateAd(newAd);
                return RedirectToAction("CheckoutComplete");
            }
            
            return View();
        }

        [Authorize]
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Благодарим Ви, че публикувахте вашата обява. Скоро ще върнете този чудесен любимец у дома!";

            return View();
        }

        public IActionResult DeleteComplete()
        {
            ViewBag.DeleteCompleteMessage = "Обявата беше успешно изтрита!";

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
                .Include(a => a.Region)
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
            return RedirectToAction("DeleteComplete");
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
            ViewData["PetCategoryId"] = new SelectList(_appDbContext.Set<PetCategory>(), "PetCategoryId", "CategoryName", ad.PetCategoryId);
            ViewData["PetTypeId"] = new SelectList(_appDbContext.Set<PetType>(), "PetTypeId", "TypeName", ad.PetTypeId);
            ViewData["RegionId"] = new SelectList(_appDbContext.Set<Region>(), "RegionId", "RegionName", ad.RegionId);
            return View(ad);
        }

        // POST: Ads/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddId,PetName,RegionId,Location,AdDescription,IsPopular,PetCategoryId,PetTypeId,GenderType,PhotoPath,DateOfPublish,PublisherEmail,PublisherPhone,UserId,IsArchived")] Ad ad)
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
            ViewData["RegionId"] = new SelectList(_appDbContext.Set<Region>(), "RegionId", "RegionName", ad.RegionId);
            return View(ad);
        }

        private bool AdExists(int id)
        {
            return _appDbContext.Ads.Any(e => e.AddId == id);
        }

    }
}
