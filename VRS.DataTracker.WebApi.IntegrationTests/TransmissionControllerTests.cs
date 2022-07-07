using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;
using VRS.DataTracker.Abstractions;
using VRS.DataTracker.Abstractions.Dto.Transmission;
using VRS.DataTracker.Core;
using VRS.DataTracker.WebApi.IntegrationTests;
using VSR.DataTracker.Infrastructure;
using VSR.DataTracker.Infrastructure.Abstractions.Repositories;

namespace VRS.DataTracker.IntegrationsTests
{
    public class TransmissionControllerTests
    {
        [Fact]
        public async Task TransmissionPostSuccessPathTest()
        {
            // Arrange
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Test.json", true, true);
            var configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddDataBase(configuration)
                .RegisterCoreServices(configuration)
                .BuildServiceProvider();
            serviceProvider.MigrateDataBase();

            var client = new WebApiApplicationFactory().CreateClient();
            client.BaseAddress = new Uri("https://localhost:7102");

            DateTime time = DateTime.Now;
            string guid = Guid.NewGuid().ToString();

            // Act
            new Thread(() => serviceProvider.GetRequiredService<ITransmissionConsumer>().Run()).Start();
            await SendApiMessagesAsync(client, time, guid);
            await Task.Delay(10000);

            // Assert
            await AssertVesselsAsync(serviceProvider, time, guid);
        }

        private async Task AssertVesselsAsync(ServiceProvider serviceProvider, DateTime time, string guid)
        {
            var repository = serviceProvider.GetRequiredService<IVesselRepository>();
            var vessels = await repository.GetVesselsAsync();

            for (int i = 1; i <= 5; i++)
            {
                var vessel = vessels.FirstOrDefault(v => v.Name == $"[{guid}] Test Name {i}" && v.IMO == $"[{guid}] Test IMO {i}");
                Assert.NotNull(vessel);
                Assert.NotNull(vessel.TrackDataRecords);
                Assert.Equal(10, vessel.TrackDataRecords.Count);
                foreach (var trackRecord in vessel.TrackDataRecords)
                {
                    Assert.Equal(time, trackRecord.DateTime);
                    Assert.Equal(1, trackRecord.Latitude);
                    Assert.Equal(-1, trackRecord.Longitude);
                }
            }
        }

        private async Task SendApiMessagesAsync(HttpClient client, DateTime time, string guid)
        {
            for (int i = 1; i <= 5; i++)
                for (int j = i * 10 + 1; j <= (i + 1) * 10; j++)
                {
                    var dto = new TransmissionDto
                    {
                        DateTime = time,
                        IMO = $"[{guid}] Test IMO {i}",
                        VesselName = $"[{guid}] Test Name {i}",
                        Position = new GeolocationPosition
                        {
                            Latitude = 1,
                            Longitude = -1
                        }
                    };

                    await client.PostAsync("/api/transmission", new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
                }
        }
    }
}