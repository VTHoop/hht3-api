using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace hht.API.Models
{
    public class YearDto
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("start_date")]
        public DateTime StartDate { get; set; }
        [BsonElement("end_date")]
        public DateTime EndDate { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
    }
}