using System;
using System.Net;

namespace CommonFlight.Logger
{
    public class ManagementExcepcion : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errors { get; }
        public ManagementExcepcion(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}
