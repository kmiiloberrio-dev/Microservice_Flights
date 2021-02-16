using GatewayFlight.Model.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayFlight.ApiCollection.Interface
{
    public interface IFlightApi
    {
        Task<string> CreateFlight(FlightModel model);
    }
}
