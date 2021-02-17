using GatewayFlight.Model.Client;
using GatewayFlight.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GatewayFlight.ApiCollection.Interface
{
    public interface IClientApi
    {
        Task<ResponseMessage> EditClient(ClientModel model);
        Task<List<ClientModel>> GetClientByFlightNumber(string FlightNumber);
    }
}
