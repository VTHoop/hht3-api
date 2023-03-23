using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Models
{
    public class GamePreviewDto
    {
        [BsonId]
        public string _id { get; set; }
        public GameDto Game { get; set; }
        public UserDto Author { get; set; }
        public string Preview { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
