using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayFlight.Setting
{
    public class ApiSettings
    {
        public string UrlFlight { get; set; }
        public string CreateFlight { get; set; }
        public string GetAllFlightPaginate { get; set; }
        public string EditClient { get; set; }
        public string GetClientByFlightNumber { get; set; }
    }
}
