using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models.Params
{
    public class UserGameParams
    {
        public string UserId { get; set; }
        public string YearName { get; set; }
        public string AttendanceStatus { get; set; }
        public string GameId { get; set; }
        public bool PriorGames { get; set; }
        public bool CurrentYear { get; set; }

    }
}
