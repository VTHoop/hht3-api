using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public interface IUserGameRepository
    {
        Task<List<UserGame>> GetAll();
        Task<UserGame> GetById(string id);
        Task Update(string id, UserGame userGame);
        void DeleteOld(UserGame oldUserGame);
        //Task<UserGameCleanup> CreateNew(UserGameCleanup userGame);
        UserGameCleanup DtoCleanup(UserGame userGameDto, Game gameObject);
        Task<UserGame> Create(User user, string game, DateTime gameDate, string attendance);
    }
}
