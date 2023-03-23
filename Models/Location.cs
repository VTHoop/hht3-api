using MongoDB.Bson.Serialization.Attributes;

namespace hht.API.Models
{
    public class Location
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("state")]
        public string State { get; set; }
        [BsonElement("zip")]
        public int Zip { get; set; }
    }
}
