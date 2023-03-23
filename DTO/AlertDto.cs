
using MongoDB.Bson.Serialization.Attributes;

namespace hht.API.Models
{
    public class AlertDto
    {
        [BsonId]
        public string _id { get; set; }
        public UserDto User { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

    }
}
