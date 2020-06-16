using LostAndFound.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Ad> PopularAds { get; set; }
    }
}
