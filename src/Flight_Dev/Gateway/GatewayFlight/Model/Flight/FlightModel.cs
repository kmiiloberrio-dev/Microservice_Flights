using GatewayFlight.Model.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayFlight.Model.Flight
{
    public class FlightModel
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public List<ClientModel> ListClient { get; set; }
    }
}
