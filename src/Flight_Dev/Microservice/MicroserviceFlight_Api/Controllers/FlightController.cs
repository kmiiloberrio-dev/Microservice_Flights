using CommonFlight.Pagination;
using MediatR;
using MicroserviceFlight_Application.ClientFlight;
using MicroserviceFlight_Application.Flight;
using MicroserviceFlight_Application.Response;
using MicroserviceFlight_Core.DataTransferObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceFlight_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IMediator _IMediator;
        public FlightController(IMediator mediator)
        {
            _IMediator = mediator;
        }

        [HttpPost("CreateFlight")]
        public async Task<ResponseMessage> CreateFlight(CreateFlight.ExecuteCreateFligh model)
        {
            return await _IMediator.Send(model);
        }

        [HttpGet("GetAllFlightPaginate")]
        public async Task<DataCollection<FlightTransportDto>> GetAllFlightPaginate(int Page, int Size, string FlightNumber)
        {
            return await _IMediator.Send(new GetAllFlightPaginate.ExecuteGetAllFlightPaginate()
            {
                Page = Page,
                Size = Size,
                FlightNumber = FlightNumber
            });
        }
    }
}
