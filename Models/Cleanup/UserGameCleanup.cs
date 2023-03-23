using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models
{
    public class UserGameCleanup
    {
        
        [BsonId]
        public string _id { get; set; }
        [BsonElement("user")]
        public UserDto User { get; set; }
        [BsonElement("game")]
        public GameDto Game { get; set; }
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
