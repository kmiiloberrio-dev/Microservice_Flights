﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceFlight_Core.DataTransferObject
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
    }
}
