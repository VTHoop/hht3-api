using hht.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Services
{
    public class LocationService
    {
        private readonly ILocationRepository _locations;

        public LocationService(ILocationRepository locations)
        {
            _locations = locations;
        }

        public async Task<Location> Create(string city, string state, int zip)
        {
            return await _locations.Create(city, state, zip);
            
        }

        public async Task<Location> getLocationForGame(string id)
        {
            return await _locations.findById(id);
        }

        public Models.Location ConvertToModel(Repositories.Location location)
        {
            return new Models.Location
            {
                _id = location._id,
                City = location.City,
                State = location.State,
                Zip = location.Zip,
            };
        }
    }
}
