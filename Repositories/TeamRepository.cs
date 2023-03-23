using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IMongoCollection<Team> _teams;

        public TeamRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HhtDb"));
            var database = client.GetDatabase("homerhokie_bkp");
            _teams = database.GetCollection<Team>("teams");
        }

        public async Task<List<Team>> GetAll()
        {
            return await (await _teams.FindAsync(team => true)).ToListAsync();
        }

        public async Task<Team> GetTeamByName(string teamName)
        {
            return await (await _teams.FindAsync(team => team.HokieSportsName == teamName)).FirstOrDefaultAsync();
        }

        public async Task<Team> CreateTeam(string schoolName, string hokieSportsName, string mascot, string shortName, string logo, Location location, string stadium)
        {
            var newTeam = new Team
            {
                _id = ObjectId.GenerateNewId().ToString(),
                SchoolName = schoolName,
                HokieSportsName = hokieSportsName,
                Mascot = mascot,
                ShortName = shortName,
                Logo = logo,
                Location = location,
                Stadium = stadium,
            };
            await _teams.InsertOneAsync(newTeam);
            return newTeam;
        }
     
    }

    public class Team
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("school_name")]
        public string SchoolName { get; set; }
        [BsonElement("hokie_sports_name")]
        public string HokieSportsName { get; set; }
        [BsonElement("mascot")]
        public string Mascot { get; set; }
        [BsonElement("short_name")]
        public string ShortName { get; set; }
        [BsonElement("logo")]
        public string Logo { get; set; }
        [BsonElement("location")]
        public Location Location { get; set; }
        [BsonElement("stadium")]
        public string Stadium { get; set; }
    }
}
