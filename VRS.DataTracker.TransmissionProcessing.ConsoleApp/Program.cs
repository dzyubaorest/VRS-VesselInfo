using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VRS.DataTracker.Abstractions;
using VRS.DataTracker.Core;
using VSR.DataTracker.Infrastructure;

var builder = new ConfigurationBuilder()
.AddJsonFile($"appsettings.json", true, true)
.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
IConfigurationRoot configuration = builder.Build();

var serviceProvider = new ServiceCollection()
    .AddDataBase(configuration)
    .RegisterCoreServices(configuration)
    .BuildServiceProvider();

serviceProvider.MigrateDataBase();


serviceProvider.GetRequiredService<ITransmissionConsumer>().Run();