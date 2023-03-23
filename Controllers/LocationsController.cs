using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hht.API.Models;
using hht.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace hht.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly LocationService _locationService;

        public LocationsController(LocationService locationService)
        {
            _locationService = locationService;
        }
        // GET api/values
        //[HttpGet]
        //public async Task<ActionResult<List<Team>>> Get()
        //{
        //    return Ok(await _teamService.GetAll());
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Location>> Post([FromBody] Location location)
        {
            var newLocation = await _locationService.Create(location.City, location.State, location.Zip);
            return CreatedAtAction(nameof(Get), new { id = newLocation._id }, newLocation._id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
