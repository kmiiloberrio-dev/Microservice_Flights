using Flight_UI.ApiCollection.Interface;
using Flight_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flight_UI.Controllers
{
    public class FlightController : Controller
    {
        private readonly ILogger<FlightController> _logger;
        private readonly IFlightApi _IFligthApi;

        public FlightController(ILogger<FlightController> logger, IFlightApi flightApi)
        {
            _logger = logger;
            _IFligthApi = flightApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchFlight(string draw, string start, string length,
            string txtOrigin, string txtDestination, string txtDate)
        {

            try
            {
                Peticion peticion = new Peticion()
                {
                    Origin = txtOrigin,
                    Destination = txtDestination,
                    From = Convert.ToDateTime(txtDate).ToString("yyy-MM-dd")
                };

                var json = JsonConvert.SerializeObject(peticion);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                var response = await client.PostAsync("http://testapi.vivaair.com/otatest/api/values", data);
                string result = response.Content.ReadAsStringAsync().Result;

                dynamic resulta = JsonConvert.DeserializeObject(result);

                List<Elementos> elementos = JsonConvert.DeserializeObject<List<Elementos>>(resulta);


                var jsonData = new { draw, recordsFiltered = start, recordsTotal = length, data = elementos };
                return Ok(jsonData);
            }
            catch
            {
                List<Elementos> elementos = new List<Elementos>();
                var jsonData = new { draw, recordsFiltered = start, recordsTotal = length, data = elementos };
                return Ok(jsonData);
            }
        }

        public IActionResult SaleFlight(string ArrivalStation, string DepartureStation,
            string DepartureDate, string Price, string Currency)
        {
            FlightModel flightModel = new FlightModel()
            {
                ArrivalStation = ArrivalStation,
                DepartureStation = DepartureStation,
                DepartureDate = DepartureDate,
                Price = Price,
                Currency = Currency,
            };

            return View(flightModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaleFlighRegistration([FromBody] FlightModel model)
        {
            try
            {
                FlightApiModel flightModel = new FlightApiModel()
                {
                    ArrivalStation = model.ArrivalStation,
                    Currency = model.Currency,
                    DepartureDate = Convert.ToDateTime(model.DepartureDate),
                    DepartureStation = model.DepartureStation,
                    Price = Convert.ToDecimal(model.Price),
                    ListClient = model.ListClient

                };

                var result = await _IFligthApi.CreateFlight(flightModel);
                return Json(new { success = result.Success, mensaje = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new { success = false, mensaje = ex.Message });
            }
        }

        public IActionResult ListFlightSale()
        {
            return View();
        }

        public async Task<IActionResult> GetAllFlightSale(string draw, string start, string length,
            string txtOrigin, string txtDestination, string txtDate)
        {
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int page = Convert.ToInt32(start) / pageSize;
            int skip = page + 1;

            var resul = await _IFligthApi.GetAllFlightPaginate(skip.ToString(), pageSize.ToString(), txtOrigin);
            var jsonData = new { draw, recordsFiltered = resul.Total, recordsTotal = resul.Total, data = resul.Items };
            return Ok(jsonData);
        }

        public async Task<IActionResult> EditSaleFlight(string ArrivalStation, string DepartureStation,
            string DepartureDate, string Price, string Currency, string FlightNumber)
        {
            var result = await _IFligthApi.GetClientByFlightNumber(FlightNumber);

            EditFlight editFlight = new EditFlight()
            {
                ArrivalStation = ArrivalStation,
                Currency = Currency,
                DepartureDate = DepartureDate,
                DepartureStation = DepartureStation,
                Price = Price,
                ListClient = result
            };
            return View(editFlight);
        }

        [HttpPost]
        public async Task<IActionResult> EditFlight([FromBody] List<ClientModel> model)
        {
            try
            {
                foreach (var item in model)
                {
                    var result = await _IFligthApi.EditClient(item);
                }

                return Json(new { success = true, mensaje = "Cambios guardados" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, mensaje = ex.Message });
            }
        }

        public async Task<IActionResult> DownloadFile(string FlightNumber)
        {
            var result = await _IFligthApi.DownloadFile(FlightNumber);
            byte[] document = Convert.FromBase64String(result.FileBase64);
            return File(document, "application/octet-stream", $"{FlightNumber}.pdf");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
