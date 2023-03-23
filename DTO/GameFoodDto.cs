using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models
{
    public class GameFoodDto
    {
        [BsonId]
        public string _id { get; set; }
        public string Food { get; set; }
        public GameDto Game { get; set; }
        public UserDto User { get; set; }
    }
}
