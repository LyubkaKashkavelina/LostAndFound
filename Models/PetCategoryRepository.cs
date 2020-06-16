using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public class PetCategoryRepository : IPetCategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public PetCategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<PetCategory> AllCategories => _appDbContext.PetCategories;

    }
}
