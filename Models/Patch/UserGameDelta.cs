using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models.Patch
{
    public class UserGameDelta
    {
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public string Attendance { get; set; }
    }
}
