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
    public class FoodRepository : IFoodRepository
    {
        private readonly IMongoCollection<Food> _food;

        public FoodRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("HhtDb"));
            var database = client.GetDatabase("homerhokie_bkp");
            _food = database.GetCollection<Food>("food");
        }

        public async Task<List<Food>> Get()
        {
            return await (await _food.FindAsync(gameFood => true)).ToListAsync();
        }

        public async Task<Food> Get(string id)
        {
            return await (await _food.FindAsync(gameFood => gameFood._id == id)).FirstOrDefaultAsync();
        }

        public async Task<List<Food>> GetByGame(string gameId)
        {
            return await(await _food.FindAsync(gameFood => gameFood.GameId == gameId)).ToListAsync();
        }

        public async Task<Food> Create(string foodName, string gameId, string userId)
        {
            var newFood = new Food
            {
                _id = ObjectId.GenerateNewId().ToString(),
                FoodName = foodName,
                GameId = gameId,
                UserId = userId
            };
            await _food.InsertOneAsync(newFood);
            return newFood;
        }

        public async Task Delete(string id)
        {
            await _food.DeleteOneAsync(food => food._id == id);
        }

    }

    public class Food
    {
        [BsonId]
        public string _id { get; set; }
        [BsonElement("food")]
        public string FoodName { get; set; }
        [BsonElement("game")]
        public string GameId { get; set; }
        [BsonElement("user")]
        public string UserId { get; set; }
    }

}
