using MicroserviceFlight_Application.Client;
using MicroserviceFlight_InfraEstructure.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace MicroserviceFlight_Test
{
    public class FlightTest
    {
        [Fact]
        public async void GetVuelosPaginados()
        {
            var options = new DbContextOptionsBuilder<FlightDBContext>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var contexto = new FlightDBContext(options);

            var request = new CreateClient.ExecuteCreateClient();
            request.Email = "georgecalderonc@gmail.com";
            request.FirstName = "Alianza";
            request.LastName = "Alianza";
            request.Phone = "198763564";

            CreateClient.Fire fire = new CreateClient.Fire(contexto);

            var result = await fire.Handle(request, new System.Threading.CancellationToken());
            Assert.True(result != 0);
        }
    }
}
