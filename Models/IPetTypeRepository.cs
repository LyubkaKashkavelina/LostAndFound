using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostAndFound.Models
{
    public interface IPetTypeRepository
    {
        IEnumerable<PetType> AllTypes { get; }
    }
}
