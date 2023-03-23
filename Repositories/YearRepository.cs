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
    public class YearRepository : IYearRepository
    {
        private readonly IMongoCollection<Year> _years;

        public YearRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HhtDb"));
            var database = client.GetDatabase("homerhokie_bkp");
            _years = database.GetCollection<Year>("years");
        }

        public async Task<List<Year>> Get()
        {
            return await (await _years.FindAsync(year => true)).ToListAsync();
        }

        public Year Get(string id)
        {
            return _years.Find(year => year._id == id).FirstOrDefault();
        }

        public async Task<Year> GetYearFromToday()
        {
            var today = DateTime.Now;
            return (await _years.FindAsync(year => year.StartDate <= today && year.EndDate >= today)).FirstOrDefault();
        }

        public Year Create(Year year)
        {
            year._id = ObjectId.GenerateNewId().ToString();
            _years.InsertOne(year);
            return year;
        }
    }

    public class Year
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
