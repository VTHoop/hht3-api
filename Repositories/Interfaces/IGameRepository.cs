using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public interface IGameRepository
    {
        Task<Game> GetGameFromId(string id);
        Task<List<Game>> GetGamesThisYear(string year);
        Task<Game> Create(int gameNum, string homeTeam, string awayTeam, Location location, string stadium, string yearId, DateTime date, string time, string hhtTheme, string schoolTheme, string tv);
    }
}
