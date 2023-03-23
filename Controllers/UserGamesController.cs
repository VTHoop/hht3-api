using hht.API.Models;
using hht.API.Models.Params;
using hht.API.Models.Patch;
using hht.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace hht.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGamesController : ControllerBase
    {
        private readonly UserGameService _userGameService;
        private readonly GameService _gameService;
        private readonly YearService _yearService;
        private readonly TeamService _teamService;

        public UserGamesController(UserGameService userGameService,
            GameService gameService,
            YearService yearService,
            TeamService teamService)
        {
            _userGameService = userGameService;
            _gameService = gameService;
            _yearService = yearService;
            _teamService = teamService;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<List<UserGame>>> Get([FromQuery] UserGameParams userGameParams)
        {
            var userGames = await _userGameService.GetAll();
            if (!string.IsNullOrEmpty(userGameParams.UserId))
            {
                userGames = userGames.Where(w => w.UserId.Equals(userGameParams.UserId)).ToList();
            }
            if (!string.IsNullOrEmpty(userGameParams.GameId))
            {
                userGames = userGames.Where(w => w.GameId.Equals(userGameParams.GameId)).ToList();
            }
            if (!string.IsNullOrEmpty(userGameParams.AttendanceStatus))
            {
                userGames = userGames.Where(w => w.Attendance.ToLower().Equals(userGameParams.AttendanceStatus.ToLower())).ToList();
            }
            if (userGameParams.CurrentYear == true)
            {
                var currentYear = (await _yearService.Get()).SingleOrDefault(w => w.StartDate <= DateTime.Now && w.EndDate >= DateTime.Now)._id;
                // this is potentially really stupid
                userGames = userGames.Where(w => currentYear.Equals(w.GameYear)).ToList();
            }
            return userGames.OrderBy(o => o.GameDate).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetUserGame")]
        public async Task<ActionResult<UserGame>> Get(string id)
        {
            var userGame = await _userGameService.GetById(id);
            if (userGame == null)
            {
                return new UserGame() { };
            }
            return userGame;

        }

        // POST api/values
        //[HttpPost]
        //public ActionResult<UserGame> Post([FromBody] UserGameDto userGame)
        //{
        //    _userGameService.Create(userGame);
        //    return CreatedAtRoute("GetUserGame", new { id = userGame._id, }, userGame);

        //}

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<UserGame>> Patch(string id, [FromBody] UserGameDelta body)
        {
            var updatedUserGame = await _userGameService.UpdateGameScore(id, body.HomeScore, body.AwayScore, body.Attendance);
            if (updatedUserGame == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private async Task<UserGame> DtoToModel(UserGameDto userGameDto)
        {
            var game = await _gameService.GetGameFromId(userGameDto.Game);

            return new UserGame
            {
                _id = userGameDto._id,
                HomeTeam = game.HomeTeam,
                AwayTeam = game.AwayTeam,
                HomeTeamAbbrev = (await _teamService.GetTeamByName(game.HomeTeam)).ShortName,
                AwayTeamAbbrev = (await _teamService.GetTeamByName(game.AwayTeam)).ShortName,
                GameDate = game.Date,
                Attendance = userGameDto.Attendance,
                HomeScore = userGameDto.HomeScore,
                AwayScore = userGameDto.AwayScore,
                TotalPoints = userGameDto.TotalPoints,
                PointsToDate = userGameDto.PointsToDate,
                AdminEntered = userGameDto.AdminEntered,
            };
        }

        
    }
}
