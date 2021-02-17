using MediatR;
using MicroserviceFlight_Core.DataTransferObject;
using MicroserviceFlight_InfraEstructure.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceFlight_Application.Client
{
    public class GetClientByFlightNumber
    {
        public class ExecuteGetClientByFlightNumber : IRequest<List<ClientDto>>
        {
            public string FlightNumber { get; set; }
        }

        public class Fire : IRequestHandler<ExecuteGetClientByFlightNumber, List<ClientDto>>
        {
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext)
            {
                _FlightDBContext = flightDBContext;
            }

            public async Task<List<ClientDto>> Handle(ExecuteGetClientByFlightNumber request, CancellationToken cancellationToken)
            {
                var result = from transport in _FlightDBContext.Transports
                             join flight in _FlightDBContext.Flights
                             on transport.Id equals flight.TransportId
                             join clientflight in _FlightDBContext.ClientFlights
                             on flight.Id equals clientflight.FlightId
                             join client in _FlightDBContext.Clients
                             on clientflight.ClientId equals client.Id
                             select new
                             {
                                 transport,
                                 client
                             };

                if(request.FlightNumber != null)
                {
                    result = result.Where(x => x.transport.FlightNumber == request.FlightNumber);
                }

                var resultList = await result.ToListAsync();

                return resultList.Select(x => new ClientDto()
                {
                    Email = x.client.Email,
                    FirstName = x.client.FirstName,
                    Id = x.client.Id.ToString(),
                    LastName = x.client.LastName,
                    Phone = x.client.Phone
                }).ToList();

            }
        }
    }
}
