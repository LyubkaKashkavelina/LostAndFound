using LostAndFound.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.ViewModels
{
    public class AdCreateViewModel
    {
        public string PetName { get; set; }
        public string Location { get; set; }
        public string AdDescription { get; set; }
        public bool IsPopular { get; set; }
        public int PetCategoryId { get; set; }
        public PetCategory PetCategory { get; set; }
        public int PetTypeId { get; set; }
        public PetType PetType { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime DateOfPublish { get; set; }
    }
}
