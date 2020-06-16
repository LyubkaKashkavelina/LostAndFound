using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public class Ad
    {
        [Key]
        public int AddId { get; set; }
        public string PetName { get; set; }
        public string Location { get; set; }
        public string AdDescription { get; set; }
        //public string ImageUrl { get; set; }
        //public string ImageThumbnailUrl { get; set; }
        public bool IsPopular { get; set; }
        public int PetCategoryId { get; set; }
        public PetCategory PetCategory { get; set; }
        public int PetTypeId { get; set; }
        public PetType PetType { get; set; }
        public string PhotoPath { get; set; }
        public DateTime DateOfPublish { get; set; }

    }
}
