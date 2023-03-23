using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IMongoCollection<Location> _locations;

        public LocationRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HhtDb"));
            var database = client.GetDatabase("homerhokie_bkp");
            _locations = database.GetCollection<Location>("locations");
        }

        public async Task<Location> findById(string id)
        {
            return await (await _locations.FindAsync(location => location._id == id)).FirstOrDefaultAsync();
        }

        public async Task<Location> Create(string city, string state, int zip)
        {
            var newLocation = new Location
            {
                _id = ObjectId.GenerateNewId().ToString(),
                City = city,
                State = state,
                Zip = zip,
            };
            await _locations.InsertOneAsync(newLocation);
            return newLocation;
        }

    }

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
