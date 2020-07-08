using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public class AdRepository : IAdRepository
    {
        private readonly AppDbContext _appDbContext;

        public AdRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Ad> AllAds
        {
            get
            {
                return _appDbContext.Ads.Include(c => c.PetCategory)
                    .Include(c => c.PetType)
                    .Include(c => c.Region);
            }
        }

        public IEnumerable<Ad> PopularAds
        {
            get
            {
                return _appDbContext.Ads.Include(c => c.PetCategory)
                    .Include(c => c.PetType)
                    .Include(c => c.Region)
                    .Where(a => a.IsPopular).Where(a => a.IsArchived == false);
            }
        }

        public IEnumerable<Ad> ActiveAds
        {
            get
            {
                return _appDbContext.Ads.Include(c => c.PetCategory)
                    .Include(c => c.PetType)
                    .Include(c => c.Region)
                    .Where(c => c.IsArchived == false);
            }
        }

        public Ad GetAdById(int adId)
        {
            return _appDbContext.Ads.Include(c => c.PetCategory).Include(c => c.PetType).Include(c => c.Region).FirstOrDefault(p => p.AddId == adId);
        }

        public void CreateAd(Ad ad)
        {
            ad.DateOfPublish = DateTime.Now;
            _appDbContext.Ads.Add(ad);

            _appDbContext.SaveChanges();
        }

        public void DeleteAd(Ad ad)
        {
            _appDbContext.Ads.Remove(ad);

            _appDbContext.SaveChanges();
        }

        public IEnumerable<Ad> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _appDbContext.Ads;
            }

                
            return _appDbContext.Ads.Include(c => c.PetCategory)
                    .Include(c => c.PetType)
                    .Include(c => c.Region).
                Where(a => a.PetName.Contains(searchTerm) ||
                                                a.Location.Contains(searchTerm) ||
                                                a.PetCategory.CategoryName.Contains(searchTerm)||
                                                a.AdDescription.Contains(searchTerm))
                                    .Where(a => a.IsArchived == false);
        }
    }
}
