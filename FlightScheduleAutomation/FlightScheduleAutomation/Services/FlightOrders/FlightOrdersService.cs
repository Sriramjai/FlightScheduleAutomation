using FlightScheduleAutomation.Models;
using FlightScheduleAutomation.Services.FlightSchedule;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightScheduleAutomation.Services.FlightOrders
{
    public class FlightOrdersService : IFlightOrdersService
    {
        private readonly ILogger<FlightOrdersService> _logger;

        public FlightOrdersService(ILogger<FlightOrdersService> logger)
        {
            _logger = logger;
        }

        public void GenerateFlightOrderSchedule()
        {
            Dictionary<string, int> flightOrders = new Dictionary<string, int>();
            Dictionary<string, string> validDestinations = new Dictionary<string, string>();
            validDestinations.Add("YYZ","4");
            validDestinations.Add("YVR","5");
            validDestinations.Add("YYC","6");
                       
            // getting the json file

            string? path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var file = Path.Combine(path,"coding-assigment-orders.json");

            if (String.IsNullOrEmpty(file))
            {
                _logger.LogError("Orders file is missing. Cannot generate flight orders schedule");
            }
            else
            {
                // reading the json file and deserialzing it

                string data = File.ReadAllText(file);

                Dictionary<string, Order> ordersData = JsonConvert.DeserializeObject<Dictionary<string, Order>>(data);

                if (ordersData != null)
                {
                    Console.WriteLine("Flight Orders Schedule Information");

                    foreach (var order in ordersData.Keys)
                    {

                        // checking if it's a valid destination then display the schedule 

                        if (validDestinations.ContainsKey(ordersData[order].Destination))
                        {
                            // add key if it doesn't exist already

                            if (!flightOrders.ContainsKey(ordersData[order].Destination))
                            {
                                flightOrders.Add(ordersData[order].Destination, 1);
                            }

                            // if it does then increment the number of occurrence

                            else
                            {
                                flightOrders[ordersData[order].Destination]++;
                            }


                            // checking if we have reached the flight's capacity of 20

                            if (Convert.ToInt32(flightOrders[ordersData[order].Destination]) <= 20)
                            {
                                Console.WriteLine($"order: {order}, flightNumber: 1, departure: YUL, arrival: {ordersData[order].Destination}, day: 1");
                            }

                            // if the flight capacity has been reached, then schedule the orders for next day

                            else if (Convert.ToInt32(flightOrders[ordersData[order].Destination]) >= 20 && validDestinations.ContainsKey(ordersData[order].Destination))
                            {
                                Console.WriteLine($"order: {order}, flightNumber: {validDestinations[ordersData[order].Destination]} , departure: YUL, arrival: {ordersData[order].Destination}, day: 2");
                            }

                        }

                        // if schedule doesn't exist then print not scheduled
                        else
                        {
                            Console.WriteLine($"order: {order}, flightNumber: notscheduled");
                        }

                    }
                }
                else
                {
                    _logger.LogError("Parsing error - Error while parsing the json file");
                }
            }

        }
            

        public class Order
        {
            public string? Destination { get; set; }
        }


    }
}
