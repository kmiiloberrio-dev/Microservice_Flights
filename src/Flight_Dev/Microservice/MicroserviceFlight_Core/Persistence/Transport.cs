using System;
using System.Collections.Generic;

#nullable disable

namespace MicroserviceFlight_Core.Persistence
{
    public partial class Transport
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
    }
}
