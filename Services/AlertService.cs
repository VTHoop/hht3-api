using hht.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hht.API.Services
{
    public class AlertService
    {
        private readonly IAlertRepository _alerts;

        public AlertService(IAlertRepository alerts)
        {
            _alerts = alerts;
        }
        public async Task SetupDefaults(string userId)
        {
            var alerts = Enum.GetNames(typeof(AlertNames)).Cast<string>().ToList();
            await _alerts.CreateDefaults(userId, alerts);

        }
        public enum AlertNames { tickets, HHT_preview, score }

    }
}
