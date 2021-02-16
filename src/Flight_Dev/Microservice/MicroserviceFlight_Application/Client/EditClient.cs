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
    public class EditClient
    {
        public class ExecuteEditClient : IRequest<bool>
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int? Phone { get; set; }
            public string Email { get; set; }
        }

        public class Fire : IRequestHandler<ExecuteEditClient, bool>
        {
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext)
            {
                _FlightDBContext = flightDBContext;
            }

            public async Task<bool> Handle(ExecuteEditClient request, CancellationToken cancellationToken)
            {
                persistence.Client client = new persistence.Client()
                {
                    FirstName = request.FirstName,
                    Email = request.Email,
                    Id = request.Id,
                    LastName = request.LastName,
                    Phone = request.Phone
                };

                _FlightDBContext.Clients.Attach(client);
                await _FlightDBContext.SaveChangesAsync();

                return true;
            }
        }
    }
}
