using MediatR;
using MicroserviceFlight_Core.DataTransferObject;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceFlight_Application.Flight
{
    public class SearchFlightByTransportNumber
    {
        public class ExecuteSearchFlightByTransportNumber : IRequest<FlightDto>
        {
            public string TransportNumber
            {
                get; set;
            }

            public class Fire : IRequestHandler<ExecuteSearchFlightByTransportNumber, FlightDto>
            {
                public Task<FlightDto> Handle(ExecuteSearchFlightByTransportNumber request, CancellationToken cancellationToken)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}