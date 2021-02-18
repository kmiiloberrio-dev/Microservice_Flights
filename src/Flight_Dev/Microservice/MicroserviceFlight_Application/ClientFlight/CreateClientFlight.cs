using MediatR;
using MicroserviceFlight_InfraEstructure.Models;
using System.Threading;
using System.Threading.Tasks;
using persistence = MicroserviceFlight_Core.Persistence;

namespace MicroserviceFlight_Application.ClientFlight
{
    public class CreateClientFlight
    {
        public class ExecuteCreateClientFlight : IRequest<int>
        {
            public int ClientId { get; set; }
            public int FlightId { get; set; }
        }

        public class Fire : IRequestHandler<ExecuteCreateClientFlight, int>
        {
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext)
            {
                _FlightDBContext = flightDBContext;
            }

            public async Task<int> Handle(ExecuteCreateClientFlight request, CancellationToken cancellationToken)
            {
                persistence.ClientFlight clientFlight = new persistence.ClientFlight()
                {
                    ClientId = request.ClientId,
                    FlightId = request.FlightId
                };

                _FlightDBContext.ClientFlights.Add(clientFlight);
                await _FlightDBContext.SaveChangesAsync();

                return clientFlight.FlightId;
            }
        }
    }
}
