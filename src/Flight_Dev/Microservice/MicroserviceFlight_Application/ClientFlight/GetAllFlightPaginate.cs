using CommonFlight.Pagination;
using MediatR;
using MicroserviceFlight_Core.DataTransferObject;
using MicroserviceFlight_InfraEstructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceFlight_Application.ClientFlight
{
    public class GetAllFlightPaginate
    {
        public class ExecuteGetAllFlightPaginate : IRequest<DataCollection<FlightTransportDto>>
        {
            public int Page { get; set; }
            public int Size { get; set; }
            public string FlightNumber { get; set; }
        }

        public class Fire : IRequestHandler<ExecuteGetAllFlightPaginate, DataCollection<FlightTransportDto>>
        {
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext)
            {
                _FlightDBContext = flightDBContext;
            }

            public async Task<DataCollection<FlightTransportDto>> Handle(ExecuteGetAllFlightPaginate request, CancellationToken cancellationToken)
            {
                var result = from flight in _FlightDBContext.Flights
                             join transport in _FlightDBContext.Transports
                             on flight.TransportId equals transport.Id
                             select new
                             {
                                 flight,
                                 transport
                             };

                if (request.FlightNumber != null)
                {
                    result = result.Where(x => x.transport.FlightNumber == request.FlightNumber);
                }

                var resultList = await result.GetPagedAsync(request.Page, request.Size);

                DataCollection<FlightTransportDto> flightDto = new DataCollection<FlightTransportDto>()
                {
                    Page = resultList.Page,
                    Pages = resultList.Pages,
                    Total = resultList.Total,
                    Items = resultList.Items.Select(x => new FlightTransportDto()
                    {
                        ArrivalStation = x.flight.ArrivalStation,
                        Currency = x.flight.Currency,
                        DepartureDate = x.flight.DepartureDate,
                        DepartureStation = x.flight.DepartureStation,
                        Id = x.flight.Id,
                        Price = x.flight.Price,
                        TransportId = x.flight.TransportId,
                        TransportNumber = x.transport.FlightNumber
                    })
                };

                return flightDto;
            }
        }
    }
}
