using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models
{
    public class UserGame
    {
        public string _id { get; set; }
        public string UserId { get; set; }
        public string GameId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string HomeTeamAbbrev { get; set; }
        public string AwayTeamAbbrev { get; set; }
        public DateTime GameDate { get; set; }
        public string GameYear { get; set; }
        public string Opponent { get; set; }
        public string Attendance { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int TotalPoints { get; set; }
        public int PointsToDate { get; set; }
        public string AdminEntered { get; set; }
    }
}
