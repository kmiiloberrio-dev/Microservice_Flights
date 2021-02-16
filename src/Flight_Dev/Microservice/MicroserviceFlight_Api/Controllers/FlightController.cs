using MediatR;
using MicroserviceFlight_Application.Flight;
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
        public async Task<bool> CreateFlight(CreateFlight.ExecuteCreateFligh model)
        {
            return await _IMediator.Send(model);
        }
    }
}
