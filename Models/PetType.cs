using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public class PetType
    {
        public int PetTypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        public List<Ad> Ads { get; set; }
    }
}
