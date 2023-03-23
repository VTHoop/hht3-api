using hht.API.Repositories;
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
    public class AlertRepository : IAlertRepository
    {
        private readonly IMongoCollection<Alert> _alerts;

        public AlertRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HhtDb"));
            var database = client.GetDatabase("homerhokie_bkp");
            _alerts = database.GetCollection<Alert>("alerts");
        }

        public async Task CreateDefaults(string userId, List<string> alertNames)
        {
            foreach(var alert in alertNames)
            {
                var newAlert = new Alert
                {
                    _id = ObjectId.GenerateNewId().ToString(),
                    User = userId,
                    Name = alert.ToString(),
                    Active = "On"
                };
                await _alerts.InsertOneAsync(newAlert);
            }
        }
    }

    public class Alert
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("user")]
        public string User { get; set; }
        [BsonElement("alert")]
        public string Name { get; set; }
        [BsonElement("yes_no")]
        public string Active { get; set; }
    }

}


