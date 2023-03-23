using hht.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Services
{
    public class FoodService
    {
        private readonly IFoodRepository _food;
        private readonly UserService _userService;

        public FoodService(IFoodRepository food, UserService userService)
        {
            _food = food;
            _userService = userService;
        }

        public async Task<List<Models.Food>> GetFoodForGame(string gameId)
        {
            var food = await _food.GetByGame(gameId);
            var convertedFoodTasks = food.Select(s => ConvertToModel(s));
            return (await Task.WhenAll(convertedFoodTasks)).ToList();
        }

        public async Task<Models.Food> CreateFood(string foodName, string gameId, string userId)
        {
            return await ConvertToModel(await _food.Create(foodName, gameId, userId));
        }

        public async Task DeleteFood(string foodId)
        {
            await _food.Delete(foodId);
            return;
        }

        private async Task<Models.Food> ConvertToModel(Repositories.Food food)
        {
            return new Models.Food
            {
                _id = food._id,
                FoodName = food.FoodName,
                UserFirstName = (await _userService.FindUserById(food.UserId)).FirstName,
                UserLastName = (await _userService.FindUserById(food.UserId)).LastName,
            };
        }

    }
}
