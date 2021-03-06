using CommonFlight.Pagination;
using Flight_UI.Models;
using Flight_UI.Models.Response;
using Flight_UI.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flight_UI.ApiCollection.Interface
{
    public interface IFlightApi
    {
        Task<ResponseMessage> CreateFlight(FlightApiModel model);
        Task<DataCollection<FlightTransportModel>> GetAllFlightPaginate(string Page, string Size, string FlightNumber);
        Task<List<ClientModel>> GetClientByFlightNumber(string FlightNumber);
        Task<ResponseMessage> EditClient(ClientModel model);
        Task<ResponseMessageFile> DownloadFile(string FlightNumber);
    }
}
