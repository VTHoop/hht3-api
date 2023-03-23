using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hht.API.Models;
using hht.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hht.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearController : ControllerBase
    {
        private readonly YearService _yearService;

        public YearController(YearService yearService)
        {
            _yearService = yearService;
        }

        //GET api/values
       [HttpGet]
        public async Task<ActionResult<List<Year>>> Get()
        {
            return await _yearService.Get();
        }


        // GET api/values/5
        [HttpGet("{id}", Name = "GetYear")]
        public ActionResult<Year> Get(string id)
        {
            var year = _yearService.Get(id);
            if (year == null) {
                return NotFound();
            }
            return year;

        }

        // POST api/values
        [HttpPost]
        public void Post()
        {

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
