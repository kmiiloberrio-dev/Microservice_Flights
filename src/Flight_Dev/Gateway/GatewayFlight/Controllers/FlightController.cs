using GatewayFlight.ApiCollection.Interface;
using GatewayFlight.Model.Flight;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayFlight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightApi _IFlightApi;
        public FlightController(IFlightApi flightApi)
        {
            _IFlightApi = flightApi;
        }

        [HttpPost("CreateFlight")]
        public async Task<string> CreateFlight(FlightModel model)
        {
            return await _IFlightApi.CreateFlight(model);
        }
    }
}
