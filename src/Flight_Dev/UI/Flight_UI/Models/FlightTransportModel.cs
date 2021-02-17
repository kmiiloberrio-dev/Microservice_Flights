using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_UI.Models
{
    public class FlightTransportModel
    {
        public int Id { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime? DepartureDate { get; set; }
        public decimal? Price { get; set; }
        public string Currency { get; set; }
        public int TransportId { get; set; }
        public string TransportNumber { get; set; }
    }
}
