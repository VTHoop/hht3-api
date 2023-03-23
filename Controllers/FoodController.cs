using hht.API.Models;
using hht.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodService _foodService;

        public FoodController(FoodService foodService)
        {
            _foodService = foodService;
        }

        // GET api/values/5
        [HttpGet("all/games/{gameId}")]
        public async Task<ActionResult<List<Food>>> Get(string gameId)
        {
            return Ok(await _foodService.GetFoodForGame(gameId));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Location>> Post([FromBody] string foodName, string gameId, string userId)
        {
            var newFood = await _foodService.CreateFood(foodName, gameId, userId);
            return CreatedAtAction(nameof(Get), new { id = newFood._id }, newFood._id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(string foodId)
        {
            await _foodService.DeleteFood(foodId);
            return;
        }
    }
}
