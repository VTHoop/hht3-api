using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models
{
    public class HaveTicketDto
    {
        [BsonId]
        public string _id { get; set; }
        public UserDto User { get; set; }
        public int Number { get; set; }
        public string Section { get; set; }
        public string Seats { get; set; }
        public GameDto Game { get; set; }
        public bool Sold { get; set; }
        public decimal Price { get; set; }

    }
}
