using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models
{
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
