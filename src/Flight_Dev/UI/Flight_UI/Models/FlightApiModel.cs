
using System;
using System.Collections.Generic;


namespace Flight_UI.Models
{
    public class FlightApiModel
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public List<ClientModel> ListClient { get; set; }
    }
}
