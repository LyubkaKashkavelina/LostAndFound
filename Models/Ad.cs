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

        [Display(Name = "Име")]
        [StringLength(50)]
        public string PetName { get; set; }

        [Required(ErrorMessage = "Моля, въведете област!")]
        [Display(Name = "Област")]
        public int RegionId { get; set; }
        public Region Region { get; set; }

        [Required(ErrorMessage = "Моля, въведете местоположение!")]
        [Display(Name = "Местоположение")]
        public string Location { get; set; }

        [Display(Name = "Описание")]
        public string AdDescription { get; set; }

        [Display(Name = "Промотирай")]
        public bool IsPopular { get; set; }

        [Required(ErrorMessage = "Моля, въведете категория!")]
        [Display(Name = "Категория")]
        public int PetCategoryId { get; set; }

        public PetCategory PetCategory { get; set; }

        [Required(ErrorMessage = "Моля, въведете тип!")]
        [Display(Name = "Тип")]
        public int PetTypeId { get; set; }

        public PetType PetType { get; set; }

        [EnumDataType(typeof(Gender))]
        [Display(Name = "Пол")]
        public Gender GenderType { get; set; }
        public string PhotoPath { get; set; }

        [Display(Name = "Дата на публикуване")]
        public DateTime DateOfPublish { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        [Display(Name = "Имейл")]
        public string PublisherEmail { get; set; }

        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        public string PublisherPhone { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Архивирай")]
        public bool IsArchived { get; set; }
    }
    public enum Gender
    {
        Мъжки = 1,
        Женски = 2,
        Неизвестен = 3
    }
}
