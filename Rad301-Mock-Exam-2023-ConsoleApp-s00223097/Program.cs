using Microsoft.EntityFrameworkCore.Diagnostics;

namespace rad301mockexam
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is where the tracking message will be.");

            // list passengers on flight 1
            list_passengers(1);
            // list all flights + revenue for each flight
            list_flight_revenue();

            // Q1c part 4 ->
            // 1. Add a new flight,
            // 2. Add a passenger to this flight
            // 3. Save changes to the db
            using (var db = new FlightContext()) // just used "db" because this entire sol is in the same namespace (easier)
            {
                var newFlight = new Flight()
                {
                    FlightId = 6,
                    FlightNumber = "DU-002",
                    DepartureDate = new DateTime(2022, 6, 29, 11, 0, 0), // y, m, d, h, m, s in tht order
                    Origin = "Dublin",
                    Destination = "Sydney",
                    Country = "Australia",
                    MaxSeats = 210,
                    Passengers = new List<Passenger>() // list of passengers on THIS flight
                    {
                        new Passenger()
                        {
                            PassengerId = 7,
                            Name = "Martha Rotter",
                            TicketType = "Business",
                            TicketCost = 399.0m,
                            BaggageCharge = 30.00m,
                            FlightId = 6 
                        }
                    }
                };
                db.SaveChanges();
                Console.WriteLine("Flight and its 1 passenger added to the db.");
            }


            Console.ReadLine();
        }

        /// <summary>
        /// Method to list passengers on a specified flight
        /// </summary>
        /// <param name="flightId"></param>
        static void list_passengers(int flightId)
        {
            using (var db = new FlightContext())
            {
                // linq query to get passengers on a flight.. 
                var passengers = db.Passengers.Where(p => p.FlightId == flightId).ToList(); // get the passengers on a flight and put them into a list
                
                foreach (var p in passengers) // p stamds for a passenger
                {
                    Console.WriteLine($"Passenger ID: {p.PassengerId} Name: {p.Name} Ticket Type: {p.TicketType} Ticket Cost: {p.TicketCost} Baggage Charge: {p.BaggageCharge}");
                }
            }
        }

        /// <summary>
        /// Method to list all flights + revenue for each fligt
        /// </summary>
        static void list_flight_revenue()
        {
            using (var db = new FlightContext())
            {
                // Get all flights first... linq
                var flights = db.Flights.ToList();

                foreach (var f in flights) // f stands for a flight
                {
                    // Calc the total revenue for each flight
                    decimal totalRevenue = f.Passengers.Sum(p => p.TicketCost + p.BaggageCharge);
                    // Display the flight details and the total revenue
                    Console.WriteLine($"Flight Number: {f.FlightNumber}, Destination: {f.Destination}, Departure Date: {f.DepartureDate}, Total Revenue: {totalRevenue:C}"); // C for currency formattng
                }
            }
        }
    }
}
