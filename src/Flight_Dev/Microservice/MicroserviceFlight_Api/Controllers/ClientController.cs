using MediatR;
using MicroserviceFlight_Application.Client;
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

        [HttpPost("CreateFlight")]
        public async Task<bool> CreateFlight(EditClient.ExecuteEditClient model)
        {
            return await _IMediator.Send(model);
        }
    }
}
