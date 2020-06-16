using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public class PetCategory
    {
        public int PetCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Ad> Ads { get; set; }
    }
}
