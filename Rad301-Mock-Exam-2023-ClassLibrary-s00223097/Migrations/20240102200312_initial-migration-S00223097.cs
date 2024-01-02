using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rad301_Mock_Exam_2023_ClassLibrary_s00223097.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrationS00223097 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BaggageCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                    table.ForeignKey(
                        name: "FK_Passengers_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Country", "DepartureDate", "Destination", "FlightNumber", "MaxSeats", "Origin" },
                values: new object[,]
                {
                    { 1, "Italy", new DateTime(2021, 12, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), "Rome", "IT-001", 110, "Dublin" },
                    { 2, "England", new DateTime(2022, 1, 23, 12, 50, 0, 0, DateTimeKind.Unspecified), "London", "EN-002", 110, "Dublin" },
                    { 3, "France", new DateTime(2022, 1, 4, 6, 0, 0, 0, DateTimeKind.Unspecified), "Paris", "FR-001", 120, "Dublin" },
                    { 4, "Belgium", new DateTime(2022, 1, 5, 16, 30, 0, 0, DateTimeKind.Unspecified), "Brussels", "BE-001", 88, "Dublin" },
                    { 5, "Ireland", new DateTime(2022, 1, 24, 11, 0, 0, 0, DateTimeKind.Unspecified), "Dublin", "DU-001", 110, "London" }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "PassengerId", "BaggageCharge", "FlightId", "Name", "TicketCost", "TicketType" },
                values: new object[,]
                {
                    { 1, 30.00m, 2, "Fred Farnell", 51.83m, "Economy" },
                    { 2, 10.00m, 2, "Tom Mc Manus", 127.00m, "First Class" },
                    { 3, 10.00m, 3, "Bill Trimble", 140.00m, "First Class" },
                    { 4, 15.00m, 4, "Freda Mc Donald", 50.92m, "Economy" },
                    { 5, 15.00m, 1, "Mary Malone", 66.22m, "Economy" },
                    { 6, 10.00m, 5, "Tom Mc Manus", 127.00m, "First Class" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FlightId",
                table: "Passengers",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
