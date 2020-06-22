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

        [Display(Name = "Pet name")]
        [StringLength(50)]
        public string PetName { get; set; }

        public string Location { get; set; }
        public string AdDescription { get; set; }

        [Required(ErrorMessage = "Please enter is popular ad!")]
        public bool IsPopular { get; set; }

        [Required(ErrorMessage = "Please enter pet category!")]
        [Display(Name = "Pet category")]
        public int PetCategoryId { get; set; }

        public PetCategory PetCategory { get; set; }

        [Required(ErrorMessage = "Please enter pet type!")]
        [Display(Name = "Pet type")]
        public int PetTypeId { get; set; }

        public PetType PetType { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender GenderType { get; set; }
        public string PhotoPath { get; set; }
        public DateTime DateOfPublish { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        [Display(Name = "Email")]
        public string PublisherEmail { get; set; }

        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PublisherPhone { get; set; }
        public string UserId { get; set; }
    }
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Unknown = 3
    }
}
