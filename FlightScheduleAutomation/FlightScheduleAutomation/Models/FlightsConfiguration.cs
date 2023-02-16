using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightScheduleAutomation.Models
{

    public class FlightsConfiguration
    {
        public int? numberOfFlights { get; set; }
        public int? numberOfDays {  get; set; }

        public Cities? cities { get; set; }
    }

    public class Cities
    {
        public string? Montreal { get; set; }
        public string? Toronto { get; set; }
        public string? Calgary { get; set; }
        public string? Vancouver { get; set; }
    }
}
