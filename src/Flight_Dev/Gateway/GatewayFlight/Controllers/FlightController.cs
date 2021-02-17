using CommonFlight.Pagination;
using GatewayFlight.ApiCollection.Interface;
using GatewayFlight.Model.Client;
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
        private readonly IClientApi _IClientApi;
        public FlightController(IFlightApi flightApi,
            IClientApi clientApi)
        {
            _IFlightApi = flightApi;
            _IClientApi = clientApi;
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

        [HttpGet("GetClientByFlightNumber")]
        public async Task<List<ClientModel>> GetClientByFlightNumber(string FlightNumber)
        {
            return await _IClientApi.GetClientByFlightNumber(FlightNumber);
        }

        [HttpPost("EditClient")]
        public async Task<ResponseMessage> EditClient([FromBody]ClientModel model)
        {
            return await _IClientApi.EditClient(model);
        }

    }
}
