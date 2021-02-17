using CommonFlight.Pagination;
using GatewayFlight.Model.Flight;
using GatewayFlight.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayFlight.ApiCollection.Interface
{
    public interface IFlightApi
    {
        Task<ResponseMessage> CreateFlight(FlightModel model);
        Task<DataCollection<FlightTransportModel>> GetAllFlightPaginate(string Page, string Size, string FlightNumber);
    }
}
