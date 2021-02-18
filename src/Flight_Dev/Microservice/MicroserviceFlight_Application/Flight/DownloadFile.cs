using CommonFlight.Logger;
using FluentValidation;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using MicroserviceFlight_Application.Response;
using MicroserviceFlight_InfraEstructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceFlight_Application.Flight
{
    public class DownloadFile
    {
        public class ExecuteDownloadFile : IRequest<ResponseMessageFile>
        {
            public string FlightNumber { get; set; }
        }

        public class ValidationDownloadFile : AbstractValidator<ExecuteDownloadFile>
        {
            public ValidationDownloadFile()
            {
                RuleFor(x => x.FlightNumber).NotNull().NotEmpty();
            }
        }

        public class Fire : IRequestHandler<ExecuteDownloadFile, ResponseMessageFile>
        {
            private readonly FlightDBContext _FlightDBContext;
            public Fire(FlightDBContext flightDBContext)
            {
                _FlightDBContext = flightDBContext;
            }

            public async Task<ResponseMessageFile> Handle(ExecuteDownloadFile request, CancellationToken cancellationToken)
            {
                var result = await (from transport in _FlightDBContext.Transports
                                    join flight in _FlightDBContext.Flights
                                    on transport.Id equals flight.TransportId
                                    join clientflight in _FlightDBContext.ClientFlights
                                    on flight.Id equals clientflight.FlightId
                                    join client in _FlightDBContext.Clients
                                    on clientflight.ClientId equals client.Id
                                    where transport.FlightNumber == request.FlightNumber
                                    select new
                                    {
                                        transport,
                                        client,
                                        flight
                                    }).ToListAsync();

                if (result == null)
                {
                    throw new ManagementExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el ticket" });
                }


                MemoryStream memoryStream = new MemoryStream();
                Rectangle rectangle = new Rectangle(PageSize.A4);
                Document document = new Document(rectangle, 0, 0, 50, 100);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                writer.CloseStream = false;

                document.Open();
                document.AddTitle($"Tikect número de reserva {result.FirstOrDefault().transport.FlightNumber}");

                PdfPTable pdfTable = new PdfPTable(2);
                PdfPCell celda = new PdfPCell(new Phrase("Código de reserva"));
                pdfTable.AddCell(celda);
                PdfPCell celda0 = new PdfPCell(new Phrase(result.FirstOrDefault().transport.FlightNumber));
                pdfTable.AddCell(celda0);
                PdfPCell celda1 = new PdfPCell(new Phrase("Origen"));
                pdfTable.AddCell(celda1);
                PdfPCell celda2 = new PdfPCell(new Phrase(result.FirstOrDefault().flight.DepartureStation));
                pdfTable.AddCell(celda2);
                PdfPCell celda3 = new PdfPCell(new Phrase("Destino"));
                pdfTable.AddCell(celda3);
                PdfPCell celda4 = new PdfPCell(new Phrase(result.FirstOrDefault().flight.ArrivalStation));
                pdfTable.AddCell(celda4);
                PdfPCell celda5 = new PdfPCell(new Phrase("Fecha"));
                pdfTable.AddCell(celda5);
                PdfPCell celda6 = new PdfPCell(new Phrase(result.FirstOrDefault().flight.DepartureDate.Value.ToString("yyyy-MM-dd hh-mm-ss")));
                pdfTable.AddCell(celda6);
                PdfPCell celda7 = new PdfPCell(new Phrase("Precio"));
                pdfTable.AddCell(celda7);
                PdfPCell celda8 = new PdfPCell(new Phrase(result.FirstOrDefault().flight.Price.Value.ToString()));
                pdfTable.AddCell(celda8);

                foreach (var item in result)
                {
                    PdfPCell title = new PdfPCell(new Phrase("Pasajero"));
                    pdfTable.AddCell(title);

                    PdfPCell description = new PdfPCell(new Phrase($"{item.client.FirstName} {item.client.LastName}"));
                    pdfTable.AddCell(description);
                }

                document.Add(pdfTable);

                document.Close();

                byte[] data = memoryStream.ToArray();
                string base64 = Convert.ToBase64String(data);

                return new ResponseMessageFile()
                {
                    FileBase64 = base64,
                    Success = true
                };



            }
        }
    }
}
