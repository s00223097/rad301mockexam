namespace rad301mockexam
{
    public class Flight
    {
        // Constructor
        public Flight()
        {
            // Initialize Passengers to an empty list
            Passengers = new List<Passenger>();
        }

        // Properties
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Country { get; set; }
        public int MaxSeats { get; set; }

        //list of passengers
        public List<Passenger> Passengers { get; set; }
    }
}
