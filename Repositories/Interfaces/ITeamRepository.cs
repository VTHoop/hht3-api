using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAll();
        Task<Team> GetTeamByName(string teamName);
        Task<Team> CreateTeam(string schoolName, string hokieSportsName, string mascot, string shortName, string logo, Location location, string stadium);
    }
}
