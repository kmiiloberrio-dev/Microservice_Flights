using System;
using System.Collections.Generic;

#nullable disable

namespace MicroserviceFlight_Core.Persistence
{
    public partial class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
    }
}
