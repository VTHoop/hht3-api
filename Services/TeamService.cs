using hht.API.Models;
using hht.API.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace hht.API.Services
{
    public class TeamService
    {
        private readonly ITeamRepository _teams;
        private readonly LocationService _locations;

        public TeamService(ITeamRepository teams, LocationService locations )
        {
            _teams = teams;
            _locations = locations;
        }

        public async Task<List<Models.Team>> GetAll()
        {
            var teams = await _teams.GetAll();
            var convertedTeams = teams.Select(s => convertToModel(s)).ToList();
            return convertedTeams;
        }

        public async Task<Models.Team> GetTeamByName(string teamName)
        {
            return convertToModel(await _teams.GetTeamByName(teamName));
        }

        public async Task<Models.Team> CreateTeam(string schoolName, string hokieSportsName, string mascot, string shortName, string logo, string location, string stadium)
        {
            var locationObj = await _locations.getLocationForGame(location);
            var newGame = await _teams.CreateTeam(schoolName, hokieSportsName, mascot, shortName, logo, locationObj, stadium);
            return convertToModel(newGame);
        }

        private Models.Team convertToModel(Repositories.Team team)
        {
            return new Models.Team
            {
                _id = team._id,
                SchoolName = team.SchoolName,
                HokieSportsName = team.HokieSportsName,
                Mascot = team.Mascot,
                ShortName = team.ShortName,
                Logo = team.Logo,
                Location = _locations.ConvertToModel(team.Location),
                Stadium = team.Stadium,
            };
        }

    }
}
