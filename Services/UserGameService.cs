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
    public class UserGameService
    {
        private readonly IUserGameRepository _userGames;
        private readonly IGameRepository _games;
        private readonly ITeamRepository _teams;
        private readonly IYearRepository _years;
        private readonly IUserRepository _users;

        public UserGameService(IUserGameRepository userGames, IGameRepository games, ITeamRepository teams, IYearRepository years, IUserRepository users)
        {
            _userGames = userGames;
            _games = games;
            _teams = teams;
            _years = years;
            _users = users;
        }

        public async Task SetupDefaults(string userId)
        {
            var thisYear = await _years.GetYearFromToday();
            var gamesThisYear = await _games.GetGamesThisYear(thisYear._id);
            foreach(var game in gamesThisYear)
            {
                var attendance = game.Location.City == "Blacksburg" ? "Yes" : "No";
                await _userGames.Create(await _users.FindById(userId), game._id, game.Date, attendance);
            }
        }

        //private async Task CleanupUserGames()
        //{
        //    var userGames = await _userGames.GetAll();
        //    foreach (var userGame in userGames)
        //    {
        //        _userGames.DeleteOld(userGame);

        //        await _userGames.CreateNew(_userGames.DtoCleanup(userGame, await _games.GetGameFromId(userGame.Game)));
        //    }
        //}

        public async Task<List<Models.UserGame>> GetAll()
        {
            var userGames = await _userGames.GetAll();
            var convertedGameTasks = userGames.Select(s => ConvertToModel(s));
            return (await Task.WhenAll(convertedGameTasks)).ToList();
        }

        public async Task<Models.UserGame> GetById(string id)
        {
            var userGame = await _userGames.GetById(id);
            return await ConvertToModel(userGame);
        }

        public async Task<Models.UserGame> UpdateGameScore(string id, int homeScore, int awayScore, string attendance)
        {
            var userGameToUpdate = await _userGames.GetById(id);
            if (userGameToUpdate == null)
                return null;
            else
            {
                userGameToUpdate.HomeScore = homeScore;
                userGameToUpdate.AwayScore = awayScore;
                userGameToUpdate.Attendance = attendance;
                await _userGames.Update(id, userGameToUpdate);
                return await ConvertToModel(userGameToUpdate);
            }
        }

        private async Task<Models.UserGame> ConvertToModel(Repositories.UserGame userGame)
        {
            var game = await _games.GetGameFromId(userGame.Game);
            return new Models.UserGame
            {
                _id = userGame._id,
                GameId = game._id,
                UserId = userGame.User._id,
                HomeTeam = game.HomeTeam,
                AwayTeam = game.AwayTeam,
                HomeTeamAbbrev = (await _teams.GetTeamByName(game.HomeTeam)).ShortName,
                AwayTeamAbbrev = (await _teams.GetTeamByName(game.AwayTeam)).ShortName,
                GameDate = game.Date,
                GameYear = game.Year,
                Attendance = userGame.Attendance,
                HomeScore = userGame.HomeScore,
                AwayScore = userGame.AwayScore,
                TotalPoints = userGame.TotalPoints,
                PointsToDate = userGame.PointsToDate,
                AdminEntered = userGame.AdminEntered
            };
        }


    }
}
