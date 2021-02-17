using GatewayFlight.ApiCollection.InfraEstructura;
using GatewayFlight.ApiCollection.Interface;
using GatewayFlight.Model.Client;
using GatewayFlight.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GatewayFlight.ApiCollection
{
    public class ClientApi : BaseHttpClientWithFactory, IClientApi
    {
        private readonly IOptions<ApiSettings> _settings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClientApi(IHttpClientFactory factory,
            IOptions<ApiSettings> settings,
            IHttpContextAccessor httpContextAccessor) : base(factory)
        {
            _settings = settings;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> EditClient(ClientModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientModel>> GetClientByFlightNumber(string FlightNumber)
        {
            throw new NotImplementedException();
        }
    }
}
