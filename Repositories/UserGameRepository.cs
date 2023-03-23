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
    public class UserGameRepository : IUserGameRepository
    {
        private readonly IMongoCollection<UserGame> _userGames;
        private readonly IMongoCollection<UserGameCleanup> _userGamesCleanup;

        public UserGameRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HhtDb"));
            var database = client.GetDatabase("homerhokie_bkp");
            _userGames = database.GetCollection<UserGame>("user_games");
            _userGamesCleanup = database.GetCollection<UserGameCleanup>("user_games");

        }

        public async Task<List<UserGame>> GetAll()
        {
            return await (await _userGames.FindAsync(usergame => true)).ToListAsync();
        }

        public async Task<UserGame> GetById(string id)
        {
            return await (await _userGames.FindAsync(ug => ug._id == id)).FirstOrDefaultAsync();
        }

        public async Task Update(string id, UserGame userGame)
        {
            await _userGames.ReplaceOneAsync(ug => ug._id == id, userGame);
        }

        public void DeleteOld(UserGame oldUserGame)
        {
            _userGames.DeleteOne(userGame => userGame._id == oldUserGame._id);
        }

        public async Task<UserGame> Create(User user, string game, DateTime gameDate, string attendance)
        {
            var newGame = new UserGame
            {
                _id = ObjectId.GenerateNewId().ToString(),
                User = user,
                Game = game,
                GameDate = gameDate,
                Attendance = attendance,
                HomeScore = 0,
                AwayScore = 0,
                HomePoints = 0,
                AwayPoints = 0,
                TotalPoints = 0,
                PointsToDate = 0,
                AdminEntered = "No",
            };

            await _userGames.InsertOneAsync(newGame);
            return newGame;
                
        }

        //public async Task<UserGameCleanup> CreateNew(UserGameCleanup userGame)
        //{
        //    userGame._id = ObjectId.GenerateNewId().ToString();
        //    await _userGamesCleanup.InsertOneAsync(userGame);
        //    return userGame;
        //}

        public UserGameCleanup DtoCleanup(UserGame userGameDto, Game gameObject)
        {
            return new UserGameCleanup
            {
                _id = userGameDto._id,
                User = userGameDto.User,
                Game = gameObject,
                Attendance = userGameDto.Attendance,
                HomeScore = userGameDto.HomeScore,
                AwayScore = userGameDto.AwayScore,
                TotalPoints = userGameDto.TotalPoints,
                PointsToDate = userGameDto.PointsToDate,
                AdminEntered = userGameDto.AdminEntered,
                AwayPoints = userGameDto.AwayPoints,
                HomePoints = userGameDto.HomePoints,
                PointsUpdatedOn = userGameDto.PointsUpdatedOn,
                ScoreUpdatedOn = userGameDto.ScoreUpdatedOn

            };
        }
    }

    public class UserGame
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("user")]
        public User User { get; set; }
        [BsonElement("game")]
        public string Game { get; set; } //old
        //public GameDto Game { get; set; }  //new
        [BsonElement("game_date")]
        public DateTime GameDate { get; set; }
        [BsonElement("attendance")]
        public string Attendance { get; set; }
        [BsonElement("home_score")]
        public int HomeScore { get; set; }
        [BsonElement("away_score")]
        public int AwayScore { get; set; }
        [BsonElement("score_updated_on")]
        public DateTime? ScoreUpdatedOn { get; set; }
        [BsonElement("home_points")]
        public int HomePoints { get; set; }
        [BsonElement("away_points")]
        public int AwayPoints { get; set; }
        [BsonElement("total_points")]
        public int TotalPoints { get; set; }
        [BsonElement("points_to_date")]
        public int PointsToDate { get; set; }
        [BsonElement("points_updated_on")]
        public DateTime? PointsUpdatedOn { get; set; }
        [BsonElement("admin_enter")]
        public string AdminEntered { get; set; }

    }

    public class UserGameCleanup
    {

        [BsonId]
        public string _id { get; set; }
        [BsonElement("user")]
        public User User { get; set; }
        [BsonElement("game")]
        public Game Game { get; set; }
        [BsonElement("attendance")]
        public string Attendance { get; set; }
        [BsonElement("home_score")]
        public int HomeScore { get; set; }
        [BsonElement("away_score")]
        public int AwayScore { get; set; }
        [BsonElement("score_updated_on")]
        public DateTime? ScoreUpdatedOn { get; set; }
        [BsonElement("home_points")]
        public int HomePoints { get; set; }
        [BsonElement("away_points")]
        public int AwayPoints { get; set; }
        [BsonElement("total_points")]
        public int TotalPoints { get; set; }
        [BsonElement("points_to_date")]
        public int PointsToDate { get; set; }
        [BsonElement("points_updated_on")]
        public DateTime? PointsUpdatedOn { get; set; }
        [BsonElement("admin_enter")]
        public string AdminEntered { get; set; }
    }

    public class UserGameOld
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("user")]
        public User User { get; set; }
        [BsonElement("game")]
        public string Game { get; set; } //old
        //public GameDto Game { get; set; }  //new
        [BsonElement("game_date")]
        public DateTime GameDate { get; set; }
        [BsonElement("attendance")]
        public string Attendance { get; set; }
        [BsonElement("home_score")]
        public int HomeScore { get; set; }
        [BsonElement("away_score")]
        public int AwayScore { get; set; }
        [BsonElement("score_updated_on")]
        public DateTime? ScoreUpdatedOn { get; set; }
        [BsonElement("home_points")]
        public int HomePoints { get; set; }
        [BsonElement("away_points")]
        public int AwayPoints { get; set; }
        [BsonElement("total_points")]
        public int TotalPoints { get; set; }
        [BsonElement("points_to_date")]
        public int PointsToDate { get; set; }
        [BsonElement("points_updated_on")]
        public DateTime? PointsUpdatedOn { get; set; }
        [BsonElement("admin_enter")]
        public string AdminEntered { get; set; }

    }
}
