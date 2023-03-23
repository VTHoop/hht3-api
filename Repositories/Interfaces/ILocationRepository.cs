using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public interface ILocationRepository
    {
        Task<Location> findById(string id);
        Task<Location> Create(string city, string state, int zip);
    }
}
