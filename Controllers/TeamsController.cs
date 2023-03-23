using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hht.API.Models;
using hht.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace hht.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamsController(TeamService teamService)
        {
            _teamService = teamService;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<List<Team>>> Get()
        {
            return Ok(await _teamService.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Team>> Post([FromBody] Team team)
        {
            var newTeam = await _teamService.CreateTeam(team.SchoolName, team.HokieSportsName, team.Mascot, team.ShortName, team.Logo, team.Location._id, team.Stadium);
            return CreatedAtAction(nameof(Get), new { id = newTeam._id }, newTeam._id);
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
