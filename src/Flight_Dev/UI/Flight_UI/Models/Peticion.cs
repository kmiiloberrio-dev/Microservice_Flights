using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_UI.Models
{
    public class Peticion
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string From { get; set; }
    }

    public class Elementos
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureDate { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string FlightNumber { get; set; }
    }
}
