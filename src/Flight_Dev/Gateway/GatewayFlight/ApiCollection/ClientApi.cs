using GatewayFlight.ApiCollection.InfraEstructura;
using GatewayFlight.ApiCollection.Interface;
using GatewayFlight.Model.Client;
using GatewayFlight.Response;
using GatewayFlight.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public async Task<ResponseMessage> EditClient(ClientModel model)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlFlight)
                        .SetPath(_settings.Value.EditClient)
                        .HttpMethod(HttpMethod.Post)
                        .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendRequest<ResponseMessage>(message);
        }

        public async Task<List<ClientModel>> GetClientByFlightNumber(string FlightNumber)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlFlight)
                                  .SetPath(_settings.Value.GetClientByFlightNumber)
                                  .AddQueryString("FlightNumber", FlightNumber)
                                  .HttpMethod(HttpMethod.Get)
                                  .GetHttpMessage();

            return await SendRequest<List<ClientModel>>(message);
        }
    }
}
