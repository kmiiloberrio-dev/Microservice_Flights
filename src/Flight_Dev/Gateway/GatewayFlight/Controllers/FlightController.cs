using CommonFlight.Pagination;
using GatewayFlight.ApiCollection.Interface;
using GatewayFlight.Model.Flight;
using GatewayFlight.Response;
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
        public async Task<ResponseMessage> CreateFlight([FromBody]FlightModel model)
        {
            return await _IFlightApi.CreateFlight(model);
        }

        [HttpGet("GetAllFlightPaginate")]
        public async Task<DataCollection<FlightTransportModel>> GetAllFlightPaginate(string Page, string Size, string FlightNumber)
        {
            return await _IFlightApi.GetAllFlightPaginate(Page, Size, FlightNumber);
        }

    }
}
