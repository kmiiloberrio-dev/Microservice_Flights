﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceFlight_Core.DataTransferObject
{
    public class ClientDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}