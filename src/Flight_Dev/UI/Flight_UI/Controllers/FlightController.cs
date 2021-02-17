using Flight_UI.ApiCollection.Interface;
using Flight_UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public IActionResult SearchFlight(string draw, string start, string length,
            string txtOrigin, string txtDestination, string txtDate)
        {
            List<VuelosEjemplo> vuelosEjemplo = new List<VuelosEjemplo>()
            {
              new VuelosEjemplo()
              {  ArrivalStation = "Bogota",
                 DepartureStation = "Medellin",
                 DepartureDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                 Price = "123.34",
                 Currency = "COP"
              },
              new VuelosEjemplo()
              {
                ArrivalStation = "Manizales",
                DepartureStation = "Bogota",
                DepartureDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Price = "123.34",
                Currency = "COP"
              }
            };

            if (txtOrigin != null)
            {
                vuelosEjemplo = vuelosEjemplo.Where(x => x.ArrivalStation == txtOrigin).ToList();
            }

            var jsonData = new { draw, recordsFiltered = start, recordsTotal = length, data = vuelosEjemplo };
            return Ok(jsonData);
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
                return Json(new { success = true, mensaje = "Compra exitosa" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new { success = false, mensaje = "ex.Message" });
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

            var resul = await  _IFligthApi.GetAllFlightPaginate(skip.ToString(), pageSize.ToString(), txtOrigin);
            var jsonData = new { draw, recordsFiltered = resul.Total, recordsTotal = resul.Total, data = resul.Items };
            return Ok(jsonData);
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
