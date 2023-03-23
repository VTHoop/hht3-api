using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public interface IYearRepository
    {
        Task<List<Year>> Get();
        Year Get(string id);
        Task<Year> GetYearFromToday();
        Year Create(Year year);
    }
}
