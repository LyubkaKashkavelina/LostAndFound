using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public class PetTypeRepository : IPetTypeRepository
    {
        private readonly AppDbContext _appDbContext;

        public PetTypeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<PetType> AllTypes => _appDbContext.PetTypes;
    }
}
