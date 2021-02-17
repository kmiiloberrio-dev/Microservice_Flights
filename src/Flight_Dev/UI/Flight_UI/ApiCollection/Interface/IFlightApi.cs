using CommonFlight.Pagination;
using Flight_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_UI.ApiCollection.Interface
{
    public interface IFlightApi
    {
        Task<string> CreateFlight(FlightApiModel model);
        Task<DataCollection<FlightTransportModel>> GetAllFlightPaginate(string Page, string Size, string FlightNumber);
    }
}
