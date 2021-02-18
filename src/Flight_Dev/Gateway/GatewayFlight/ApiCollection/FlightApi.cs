using CommonFlight.Pagination;
using GatewayFlight.ApiCollection.InfraEstructura;
using GatewayFlight.ApiCollection.Interface;
using GatewayFlight.Model.Flight;
using GatewayFlight.Model.Response;
using GatewayFlight.Response;
using GatewayFlight.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GatewayFlight.ApiCollection
{
    public class FlightApi : BaseHttpClientWithFactory, IFlightApi
    {
        private readonly IOptions<ApiSettings> _settings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FlightApi(IHttpClientFactory factory,
            IOptions<ApiSettings> settings,
            IHttpContextAccessor httpContextAccessor) : base(factory)
        {
            _settings = settings;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseMessage> CreateFlight(FlightModel model)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlFlight)
                        .SetPath(_settings.Value.CreateFlight)
                        .HttpMethod(HttpMethod.Post)
                        .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendRequest<ResponseMessage>(message);
        }

        public async Task<ResponseMessageFile> DownloadFile(string FlightNumber)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlFlight)
                               .SetPath(_settings.Value.DownloadFile)
                               .AddQueryString("FlightNumber", FlightNumber)
                               .HttpMethod(HttpMethod.Get)
                               .GetHttpMessage();

            return await SendRequest<ResponseMessageFile>(message);
        }

        public async Task<DataCollection<FlightTransportModel>> GetAllFlightPaginate(string Page, string Size, string FlightNumber)
        {
            var message = new HttpRequestBuilder(_settings.Value.UrlFlight)
                                  .SetPath(_settings.Value.GetAllFlightPaginate)
                                  .AddQueryString("FlightNumber", FlightNumber)
                                  .AddQueryString("Page", Page)
                                  .AddQueryString("Size", Size)
                                  .HttpMethod(HttpMethod.Get)
                                  .GetHttpMessage();

            return await SendRequest<DataCollection<FlightTransportModel>>(message);
        }
    }
}
