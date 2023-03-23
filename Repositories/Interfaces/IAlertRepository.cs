using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Repositories
{
    public interface IAlertRepository
    {
        Task CreateDefaults(string userId, List<string> alertNames);
    }
}
