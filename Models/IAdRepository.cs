using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public interface IAdRepository
    {
        IEnumerable<Ad> AllAds { get; } 
        IEnumerable<Ad> PopularPies { get; }
        Ad GetAdById(int adId);
        void CreateAd(Ad ad);
        void DeleteAd(Ad ad);
        IEnumerable<Ad> Search(string searchTerm);
    }
}
