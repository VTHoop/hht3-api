using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models
{
    public class WantTicketDto
    {
        [BsonId]
        public string _id { get; set; }
        public UserDto User { get; set; }
        public GameDto Game { get; set; }
        public int Number { get; set; }
        public bool Purchased { get; set; }

    }
}
