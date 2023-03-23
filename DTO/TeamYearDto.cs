using MongoDB.Bson.Serialization.Attributes;

namespace hht.API.Models
{
    public class TeamYearDto
    {
        [BsonId]
        public string _id { get; set; }
        public TeamDto Team { get; set; }
        public YearDto Year { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int ConfWins { get; set; }
        public int ConfLosses { get; set; }
        public int APRank { get; set; }
        public string Conference { get; set; }
               
    }
}