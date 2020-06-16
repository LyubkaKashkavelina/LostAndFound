using LostAndFound.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.ViewModels
{
    public class AdsListViewModel
    {
        public IEnumerable<Ad> Ads { get; set; }
        public string CurrentCategory { get; set; }
    }
}
