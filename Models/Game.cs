using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models
{
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
