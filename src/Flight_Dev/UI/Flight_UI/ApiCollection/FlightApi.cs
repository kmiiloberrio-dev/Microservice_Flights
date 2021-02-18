using CommonFlight.Pagination;
using Flight_UI.ApiCollection.InfraEstructura;
using Flight_UI.ApiCollection.Interface;
using Flight_UI.Models;
using Flight_UI.Models.Response;
using Flight_UI.Response;
using Flight_UI.Setting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flight_UI.ApiCollection
{
    public class FlightApi : BaseHttpClientWithFactory, IFlightApi
    {
        private readonly IOptions<ApiSettings> _settings;
        public FlightApi(IHttpClientFactory factory,
            IOptions<ApiSettings> settings) : base(factory)
        {
            _settings = settings;
        }

        public async Task<ResponseMessage> CreateFlight(FlightApiModel model)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlGateway)
                        .SetPath(_settings.Value.CreateFlight)
                        .HttpMethod(HttpMethod.Post)
                        .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendRequest<ResponseMessage>(message);
        }

        public async Task<DataCollection<FlightTransportModel>> GetAllFlightPaginate(string Page, string Size, string FlightNumber)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlGateway)
                                  .SetPath(_settings.Value.GetAllFlightPaginate)
                                  .AddQueryString("FlightNumber", FlightNumber)
                                  .AddQueryString("Page", Page)
                                  .AddQueryString("Size", Size)
                                  .HttpMethod(HttpMethod.Get)
                                  .GetHttpMessage();

            return await SendRequest<DataCollection<FlightTransportModel>>(message);
        }

        public async Task<List<ClientModel>> GetClientByFlightNumber(string FlightNumber)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlGateway)
                                  .SetPath(_settings.Value.GetClientByFlightNumber)
                                  .AddQueryString("FlightNumber", FlightNumber)
                                  .HttpMethod(HttpMethod.Get)
                                  .GetHttpMessage();

            return await SendRequest<List<ClientModel>>(message);
        }

        public async Task<ResponseMessage> EditClient(ClientModel model)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlGateway)
                        .SetPath(_settings.Value.EditClient)
                        .HttpMethod(HttpMethod.Post)
                        .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendRequest<ResponseMessage>(message);
        }

        public async Task<ResponseMessageFile> DownloadFile(string FlightNumber)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlGateway)
                               .SetPath(_settings.Value.DownloadFile)
                               .AddQueryString("FlightNumber", FlightNumber)
                               .HttpMethod(HttpMethod.Get)
                               .GetHttpMessage();

            return await SendRequest<ResponseMessageFile>(message);
        }
    }
}
