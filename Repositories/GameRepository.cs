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
    public class GameRepository : IGameRepository
    {
        private readonly IMongoCollection<Game> _games;

        public GameRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HhtDb"));
            var database = client.GetDatabase("homerhokie_bkp");
            _games = database.GetCollection<Game>("games");
        }

        public async Task<Game> GetGameFromId(string id)
        {
            return await (await _games.FindAsync(game => game._id == id)).FirstOrDefaultAsync();
        }

        public async Task<List<Game>> GetGamesThisYear(string year)
        {
            return await (await _games.FindAsync(game => game.Year == year)).ToListAsync();
        }

        public async Task<Game> Create(int gameNum, string homeTeam, string awayTeam, Location location, string stadium, string yearId, DateTime date, string time, string hhtTheme, string schoolTheme, string tv)
        {
            var newGame = new Game
            {
                _id = ObjectId.GenerateNewId().ToString(),
                GameNum = gameNum,
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                Location = location,
                Stadium = stadium,
                Year = yearId,
                Date = date,
                Time = time,
                HHTTheme = hhtTheme,
                SchoolTheme = schoolTheme,
                TV = tv,
                HomeScore = 0,
                AwayScore = 0,
            };
            await _games.InsertOneAsync(newGame);
            return newGame;
        }
    }

    public class Game
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("game_num")]
        public int GameNum { get; set; }
        [BsonElement("home_team")]
        public string HomeTeam { get; set; }
        [BsonElement("away_team")]
        public string AwayTeam { get; set; }
        [BsonElement("location")]
        public Location Location { get; set; }
        [BsonElement("stadium")]
        public string Stadium { get; set; }
        [BsonElement("year")]
        public string Year { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("time")]
        public string Time { get; set; }
        [BsonElement("hht_theme")]
        public string HHTTheme { get; set; }
        [BsonElement("theme")]
        public string SchoolTheme { get; set; }
        [BsonElement("TV")]
        public string TV { get; set; }
        [BsonElement("home_score")]
        public int HomeScore { get; set; }
        [BsonElement("away_score")]
        public int AwayScore { get; set; }
        [BsonElement("score_updated_on")]
        public DateTime? ScoreUpdatedOn { get; set; }
        //[BsonElement("game_date_time")]
        //public DateTime GameDateTime { get; set; }
    }
}
