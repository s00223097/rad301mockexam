using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rad301mockexam
{
    public class Passenger
    {
        public int PassengerId { get; set; }
        public string Name { get; set; }
        public string TicketType { get; set; }
        public decimal TicketCost { get; set; }
        public decimal BaggageCharge { get; set; }

        // Foreign Key
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

    }
}
