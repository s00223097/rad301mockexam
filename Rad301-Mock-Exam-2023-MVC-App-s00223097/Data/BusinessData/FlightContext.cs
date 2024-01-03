using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace rad301mockexam
{
    public class FlightContext : DbContext
    {
        // Properties

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        // Constructor
        public FlightContext()
        {
        }

        // parameeterised constructor
        public FlightContext(DbContextOptions<FlightContext> options) : base(options)
        {
            //Database.EnsureDeleted(); // drop the db everytime a new instance of the context is created
            Database.EnsureCreated(); // create the db if it doesn't exist
        }

        // Connect to local db
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionstring = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FlightDB-s00223097";
            optionsBuilder.UseSqlServer(connectionstring)
                .LogTo(Console.WriteLine,
                        new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information).EnableSensitiveDataLogging(true);
        }


        // Define primary and foreign key relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Deifine the primary keys for the tables:

            modelBuilder.Entity<Flight>(e =>
            {
                e.HasKey(f => f.FlightId); // primary key
                e.Property(f => f.FlightId).UseIdentityColumn(1,1); //auto increment each value by 1 starting from 1

            });
            modelBuilder.Entity<Passenger>(e =>
            {
                e.HasKey(p => p.PassengerId); // primary key
                e.Property(p => p.PassengerId).UseIdentityColumn(1,1); 
            });
                
            // Define relationships between tables - foreign keys
 
            modelBuilder.Entity<Passenger>() // A passenger can only be on 1 flight at a time
                .HasOne(p => p.Flight)
                .WithMany(f => f.Passengers) // a flight can have many passengers
                .HasForeignKey(p => p.FlightId);

            modelBuilder.Entity<Flight>() // 1 flight can have many passengers
                .HasMany(f => f.Passengers)
                .WithOne(p => p.Flight)
                .HasForeignKey(p => p.FlightId);    
            
            // seed the data -> here or in the console app, doing it here because it's easier

            //Flight data from appendix
                modelBuilder.Entity<Flight>().HasData(
                    new Flight { FlightId = 1, FlightNumber = "IT-001", DepartureDate = new DateTime(2021, 12, 1, 22, 0, 0), Origin = "Dublin", Destination = "Rome", Country = "Italy", MaxSeats = 110 },
                    new Flight { FlightId = 2, FlightNumber = "EN-002", DepartureDate = new DateTime(2022, 1, 23, 12, 50, 0), Origin = "Dublin", Destination = "London", Country = "England", MaxSeats = 110 },
                    new Flight { FlightId = 3, FlightNumber = "FR-001", DepartureDate = new DateTime(2022, 1, 4, 6, 0, 0), Origin = "Dublin", Destination = "Paris", Country = "France", MaxSeats = 120 },
                    new Flight { FlightId = 4, FlightNumber = "BE-001", DepartureDate = new DateTime(2022, 1, 5, 16, 30, 0), Origin = "Dublin", Destination = "Brussels", Country = "Belgium", MaxSeats = 88 },
                    new Flight { FlightId = 5, FlightNumber = "DU-001", DepartureDate = new DateTime(2022, 1, 24, 11, 0, 0), Origin = "London", Destination = "Dublin", Country = "Ireland", MaxSeats = 110 }
                );

            // Passenger data from appendix
            modelBuilder.Entity<Passenger>().HasData(
                new Passenger { PassengerId = 1, Name = "Fred Farnell", TicketType = "Economy", TicketCost = 51.83m, BaggageCharge = 30.00m, FlightId = 2 },
                new Passenger { PassengerId = 2, Name = "Tom Mc Manus", TicketType = "First Class", TicketCost = 127.00m, BaggageCharge = 10.00m, FlightId = 2 },
                new Passenger { PassengerId = 3, Name = "Bill Trimble", TicketType = "First Class", TicketCost = 140.00m, BaggageCharge = 10.00m, FlightId = 3 },
                new Passenger { PassengerId = 4, Name = "Freda Mc Donald", TicketType = "Economy", TicketCost = 50.92m, BaggageCharge = 15.00m, FlightId = 4 },
                new Passenger { PassengerId = 5, Name = "Mary Malone", TicketType = "Economy", TicketCost = 66.22m, BaggageCharge = 15.00m, FlightId = 1 },
                new Passenger { PassengerId = 6, Name = "Tom Mc Manus", TicketType = "First Class", TicketCost = 127.00m, BaggageCharge = 10.00m, FlightId = 5 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
