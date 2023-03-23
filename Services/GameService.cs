using hht.API.Models;
using hht.API.Models.Params;
using hht.API.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Services
{
    public class GameService
    {
        private readonly IGameRepository _games;
        private readonly LocationService _locations;
        private readonly YearService _years;

        public GameService(IGameRepository games, LocationService locations, YearService years)
        {
            _games = games;
            _locations = locations;
            _years = years;
        }

        public async Task<Models.Game> GetGameFromId(string id)
        {
            return ConvertToModel(await _games.GetGameFromId(id));
        }

        public async Task<List<Models.Game>> GetGamesThisYear()
        {
            var year = await _years.GetYearFromToday();
            return (await _games.GetGamesThisYear(year._id))
                .Select(s => ConvertToModel(s))
                .OrderBy(o => o.Date)
                .ToList();
        }

        public async Task<Models.Game> CreateGame(int gameNum, string homeTeam, string awayTeam, string location, string stadium, string yearId, DateTime date, string time, string hhtTheme, string schoolTheme, string tv)
        {
            var locationObj = await _locations.getLocationForGame(location);
            var newGame = await _games.Create(gameNum, homeTeam, awayTeam, locationObj, stadium, yearId, date, time, hhtTheme, schoolTheme, tv);
            return ConvertToModel(newGame);
        }

        private Models.Game ConvertToModel(Repositories.Game game)
        {
            return new Models.Game
            {
                _id = game._id,
                GameNum = game.GameNum,
                HomeTeam = game.HomeTeam,
                AwayTeam = game.AwayTeam,
                Location = _locations.ConvertToModel(game.Location),
                Stadium = game.Stadium,
                Year = game.Year,
                Date = game.Date,
                Time = game.Time,
                HHTTheme = game.HHTTheme,
                SchoolTheme = game.SchoolTheme,
                TV = game.TV,
                HomeScore = game.HomeScore,
                AwayScore = game.AwayScore,
                ScoreUpdatedOn = game.ScoreUpdatedOn,
            };
        }

    }
}
