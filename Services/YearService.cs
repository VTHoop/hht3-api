using hht.API.Models;
using hht.API.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Services
{
    public class YearService
    {
        private readonly IYearRepository _years;

        public YearService(IYearRepository years)
        {
            _years = years;
        }

        public async Task<List<Models.Year>> Get()
        {
            var years = (await _years.Get()).Select(s => ConvertToModel(s)).ToList();
            return years;
        }

        public Models.Year Get(string id)
        {
            return ConvertToModel(_years.Get(id));
        }

        public async Task<Models.Year> GetYearFromToday()
        {
            return ConvertToModel(await _years.GetYearFromToday());
        }

        public Models.Year Create(Repositories.Year year)
        {
            return ConvertToModel(_years.Create(year));
        }

        private Models.Year ConvertToModel(Repositories.Year year)
        {
            return new Models.Year
            {
                _id = year._id,
                StartDate = year.StartDate,
                EndDate = year.EndDate,
                Name = year.Name,
            };
        }
    }
}
