using hht.API.Models;
using hht.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameService _gameService;

        public GamesController(GameService gameService)
        {
            _gameService = gameService;
        }

        // GET api/values
        //[HttpGet]
        //public ActionResult<List<UserGame>> Get([FromQuery] UserGameParams userGameParams)
        //{
        //    var userGames = _userGameService.Get();
        //    if (!string.IsNullOrEmpty(userGameParams.UserId))
        //    {
        //        userGames = userGames.Where(w => w.User._id.Equals(userGameParams.UserId)).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(userGameParams.GameId))
        //    {
        //        userGames = userGames.Where(w => w.Game.Equals(userGameParams.GameId)).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(userGameParams.AttendanceStatus))
        //    {
        //        userGames = userGames.Where(w => w.Attendance.ToLower().Equals(userGameParams.AttendanceStatus.ToLower())).ToList();
        //    }
        //    return userGames.Select(s =>
        //    {
        //        return DtoToModel(s);
        //    }).ToList();
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> Get(string id)
        {
            var game = await _gameService.GetGameFromId(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpGet("current-year")]
        public async Task<ActionResult<List<Game>>> GetForCurrentYear()
        {
            return Ok(await _gameService.GetGamesThisYear());
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Game>> Post([FromBody] Game game)
        {
            var newGame = await _gameService.CreateGame(game.GameNum, game.HomeTeam, game.AwayTeam, game.Location._id, game.Stadium, game.Year, game.Date, game.Time, game.HHTTheme, game.SchoolTheme, game.TV);
            return CreatedAtAction(nameof(Get), new { id = newGame._id }, newGame._id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
