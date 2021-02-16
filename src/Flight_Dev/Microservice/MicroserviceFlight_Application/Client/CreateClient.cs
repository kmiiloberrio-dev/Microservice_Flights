using MediatR;
using MicroserviceFlight_InfraEstructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using persistence = MicroserviceFlight_Core.Persistence;

namespace MicroserviceFlight_Application.Client
{
    public class CreateClient
    {
        public class ExecuteCreateClient : IRequest<int>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? Phone { get; set; }
            public string Email { get; set; }
        }

        public class Fire : IRequestHandler<ExecuteCreateClient, int>
        {
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext)
            {
                _FlightDBContext = flightDBContext;
            }

            public async Task<int> Handle(ExecuteCreateClient request, CancellationToken cancellationToken)
            {
                persistence.Client client = new persistence.Client()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Phone = request.Phone
                };

                _FlightDBContext.Clients.Add(client);
                await _FlightDBContext.SaveChangesAsync();

                return client.Id;
            }
        }
    }
}
