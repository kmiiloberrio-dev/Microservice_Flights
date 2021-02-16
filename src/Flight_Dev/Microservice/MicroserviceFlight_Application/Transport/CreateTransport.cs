using CommonFlight.FlightNumber;
using MediatR;
using MicroserviceFlight_InfraEstructure.Models;
using System.Threading;
using System.Threading.Tasks;
using persistence = MicroserviceFlight_Core.Persistence;

namespace MicroserviceFlight_Application.Transport
{
    public class CreateTransport
    {
        public class ExecuteCreateTransport : IRequest<int>
        {

        }

        public class Fire : IRequestHandler<ExecuteCreateTransport, long>
        {
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext)
            {
                _FlightDBContext = flightDBContext;
            }

            public async Task<int> Handle(ExecuteCreateTransport request, CancellationToken cancellationToken)
            {
                string flightNumber = new RandomNumber().RandomString(7);
                persistence.Transport transport = new persistence.Transport()
                {
                    FlightNumber = flightNumber
                };

                _FlightDBContext.Transports.Add(transport);
                await _FlightDBContext.SaveChangesAsync();

                return transport.Id;
            }
        }
    }
}
