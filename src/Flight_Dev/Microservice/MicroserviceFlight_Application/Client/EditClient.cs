using MediatR;
using MicroserviceFlight_Application.Response;
using MicroserviceFlight_InfraEstructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using persistence = MicroserviceFlight_Core.Persistence;

namespace MicroserviceFlight_Application.Client
{
    public class EditClient
    {
        public class ExecuteEditClient : IRequest<ResponseMessage>
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }

        public class Fire : IRequestHandler<ExecuteEditClient, ResponseMessage>
        {
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext)
            {
                _FlightDBContext = flightDBContext;
            }

            public async Task<ResponseMessage> Handle(ExecuteEditClient request, CancellationToken cancellationToken)
            {
                var client = await _FlightDBContext.Clients.Where(x => x.Id == Convert.ToInt32(request.Id)).FirstOrDefaultAsync();
                client.Phone = request.Phone;
                client.Email = request.Email;

                _FlightDBContext.Clients.Attach(client);
                await _FlightDBContext.SaveChangesAsync();

                return new ResponseMessage()
                {
                    Success = true,
                    Message = "Cambios guardados"
                };
            }
        }
    }
}
