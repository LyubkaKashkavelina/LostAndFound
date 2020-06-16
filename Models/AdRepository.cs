﻿using Microsoft.EntityFrameworkCore;
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
                return _appDbContext.Ads.Include(c => c.PetCategory);
            }
        }

        public IEnumerable<Ad> PopularPies
        {
            get
            {
                return _appDbContext.Ads.Include(c => c.PetCategory).Where(p => p.IsPopular);
            }
        }

        public Ad GetAdById(int adId)
        {
            return _appDbContext.Ads.FirstOrDefault(p => p.AddId == adId);
        }

        public void CreateAd(Ad ad)
        {
            ad.DateOfPublish = DateTime.Now;
            _appDbContext.Ads.Add(ad);

            _appDbContext.SaveChanges();
        }
    }
}