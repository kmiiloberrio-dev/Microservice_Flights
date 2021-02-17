﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_UI.Models
{
    public class FlightModel
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureDate { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public List<ClientModel> ListClient { get; set; }
    }
}
