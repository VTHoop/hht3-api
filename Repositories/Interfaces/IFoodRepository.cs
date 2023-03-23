using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public interface IFoodRepository
    {
        Task<List<Food>> Get();
        Task<Food> Get(string id);
        Task<List<Food>> GetByGame(string gameId);
        Task<Food> Create(string foodName, string gameId, string userId);
        Task Delete(string id);
    }
}
