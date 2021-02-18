using CommonFlight.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MicroserviceFlight_Api.Middleware
{
    public class MagamentExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MagamentExceptionMiddleware> _logger;
        public MagamentExceptionMiddleware(RequestDelegate next, ILogger<MagamentExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManagementExcepcionAsincrono(context, ex, _logger);
            }
        }

        private async Task ManagementExcepcionAsincrono(HttpContext context, Exception ex, ILogger<MagamentExceptionMiddleware> logger)
        {
            object errors = null;
            switch (ex)
            {
                case ManagementExcepcion me:
                    logger.LogInformation(ex, "Manejador Error");
                    errors = me.Errors;
                    context.Response.StatusCode = (int)me.Code;
                    break;
                case Exception e:
                    logger.LogError(ex, "Error de Servidor");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errors });
                await context.Response.WriteAsync(resultados);
            }

        }
    }
}