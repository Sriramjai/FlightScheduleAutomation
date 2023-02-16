using FlightScheduleAutomation.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduleAutomation.Services.FlightSchedule
{
    public class FlightScheduleService : IFlightScheduleService
    {
        private readonly ILogger<FlightScheduleService> _logger;
        private readonly FlightsConfiguration _flightConfigurationSettings;



        public FlightScheduleService(IOptions<FlightsConfiguration> flightConfigurationSettings, ILogger<FlightScheduleService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _flightConfigurationSettings = flightConfigurationSettings?.Value ?? throw new ArgumentNullException(nameof(flightConfigurationSettings));

        }

        public void GenerateFlightSchedule()
        {
            int? days = _flightConfigurationSettings.numberOfDays;
            int? flightNumber = 1;
            Cities? city = _flightConfigurationSettings.cities;
            string departingCity = city.Montreal;
            Dictionary<string, string> arrivalCities = new Dictionary<string, string>();
            arrivalCities.Add("Toronto", city.Toronto);
            arrivalCities.Add("Calgary", city.Calgary);
            arrivalCities.Add("Vancouver", city.Vancouver);

            Console.WriteLine("Flight Schedule");

            for (int day = 1; day <= days; day++)
            {
                Console.WriteLine($"Day {day}:");
                foreach (var arrivalCityName in arrivalCities)
                {
                    Console.WriteLine($"Flight: {flightNumber}, departure: {departingCity}, arrival: {arrivalCities[arrivalCityName.Key]}, day: {day} ");
                    flightNumber++;
                }
            }

        }
    }
}
