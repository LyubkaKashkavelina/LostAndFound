using LostAndFound.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly IPetCategoryRepository _petCategoryRepository;
        public CategoryMenu(IPetCategoryRepository petCategoryRepository)
        {
            _petCategoryRepository = petCategoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _petCategoryRepository.AllCategories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}
