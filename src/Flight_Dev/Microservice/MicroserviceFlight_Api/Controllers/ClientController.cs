using MediatR;
using MicroserviceFlight_Application.Client;
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
    public class ClientController : ControllerBase
    {
        private readonly IMediator _IMediator;
        public ClientController(IMediator mediator)
        {
            _IMediator = mediator;
        }

        [HttpPost("EditClient")]
        public async Task<ResponseMessage> EditClient(EditClient.ExecuteEditClient model)
        {
            return await _IMediator.Send(model);
        }

        [HttpGet("GetClientByFlightNumber")]
        public async Task<List<ClientDto>>GetClientByFlightNumber(string FlightNumber)
        {
            return await _IMediator.Send(new GetClientByFlightNumber.ExecuteGetClientByFlightNumber()
            {
                FlightNumber = FlightNumber
            });
        }
    }
}
