using LostAndFound.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Ad> PopularAds { get; set; }
        
        [BindProperty (SupportsGet = true)]
        public string SearchTerm { get; set; }
    }
}
