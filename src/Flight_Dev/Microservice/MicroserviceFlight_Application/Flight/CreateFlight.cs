using FluentValidation;
using MediatR;
using MicroserviceFlight_Application.Transport;
using MicroserviceFlight_Core.DataTransferObject;
using MicroserviceFlight_InfraEstructure.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using persistence = MicroserviceFlight_Core.Persistence;

namespace MicroserviceFlight_Application.Flight
{
    public class CreateFlight
    {
        public class ExecuteCreateFligh : IRequest<bool>
        {
            public string DepartureStation { get; set; }
            public string ArrivalStation { get; set; }
            public DateTime DepartureDate { get; set; }
            public decimal Price { get; set; }
            public string Currency { get; set; }
            public List<ClientDto> ListClient { get; set; }
        }

        public class ValidationCreateFligth : AbstractValidator<ExecuteCreateFligh>
        {

        }

        public class Fire : IRequestHandler<ExecuteCreateFligh, bool>
        {
            private readonly IMediator _IMediator;
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext,
                IMediator mediator)
            {
                _FlightDBContext = flightDBContext;
                _IMediator = mediator;
            }

            public async Task<bool> Handle(ExecuteCreateFligh request, CancellationToken cancellationToken)
            {
                int transportId = await _IMediator.Send(new CreateTransport.ExecuteCreateTransport());

                persistence.Flight flight = new persistence.Flight()
                {
                    ArrivalStation = request.ArrivalStation,
                    Currency = request.Currency,
                    DepartureDate = request.DepartureDate,
                    DepartureStation = request.DepartureStation,
                    Price = request.Price,
                    TransportId = transportId
                };

                _FlightDBContext.Flights.Add(flight);
                await _FlightDBContext.SaveChangesAsync();

                foreach(var item in request.ListClient)
                {

                }

            }
        }
    }
}
