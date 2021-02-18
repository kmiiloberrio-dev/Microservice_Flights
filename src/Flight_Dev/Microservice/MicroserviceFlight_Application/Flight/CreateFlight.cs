using FluentValidation;
using MediatR;
using MicroserviceFlight_Application.Client;
using MicroserviceFlight_Application.ClientFlight;
using MicroserviceFlight_Application.Response;
using MicroserviceFlight_Application.Transport;
using MicroserviceFlight_Core.DataTransferObject;
using MicroserviceFlight_InfraEstructure.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using persistence = MicroserviceFlight_Core.Persistence;

namespace MicroserviceFlight_Application.Flight
{
    public class CreateFlight
    {
        public class ExecuteCreateFligh : IRequest<ResponseMessage>
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
            public ValidationCreateFligth()
            {
                RuleFor(x => x.ArrivalStation).NotNull();
            }
        }

        public class Fire : IRequestHandler<ExecuteCreateFligh, ResponseMessage>
        {
            private readonly IMediator _IMediator;
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext,
                IMediator mediator)
            {
                _FlightDBContext = flightDBContext;
                _IMediator = mediator;
            }

            public async Task<ResponseMessage> Handle(ExecuteCreateFligh request, CancellationToken cancellationToken)
            {

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
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

                    foreach (var item in request.ListClient)
                    {
                        int clientId = await _IMediator.Send(new CreateClient.ExecuteCreateClient()
                        {
                            Email = item.Email,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Phone = item.Phone
                        });

                        int clientFlightId = await _IMediator.Send(new CreateClientFlight.ExecuteCreateClientFlight()
                        {
                            ClientId = clientId,
                            FlightId = flight.Id
                        });
                    }

                    scope.Complete();
                    return new ResponseMessage()
                    {
                        Message = "Compra exitosa",
                        Success = true
                    };
                }
            }
        }
    }
}
