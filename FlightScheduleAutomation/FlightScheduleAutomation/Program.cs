using FlightScheduleAutomation.Models;
using FlightScheduleAutomation.Services.FlightOrders;
using FlightScheduleAutomation.Services.FlightSchedule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FlightScheduleAutomation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // create service collection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            using var serviceProvider = services.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetService<FlightScheduleService>().GenerateFlightSchedule();
            serviceProvider.GetService<FlightOrdersService>().GenerateFlightOrderSchedule();


        }

        static void ConfigureServices(IServiceCollection services)
        {
            //configure logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();

            });

            // build config
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfiguration config = builder.Build();

            services.Configure<FlightsConfiguration>(config.GetSection("Airports"));

            //add app
            services.AddTransient<FlightScheduleService>();
            services.AddTransient<FlightOrdersService>();
        }
       
    }

}